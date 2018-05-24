namespace UnityProjectBrowser
{
	partial class GotoForm
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
			System.Windows.Forms.Label guidLabel;
			System.Windows.Forms.Label fileIdLabel;
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.guidTextBox = new System.Windows.Forms.TextBox();
			this.fileIdNumeric = new System.Windows.Forms.NumericUpDown();
			guidLabel = new System.Windows.Forms.Label();
			fileIdLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.fileIdNumeric)).BeginInit();
			this.SuspendLayout();
			// 
			// guidLabel
			// 
			guidLabel.AutoSize = true;
			guidLabel.Location = new System.Drawing.Point(14, 15);
			guidLabel.Name = "guidLabel";
			guidLabel.Size = new System.Drawing.Size(34, 13);
			guidLabel.TabIndex = 2;
			guidLabel.Text = "GUID";
			// 
			// fileIdLabel
			// 
			fileIdLabel.AutoSize = true;
			fileIdLabel.Location = new System.Drawing.Point(14, 42);
			fileIdLabel.Name = "fileIdLabel";
			fileIdLabel.Size = new System.Drawing.Size(37, 13);
			fileIdLabel.TabIndex = 4;
			fileIdLabel.Text = "File ID";
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(203, 72);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(122, 71);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 2;
			this.okButton.Text = "Go";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// guidTextBox
			// 
			this.guidTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
			this.guidTextBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.guidTextBox.Location = new System.Drawing.Point(54, 13);
			this.guidTextBox.MaxLength = 36;
			this.guidTextBox.Name = "guidTextBox";
			this.guidTextBox.Size = new System.Drawing.Size(224, 20);
			this.guidTextBox.TabIndex = 0;
			this.guidTextBox.Text = "00000000-0000-0000-0000-000000000000";
			this.guidTextBox.TextChanged += new System.EventHandler(this.guidTextBox_TextChanged);
			this.guidTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.guidTextBox_Validating);
			// 
			// fileIdNumeric
			// 
			this.fileIdNumeric.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.fileIdNumeric.Location = new System.Drawing.Point(54, 40);
			this.fileIdNumeric.Name = "fileIdNumeric";
			this.fileIdNumeric.Size = new System.Drawing.Size(224, 20);
			this.fileIdNumeric.TabIndex = 1;
			// 
			// GotoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(290, 107);
			this.Controls.Add(this.fileIdNumeric);
			this.Controls.Add(fileIdLabel);
			this.Controls.Add(this.guidTextBox);
			this.Controls.Add(guidLabel);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GotoForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Go To";
			((System.ComponentModel.ISupportInitialize)(this.fileIdNumeric)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.TextBox guidTextBox;
		private System.Windows.Forms.NumericUpDown fileIdNumeric;
	}
}