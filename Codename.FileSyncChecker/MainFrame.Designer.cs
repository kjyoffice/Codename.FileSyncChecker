namespace Codename.FileSyncChecker
{
    partial class MainFrame
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
            this.tbRightDirectory = new System.Windows.Forms.TextBox();
            this.tbLeftDirectory = new System.Windows.Forms.TextBox();
            this.btnRightDirectory = new System.Windows.Forms.Button();
            this.btnLeftDirectory = new System.Windows.Forms.Button();
            this.ssMainStatusBar = new System.Windows.Forms.StatusStrip();
            this.tsslblWorkMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbWorkProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.lblLeftDirectory = new System.Windows.Forms.Label();
            this.lblRightDirectory = new System.Windows.Forms.Label();
            this.fbdWorkDirectory = new System.Windows.Forms.FolderBrowserDialog();
            this.btnStartFileHashCheck = new System.Windows.Forms.Button();
            this.tcCheckResultItem = new System.Windows.Forms.TabControl();
            this.tpMatchPartTab = new System.Windows.Forms.TabPage();
            this.tpOnlyLeftPartTab = new System.Windows.Forms.TabPage();
            this.tpOnlyRightPartTab = new System.Windows.Forms.TabPage();
            this.btnStartGetSyncFile = new System.Windows.Forms.Button();
            this.btnStartFileSync = new System.Windows.Forms.Button();
            this.cbSyncDirectoryPosition = new System.Windows.Forms.ComboBox();
            this.btnAllClear = new System.Windows.Forms.Button();
            this.msMainMenuBar = new System.Windows.Forms.MenuStrip();
            this.tsmiItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiItem_UpdateCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiItem_EULA = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiItem_Help_CutLine1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiItem_About = new System.Windows.Forms.ToolStripMenuItem();
            this.gbControlBox = new System.Windows.Forms.GroupBox();
            this.ssMainStatusBar.SuspendLayout();
            this.tcCheckResultItem.SuspendLayout();
            this.msMainMenuBar.SuspendLayout();
            this.gbControlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbRightDirectory
            // 
            this.tbRightDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRightDirectory.BackColor = System.Drawing.SystemColors.Window;
            this.tbRightDirectory.Location = new System.Drawing.Point(53, 47);
            this.tbRightDirectory.Name = "tbRightDirectory";
            this.tbRightDirectory.ReadOnly = true;
            this.tbRightDirectory.Size = new System.Drawing.Size(807, 21);
            this.tbRightDirectory.TabIndex = 6;
            // 
            // tbLeftDirectory
            // 
            this.tbLeftDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLeftDirectory.BackColor = System.Drawing.SystemColors.Window;
            this.tbLeftDirectory.Location = new System.Drawing.Point(53, 20);
            this.tbLeftDirectory.Name = "tbLeftDirectory";
            this.tbLeftDirectory.ReadOnly = true;
            this.tbLeftDirectory.Size = new System.Drawing.Size(807, 21);
            this.tbLeftDirectory.TabIndex = 3;
            // 
            // btnRightDirectory
            // 
            this.btnRightDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRightDirectory.Location = new System.Drawing.Point(866, 45);
            this.btnRightDirectory.Name = "btnRightDirectory";
            this.btnRightDirectory.Size = new System.Drawing.Size(46, 23);
            this.btnRightDirectory.TabIndex = 7;
            this.btnRightDirectory.Text = "...";
            this.btnRightDirectory.UseVisualStyleBackColor = true;
            this.btnRightDirectory.Click += new System.EventHandler(this.btnLeftOrRightDirectory_Click);
            // 
            // btnLeftDirectory
            // 
            this.btnLeftDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLeftDirectory.Location = new System.Drawing.Point(866, 18);
            this.btnLeftDirectory.Name = "btnLeftDirectory";
            this.btnLeftDirectory.Size = new System.Drawing.Size(46, 23);
            this.btnLeftDirectory.TabIndex = 4;
            this.btnLeftDirectory.Text = "...";
            this.btnLeftDirectory.UseVisualStyleBackColor = true;
            this.btnLeftDirectory.Click += new System.EventHandler(this.btnLeftOrRightDirectory_Click);
            // 
            // ssMainStatusBar
            // 
            this.ssMainStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslblWorkMessage,
            this.tspbWorkProgress});
            this.ssMainStatusBar.Location = new System.Drawing.Point(0, 651);
            this.ssMainStatusBar.Name = "ssMainStatusBar";
            this.ssMainStatusBar.Size = new System.Drawing.Size(942, 22);
            this.ssMainStatusBar.TabIndex = 13;
            this.ssMainStatusBar.Text = "statusStrip1";
            // 
            // tsslblWorkMessage
            // 
            this.tsslblWorkMessage.Name = "tsslblWorkMessage";
            this.tsslblWorkMessage.Size = new System.Drawing.Size(725, 17);
            this.tsslblWorkMessage.Spring = true;
            this.tsslblWorkMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tspbWorkProgress
            // 
            this.tspbWorkProgress.Name = "tspbWorkProgress";
            this.tspbWorkProgress.Size = new System.Drawing.Size(200, 16);
            // 
            // lblLeftDirectory
            // 
            this.lblLeftDirectory.Location = new System.Drawing.Point(6, 25);
            this.lblLeftDirectory.Name = "lblLeftDirectory";
            this.lblLeftDirectory.Size = new System.Drawing.Size(41, 12);
            this.lblLeftDirectory.TabIndex = 2;
            this.lblLeftDirectory.Text = "Left";
            this.lblLeftDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRightDirectory
            // 
            this.lblRightDirectory.Location = new System.Drawing.Point(6, 51);
            this.lblRightDirectory.Name = "lblRightDirectory";
            this.lblRightDirectory.Size = new System.Drawing.Size(41, 12);
            this.lblRightDirectory.TabIndex = 5;
            this.lblRightDirectory.Text = "Right";
            this.lblRightDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fbdWorkDirectory
            // 
            this.fbdWorkDirectory.ShowNewFolderButton = false;
            // 
            // btnStartFileHashCheck
            // 
            this.btnStartFileHashCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartFileHashCheck.Location = new System.Drawing.Point(6, 74);
            this.btnStartFileHashCheck.Name = "btnStartFileHashCheck";
            this.btnStartFileHashCheck.Size = new System.Drawing.Size(906, 23);
            this.btnStartFileHashCheck.TabIndex = 1;
            this.btnStartFileHashCheck.Text = "FileHash";
            this.btnStartFileHashCheck.UseVisualStyleBackColor = true;
            this.btnStartFileHashCheck.Click += new System.EventHandler(this.btnStartFileXCommand_Click);
            // 
            // tcCheckResultItem
            // 
            this.tcCheckResultItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcCheckResultItem.Controls.Add(this.tpMatchPartTab);
            this.tcCheckResultItem.Controls.Add(this.tpOnlyLeftPartTab);
            this.tcCheckResultItem.Controls.Add(this.tpOnlyRightPartTab);
            this.tcCheckResultItem.Location = new System.Drawing.Point(12, 165);
            this.tcCheckResultItem.Name = "tcCheckResultItem";
            this.tcCheckResultItem.SelectedIndex = 0;
            this.tcCheckResultItem.Size = new System.Drawing.Size(918, 483);
            this.tcCheckResultItem.TabIndex = 8;
            // 
            // tpMatchPartTab
            // 
            this.tpMatchPartTab.Location = new System.Drawing.Point(4, 21);
            this.tpMatchPartTab.Name = "tpMatchPartTab";
            this.tpMatchPartTab.Padding = new System.Windows.Forms.Padding(3);
            this.tpMatchPartTab.Size = new System.Drawing.Size(910, 458);
            this.tpMatchPartTab.TabIndex = 0;
            this.tpMatchPartTab.Text = "Match";
            this.tpMatchPartTab.UseVisualStyleBackColor = true;
            // 
            // tpOnlyLeftPartTab
            // 
            this.tpOnlyLeftPartTab.Location = new System.Drawing.Point(4, 21);
            this.tpOnlyLeftPartTab.Name = "tpOnlyLeftPartTab";
            this.tpOnlyLeftPartTab.Padding = new System.Windows.Forms.Padding(3);
            this.tpOnlyLeftPartTab.Size = new System.Drawing.Size(910, 458);
            this.tpOnlyLeftPartTab.TabIndex = 1;
            this.tpOnlyLeftPartTab.Text = "OnlyLeft";
            this.tpOnlyLeftPartTab.UseVisualStyleBackColor = true;
            // 
            // tpOnlyRightPartTab
            // 
            this.tpOnlyRightPartTab.Location = new System.Drawing.Point(4, 21);
            this.tpOnlyRightPartTab.Name = "tpOnlyRightPartTab";
            this.tpOnlyRightPartTab.Padding = new System.Windows.Forms.Padding(3);
            this.tpOnlyRightPartTab.Size = new System.Drawing.Size(910, 458);
            this.tpOnlyRightPartTab.TabIndex = 2;
            this.tpOnlyRightPartTab.Text = "OnlyRight";
            this.tpOnlyRightPartTab.UseVisualStyleBackColor = true;
            // 
            // btnStartGetSyncFile
            // 
            this.btnStartGetSyncFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartGetSyncFile.Location = new System.Drawing.Point(773, 103);
            this.btnStartGetSyncFile.Name = "btnStartGetSyncFile";
            this.btnStartGetSyncFile.Size = new System.Drawing.Size(139, 23);
            this.btnStartGetSyncFile.TabIndex = 11;
            this.btnStartGetSyncFile.Text = "btnGetSyncFile";
            this.btnStartGetSyncFile.UseVisualStyleBackColor = true;
            this.btnStartGetSyncFile.Click += new System.EventHandler(this.btnStartFileXCommand_Click);
            // 
            // btnStartFileSync
            // 
            this.btnStartFileSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartFileSync.Location = new System.Drawing.Point(628, 103);
            this.btnStartFileSync.Name = "btnStartFileSync";
            this.btnStartFileSync.Size = new System.Drawing.Size(139, 23);
            this.btnStartFileSync.TabIndex = 10;
            this.btnStartFileSync.Text = "FileSync";
            this.btnStartFileSync.UseVisualStyleBackColor = true;
            this.btnStartFileSync.Click += new System.EventHandler(this.btnStartFileXCommand_Click);
            // 
            // cbSyncDirectoryPosition
            // 
            this.cbSyncDirectoryPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSyncDirectoryPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSyncDirectoryPosition.FormattingEnabled = true;
            this.cbSyncDirectoryPosition.Location = new System.Drawing.Point(480, 105);
            this.cbSyncDirectoryPosition.Name = "cbSyncDirectoryPosition";
            this.cbSyncDirectoryPosition.Size = new System.Drawing.Size(142, 20);
            this.cbSyncDirectoryPosition.TabIndex = 9;
            // 
            // btnAllClear
            // 
            this.btnAllClear.Location = new System.Drawing.Point(6, 103);
            this.btnAllClear.Name = "btnAllClear";
            this.btnAllClear.Size = new System.Drawing.Size(139, 23);
            this.btnAllClear.TabIndex = 12;
            this.btnAllClear.Text = "Clear";
            this.btnAllClear.UseVisualStyleBackColor = true;
            this.btnAllClear.Click += new System.EventHandler(this.btnAllClear_Click);
            // 
            // msMainMenuBar
            // 
            this.msMainMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiItem_File,
            this.tsmiItem_Help});
            this.msMainMenuBar.Location = new System.Drawing.Point(0, 0);
            this.msMainMenuBar.Name = "msMainMenuBar";
            this.msMainMenuBar.Size = new System.Drawing.Size(942, 24);
            this.msMainMenuBar.TabIndex = 15;
            this.msMainMenuBar.Text = "msMainMenuBar";
            // 
            // tsmiItem_File
            // 
            this.tsmiItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiItem_Exit});
            this.tsmiItem_File.Name = "tsmiItem_File";
            this.tsmiItem_File.Size = new System.Drawing.Size(37, 20);
            this.tsmiItem_File.Text = "File";
            // 
            // tsmiItem_Exit
            // 
            this.tsmiItem_Exit.Name = "tsmiItem_Exit";
            this.tsmiItem_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.tsmiItem_Exit.Size = new System.Drawing.Size(152, 22);
            this.tsmiItem_Exit.Text = "Exit";
            this.tsmiItem_Exit.Click += new System.EventHandler(this.tsmiItem_Exit_Click);
            // 
            // tsmiItem_Help
            // 
            this.tsmiItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiItem_UpdateCheck,
            this.tsmiItem_EULA,
            this.tsmiItem_Help_CutLine1,
            this.tsmiItem_About});
            this.tsmiItem_Help.Name = "tsmiItem_Help";
            this.tsmiItem_Help.Size = new System.Drawing.Size(42, 20);
            this.tsmiItem_Help.Text = "Help";
            // 
            // tsmiItem_UpdateCheck
            // 
            this.tsmiItem_UpdateCheck.Name = "tsmiItem_UpdateCheck";
            this.tsmiItem_UpdateCheck.Size = new System.Drawing.Size(152, 22);
            this.tsmiItem_UpdateCheck.Text = "UpdateCheck";
            this.tsmiItem_UpdateCheck.Click += new System.EventHandler(this.tsmiItem_UpdateCheck_Click);
            // 
            // tsmiItem_EULA
            // 
            this.tsmiItem_EULA.Name = "tsmiItem_EULA";
            this.tsmiItem_EULA.Size = new System.Drawing.Size(152, 22);
            this.tsmiItem_EULA.Text = "EULA";
            this.tsmiItem_EULA.Click += new System.EventHandler(this.tsmiItem_EULA_Click);
            // 
            // tsmiItem_Help_CutLine1
            // 
            this.tsmiItem_Help_CutLine1.Name = "tsmiItem_Help_CutLine1";
            this.tsmiItem_Help_CutLine1.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiItem_About
            // 
            this.tsmiItem_About.Name = "tsmiItem_About";
            this.tsmiItem_About.ShortcutKeyDisplayString = "";
            this.tsmiItem_About.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.tsmiItem_About.Size = new System.Drawing.Size(152, 22);
            this.tsmiItem_About.Text = "About";
            this.tsmiItem_About.Click += new System.EventHandler(this.tsmiItem_About_Click);
            // 
            // gbControlBox
            // 
            this.gbControlBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbControlBox.Controls.Add(this.btnStartFileHashCheck);
            this.gbControlBox.Controls.Add(this.btnAllClear);
            this.gbControlBox.Controls.Add(this.lblRightDirectory);
            this.gbControlBox.Controls.Add(this.tbRightDirectory);
            this.gbControlBox.Controls.Add(this.lblLeftDirectory);
            this.gbControlBox.Controls.Add(this.cbSyncDirectoryPosition);
            this.gbControlBox.Controls.Add(this.btnLeftDirectory);
            this.gbControlBox.Controls.Add(this.tbLeftDirectory);
            this.gbControlBox.Controls.Add(this.btnStartGetSyncFile);
            this.gbControlBox.Controls.Add(this.btnStartFileSync);
            this.gbControlBox.Controls.Add(this.btnRightDirectory);
            this.gbControlBox.Location = new System.Drawing.Point(12, 27);
            this.gbControlBox.Name = "gbControlBox";
            this.gbControlBox.Size = new System.Drawing.Size(918, 132);
            this.gbControlBox.TabIndex = 16;
            this.gbControlBox.TabStop = false;
            this.gbControlBox.Text = "ControlBox";
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 673);
            this.Controls.Add(this.gbControlBox);
            this.Controls.Add(this.tcCheckResultItem);
            this.Controls.Add(this.ssMainStatusBar);
            this.Controls.Add(this.msMainMenuBar);
            this.MinimumSize = new System.Drawing.Size(950, 700);
            this.Name = "MainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FileSyncChecker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrame_FormClosing);
            this.Load += new System.EventHandler(this.MainFrame_Load);
            this.ssMainStatusBar.ResumeLayout(false);
            this.ssMainStatusBar.PerformLayout();
            this.tcCheckResultItem.ResumeLayout(false);
            this.msMainMenuBar.ResumeLayout(false);
            this.msMainMenuBar.PerformLayout();
            this.gbControlBox.ResumeLayout(false);
            this.gbControlBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbRightDirectory;
        private System.Windows.Forms.TextBox tbLeftDirectory;
        private System.Windows.Forms.Button btnRightDirectory;
        private System.Windows.Forms.Button btnLeftDirectory;
        private System.Windows.Forms.StatusStrip ssMainStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel tsslblWorkMessage;
        private System.Windows.Forms.ToolStripProgressBar tspbWorkProgress;
        private System.Windows.Forms.Label lblLeftDirectory;
        private System.Windows.Forms.Label lblRightDirectory;
        private System.Windows.Forms.FolderBrowserDialog fbdWorkDirectory;
        private System.Windows.Forms.Button btnStartFileHashCheck;
        private System.Windows.Forms.TabControl tcCheckResultItem;
        private System.Windows.Forms.TabPage tpMatchPartTab;
        private System.Windows.Forms.TabPage tpOnlyLeftPartTab;
        private System.Windows.Forms.TabPage tpOnlyRightPartTab;
        private System.Windows.Forms.Button btnStartGetSyncFile;
        private System.Windows.Forms.Button btnStartFileSync;
        private System.Windows.Forms.ComboBox cbSyncDirectoryPosition;
        private System.Windows.Forms.Button btnAllClear;
        private System.Windows.Forms.MenuStrip msMainMenuBar;
        private System.Windows.Forms.GroupBox gbControlBox;
        private System.Windows.Forms.ToolStripMenuItem tsmiItem_File;
        private System.Windows.Forms.ToolStripMenuItem tsmiItem_Help;
        private System.Windows.Forms.ToolStripMenuItem tsmiItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem tsmiItem_EULA;
        private System.Windows.Forms.ToolStripSeparator tsmiItem_Help_CutLine1;
        private System.Windows.Forms.ToolStripMenuItem tsmiItem_About;
        private System.Windows.Forms.ToolStripMenuItem tsmiItem_UpdateCheck;
    }
}