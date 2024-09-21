using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Codename.FileSyncChecker.XProvider.WorkControl
{
    public class FileHashCheckViewer
    {
        public DataGridView dgvViewer { get; private set; }
        public List<DataGridViewRow> lViewerRow { get; private set; }
        private bool bLeftShow { get; set; }
        private bool bRightShow { get; set; }

        public string sColumnName_No
        {
            get
            {
                return "colNo";
            }
        }

        public string sColumnName_FileName
        {
            get
            {
                return "colFileName";
            }
        }

        public string sColumnName_FileSizeLeft
        {
            get
            {
                return "colFileSizeLeft";
            }
        }

        public string sColumnName_FileSizeRight
        {
            get
            {
                return "colFileSizeRight";
            }
        }

        public string sColumnName_FileEditDateLeft
        {
            get
            {
                return "colFileEditDateLeft";
            }
        }

        public string sColumnName_FileEditDateRight
        {
            get
            {
                return "colFileEditDateRight";
            }
        }

        public string sColumnName_FileHashLeft
        {
            get
            {
                return "colFileHashLeft";
            }
        }

        public string sColumnName_FileHashRight
        {
            get
            {
                return "colFileHashRight";
            }
        }

        public string sColumnName_FileMatch
        {
            get
            {
                return "colFileMatch";
            }
        }

        public string sColumnName_FileSync
        {
            get
            {
                return "colFileSync";
            }
        }

        // ---------------------------------------------------------

        private void DataGridViewSetting()
        {
            this.dgvViewer.AllowUserToAddRows = false;
            this.dgvViewer.AllowUserToDeleteRows = false;
            this.dgvViewer.AllowUserToResizeRows = false;
            this.dgvViewer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvViewer.Dock = DockStyle.Fill;
            this.dgvViewer.Location = new Point(3, 3);
            this.dgvViewer.ReadOnly = true;
            this.dgvViewer.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvViewer.RowTemplate.Height = 23;
            this.dgvViewer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            ResourceValue.FileHashCheckViewer.ControlValue.ResourceManager.IgnoreCase = true;
        }

        private void CreateColumnItem()
        {
            DataGridViewColumnCollection dgvccColumn;
            DataGridViewColumn dgvcNo;
            DataGridViewColumn dgvcFileName;
            DataGridViewColumn dgvcFileSizeLeft;
            DataGridViewColumn dgvcFileEditDateLeft;
            DataGridViewColumn dgvcFileHashLeft;
            DataGridViewColumn dgvcFileSizeRight;            
            DataGridViewColumn dgvcFileEditDateRight;
            DataGridViewColumn dgvcFileHashRight;
            DataGridViewColumn dgvcFileMatch;
            DataGridViewColumn dgvcFileSync;

            dgvccColumn = this.dgvViewer.Columns;
            
            dgvcNo = dgvccColumn[dgvccColumn.Add(this.sColumnName_No, "No")];
            dgvcNo.Width = 70;

            dgvcFileName = dgvccColumn[dgvccColumn.Add(this.sColumnName_FileName, "FileName")];
            dgvcFileName.Width = 350;
            dgvcFileName.MinimumWidth = 350;

            dgvcFileSizeLeft = dgvccColumn[dgvccColumn.Add(this.sColumnName_FileSizeLeft, "FileSizeLeft")];
            dgvcFileSizeLeft.Width = 100;
            dgvcFileSizeLeft.Visible = this.bLeftShow;

            dgvcFileEditDateLeft = dgvccColumn[dgvccColumn.Add(this.sColumnName_FileEditDateLeft, "FileEditDateLeft")];
            dgvcFileEditDateLeft.Width = 130;
            dgvcFileEditDateLeft.Visible = this.bLeftShow;

            dgvcFileHashLeft = dgvccColumn[dgvccColumn.Add(this.sColumnName_FileHashLeft, "FileHashLeft")];
            dgvcFileHashLeft.Width = 230;
            dgvcFileHashLeft.Visible = this.bLeftShow;

            dgvcFileSizeRight = dgvccColumn[dgvccColumn.Add(this.sColumnName_FileSizeRight, "FileSizeRight")];
            dgvcFileSizeRight.Width = 100;
            dgvcFileSizeRight.Visible = this.bRightShow;

            dgvcFileEditDateRight = dgvccColumn[dgvccColumn.Add(this.sColumnName_FileEditDateRight, "FileEditDateRight")];
            dgvcFileEditDateRight.Width = 130;
            dgvcFileEditDateRight.Visible = this.bRightShow;

            dgvcFileHashRight = dgvccColumn[dgvccColumn.Add(this.sColumnName_FileHashRight, "FileHashRight")];
            dgvcFileHashRight.Width = 230;
            dgvcFileHashRight.Visible = this.bRightShow;

            dgvcFileMatch = dgvccColumn[dgvccColumn.Add(this.sColumnName_FileMatch, "FileMatch")];
            dgvcFileMatch.Width = 80;
            dgvcFileMatch.Visible = ((this.bLeftShow == true) && (this.bRightShow == true));

            dgvcFileSync = dgvccColumn[dgvccColumn.Add(this.sColumnName_FileSync, "FileSync")];
            dgvcFileSync.Width = 80;

            foreach (DataGridViewColumn dgvcItem in this.dgvViewer.Columns)
            {
                dgvcItem.ReadOnly = true;
                dgvcItem.Resizable = DataGridViewTriState.False;
                dgvcItem.HeaderText = ResourceValue.FileHashCheckViewer.ControlValue.ResourceManager.GetString(dgvcItem.Name);
            }

            dgvcFileName.Resizable = DataGridViewTriState.True;
            dgvcFileSizeLeft.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvcFileSizeRight.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void CreateEventHandler()
        {
            // Row생성 후
            this.dgvViewer.RowsAdded += new DataGridViewRowsAddedEventHandler(
                delegate(object sender, DataGridViewRowsAddedEventArgs e)
                {
                    // 해당 Row List에도 저장 (* 인덱스 손실 방지용)
                    this.lViewerRow.Add(this.dgvViewer.Rows[e.RowIndex]);
                }
            );
        }

        // ---------------------------------------------------------

        public FileHashCheckViewer(bool bLeftShow, bool bRightShow)
        {
            this.bLeftShow = bLeftShow;
            this.bRightShow = bRightShow;
            this.dgvViewer = new DataGridView();
            this.lViewerRow = new List<DataGridViewRow>();
            this.DataGridViewSetting();
            this.CreateColumnItem();
            this.CreateEventHandler();
        }

        public void ClearRow()
        {
            this.dgvViewer.Rows.Clear();
            this.lViewerRow.Clear();
        }
    }
}
