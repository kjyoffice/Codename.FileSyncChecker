namespace Codename.FileSyncChecker
{
    partial class AboutBox
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
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDeveloper = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.SystemColors.Control;
            this.lblVersion.ForeColor = System.Drawing.Color.Gray;
            this.lblVersion.Location = new System.Drawing.Point(12, 329);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(48, 12);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Version";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(57, 27);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Title";
            // 
            // lblDeveloper
            // 
            this.lblDeveloper.ForeColor = System.Drawing.Color.Gray;
            this.lblDeveloper.Location = new System.Drawing.Point(237, 329);
            this.lblDeveloper.Name = "lblDeveloper";
            this.lblDeveloper.Size = new System.Drawing.Size(251, 12);
            this.lblDeveloper.TabIndex = 7;
            this.lblDeveloper.Text = "Developer";
            this.lblDeveloper.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbLogo
            // 
            this.pbLogo.Location = new System.Drawing.Point(50, 70);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(300, 200);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbLogo.TabIndex = 8;
            this.pbLogo.TabStop = false;
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.lblDeveloper);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AboutBox";
            this.Load += new System.EventHandler(this.AboutBox_Load);
            this.Click += new System.EventHandler(this.AboutBox_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AboutBox_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDeveloper;
        private System.Windows.Forms.PictureBox pbLogo;
    }
}