// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System.Windows.Forms;

namespace ProjectBrowser
{
	public static class ListViewExtender
	{
		/// <summary>
		/// Clears all selections in the <see cref="ListView"/>.
		/// </summary>
		public static void ClearSelected(this ListView listView)
		{
			foreach (ListViewItem item in listView.SelectedItems)
			{
				item.Selected = false;
			}
		}
	}
}
