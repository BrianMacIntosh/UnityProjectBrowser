namespace UnityProjectBrowser
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.GroupBox allObjectsGroupBox;
			System.Windows.Forms.Label filterLabel;
			System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
			System.Windows.Forms.GroupBox objectInspectorBox;
			System.Windows.Forms.Label projectObjectTypeLabel;
			System.Windows.Forms.Label selectedObjectFilePathLabel;
			System.Windows.Forms.Label selectedObjectFileIdLabel;
			System.Windows.Forms.Label selectedObjectGuidLabel;
			System.Windows.Forms.Label relationshipsLabel;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.panel2 = new System.Windows.Forms.Panel();
			this.allObjectsFlatViewRadioButton = new System.Windows.Forms.RadioButton();
			this.allObjectsTreeViewRadioButton = new System.Windows.Forms.RadioButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.filterCaseSensitiveCheckBox = new System.Windows.Forms.CheckBox();
			this.filterTextBox = new System.Windows.Forms.TextBox();
			this.allObjectsTreeView = new System.Windows.Forms.TreeView();
			this.relationshipsIncludeChildrenCheckbox = new System.Windows.Forms.CheckBox();
			this.projectObjectTypeTextBox = new System.Windows.Forms.TextBox();
			this.selectedObjectFilePathTextBox = new System.Windows.Forms.TextBox();
			this.selectedObjectFileIdTextBox = new System.Windows.Forms.TextBox();
			this.selectedObjectGuidTextBox = new System.Windows.Forms.TextBox();
			this.relationshipsListView = new System.Windows.Forms.ListView();
			this.targetObjectHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.relationshipTypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.assetGuidHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.fileIdHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.targetAssetHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.recentFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.goToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
			this.parseQueueStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.mainToolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripBackButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripForwardButton = new System.Windows.Forms.ToolStripButton();
			this.allObjectsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.reparseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.relationshipsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.showInAllObjectsViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			allObjectsGroupBox = new System.Windows.Forms.GroupBox();
			filterLabel = new System.Windows.Forms.Label();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			objectInspectorBox = new System.Windows.Forms.GroupBox();
			projectObjectTypeLabel = new System.Windows.Forms.Label();
			selectedObjectFilePathLabel = new System.Windows.Forms.Label();
			selectedObjectFileIdLabel = new System.Windows.Forms.Label();
			selectedObjectGuidLabel = new System.Windows.Forms.Label();
			relationshipsLabel = new System.Windows.Forms.Label();
			allObjectsGroupBox.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			objectInspectorBox.SuspendLayout();
			this.mainMenuStrip.SuspendLayout();
			this.mainStatusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.mainToolStrip.SuspendLayout();
			this.allObjectsContextMenu.SuspendLayout();
			this.relationshipsContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// allObjectsGroupBox
			// 
			allObjectsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			allObjectsGroupBox.Controls.Add(this.panel2);
			allObjectsGroupBox.Controls.Add(this.panel1);
			allObjectsGroupBox.Controls.Add(this.allObjectsTreeView);
			allObjectsGroupBox.Location = new System.Drawing.Point(3, 25);
			allObjectsGroupBox.Name = "allObjectsGroupBox";
			allObjectsGroupBox.Size = new System.Drawing.Size(273, 554);
			allObjectsGroupBox.TabIndex = 0;
			allObjectsGroupBox.TabStop = false;
			allObjectsGroupBox.Text = "All Objects";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.allObjectsFlatViewRadioButton);
			this.panel2.Controls.Add(this.allObjectsTreeViewRadioButton);
			this.panel2.Location = new System.Drawing.Point(7, 20);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(260, 22);
			this.panel2.TabIndex = 10;
			// 
			// allObjectsFlatViewRadioButton
			// 
			this.allObjectsFlatViewRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.allObjectsFlatViewRadioButton.AutoSize = true;
			this.allObjectsFlatViewRadioButton.Image = global::UnityProjectBrowser.Properties.Resources.FlatView;
			this.allObjectsFlatViewRadioButton.Location = new System.Drawing.Point(28, 0);
			this.allObjectsFlatViewRadioButton.Name = "allObjectsFlatViewRadioButton";
			this.allObjectsFlatViewRadioButton.Size = new System.Drawing.Size(22, 22);
			this.allObjectsFlatViewRadioButton.TabIndex = 1;
			this.allObjectsFlatViewRadioButton.TabStop = true;
			this.toolTip.SetToolTip(this.allObjectsFlatViewRadioButton, "Flat List View");
			this.allObjectsFlatViewRadioButton.UseVisualStyleBackColor = true;
			// 
			// allObjectsTreeViewRadioButton
			// 
			this.allObjectsTreeViewRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
			this.allObjectsTreeViewRadioButton.AutoSize = true;
			this.allObjectsTreeViewRadioButton.Image = global::UnityProjectBrowser.Properties.Resources.TreeView_713;
			this.allObjectsTreeViewRadioButton.Location = new System.Drawing.Point(0, 0);
			this.allObjectsTreeViewRadioButton.Name = "allObjectsTreeViewRadioButton";
			this.allObjectsTreeViewRadioButton.Size = new System.Drawing.Size(22, 22);
			this.allObjectsTreeViewRadioButton.TabIndex = 0;
			this.allObjectsTreeViewRadioButton.TabStop = true;
			this.toolTip.SetToolTip(this.allObjectsTreeViewRadioButton, "Directory Tree View");
			this.allObjectsTreeViewRadioButton.UseVisualStyleBackColor = true;
			this.allObjectsTreeViewRadioButton.CheckedChanged += new System.EventHandler(this.OnAllObjectsViewChanged);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(filterLabel);
			this.panel1.Controls.Add(this.filterCaseSensitiveCheckBox);
			this.panel1.Controls.Add(this.filterTextBox);
			this.panel1.Location = new System.Drawing.Point(6, 48);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(261, 21);
			this.panel1.TabIndex = 9;
			// 
			// filterLabel
			// 
			filterLabel.AutoSize = true;
			filterLabel.Location = new System.Drawing.Point(0, 3);
			filterLabel.Name = "filterLabel";
			filterLabel.Size = new System.Drawing.Size(29, 13);
			filterLabel.TabIndex = 5;
			filterLabel.Text = "Filter";
			// 
			// filterCaseSensitiveCheckBox
			// 
			this.filterCaseSensitiveCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.filterCaseSensitiveCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.filterCaseSensitiveCheckBox.AutoSize = true;
			this.filterCaseSensitiveCheckBox.Image = global::UnityProjectBrowser.Properties.Resources.case_sensitive;
			this.filterCaseSensitiveCheckBox.Location = new System.Drawing.Point(239, 0);
			this.filterCaseSensitiveCheckBox.Name = "filterCaseSensitiveCheckBox";
			this.filterCaseSensitiveCheckBox.Size = new System.Drawing.Size(22, 22);
			this.filterCaseSensitiveCheckBox.TabIndex = 8;
			this.toolTip.SetToolTip(this.filterCaseSensitiveCheckBox, "Case Sensitive");
			this.filterCaseSensitiveCheckBox.UseVisualStyleBackColor = true;
			// 
			// filterTextBox
			// 
			this.filterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.filterTextBox.Location = new System.Drawing.Point(35, 0);
			this.filterTextBox.Name = "filterTextBox";
			this.filterTextBox.Size = new System.Drawing.Size(198, 20);
			this.filterTextBox.TabIndex = 6;
			// 
			// allObjectsTreeView
			// 
			this.allObjectsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.allObjectsTreeView.Location = new System.Drawing.Point(6, 74);
			this.allObjectsTreeView.Name = "allObjectsTreeView";
			this.allObjectsTreeView.Size = new System.Drawing.Size(261, 474);
			this.allObjectsTreeView.TabIndex = 4;
			this.allObjectsTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnAllObjectsTreeViewSelectionChanged);
			this.allObjectsTreeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.allObjectsTreeView_MouseClick);
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(148, 6);
			// 
			// objectInspectorBox
			// 
			objectInspectorBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			objectInspectorBox.Controls.Add(this.relationshipsIncludeChildrenCheckbox);
			objectInspectorBox.Controls.Add(projectObjectTypeLabel);
			objectInspectorBox.Controls.Add(this.projectObjectTypeTextBox);
			objectInspectorBox.Controls.Add(selectedObjectFilePathLabel);
			objectInspectorBox.Controls.Add(this.selectedObjectFilePathTextBox);
			objectInspectorBox.Controls.Add(selectedObjectFileIdLabel);
			objectInspectorBox.Controls.Add(this.selectedObjectFileIdTextBox);
			objectInspectorBox.Controls.Add(this.selectedObjectGuidTextBox);
			objectInspectorBox.Controls.Add(selectedObjectGuidLabel);
			objectInspectorBox.Controls.Add(this.relationshipsListView);
			objectInspectorBox.Controls.Add(relationshipsLabel);
			objectInspectorBox.Location = new System.Drawing.Point(3, 22);
			objectInspectorBox.Name = "objectInspectorBox";
			objectInspectorBox.Size = new System.Drawing.Size(549, 557);
			objectInspectorBox.TabIndex = 3;
			objectInspectorBox.TabStop = false;
			objectInspectorBox.Text = "Selected Object";
			// 
			// relationshipsIncludeChildrenCheckbox
			// 
			this.relationshipsIncludeChildrenCheckbox.Appearance = System.Windows.Forms.Appearance.Button;
			this.relationshipsIncludeChildrenCheckbox.AutoSize = true;
			this.relationshipsIncludeChildrenCheckbox.Checked = true;
			this.relationshipsIncludeChildrenCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.relationshipsIncludeChildrenCheckbox.Image = global::UnityProjectBrowser.Properties.Resources.DialogGroup_5846_16x;
			this.relationshipsIncludeChildrenCheckbox.Location = new System.Drawing.Point(87, 99);
			this.relationshipsIncludeChildrenCheckbox.Name = "relationshipsIncludeChildrenCheckbox";
			this.relationshipsIncludeChildrenCheckbox.Size = new System.Drawing.Size(22, 22);
			this.relationshipsIncludeChildrenCheckbox.TabIndex = 10;
			this.toolTip.SetToolTip(this.relationshipsIncludeChildrenCheckbox, "Include Child Relationships");
			this.relationshipsIncludeChildrenCheckbox.UseVisualStyleBackColor = true;
			this.relationshipsIncludeChildrenCheckbox.CheckedChanged += new System.EventHandler(this.relationshipsIncludeChildrenCheckbox_CheckedChanged);
			// 
			// projectObjectTypeLabel
			// 
			projectObjectTypeLabel.AutoSize = true;
			projectObjectTypeLabel.Location = new System.Drawing.Point(11, 76);
			projectObjectTypeLabel.Name = "projectObjectTypeLabel";
			projectObjectTypeLabel.Size = new System.Drawing.Size(65, 13);
			projectObjectTypeLabel.TabIndex = 9;
			projectObjectTypeLabel.Text = "Object Type";
			// 
			// projectObjectTypeTextBox
			// 
			this.projectObjectTypeTextBox.Location = new System.Drawing.Point(82, 73);
			this.projectObjectTypeTextBox.Name = "projectObjectTypeTextBox";
			this.projectObjectTypeTextBox.ReadOnly = true;
			this.projectObjectTypeTextBox.Size = new System.Drawing.Size(135, 20);
			this.projectObjectTypeTextBox.TabIndex = 8;
			// 
			// selectedObjectFilePathLabel
			// 
			selectedObjectFilePathLabel.AutoSize = true;
			selectedObjectFilePathLabel.Location = new System.Drawing.Point(10, 49);
			selectedObjectFilePathLabel.Name = "selectedObjectFilePathLabel";
			selectedObjectFilePathLabel.Size = new System.Drawing.Size(48, 13);
			selectedObjectFilePathLabel.TabIndex = 7;
			selectedObjectFilePathLabel.Text = "File Path";
			// 
			// selectedObjectFilePathTextBox
			// 
			this.selectedObjectFilePathTextBox.Location = new System.Drawing.Point(82, 46);
			this.selectedObjectFilePathTextBox.Name = "selectedObjectFilePathTextBox";
			this.selectedObjectFilePathTextBox.ReadOnly = true;
			this.selectedObjectFilePathTextBox.Size = new System.Drawing.Size(461, 20);
			this.selectedObjectFilePathTextBox.TabIndex = 6;
			// 
			// selectedObjectFileIdLabel
			// 
			selectedObjectFileIdLabel.AutoSize = true;
			selectedObjectFileIdLabel.Location = new System.Drawing.Point(316, 22);
			selectedObjectFileIdLabel.Name = "selectedObjectFileIdLabel";
			selectedObjectFileIdLabel.Size = new System.Drawing.Size(35, 13);
			selectedObjectFileIdLabel.TabIndex = 5;
			selectedObjectFileIdLabel.Text = "File Id";
			// 
			// selectedObjectFileIdTextBox
			// 
			this.selectedObjectFileIdTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.selectedObjectFileIdTextBox.Location = new System.Drawing.Point(357, 20);
			this.selectedObjectFileIdTextBox.Name = "selectedObjectFileIdTextBox";
			this.selectedObjectFileIdTextBox.ReadOnly = true;
			this.selectedObjectFileIdTextBox.Size = new System.Drawing.Size(186, 20);
			this.selectedObjectFileIdTextBox.TabIndex = 4;
			this.selectedObjectFileIdTextBox.Text = "0";
			// 
			// selectedObjectGuidTextBox
			// 
			this.selectedObjectGuidTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.selectedObjectGuidTextBox.Location = new System.Drawing.Point(82, 20);
			this.selectedObjectGuidTextBox.Name = "selectedObjectGuidTextBox";
			this.selectedObjectGuidTextBox.ReadOnly = true;
			this.selectedObjectGuidTextBox.Size = new System.Drawing.Size(228, 20);
			this.selectedObjectGuidTextBox.TabIndex = 3;
			this.selectedObjectGuidTextBox.Text = "00000000-0000-0000-0000-000000000000";
			// 
			// selectedObjectGuidLabel
			// 
			selectedObjectGuidLabel.AutoSize = true;
			selectedObjectGuidLabel.Location = new System.Drawing.Point(11, 23);
			selectedObjectGuidLabel.Name = "selectedObjectGuidLabel";
			selectedObjectGuidLabel.Size = new System.Drawing.Size(34, 13);
			selectedObjectGuidLabel.TabIndex = 2;
			selectedObjectGuidLabel.Text = "GUID";
			// 
			// relationshipsListView
			// 
			this.relationshipsListView.AllowColumnReorder = true;
			this.relationshipsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.relationshipsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.targetObjectHeader,
            this.relationshipTypeHeader,
            this.assetGuidHeader,
            this.fileIdHeader,
            this.targetAssetHeader});
			this.relationshipsListView.FullRowSelect = true;
			this.relationshipsListView.HideSelection = false;
			this.relationshipsListView.Location = new System.Drawing.Point(10, 126);
			this.relationshipsListView.Name = "relationshipsListView";
			this.relationshipsListView.Size = new System.Drawing.Size(533, 425);
			this.relationshipsListView.TabIndex = 1;
			this.relationshipsListView.UseCompatibleStateImageBehavior = false;
			this.relationshipsListView.View = System.Windows.Forms.View.Details;
			this.relationshipsListView.DoubleClick += new System.EventHandler(this.OnRelationshipsListViewDoubleClick);
			this.relationshipsListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.relationshipsListView_MouseClick);
			// 
			// targetObjectHeader
			// 
			this.targetObjectHeader.DisplayIndex = 2;
			this.targetObjectHeader.Text = "Object";
			this.targetObjectHeader.Width = 120;
			// 
			// relationshipTypeHeader
			// 
			this.relationshipTypeHeader.DisplayIndex = 0;
			this.relationshipTypeHeader.Text = "Relationship";
			this.relationshipTypeHeader.Width = 120;
			// 
			// assetGuidHeader
			// 
			this.assetGuidHeader.DisplayIndex = 3;
			this.assetGuidHeader.Text = "Asset Guid";
			this.assetGuidHeader.Width = 120;
			// 
			// fileIdHeader
			// 
			this.fileIdHeader.DisplayIndex = 4;
			this.fileIdHeader.Text = "File Id";
			this.fileIdHeader.Width = 100;
			// 
			// targetAssetHeader
			// 
			this.targetAssetHeader.DisplayIndex = 1;
			this.targetAssetHeader.Text = "Asset";
			this.targetAssetHeader.Width = 120;
			// 
			// relationshipsLabel
			// 
			relationshipsLabel.AutoSize = true;
			relationshipsLabel.Location = new System.Drawing.Point(11, 104);
			relationshipsLabel.Name = "relationshipsLabel";
			relationshipsLabel.Size = new System.Drawing.Size(70, 13);
			relationshipsLabel.TabIndex = 0;
			relationshipsLabel.Text = "Relationships";
			// 
			// mainMenuStrip
			// 
			this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
			this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.mainMenuStrip.Name = "mainMenuStrip";
			this.mainMenuStrip.Size = new System.Drawing.Size(862, 24);
			this.mainMenuStrip.TabIndex = 1;
			this.mainMenuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFolderToolStripMenuItem,
            this.recentFoldersToolStripMenuItem,
            toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// openFolderToolStripMenuItem
			// 
			this.openFolderToolStripMenuItem.Image = global::UnityProjectBrowser.Properties.Resources.Open_6529;
			this.openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
			this.openFolderToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.openFolderToolStripMenuItem.Text = "Open Folder";
			this.openFolderToolStripMenuItem.Click += new System.EventHandler(this.openFolderToolStripMenuItem_Click);
			// 
			// recentFoldersToolStripMenuItem
			// 
			this.recentFoldersToolStripMenuItem.Name = "recentFoldersToolStripMenuItem";
			this.recentFoldersToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.recentFoldersToolStripMenuItem.Text = "Recent Folders";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Image = global::UnityProjectBrowser.Properties.Resources.Close_16xLG;
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// viewToolStripMenuItem
			// 
			this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToToolStripMenuItem});
			this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.viewToolStripMenuItem.Text = "View";
			// 
			// goToToolStripMenuItem
			// 
			this.goToToolStripMenuItem.Enabled = false;
			this.goToToolStripMenuItem.Image = global::UnityProjectBrowser.Properties.Resources.Find_VS;
			this.goToToolStripMenuItem.Name = "goToToolStripMenuItem";
			this.goToToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.goToToolStripMenuItem.Text = "Go To";
			this.goToToolStripMenuItem.Click += new System.EventHandler(this.goToToolStripMenuItem_Click);
			// 
			// mainStatusStrip
			// 
			this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parseQueueStatusLabel});
			this.mainStatusStrip.Location = new System.Drawing.Point(0, 612);
			this.mainStatusStrip.Name = "mainStatusStrip";
			this.mainStatusStrip.Size = new System.Drawing.Size(862, 22);
			this.mainStatusStrip.TabIndex = 2;
			this.mainStatusStrip.Text = "statusStrip1";
			// 
			// parseQueueStatusLabel
			// 
			this.parseQueueStatusLabel.Name = "parseQueueStatusLabel";
			this.parseQueueStatusLabel.Size = new System.Drawing.Size(106, 17);
			this.parseQueueStatusLabel.Text = "Files To Parse: 0000";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(12, 27);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(allObjectsGroupBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(objectInspectorBox);
			this.splitContainer1.Size = new System.Drawing.Size(838, 582);
			this.splitContainer1.SplitterDistance = 279;
			this.splitContainer1.TabIndex = 4;
			// 
			// mainToolStrip
			// 
			this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBackButton,
            this.toolStripForwardButton});
			this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
			this.mainToolStrip.Name = "mainToolStrip";
			this.mainToolStrip.Size = new System.Drawing.Size(862, 25);
			this.mainToolStrip.TabIndex = 5;
			this.mainToolStrip.Text = "toolStrip1";
			// 
			// toolStripBackButton
			// 
			this.toolStripBackButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripBackButton.Image = global::UnityProjectBrowser.Properties.Resources.arrow_back_16xLG;
			this.toolStripBackButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripBackButton.Name = "toolStripBackButton";
			this.toolStripBackButton.Size = new System.Drawing.Size(23, 22);
			this.toolStripBackButton.Text = "Back";
			this.toolStripBackButton.Click += new System.EventHandler(this.toolStripBackButton_Click);
			// 
			// toolStripForwardButton
			// 
			this.toolStripForwardButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripForwardButton.Image = global::UnityProjectBrowser.Properties.Resources.arrow_Forward_16xLG;
			this.toolStripForwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripForwardButton.Name = "toolStripForwardButton";
			this.toolStripForwardButton.Size = new System.Drawing.Size(23, 22);
			this.toolStripForwardButton.Text = "Forward";
			this.toolStripForwardButton.Click += new System.EventHandler(this.toolStripForwardButton_Click);
			// 
			// allObjectsContextMenu
			// 
			this.allObjectsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openInExplorerToolStripMenuItem,
            this.editToolStripMenuItem,
            this.reparseToolStripMenuItem});
			this.allObjectsContextMenu.Name = "allObjectsContextMenu";
			this.allObjectsContextMenu.Size = new System.Drawing.Size(181, 92);
			// 
			// openInExplorerToolStripMenuItem
			// 
			this.openInExplorerToolStripMenuItem.Image = global::UnityProjectBrowser.Properties.Resources.FolderOpen_16x16_72;
			this.openInExplorerToolStripMenuItem.Name = "openInExplorerToolStripMenuItem";
			this.openInExplorerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.openInExplorerToolStripMenuItem.Text = "Open in Explorer";
			this.openInExplorerToolStripMenuItem.Click += new System.EventHandler(this.openInExplorerToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.editToolStripMenuItem.Text = "Edit";
			this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
			// 
			// reparseToolStripMenuItem
			// 
			this.reparseToolStripMenuItem.Enabled = false;
			this.reparseToolStripMenuItem.Name = "reparseToolStripMenuItem";
			this.reparseToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.reparseToolStripMenuItem.Text = "Reparse";
			this.reparseToolStripMenuItem.Click += new System.EventHandler(this.reparseToolStripMenuItem_Click);
			// 
			// relationshipsContextMenu
			// 
			this.relationshipsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showInAllObjectsViewToolStripMenuItem});
			this.relationshipsContextMenu.Name = "relationshipsContextMenu";
			this.relationshipsContextMenu.Size = new System.Drawing.Size(205, 26);
			// 
			// showInAllObjectsViewToolStripMenuItem
			// 
			this.showInAllObjectsViewToolStripMenuItem.Name = "showInAllObjectsViewToolStripMenuItem";
			this.showInAllObjectsViewToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.showInAllObjectsViewToolStripMenuItem.Text = "Show in All Objects View";
			this.showInAllObjectsViewToolStripMenuItem.Click += new System.EventHandler(this.showInAllObjectsViewToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(862, 634);
			this.Controls.Add(this.mainToolStrip);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.mainStatusStrip);
			this.Controls.Add(this.mainMenuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.mainMenuStrip;
			this.Name = "MainForm";
			this.Text = "Unity Project Browser";
			allObjectsGroupBox.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			objectInspectorBox.ResumeLayout(false);
			objectInspectorBox.PerformLayout();
			this.mainMenuStrip.ResumeLayout(false);
			this.mainMenuStrip.PerformLayout();
			this.mainStatusStrip.ResumeLayout(false);
			this.mainStatusStrip.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.mainToolStrip.ResumeLayout(false);
			this.mainToolStrip.PerformLayout();
			this.allObjectsContextMenu.ResumeLayout(false);
			this.relationshipsContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.MenuStrip mainMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.StatusStrip mainStatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel parseQueueStatusLabel;
		private System.Windows.Forms.ListView relationshipsListView;
		private System.Windows.Forms.ColumnHeader targetObjectHeader;
		private System.Windows.Forms.ColumnHeader relationshipTypeHeader;
		private System.Windows.Forms.ColumnHeader assetGuidHeader;
		private System.Windows.Forms.ColumnHeader fileIdHeader;
		private System.Windows.Forms.TextBox selectedObjectGuidTextBox;
		private System.Windows.Forms.TreeView allObjectsTreeView;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ToolStrip mainToolStrip;
		private System.Windows.Forms.ToolStripButton toolStripBackButton;
		private System.Windows.Forms.ToolStripButton toolStripForwardButton;
		private System.Windows.Forms.ContextMenuStrip allObjectsContextMenu;
		private System.Windows.Forms.ToolStripMenuItem openInExplorerToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip relationshipsContextMenu;
		private System.Windows.Forms.ToolStripMenuItem showInAllObjectsViewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openFolderToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem recentFoldersToolStripMenuItem;
		private System.Windows.Forms.TextBox filterTextBox;
		private System.Windows.Forms.CheckBox filterCaseSensitiveCheckBox;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.RadioButton allObjectsFlatViewRadioButton;
		private System.Windows.Forms.RadioButton allObjectsTreeViewRadioButton;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.ColumnHeader targetAssetHeader;
		private System.Windows.Forms.TextBox selectedObjectFileIdTextBox;
		private System.Windows.Forms.TextBox selectedObjectFilePathTextBox;
		private System.Windows.Forms.TextBox projectObjectTypeTextBox;
		private System.Windows.Forms.CheckBox relationshipsIncludeChildrenCheckbox;
		private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem goToToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem reparseToolStripMenuItem;
	}
}

