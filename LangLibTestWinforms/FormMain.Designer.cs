namespace LangLibTestWinforms
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.btMessageTest = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tbShortDesc = new System.Windows.Forms.TextBox();
            this.lbCurrentSysCultureText = new System.Windows.Forms.Label();
            this.lbFallbackCultureText = new System.Windows.Forms.Label();
            this.lbTimeTookToLoad = new System.Windows.Forms.Label();
            this.lbCurrentSysCulture = new System.Windows.Forms.Label();
            this.lbFallbackCulture = new System.Windows.Forms.Label();
            this.lbLoadSec = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btMessageTest
            // 
            this.btMessageTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btMessageTest.Location = new System.Drawing.Point(302, 195);
            this.btMessageTest.Name = "btMessageTest";
            this.btMessageTest.Size = new System.Drawing.Size(99, 23);
            this.btMessageTest.TabIndex = 0;
            this.btMessageTest.Text = "Message Test";
            this.btMessageTest.UseVisualStyleBackColor = true;
            this.btMessageTest.Click += new System.EventHandler(this.btMessageTest_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(413, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Tag = "Name=mnuHelp";
            this.mnuHelp.Text = "Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(152, 22);
            this.mnuAbout.Tag = "Name=mnuAbout";
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(12, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbShortDesc);
            this.splitContainer1.Panel1.Controls.Add(this.lbCurrentSysCultureText);
            this.splitContainer1.Panel1.Controls.Add(this.lbFallbackCultureText);
            this.splitContainer1.Panel1.Controls.Add(this.lbTimeTookToLoad);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbCurrentSysCulture);
            this.splitContainer1.Panel2.Controls.Add(this.lbFallbackCulture);
            this.splitContainer1.Panel2.Controls.Add(this.lbLoadSec);
            this.splitContainer1.Size = new System.Drawing.Size(389, 162);
            this.splitContainer1.SplitterDistance = 282;
            this.splitContainer1.TabIndex = 3;
            // 
            // tbShortDesc
            // 
            this.tbShortDesc.Location = new System.Drawing.Point(6, 72);
            this.tbShortDesc.Multiline = true;
            this.tbShortDesc.Name = "tbShortDesc";
            this.tbShortDesc.ReadOnly = true;
            this.tbShortDesc.Size = new System.Drawing.Size(267, 79);
            this.tbShortDesc.TabIndex = 6;
            this.tbShortDesc.Text = "Use help --> About menu to see the loading time\r\nto increase... (very small amoun" +
    "ts)\"\r\nThe first time the library creates a SQLite database\r\nconnection so the Ma" +
    "inWindow loading took\r\na bit longer.\r\n";
            // 
            // lbCurrentSysCultureText
            // 
            this.lbCurrentSysCultureText.Location = new System.Drawing.Point(3, 46);
            this.lbCurrentSysCultureText.Name = "lbCurrentSysCultureText";
            this.lbCurrentSysCultureText.Size = new System.Drawing.Size(204, 23);
            this.lbCurrentSysCultureText.TabIndex = 5;
            this.lbCurrentSysCultureText.Text = "Current system culture:";
            // 
            // lbFallbackCultureText
            // 
            this.lbFallbackCultureText.Location = new System.Drawing.Point(3, 23);
            this.lbFallbackCultureText.Name = "lbFallbackCultureText";
            this.lbFallbackCultureText.Size = new System.Drawing.Size(204, 23);
            this.lbFallbackCultureText.TabIndex = 4;
            this.lbFallbackCultureText.Text = "Fallback culture name for localization:";
            // 
            // lbTimeTookToLoad
            // 
            this.lbTimeTookToLoad.Location = new System.Drawing.Point(3, 0);
            this.lbTimeTookToLoad.Name = "lbTimeTookToLoad";
            this.lbTimeTookToLoad.Size = new System.Drawing.Size(204, 23);
            this.lbTimeTookToLoad.TabIndex = 3;
            this.lbTimeTookToLoad.Text = "Time the LangLib took to load(s):";
            // 
            // lbCurrentSysCulture
            // 
            this.lbCurrentSysCulture.Location = new System.Drawing.Point(3, 46);
            this.lbCurrentSysCulture.Name = "lbCurrentSysCulture";
            this.lbCurrentSysCulture.Size = new System.Drawing.Size(94, 23);
            this.lbCurrentSysCulture.TabIndex = 2;
            this.lbCurrentSysCulture.Text = "loadTime(s)";
            // 
            // lbFallbackCulture
            // 
            this.lbFallbackCulture.Location = new System.Drawing.Point(3, 23);
            this.lbFallbackCulture.Name = "lbFallbackCulture";
            this.lbFallbackCulture.Size = new System.Drawing.Size(94, 23);
            this.lbFallbackCulture.TabIndex = 1;
            this.lbFallbackCulture.Text = "Fallback culture";
            // 
            // lbLoadSec
            // 
            this.lbLoadSec.Location = new System.Drawing.Point(3, 0);
            this.lbLoadSec.Name = "lbLoadSec";
            this.lbLoadSec.Size = new System.Drawing.Size(94, 23);
            this.lbLoadSec.TabIndex = 0;
            this.lbLoadSec.Text = "loadTime(s)";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 230);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btMessageTest);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "LangLibTestWinforms";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btMessageTest;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lbTimeTookToLoad;
        private System.Windows.Forms.Label lbLoadSec;
        private System.Windows.Forms.Label lbFallbackCultureText;
        private System.Windows.Forms.Label lbCurrentSysCultureText;
        private System.Windows.Forms.Label lbCurrentSysCulture;
        private System.Windows.Forms.Label lbFallbackCulture;
        private System.Windows.Forms.TextBox tbShortDesc;

    }
}

