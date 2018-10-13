namespace DBLocalization
{
    partial class AddFromCulture
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFromCulture));
            this.lbCulture = new System.Windows.Forms.Label();
            this.cbCulture = new System.Windows.Forms.ComboBox();
            this.btCancel = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbCulture
            // 
            this.lbCulture.AutoSize = true;
            this.lbCulture.Location = new System.Drawing.Point(12, 12);
            this.lbCulture.Name = "lbCulture";
            this.lbCulture.Size = new System.Drawing.Size(43, 13);
            this.lbCulture.TabIndex = 4;
            this.lbCulture.Text = "Culture:";
            // 
            // cbCulture
            // 
            this.cbCulture.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cbCulture.FormattingEnabled = true;
            this.cbCulture.Location = new System.Drawing.Point(61, 12);
            this.cbCulture.Name = "cbCulture";
            this.cbCulture.Size = new System.Drawing.Size(407, 21);
            this.cbCulture.TabIndex = 3;
            this.cbCulture.SelectedIndexChanged += new System.EventHandler(this.cbCulture_SelectedIndexChanged);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(12, 39);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOK
            // 
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Enabled = false;
            this.btOK.Location = new System.Drawing.Point(393, 39);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 23);
            this.btOK.TabIndex = 6;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // AddFromCulture
            // 
            this.AcceptButton = this.btOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(480, 72);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.lbCulture);
            this.Controls.Add(this.cbCulture);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddFromCulture";
            this.Text = "Add from culture";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbCulture;
        private System.Windows.Forms.ComboBox cbCulture;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOK;
    }
}