namespace VPKSoft.DBLocalization
{
    partial class LocalizeMainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocalizeMainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLoadDB = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFomEN_US = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAddFromCulture = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRemoveUnused = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelectCurrentCulture = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelectSomeCulture = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExportDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tcTranslate = new System.Windows.Forms.TabControl();
            this.tabFormItems = new System.Windows.Forms.TabPage();
            this.gvFormItems = new System.Windows.Forms.DataGridView();
            this.colApp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colForm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCulture = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPropertyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValueType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInUse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lbCulture = new System.Windows.Forms.Label();
            this.cbCulture = new System.Windows.Forms.ComboBox();
            this.tabMessages = new System.Windows.Forms.TabPage();
            this.gvMessages = new System.Windows.Forms.DataGridView();
            this.colMessageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessageValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCommentEnUs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMessageCulture = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMsgInUse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.odSQLite = new System.Windows.Forms.OpenFileDialog();
            this.sdSQLite = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.tcTranslate.SuspendLayout();
            this.tabFormItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvFormItems)).BeginInit();
            this.tabMessages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvMessages)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1138, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "msMain";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLoadDB,
            this.mnuSave,
            this.mnuAddFomEN_US,
            this.mnuAddFromCulture,
            this.mnuRemoveUnused,
            this.mnuSelectCurrentCulture,
            this.mnuSelectSomeCulture,
            this.toolStripMenuItem1,
            this.mnuExportDatabase});
            this.mnuFile.Image = global::VPKSoft.Properties.Resources.List;
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(53, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuLoadDB
            // 
            this.mnuLoadDB.Image = global::VPKSoft.Properties.Resources.Load;
            this.mnuLoadDB.Name = "mnuLoadDB";
            this.mnuLoadDB.Size = new System.Drawing.Size(208, 22);
            this.mnuLoadDB.Text = "Load language database";
            this.mnuLoadDB.Click += new System.EventHandler(this.mnuLoadDB_Click);
            // 
            // mnuSave
            // 
            this.mnuSave.Enabled = false;
            this.mnuSave.Image = global::VPKSoft.Properties.Resources.Save;
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(208, 22);
            this.mnuSave.Text = "Save";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuAddFomEN_US
            // 
            this.mnuAddFomEN_US.Enabled = false;
            this.mnuAddFomEN_US.Image = global::VPKSoft.Properties.Resources.Sync;
            this.mnuAddFomEN_US.Name = "mnuAddFomEN_US";
            this.mnuAddFomEN_US.Size = new System.Drawing.Size(208, 22);
            this.mnuAddFomEN_US.Text = "Add from culture (en_US)";
            this.mnuAddFomEN_US.Click += new System.EventHandler(this.mnuAddFomEN_US_Click);
            // 
            // mnuAddFromCulture
            // 
            this.mnuAddFromCulture.Enabled = false;
            this.mnuAddFromCulture.Image = global::VPKSoft.Properties.Resources.Refresh;
            this.mnuAddFromCulture.Name = "mnuAddFromCulture";
            this.mnuAddFromCulture.Size = new System.Drawing.Size(208, 22);
            this.mnuAddFromCulture.Text = "Add from culture...";
            this.mnuAddFromCulture.Click += new System.EventHandler(this.mnuAddFromCulture_Click);
            // 
            // mnuRemoveUnused
            // 
            this.mnuRemoveUnused.Enabled = false;
            this.mnuRemoveUnused.Image = global::VPKSoft.Properties.Resources.Delete;
            this.mnuRemoveUnused.Name = "mnuRemoveUnused";
            this.mnuRemoveUnused.Size = new System.Drawing.Size(208, 22);
            this.mnuRemoveUnused.Text = "Remove unused entries";
            this.mnuRemoveUnused.Click += new System.EventHandler(this.mnuRemoveUnused_Click);
            // 
            // mnuSelectCurrentCulture
            // 
            this.mnuSelectCurrentCulture.Enabled = false;
            this.mnuSelectCurrentCulture.Image = global::VPKSoft.Properties.Resources.Target;
            this.mnuSelectCurrentCulture.Name = "mnuSelectCurrentCulture";
            this.mnuSelectCurrentCulture.Size = new System.Drawing.Size(208, 22);
            this.mnuSelectCurrentCulture.Text = "Select current culture";
            this.mnuSelectCurrentCulture.Click += new System.EventHandler(this.mnuSelectCurrentCulture_Click);
            // 
            // mnuSelectSomeCulture
            // 
            this.mnuSelectSomeCulture.Enabled = false;
            this.mnuSelectSomeCulture.Image = global::VPKSoft.Properties.Resources.Yellow_pin;
            this.mnuSelectSomeCulture.Name = "mnuSelectSomeCulture";
            this.mnuSelectSomeCulture.Size = new System.Drawing.Size(208, 22);
            this.mnuSelectSomeCulture.Text = "Select culture...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(205, 6);
            // 
            // mnuExportDatabase
            // 
            this.mnuExportDatabase.Enabled = false;
            this.mnuExportDatabase.Image = global::VPKSoft.Properties.Resources.database_down;
            this.mnuExportDatabase.Name = "mnuExportDatabase";
            this.mnuExportDatabase.Size = new System.Drawing.Size(208, 22);
            this.mnuExportDatabase.Text = "Export database as";
            this.mnuExportDatabase.Click += new System.EventHandler(this.mnuExportDatabase_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
            this.mnuHelp.Image = global::VPKSoft.Properties.Resources.Help_symbol;
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(60, 20);
            this.mnuHelp.Text = "Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Image = global::VPKSoft.Properties.Resources.About;
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(107, 22);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // tcTranslate
            // 
            this.tcTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcTranslate.Controls.Add(this.tabFormItems);
            this.tcTranslate.Controls.Add(this.tabMessages);
            this.tcTranslate.Location = new System.Drawing.Point(12, 27);
            this.tcTranslate.Name = "tcTranslate";
            this.tcTranslate.SelectedIndex = 0;
            this.tcTranslate.Size = new System.Drawing.Size(1114, 518);
            this.tcTranslate.TabIndex = 1;
            // 
            // tabFormItems
            // 
            this.tabFormItems.Controls.Add(this.gvFormItems);
            this.tabFormItems.Controls.Add(this.lbCulture);
            this.tabFormItems.Controls.Add(this.cbCulture);
            this.tabFormItems.Location = new System.Drawing.Point(4, 22);
            this.tabFormItems.Name = "tabFormItems";
            this.tabFormItems.Padding = new System.Windows.Forms.Padding(3);
            this.tabFormItems.Size = new System.Drawing.Size(1106, 492);
            this.tabFormItems.TabIndex = 0;
            this.tabFormItems.Text = "Form items";
            this.tabFormItems.UseVisualStyleBackColor = true;
            // 
            // gvFormItems
            // 
            this.gvFormItems.AllowUserToAddRows = false;
            this.gvFormItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvFormItems.BackgroundColor = System.Drawing.Color.White;
            this.gvFormItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvFormItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colApp,
            this.colForm,
            this.colItem,
            this.colCulture,
            this.colPropertyName,
            this.colValue,
            this.colValueType,
            this.colInUse});
            this.gvFormItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.gvFormItems.GridColor = System.Drawing.Color.Black;
            this.gvFormItems.Location = new System.Drawing.Point(3, 33);
            this.gvFormItems.Name = "gvFormItems";
            this.gvFormItems.Size = new System.Drawing.Size(1100, 456);
            this.gvFormItems.TabIndex = 2;
            this.gvFormItems.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvFormItems_CellDoubleClick);
            this.gvFormItems.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvFormItems_CellMouseDoubleClick);
            // 
            // colApp
            // 
            this.colApp.HeaderText = "Application";
            this.colApp.Name = "colApp";
            this.colApp.ReadOnly = true;
            // 
            // colForm
            // 
            this.colForm.HeaderText = "Form";
            this.colForm.Name = "colForm";
            this.colForm.ReadOnly = true;
            this.colForm.Width = 150;
            // 
            // colItem
            // 
            this.colItem.HeaderText = "Item";
            this.colItem.Name = "colItem";
            this.colItem.ReadOnly = true;
            this.colItem.Width = 150;
            // 
            // colCulture
            // 
            this.colCulture.HeaderText = "Culture";
            this.colCulture.Name = "colCulture";
            this.colCulture.ReadOnly = true;
            this.colCulture.Width = 80;
            // 
            // colPropertyName
            // 
            this.colPropertyName.HeaderText = "Property name";
            this.colPropertyName.Name = "colPropertyName";
            this.colPropertyName.ReadOnly = true;
            this.colPropertyName.Width = 150;
            // 
            // colValue
            // 
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            this.colValue.Width = 200;
            // 
            // colValueType
            // 
            this.colValueType.HeaderText = "Value type";
            this.colValueType.Name = "colValueType";
            this.colValueType.ReadOnly = true;
            this.colValueType.Width = 150;
            // 
            // colInUse
            // 
            this.colInUse.HeaderText = "In use";
            this.colInUse.Name = "colInUse";
            this.colInUse.ReadOnly = true;
            this.colInUse.Width = 50;
            // 
            // lbCulture
            // 
            this.lbCulture.AutoSize = true;
            this.lbCulture.Location = new System.Drawing.Point(6, 9);
            this.lbCulture.Name = "lbCulture";
            this.lbCulture.Size = new System.Drawing.Size(43, 13);
            this.lbCulture.TabIndex = 2;
            this.lbCulture.Text = "Culture:";
            // 
            // cbCulture
            // 
            this.cbCulture.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cbCulture.Enabled = false;
            this.cbCulture.FormattingEnabled = true;
            this.cbCulture.Location = new System.Drawing.Point(55, 6);
            this.cbCulture.Name = "cbCulture";
            this.cbCulture.Size = new System.Drawing.Size(402, 21);
            this.cbCulture.TabIndex = 1;
            this.cbCulture.SelectedIndexChanged += new System.EventHandler(this.cbCulture_SelectedIndexChanged);
            // 
            // tabMessages
            // 
            this.tabMessages.Controls.Add(this.gvMessages);
            this.tabMessages.Location = new System.Drawing.Point(4, 22);
            this.tabMessages.Name = "tabMessages";
            this.tabMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tabMessages.Size = new System.Drawing.Size(1106, 492);
            this.tabMessages.TabIndex = 1;
            this.tabMessages.Text = "Messages";
            this.tabMessages.UseVisualStyleBackColor = true;
            // 
            // gvMessages
            // 
            this.gvMessages.AllowUserToAddRows = false;
            this.gvMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvMessages.BackgroundColor = System.Drawing.Color.White;
            this.gvMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMessageName,
            this.colMessageValue,
            this.colCommentEnUs,
            this.colMessageCulture,
            this.colMsgInUse});
            this.gvMessages.GridColor = System.Drawing.Color.Black;
            this.gvMessages.Location = new System.Drawing.Point(3, 3);
            this.gvMessages.Name = "gvMessages";
            this.gvMessages.Size = new System.Drawing.Size(1100, 486);
            this.gvMessages.TabIndex = 3;
            // 
            // colMessageName
            // 
            this.colMessageName.HeaderText = "Message name";
            this.colMessageName.Name = "colMessageName";
            this.colMessageName.ReadOnly = true;
            this.colMessageName.Width = 150;
            // 
            // colMessageValue
            // 
            this.colMessageValue.HeaderText = "Value";
            this.colMessageValue.Name = "colMessageValue";
            this.colMessageValue.Width = 200;
            // 
            // colCommentEnUs
            // 
            this.colCommentEnUs.HeaderText = "Comment (en-US)";
            this.colCommentEnUs.Name = "colCommentEnUs";
            this.colCommentEnUs.ReadOnly = true;
            this.colCommentEnUs.Width = 300;
            // 
            // colMessageCulture
            // 
            this.colMessageCulture.HeaderText = "Culture";
            this.colMessageCulture.Name = "colMessageCulture";
            this.colMessageCulture.ReadOnly = true;
            this.colMessageCulture.Width = 80;
            // 
            // colMsgInUse
            // 
            this.colMsgInUse.HeaderText = "In use";
            this.colMsgInUse.Name = "colMsgInUse";
            this.colMsgInUse.ReadOnly = true;
            this.colMsgInUse.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colMsgInUse.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colMsgInUse.Width = 60;
            // 
            // odSQLite
            // 
            this.odSQLite.Filter = "SQLite database (*.sqlite*)|*.sqlite*|All files (*.*)|*.*";
            this.odSQLite.Title = "Select database file";
            // 
            // sdSQLite
            // 
            this.sdSQLite.Filter = "SQLite database (*.sqlite*)|*.sqlite*|All files (*.*)|*.*";
            // 
            // LocalizeMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 557);
            this.Controls.Add(this.tcTranslate);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "LocalizeMainWindow";
            this.Text = "DBLocalization";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tcTranslate.ResumeLayout(false);
            this.tabFormItems.ResumeLayout(false);
            this.tabFormItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvFormItems)).EndInit();
            this.tabMessages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvMessages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuLoadDB;
        private System.Windows.Forms.TabControl tcTranslate;
        private System.Windows.Forms.TabPage tabFormItems;
        private System.Windows.Forms.Label lbCulture;
        private System.Windows.Forms.ComboBox cbCulture;
        private System.Windows.Forms.TabPage tabMessages;
        private System.Windows.Forms.OpenFileDialog odSQLite;
        private System.Windows.Forms.DataGridView gvFormItems;
        private System.Windows.Forms.DataGridView gvMessages;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFomEN_US;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessageName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessageValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCommentEnUs;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMessageCulture;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colMsgInUse;
        private System.Windows.Forms.ToolStripMenuItem mnuRemoveUnused;
        private System.Windows.Forms.DataGridViewTextBoxColumn colApp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colForm;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCulture;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPropertyName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValueType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colInUse;
        private System.Windows.Forms.ToolStripMenuItem mnuSelectCurrentCulture;
        private System.Windows.Forms.ToolStripMenuItem mnuAddFromCulture;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuSelectSomeCulture;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuExportDatabase;
        private System.Windows.Forms.SaveFileDialog sdSQLite;
    }
}

