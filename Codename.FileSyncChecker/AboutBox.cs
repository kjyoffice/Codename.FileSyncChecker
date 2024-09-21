using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Codename.FileSyncChecker
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            this.InitializeComponent();

            //
            this.Text = XProvider.ResourceValue.AboutBox.ControlValue.AboutBox;
            this.Icon = XProvider.ResourceValue.MainFrame.ControlValue.MainFrame_Icon;
            this.BackColor = Color.White;
            //
            this.lblTitle.Text = XProvider.DataValue.AssemblyInfoValue.sName;
            //
            this.lblVersion.Text = XProvider.DataValue.AssemblyInfoValue.vVersion.ToString();
            //
            this.lblDeveloper.Text = XProvider.ResourceValue.AboutBox.ControlValue.lblDeveloper;
            //
            this.pbLogo.Image = XProvider.ResourceValue.AboutBox.ControlValue.AppLogo;

            // 컨트롤의 공통명령 지정
            foreach (Control ctlLabel in this.Controls)
            {
                if ((ctlLabel as Label) != null)
                {
                    ctlLabel.BackColor = this.BackColor;
                }

                if ((ctlLabel as Button) == null)
                {
                    ctlLabel.Click += new EventHandler(this.AboutBox_Click);
                }
            }
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            //>
        }

        private void AboutBox_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        private void AboutBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
