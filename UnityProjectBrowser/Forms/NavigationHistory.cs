// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjectBrowser
{
	/// <summary>
	/// Tracks the history of navigation through an element.
	/// </summary>
	public class NavigationHistory : IDisposable
	{
		public readonly TreeView TargetView;

		public readonly int MaxHistory;

		private int m_selectionPointer = 0;

		private List<TreeNode> m_selectionHistory;

		private bool m_isSelecting = false;

		public NavigationHistory(TreeView targetView, int maxHistory)
		{
			MaxHistory = maxHistory;

			TargetView = targetView;
			TargetView.AfterSelect += TargetView_AfterSelect;

			//TODO: remove nodes that go missing from the tree?

			m_selectionHistory = new List<TreeNode>(maxHistory);
		}

		/// <summary>
		/// Goes back one step in the history.
		/// </summary>
		public void Back()
		{
			if (m_selectionHistory.Count > 0)
			{
				m_selectionPointer = Math.Max(m_selectionPointer - 1, 0);
				SelectNode(m_selectionHistory[m_selectionPointer]);
			}
		}

		/// <summary>
		/// Goes forward one step in the history.
		/// </summary>
		public void Forward()
		{
			if (m_selectionHistory.Count > 0)
			{
				m_selectionPointer = Math.Min(m_selectionPointer + 1, m_selectionHistory.Count - 1);
				SelectNode(m_selectionHistory[m_selectionPointer]);
			}
		}

		private void SelectNode(TreeNode node)
		{
			m_isSelecting = true;
			try
			{
				TargetView.SelectedNode = node;
			}
			finally
			{
				m_isSelecting = false;
			}
		}

		private void TargetView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (m_isSelecting)
			{
				// do not record a history item if I'm the one making the change
				return;
			}
			if (m_selectionPointer < m_selectionHistory.Count)
			{
				m_selectionHistory.RemoveRange(m_selectionPointer + 1, m_selectionHistory.Count - (m_selectionPointer + 1));
			}
			if (m_selectionHistory.Count >= MaxHistory)
			{
				m_selectionHistory.RemoveAt(0);
			}
			m_selectionHistory.Add(e.Node);
			m_selectionPointer = m_selectionHistory.Count - 1;
		}

		#region IDisposable Support

		private bool disposedValue = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					TargetView.AfterSelect -= TargetView_AfterSelect;
				}
				
				disposedValue = true;
			}
		}
		
		public void Dispose()
		{
			Dispose(true);
		}

		#endregion
	}
}
