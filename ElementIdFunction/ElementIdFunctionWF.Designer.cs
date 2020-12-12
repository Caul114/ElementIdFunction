
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
            this.pickerButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.okButton = new System.Windows.Forms.Button();
            this.functionListBox = new System.Windows.Forms.ListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.functionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // functionGroupBox
            // 
            this.functionGroupBox.Controls.Add(this.pickerButton);
            this.functionGroupBox.Controls.Add(this.functionListBox);
            this.functionGroupBox.Controls.Add(this.pictureBox1);
            this.functionGroupBox.Location = new System.Drawing.Point(13, 12);
            this.functionGroupBox.Name = "functionGroupBox";
            this.functionGroupBox.Size = new System.Drawing.Size(302, 464);
            this.functionGroupBox.TabIndex = 0;
            this.functionGroupBox.TabStop = false;
            this.functionGroupBox.Text = "Famiglia scelta";
            // 
            // pickerButton
            // 
            this.pickerButton.Location = new System.Drawing.Point(7, 31);
            this.pickerButton.Name = "pickerButton";
            this.pickerButton.Size = new System.Drawing.Size(289, 23);
            this.pickerButton.TabIndex = 2;
            this.pickerButton.Text = "Cattura l\'oggetto";
            this.pickerButton.UseVisualStyleBackColor = false;
            this.pickerButton.Click += new System.EventHandler(this.pickerButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(7, 69);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(289, 219);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(892, 453);
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
            this.functionListBox.Location = new System.Drawing.Point(7, 294);
            this.functionListBox.Name = "functionListBox";
            this.functionListBox.Size = new System.Drawing.Size(289, 164);
            this.functionListBox.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(349, 43);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(618, 394);
            this.dataGridView1.TabIndex = 2;
            // 
            // ElementIdFunctionWF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 488);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.functionGroupBox);
            this.MaximizeBox = false;
            this.Name = "ElementIdFunctionWF";
            this.ShowInTaskbar = false;
            this.Text = "Element Id Function WF";
            this.functionGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox functionGroupBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button pickerButton;
        private System.Windows.Forms.ListBox functionListBox;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}