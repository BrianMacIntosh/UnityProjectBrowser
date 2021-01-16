// Copyright(c) 2018 Brian MacIntosh
// MIT License

using ProjectBrowser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace UnityProjectBrowser
{
	public partial class MainForm : Form
	{
		private static readonly TimeSpan UpdateInterval = TimeSpan.FromMilliseconds(500);

		private Thread m_updateLoopThread;
		private ManualResetEvent m_updateThreadSync = new ManualResetEvent(false);

		private Dictionary<string, TreeNode> m_treeNodes = new Dictionary<string, TreeNode>();

		private ReaderWriterLockSlim m_objectBufferLock = new ReaderWriterLockSlim();

		/// <summary>
		/// Buffer of objects that need to be added to the allObjects view.
		/// </summary>
		private List<ProjectObject> m_objectsToAdd = new List<ProjectObject>();

		/// <summary>
		/// Buffer of objects that need to be removed from the allObjects view.
		/// </summary>
		private List<ProjectObject> m_objectsToRemove = new List<ProjectObject>();

		/// <summary>
		/// If set, all objects will be cleared from the allObjects view.
		/// </summary>
		private bool m_objectsClear = false;

		private TreeNode m_lastSelectedTreeNode;

		private NavigationHistory m_treeViewNavHistory;
		private RecentlyOpenedList m_recentProjects;

		public MainForm()
		{
			InitializeComponent();

			allObjectsTreeView.ImageList = IconImages.ImageList;
			relationshipsListView.SmallImageList = IconImages.ImageList;

			allObjectsTreeViewRadioButton.Checked = true;

			m_treeViewNavHistory = new NavigationHistory(allObjectsTreeView, 64);
			m_recentProjects = new RecentlyOpenedList("projects", 8, recentFoldersToolStripMenuItem);
			m_recentProjects.OnItemSelected += OnRecentPathSelected;

			ObjectDatabase.Added += OnObjectAdded;
			ObjectDatabase.Removed += OnObjectRemoved;
			ObjectDatabase.Cleared += OnDatabaseCleared;

			m_updateLoopThread = new Thread(UpdateLoop);
			m_updateLoopThread.Start();
		}

		private void UpdateLoop()
		{
			//HACK: wait for the form to initialize
			Thread.Sleep(1000);
			Stopwatch stopwatch = new Stopwatch();
			while (true)
			{
				stopwatch.Restart();
				m_updateThreadSync.Reset();
				if (allObjectsTreeView.InvokeRequired)
				{
					Action updateList = new Action(UpdateAllObjectsList);
					Invoke(updateList);
				}
				else
				{
					UpdateAllObjectsList();
				}
				m_updateThreadSync.WaitOne();
				Thread.Sleep(TimeSpanUtility.Max(TimeSpan.Zero, UpdateInterval - stopwatch.Elapsed));
			}
		}

		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			base.OnFormClosed(e);

			FileUpdater.Stop();
			m_updateLoopThread.Abort();
			m_updateLoopThread = null;
		}

		void OnObjectAdded(object sender, ObjectDatabaseEventArgs e)
		{
			m_objectBufferLock.EnterWriteLock();
			try
			{
				m_objectsToAdd.Add(e.Object);
				m_objectsToRemove.Remove(e.Object);
			}
			finally
			{
				m_objectBufferLock.ExitWriteLock();
			}
		}

		void OnObjectRemoved(object sender, ObjectDatabaseEventArgs e)
		{
			m_objectBufferLock.EnterWriteLock();
			try
			{
				m_objectsToAdd.Remove(e.Object);
				m_objectsToRemove.Add(e.Object);
			}
			finally
			{
				m_objectBufferLock.ExitWriteLock();
			}
		}

		void OnDatabaseCleared(object sender, ObjectDatabaseEventArgs e)
		{
			m_objectBufferLock.EnterWriteLock();
			try
			{
				m_objectsToAdd.Clear();
				m_objectsToRemove.Clear();
				m_objectsClear = true;
			}
			finally
			{
				m_objectBufferLock.ExitWriteLock();
			}
		}

		/// <summary>
		/// Gets the <see cref="TreeNode"/> associated with the object with the specified unique ID.
		/// </summary>
		private TreeNode GetTreeNode(string uniqueId, bool createPlaceholder)
		{
			TreeNode existingNode;
			if (m_treeNodes.TryGetValue(uniqueId, out existingNode))
			{
				return existingNode;
			}
			else if (createPlaceholder)
			{
				// create a placeholder node
				TreeNode newNode = new TreeNode(uniqueId)
				{
					Name = uniqueId
				};
				return AddTreeNode(allObjectsTreeView.Nodes, newNode);
			}
			else
			{
				return null;
			}
		}

		private TreeNode AddTreeNode(TreeNodeCollection parent, TreeNode node)
		{
			m_treeNodes.Add(node.Name, node);
			parent.Add(node);
			return node;
		}

		private void UpdateParseProgress()
		{
			int outstandingCount = FileUpdater.GetOutstandingFileCount();
			if (outstandingCount > 0)
			{
				parseQueueStatusLabel.Text = "Files to Parse: " + outstandingCount.ToString();
			}
			else
			{
				parseQueueStatusLabel.Text = "";
			}
		}

		private void UpdateAllObjectsList()
		{
			List<ProjectObject> objectsToRemove = new List<ProjectObject>();
			List<ProjectObject> objectsToAdd = new List<ProjectObject>();
			bool doClear = false;

			m_objectBufferLock.EnterWriteLock();
			try
			{
				objectsToRemove.AddRange(m_objectsToRemove);
				objectsToAdd.AddRange(m_objectsToAdd);
				doClear = m_objectsClear;
				m_objectsToAdd.Clear();
				m_objectsToRemove.Clear();
				m_objectsClear = false;
			}
			finally
			{
				m_objectBufferLock.ExitWriteLock();
			}

			if (doClear
				|| objectsToRemove.Count > 0
				|| objectsToAdd.Count > 0)
			{
				allObjectsTreeView.BeginUpdate();

				// clear first in case new objects were added after the clear but we haven't run this update yet
				if (doClear)
				{
					m_treeNodes.Clear();
					allObjectsTreeView.Nodes.Clear();
				}
				foreach (ProjectObject obj in objectsToAdd)
				{
					AddObject(obj);
				}
				foreach (ProjectObject obj in objectsToRemove)
				{
					RemoveObject(obj);
				}

				allObjectsTreeView.EndUpdate();

				UpdateParseProgress();
			}

			m_updateThreadSync.Set();
		}

		/// <summary>
		/// Adds a new object to the allObjects view.
		/// </summary>
		private void AddObject(ProjectObject obj)
		{
			if (obj.HideFromHierarchy)
			{
				return;
			}

			string parentKey = GetParentKey(obj);

			TreeNodeCollection addTo;
			if (!string.IsNullOrEmpty(parentKey))
			{
				TreeNode parentNode = GetTreeNode(parentKey, true);
				addTo = parentNode.Nodes;
			}
			else
			{
				addTo = allObjectsTreeView.Nodes;
			}
			
			string uniqueId = obj.UniqueId.ToString();

			// see if there is a Windows folder node that corresponds to this newly-added Unity folder node
			TreeNode existingFolderNode = null;
			UnityFolder folderObj = obj as UnityFolder;
			if (folderObj != null)
			{
				string folderPath = folderObj.FolderPath;
				m_treeNodes.TryGetValue(folderPath, out existingFolderNode);
			}

			// see if there is an existing node of any kind for this object
			TreeNode node;
			m_treeNodes.TryGetValue(uniqueId, out node);

			if (existingFolderNode != null) // there is a Windows folder node
			{
				if (node == null) // there isn't a Unity node
				{
					// recycle the Windows folder node as a Unity folder node
					if (!m_treeNodes.Remove(existingFolderNode.Name))
					{
						throw new InvalidOperationException("Programming error");
					}
					m_treeNodes.Add(folderObj.UniqueId, existingFolderNode);
					node = existingFolderNode;
				}
				else // there is a Unity node
				{
					// trash the Windows folder node and use the existing Unity folder node
					if (!m_treeNodes.Remove(existingFolderNode.Name))
					{
						throw new InvalidOperationException("Programming error");
					}
					foreach (TreeNode childNode in existingFolderNode.Nodes)
					{
						if (childNode != null) //HACK: why?
						{
							childNode.Remove();
							node.Nodes.Add(childNode);
						}
					}
					existingFolderNode.Remove();
					existingFolderNode = null;
				}
			}

			if (node != null)
			{
				// ensure the existing node is parented correctly
				node.Remove();
				addTo.Add(node);

				// update node info
				node.Text = obj.ToString();
				node.Name = obj.UniqueId.ToString();
				string imageKey = obj.GetIconKey();
				int imageIndex = IconImages.GetImageIndex(imageKey);
				node.ImageIndex = node.SelectedImageIndex = imageIndex;
				node.ImageKey = node.SelectedImageKey = imageKey;
			}
			else
			{
				// create a new node
				string imageKey = obj.GetIconKey();
				int imageIndex = IconImages.GetImageIndex(imageKey);
				node = new TreeNode(obj.ToString(), imageIndex, imageIndex)
				{
					Name = obj.UniqueId.ToString(),
					ImageKey = imageKey,
					SelectedImageKey = imageKey,
				};
				AddTreeNode(addTo, node);
			}

			UpdateParseProgress();
		}

		/// <summary>
		/// Removes an object from the allObjects view.
		/// </summary>
		private void RemoveObject(ProjectObject obj)
		{
			TreeNode existingNode;
			if (m_treeNodes.TryGetValue(obj.UniqueId, out existingNode))
			{
				existingNode.Remove();
				m_treeNodes.Remove(obj.UniqueId);
			}
		}

		private void OnAllObjectsTreeViewSelectionChanged(object sender, TreeViewEventArgs e)
		{
			if (allObjectsTreeView.SelectedNode == null)
			{
				relationshipsListView.Items.Clear();
				return;
			}

			TreeNode selectedItem = allObjectsTreeView.SelectedNode;

			//HACK: ensure that the selection sticks when the element doesn't have focus
			if (m_lastSelectedTreeNode != null)
			{
				m_lastSelectedTreeNode.BackColor = SystemColors.Window;
				m_lastSelectedTreeNode.ForeColor = SystemColors.WindowText;
			}
			m_lastSelectedTreeNode = selectedItem;
			selectedItem.BackColor = SystemColors.ActiveBorder;
			selectedItem.ForeColor = SystemColors.HighlightText;

			ProjectObject selectedObject;
			if (ObjectDatabase.TryGetObject(selectedItem.Name, out selectedObject))
			{
				// load the object into the inspect panel
				UnityObjectKey unityObjectKey;
				unityObjectKey = new UnityObjectKey(selectedObject.UniqueId);
				selectedObjectGuidTextBox.Text = unityObjectKey.AssetGuid.ToString();
				selectedObjectFileIdTextBox.Text = unityObjectKey.FileId.ToString();
				selectedObjectFilePathTextBox.Text = selectedObject.GetFilePath();
				projectObjectTypeTextBox.Text = selectedObject.GetType().Name;
				if (selectedObject.ParseError != null)
				{
					viewErrorButton.Visible = true;
					toolTip.SetToolTip(viewErrorButton, selectedObject.ParseError.ToString());
				}
				else
				{
					viewErrorButton.Visible = false;
				}
			}
			else
			{
				selectedObjectGuidTextBox.Text = "";
				selectedObjectFileIdTextBox.Text = "";
				selectedObjectFilePathTextBox.Text = "";
				projectObjectTypeTextBox.Text = "";
			}

			ReloadRelationships();
		}

		/// <summary>
		/// Reloads the <see cref="relationshipsListView"/>.
		/// </summary>
		private void ReloadRelationships()
		{
			TreeNode selectedItem = allObjectsTreeView.SelectedNode;

			ProjectObject selectedObject;
			if (!ObjectDatabase.TryGetObject(selectedItem.Name, out selectedObject))
			{
				relationshipsListView.Items.Clear();
				return;
			}

			relationshipsListView.BeginUpdate();
			relationshipsListView.Items.Clear();
			foreach (ObjectRelationship relationship in ObjectDatabase.GetRelationships(selectedObject.UniqueId))
			{
				ListViewItem listItem;
				ProjectObject targetObject;

				// populate the target object
				if (ObjectDatabase.TryGetObject(relationship.TargetObjectId, out targetObject))
				{
					listItem = relationshipsListView.Items.Add(
						relationship.TargetObjectId,
						targetObject.ToString(),
						targetObject.GetIconKey());
				}
				else
				{
					listItem = relationshipsListView.Items.Add(
						relationship.TargetObjectId,
						"*missing*",
						"DefaultAsset_Icon");
				}

				// relationship type
				listItem.SubItems.Add(relationship.RelationshipType);

				// target object raw data
				UnityObjectKey objectKey = new UnityObjectKey(relationship.TargetObjectId);
				listItem.SubItems.Add(objectKey.AssetGuid.ToString());
				listItem.SubItems.Add(objectKey.FileId.ToString());

				// populate the target object's parent asset
				UnityObjectKey targetObjectKey = new UnityObjectKey(relationship.TargetObjectId);
				if (ObjectDatabase.TryGetObject(targetObjectKey.AssetGuid.ToString(), out targetObject))
				{
					listItem.SubItems.Add(targetObject.ToString());
				}
				else
				{
					listItem.SubItems.Add("*missing*");
				}
			}

			if (relationshipsIncludeChildrenCheckbox.Checked)
			{
				//TODO: load child relationships
			}

			relationshipsListView.EndUpdate();
		}

		/// <summary>
		/// Sets the selected object in the All Objects tree view.
		/// </summary>
		public void SetAllObjectsSelection(string uniqueId)
		{
			TreeNode selectedItemTreeNode;
			if (m_treeNodes.TryGetValue(uniqueId, out selectedItemTreeNode))
			{
				allObjectsTreeView.SelectedNode = selectedItemTreeNode;
			}
		}

		/// <summary>
		/// Event called when the relationships list view is double-clicked.
		/// </summary>
		private void OnRelationshipsListViewDoubleClick(object sender, EventArgs e)
		{
			// navigate to the related object in the tree view
			ListView senderListView = (ListView)sender;
			if (senderListView.SelectedItems.Count > 0)
			{
				ListViewItem selectedItem = senderListView.SelectedItems[0];
				SetAllObjectsSelection(selectedItem.Name);
			}
		}

		/// <summary>
		/// Moves backward in the navigation history.
		/// </summary>
		private void toolStripBackButton_Click(object sender, EventArgs e)
		{
			m_treeViewNavHistory.Back();
		}

		/// <summary>
		/// Moves forward in the navigation history.
		/// </summary>
		private void toolStripForwardButton_Click(object sender, EventArgs e)
		{
			m_treeViewNavHistory.Forward();
		}

		/// <summary>
		/// Shows the allObjects view context menu over the selected item.
		/// </summary>
		private void allObjectsTreeView_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				// show the context menu
				Point mousePos = new Point(e.X, e.Y);
				TreeNode selectedNode = allObjectsTreeView.GetNodeAt(mousePos);
				if (selectedNode != null)
				{
					allObjectsTreeView.SelectedNode = selectedNode;
					allObjectsContextMenu.Show(allObjectsTreeView, mousePos);
				}
			}
		}

		/// <summary>
		/// Shows the relationships view context menu over the selected item.
		/// </summary>
		private void relationshipsListView_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				// show the context menu
				Point mousePos = new Point(e.X, e.Y);
				ListViewItem selectedNode = relationshipsListView.GetItemAt(e.X, e.Y);
				if (selectedNode != null)
				{
					relationshipsListView.ClearSelected();
					selectedNode.Selected = true;
					relationshipsContextMenu.Show(relationshipsListView, mousePos);
				}
			}
		}

		/// <summary>
		/// Selects the selected object in the relationship view in the allObjects view.
		/// </summary>
		private void showInAllObjectsViewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (relationshipsListView.SelectedItems.Count > 0)
			{
				ListViewItem selectedItem = relationshipsListView.SelectedItems[0];
				SetAllObjectsSelection(selectedItem.Name);
			}
		}

		/// <summary>
		/// Opens the selected object in the allObjects view in explorer.
		/// </summary>
		private void openInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProjectObject projectObject;
			if (allObjectsTreeView.SelectedNode != null
				&& ObjectDatabase.TryGetObject(allObjectsTreeView.SelectedNode.Name, out projectObject))
			{
				string objectPath = projectObject.GetFilePath();
				if (!string.IsNullOrEmpty(objectPath))
				{
					Process.Start("explorer.exe", "/select,\"" + objectPath + "\"");
				}
			}
		}

		/// <summary>
		/// Opens the selected object in the allObjects view in the default editor.
		/// </summary>
		private void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProjectObject projectObject;
			if (allObjectsTreeView.SelectedNode != null
				&& ObjectDatabase.TryGetObject(allObjectsTreeView.SelectedNode.Name, out projectObject))
			{
				string objectPath = projectObject.GetFilePath();
				if (!string.IsNullOrEmpty(objectPath))
				{
					try
					{
						Process.Start(objectPath);
					}
					catch (System.ComponentModel.Win32Exception except)
					{
						MessageBox.Show(except.Message, "Error", MessageBoxButtons.OK);
					}
				}
			}
		}

		/// <summary>
		/// Copies the full path of the selected object in the allObjects view to the clipboard.
		/// </summary>
		private void copyPathToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProjectObject projectObject;
			if (allObjectsTreeView.SelectedNode != null
				&& ObjectDatabase.TryGetObject(allObjectsTreeView.SelectedNode.Name, out projectObject))
			{
				string objectPath = projectObject.GetFilePath();
				Clipboard.SetText(objectPath);
			}
			else
			{
				Clipboard.SetText(allObjectsTreeView.SelectedNode.Name);
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		void OnRecentPathSelected(object sender, string path)
		{
			try
			{
				FileUpdater.SetProjectPath(path);
			}
			catch (UnityProjectException exception)
			{
				MessageBox.Show(
					exception.Message, "Invalid Directory",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog browser = new FolderBrowserDialog())
			{
				browser.Description = "Select Unity Project folder";
				browser.SelectedPath = FileUpdater.ProjectPath;
				browser.ShowNewFolderButton = false;
				DialogResult result = browser.ShowDialog();

				if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(browser.SelectedPath))
				{
					try
					{
						FileUpdater.SetProjectPath(browser.SelectedPath);

						// add or promote the folder in the Recent Folders list
						m_recentProjects.Access(browser.SelectedPath);
					}
					catch (UnityProjectException exception)
					{
						MessageBox.Show(
							exception.Message, "Invalid Directory",
							MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
		}

		private void OnAllObjectsViewChanged(object sender, EventArgs e)
		{
			RebuildAllObjectsParents();
		}

		/// <summary>
		/// Reparents all objects in the allObjects treeview to the correct parent.
		/// </summary>
		private void RebuildAllObjectsParents()
		{
			foreach (TreeNode node in m_treeNodes.Values)
			{
				ProjectObject nodeObject;
				if (ObjectDatabase.TryGetObject(node.Name, out nodeObject))
				{
					AddObject(nodeObject);
				}
			}
		}

		private string GetParentKey(ProjectObject projectObject)
		{
			// gets the key of the node that should be the parent of the specified object
			if (allObjectsTreeViewRadioButton.Checked)
			{
				string parentId = projectObject.GetParentId();
				if (!string.IsNullOrEmpty(parentId))
				{
					return parentId;
				}
				else
				{
					// return the document's parent folder
					string filePath = Path.GetDirectoryName(projectObject.GetFilePath());
					return ObjectDatabase.GetFolderUniqueId(filePath);
				}
			}
			else
			{
				return projectObject.GetParentId();
			}
		}

		private void relationshipsIncludeChildrenCheckbox_CheckedChanged(object sender, EventArgs e)
		{
			ReloadRelationships();
		}

		private void goToToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GotoForm dialog = new GotoForm();
			DialogResult result = dialog.ShowDialog();
			if (result == DialogResult.OK || result == DialogResult.Yes)
			{
				//TODO:
			}
		}

		private void reparseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ProjectObject projectObject;
			if (allObjectsTreeView.SelectedNode != null
				&& ObjectDatabase.TryGetObject(allObjectsTreeView.SelectedNode.Name, out projectObject))
			{
				//TODO: discard existing relationships, re-trigger the correct feeder
			}
		}
	}
}
