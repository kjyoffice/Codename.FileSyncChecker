namespace Codename.FileSyncChecker
{
    partial class EULABox
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
            this.pnlOnlyView = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.wbContent = new System.Windows.Forms.WebBrowser();
            this.pnlOnlyView.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOnlyView
            // 
            this.pnlOnlyView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlOnlyView.Controls.Add(this.btnPrint);
            this.pnlOnlyView.Controls.Add(this.btnClose);
            this.pnlOnlyView.Location = new System.Drawing.Point(12, 434);
            this.pnlOnlyView.Name = "pnlOnlyView";
            this.pnlOnlyView.Size = new System.Drawing.Size(670, 29);
            this.pnlOnlyView.TabIndex = 2;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(0, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(103, 29);
            this.btnPrint.TabIndex = 5;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.Location = new System.Drawing.Point(567, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(103, 29);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // wbContent
            // 
            this.wbContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.wbContent.IsWebBrowserContextMenuEnabled = false;
            this.wbContent.Location = new System.Drawing.Point(12, 12);
            this.wbContent.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbContent.Name = "wbContent";
            this.wbContent.Size = new System.Drawing.Size(670, 416);
            this.wbContent.TabIndex = 7;
            // 
            // EULABox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 475);
            this.Controls.Add(this.pnlOnlyView);
            this.Controls.Add(this.wbContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EULABox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EULABox";
            this.Load += new System.EventHandler(this.EULABox_Load);
            this.pnlOnlyView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlOnlyView;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.WebBrowser wbContent;
    }
}