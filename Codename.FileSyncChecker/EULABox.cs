using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Reflection;

namespace Codename.FileSyncChecker
{
    public partial class EULABox : Form
    {
        public EULABox()
        {
            this.InitializeComponent();

            //
            this.Text = XProvider.ResourceValue.EULABox.ControlValue.EULABox;
            this.Icon = XProvider.ResourceValue.MainFrame.ControlValue.MainFrame_Icon;
            //
            this.wbContent.DocumentText = String.Format(
                    XProvider.ResourceValue.EULABox.ControlValue.EULAContent_HTML,
                    XProvider.DataValue.AssemblyInfoValue.sName,
                    XProvider.DataValue.AssemblyInfoValue.vVersion.ToString(),
                    XProvider.ResourceValue.EULABox.ControlValue.EULAContent.Replace(Environment.NewLine, ("<br />" + Environment.NewLine)),
                    XProvider.DataValue.AssemblyInfoValue.sCopyright
                );
            //
            this.btnPrint.Text = XProvider.ResourceValue.EULABox.ControlValue.btnPrint;
            //
            this.btnClose.Text = XProvider.ResourceValue.EULABox.ControlValue.btnClose;
        }

        private void EULABox_Load(object sender, EventArgs e)
        {
            //>
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.wbContent.ShowPrintDialog();
        }
    }
}
