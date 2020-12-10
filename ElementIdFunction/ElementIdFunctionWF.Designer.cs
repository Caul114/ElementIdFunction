
namespace ElementIdFunction
{
    partial class ElementIdFunctionWF
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
            this.functionGroupBox = new System.Windows.Forms.GroupBox();
            this.okButton = new System.Windows.Forms.Button();
            this.functionListBox = new System.Windows.Forms.ListBox();
            this.functionGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // functionGroupBox
            // 
            this.functionGroupBox.Controls.Add(this.okButton);
            this.functionGroupBox.Controls.Add(this.functionListBox);
            this.functionGroupBox.Location = new System.Drawing.Point(13, 13);
            this.functionGroupBox.Name = "functionGroupBox";
            this.functionGroupBox.Size = new System.Drawing.Size(302, 449);
            this.functionGroupBox.TabIndex = 0;
            this.functionGroupBox.TabStop = false;
            this.functionGroupBox.Text = "List of ElementsID";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(221, 416);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "Close";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // functionListBox
            // 
            this.functionListBox.FormattingEnabled = true;
            this.functionListBox.ItemHeight = 16;
            this.functionListBox.Location = new System.Drawing.Point(7, 22);
            this.functionListBox.Name = "functionListBox";
            this.functionListBox.Size = new System.Drawing.Size(289, 388);
            this.functionListBox.TabIndex = 0;
            // 
            // ElementIdFunctionWF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 474);
            this.Controls.Add(this.functionGroupBox);
            this.MaximizeBox = false;
            this.Name = "ElementIdFunctionWF";
            this.ShowInTaskbar = false;
            this.Text = "Element Id Function WF";
            this.functionGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox functionGroupBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.ListBox functionListBox;
    }
}