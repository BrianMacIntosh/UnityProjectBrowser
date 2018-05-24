// Copyright(c) 2018 Brian MacIntosh
// MIT License

using System;
using System.Text;
using System.Windows.Forms;

namespace UnityProjectBrowser
{
	public partial class GotoForm : Form
	{
		/// <summary>
		/// Returns the key specified in this dialog.
		/// </summary>
		public ProjectBrowser.UnityObjectKey Key
		{
			get
			{
				return new ProjectBrowser.UnityObjectKey(new Guid(guidTextBox.Text), (long)fileIdNumeric.Value);
			}
		}

		public GotoForm()
		{
			InitializeComponent();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void guidTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				Guid newGuid = new Guid(guidTextBox.Text);
			}
			catch (FormatException)
			{
				//TODO: reset to last valid value
				e.Cancel = true;
			}
		}

		private void guidTextBox_TextChanged(object sender, EventArgs e)
		{
			int selection = guidTextBox.SelectionStart;
			guidTextBox.Text = RemoveNonGuidCharacters(guidTextBox.Text);
			guidTextBox.SelectionStart = selection;
		}

		private static string RemoveNonGuidCharacters(string str)
		{
			StringBuilder newString = new StringBuilder();
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] >= 'a' && str[i] <= 'f'
					|| str[i] >= 'A' && str[i] <= 'F'
					|| str[i] >= '0' && str[i] <= '9'
					|| str[i] == '-')
				{
					newString.Append(str[i]);
				}
			}
			return newString.ToString();
		}
	}
}
