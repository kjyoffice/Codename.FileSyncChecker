using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Codename.FileSyncChecker
{
    public partial class MainFrame : Form
    {
        private XProvider.DataModel.LuncherArgs laArgs { get; set; }
        private XProvider.WorkControl.FileHashCheckViewer fhcvMatch { get; set; }
        private XProvider.WorkControl.FileHashCheckViewer fhcvOnlyLeft { get; set; }
        private XProvider.WorkControl.FileHashCheckViewer fhcvOnlyRight { get; set; }
        private XProvider.WorkCommand.CoreWorker cwWorker { get; set; }
        private bool bWorking { get; set; }
        private bool bTabTextUpdateItemCount { get; set; }

        // ---------------------------------------------------------------------

        // 스레드에 포함되지 않음
        private void WorkStartSign(XProvider.DataType.WorkCommandType wctCommandType)
        {
            this.bWorking = true;

            this.tspbWorkProgress.Style = ProgressBarStyle.Marquee;
            this.tspbWorkProgress.Value = 0;
            this.tspbWorkProgress.Visible = true;

            this.tbLeftDirectory.Enabled = false;
            this.btnLeftDirectory.Enabled = false;
            this.tbRightDirectory.Enabled = false;
            this.btnRightDirectory.Enabled = false;
            this.cbSyncDirectoryPosition.Enabled = false;
            this.btnAllClear.Enabled = false;
            this.msMainMenuBar.Enabled = false;

            this.btnStartFileHashCheck.Enabled = false;
            this.btnStartFileSync.Enabled = false;
            this.btnStartGetSyncFile.Enabled = false;

            this.tsslblWorkMessage.Text = XProvider.ResourceValue.MainFrame.WorkMessageValue.CreateWorkItemList;

            // 이벤트에 해당되는 버튼에 대해서만 "Text"변경 및 "Enabled"해제
            if (wctCommandType == XProvider.DataType.WorkCommandType.FileHash)
            {
                this.btnStartFileHashCheck.Text = XProvider.ResourceValue.MainFrame.ControlValue.btnStartFileXCommand_StopWorking;
                this.btnStartFileHashCheck.Enabled = true;
            }
            else if (wctCommandType == XProvider.DataType.WorkCommandType.FileSync)
            {
                this.btnStartFileSync.Text = XProvider.ResourceValue.MainFrame.ControlValue.btnStartFileXCommand_StopWorking;
                this.btnStartFileSync.Enabled = true;
            }
            else if (wctCommandType == XProvider.DataType.WorkCommandType.GetSyncFile)
            {
                this.btnStartGetSyncFile.Text = XProvider.ResourceValue.MainFrame.ControlValue.btnStartFileXCommand_StopWorking;
                this.btnStartGetSyncFile.Enabled = true;
            }
        }

        // 스레드에 포함되지 않음
        private void WorkEndSign(XProvider.DataType.WorkCommandType wctCommandType)
        {
            bool bEnabledNextCommand = true;

            this.btnStartFileHashCheck.Text = XProvider.ResourceValue.MainFrame.ControlValue.btnStartFileHashCheck;
            this.btnStartFileSync.Text = XProvider.ResourceValue.MainFrame.ControlValue.btnStartFileSync;
            this.btnStartGetSyncFile.Text = XProvider.ResourceValue.MainFrame.ControlValue.btnStartGetSyncFile;

            this.btnStartFileHashCheck.Enabled = true;

            if (wctCommandType == XProvider.DataType.WorkCommandType.FileHash)
            {
                this.btnStartFileSync.Enabled = !this.laArgs.bStop;
                this.btnStartGetSyncFile.Enabled = !this.laArgs.bStop;
                this.cbSyncDirectoryPosition.Enabled = !this.laArgs.bStop;
            }
            else if ((wctCommandType == XProvider.DataType.WorkCommandType.FileSync) || (wctCommandType == XProvider.DataType.WorkCommandType.GetSyncFile))
            {
                // 파일싱크 명령에 대해서는 중단이 되었던 되지 않았던 파일해쉬 리스트에 대해 1번의 명령만 허용
                this.btnStartFileSync.Enabled = false;
                this.btnStartGetSyncFile.Enabled = false;
                this.cbSyncDirectoryPosition.Enabled = false;
            }

            this.tbLeftDirectory.Enabled = true;
            this.btnLeftDirectory.Enabled = true;
            this.tbRightDirectory.Enabled = true;
            this.btnRightDirectory.Enabled = true;
            this.btnAllClear.Enabled = true;
            this.msMainMenuBar.Enabled = true;

            this.tspbWorkProgress.Visible = false;

            if (this.laArgs.bStop == false)
            {
                // 명령이 파일해쉬인 경우, 왼쪽만 존재, 오른쪽만 존재 리스트가 없고 동일한 파일에서도 틀린파일이 없는경우 중단!
                if (wctCommandType == XProvider.DataType.WorkCommandType.FileHash)
                {
                    if ((this.fhcvOnlyLeft.dgvViewer.Rows.Count <= 0) && (this.fhcvOnlyRight.dgvViewer.Rows.Count <= 0))
                    {
                        if (this.fhcvMatch.dgvViewer.Rows.Count > 0)
                        {
                            if (this.fhcvMatch.dgvViewer.Rows.Count == this.laArgs.lMatchItem.Where((x) => x.bFileMatch == true).Count())
                            {
                                // 양쪽에 일치하는 파일만 있음
                                this.tsslblWorkMessage.Text = XProvider.ResourceValue.MainFrame.WorkMessageValue.AllMatchFileHashItem;
                                bEnabledNextCommand = false;
                            }
                        }
                        else
                        {
                            // 아예 검색된 항목이 없음
                            this.tsslblWorkMessage.Text = XProvider.ResourceValue.MainFrame.WorkMessageValue.EmptyFileHashItemList;
                            bEnabledNextCommand = false;
                        }
                    }
                }
                else if ((wctCommandType == XProvider.DataType.WorkCommandType.FileSync) || (wctCommandType == XProvider.DataType.WorkCommandType.GetSyncFile))
                {
                    this.tsslblWorkMessage.Text = XProvider.ResourceValue.MainFrame.WorkMessageValue.AllEndFileSyncCommand;
                    bEnabledNextCommand = false;
                }
            }
            else
            {
                // 중단되었음!
                bEnabledNextCommand = false;
            }

            // 파일싱크명령 버튼 활성화 선택
            this.btnStartFileSync.Enabled = bEnabledNextCommand;
            this.btnStartGetSyncFile.Enabled = bEnabledNextCommand;
            this.cbSyncDirectoryPosition.Enabled = bEnabledNextCommand;

            // 다음작업
            if (bEnabledNextCommand == true)
            {
                this.tsslblWorkMessage.Text = XProvider.ResourceValue.MainFrame.WorkMessageValue.WorkEndReadyNextWork;
            }

            this.bWorking = false;
        }

        // 스레드에 포함되지 않음
        private void WorkStopSign(XProvider.DataType.WorkCommandType wctCommandType)
        {
            this.tsslblWorkMessage.Text = XProvider.ResourceValue.MainFrame.WorkMessageValue.WorkStopPleaseNewWork;
        }

        private void SetWorkingItemCount(int iCount)
        {
            if (this.ssMainStatusBar.InvokeRequired == true)
            {
                this.ssMainStatusBar.Invoke(
                    new XProvider.WorkDelegate.dgNoParameter(
                        delegate
                        {
                            this.tspbWorkProgress.Style = ProgressBarStyle.Blocks;
                            this.tspbWorkProgress.Minimum = 0;
                            this.tspbWorkProgress.Maximum = iCount;
                            this.tspbWorkProgress.Value = 0;
                        }
                    )
                );
            }
            else
            {
                this.tspbWorkProgress.Style = ProgressBarStyle.Blocks;
                this.tspbWorkProgress.Minimum = 0;
                this.tspbWorkProgress.Maximum = iCount;
                this.tspbWorkProgress.Value = 0;
            }
        }

        private void UpdateWorkingItem()
        {
            if (this.ssMainStatusBar.InvokeRequired == true)
            {
                this.ssMainStatusBar.Invoke(
                    new XProvider.WorkDelegate.dgNoParameter(
                        delegate
                        {
                            this.tspbWorkProgress.Value++;
                            this.tsslblWorkMessage.Text = (XProvider.ResourceValue.MainFrame.WorkMessageValue.NowWorking + " >>> " + String.Format("{0:N0}", this.tspbWorkProgress.Value) + " / " + String.Format("{0:N0}", this.tspbWorkProgress.Maximum) + "");
                        }
                    )
                );
            }
            else
            {
                this.tspbWorkProgress.Value++;
                this.tsslblWorkMessage.Text = (XProvider.ResourceValue.MainFrame.WorkMessageValue.NowWorking + " >>> " + String.Format("{0:N0}", this.tspbWorkProgress.Value) + " / " + String.Format("{0:N0}", this.tspbWorkProgress.Maximum) + "");
            }            
        }

        private void FileHashCheckResult(XProvider.DataType.WorkCommandType wctCommandType, XProvider.DataModel.FileCheckItem fciItem)
        {
            XProvider.WorkControl.FileHashCheckViewer fhcvViewer = null;
            TabPage tpTab;

            if (fciItem.fitItemType == XProvider.DataType.FileItemType.Match)
            {
                fhcvViewer = this.fhcvMatch;
            }
            else if (fciItem.fitItemType == XProvider.DataType.FileItemType.OnlyLeft)
            {
                fhcvViewer = this.fhcvOnlyLeft;
            }
            else if (fciItem.fitItemType == XProvider.DataType.FileItemType.OnlyRight)
            {
                fhcvViewer = this.fhcvOnlyRight;
            }

            if (fhcvViewer != null)
            {
                if (fhcvViewer.dgvViewer.InvokeRequired == true)
                {
                    fhcvViewer.dgvViewer.Invoke(new XProvider.WorkDelegate.dgFileHashCheckResult_Worker(this.FileHashCheckResult_Worker), new object[] { fhcvViewer, wctCommandType, fciItem });
                    fhcvViewer.dgvViewer.Invoke(new XProvider.WorkDelegate.dgFileHashCheckResult_Worker(this.FileHashCheckResult_Worker_UpdateRowCellStyle), new object[] { fhcvViewer, wctCommandType, fciItem });
                }
                else
                {
                    this.FileHashCheckResult_Worker(fhcvViewer, wctCommandType, fciItem);
                    this.FileHashCheckResult_Worker_UpdateRowCellStyle(fhcvViewer, wctCommandType, fciItem);
                }

                if (this.bTabTextUpdateItemCount == true)
                {
                    tpTab = (fhcvViewer.dgvViewer.Tag as TabPage);

                    if (tpTab != null)
                    {
                        if (tpTab.InvokeRequired == true)
                        {
                            tpTab.Invoke(new XProvider.WorkDelegate.dgFileHashCheckResult_CountWorker(this.FileHashCheckResult_Worker_UpdateItemCount), new object[] { fhcvViewer });
                        }
                        else
                        {
                            this.FileHashCheckResult_Worker_UpdateItemCount(fhcvViewer);
                        }
                    }
                }
            }
        }

        // 똑같은 코드 2번은 상관없는데, 코드가 너무 길어서... 쿨럭;;; - 1
        private void FileHashCheckResult_Worker(XProvider.WorkControl.FileHashCheckViewer fhcvViewer, XProvider.DataType.WorkCommandType wctCommandType, XProvider.DataModel.FileCheckItem fciItem)
        {
            DataGridViewRow dgvrRow;
            string sDateDisplayForamt = XProvider.DataValue.DateTimeDisplay.sNormalFormat;

            if (wctCommandType == XProvider.DataType.WorkCommandType.FileHash)
            {
                fciItem.iRowIndex = fhcvViewer.dgvViewer.RowCount;
                
                fhcvViewer.dgvViewer.Rows.Add(
                        new object[] {
                            (fciItem.iRowIndex + 1),
                            fciItem.sFileName,
                            String.Format("{0:N0}", fciItem.lFileSizeLeft),
                            fciItem.dtFileEditDateLeft.ToString(sDateDisplayForamt),
                            fciItem.sFileHashLeft,
                            String.Format("{0:N0}", fciItem.lFileSizeRight),
                            fciItem.dtFileEditDateRight.ToString(sDateDisplayForamt),
                            fciItem.sFileHashRight,
                            fciItem.bFileMatch,
                            fciItem.fsktSyncType
                        }
                    );

                dgvrRow = fhcvViewer.lViewerRow[fciItem.iRowIndex];
            }
            else if (wctCommandType == XProvider.DataType.WorkCommandType.FileSync)
            {
                dgvrRow = fhcvViewer.lViewerRow[fciItem.iRowIndex];

                if (fciItem.fsktSyncType != XProvider.DataType.FileSyncKeyType.Skip)
                {
                    dgvrRow.Cells[fhcvViewer.sColumnName_FileSizeLeft].Value = String.Format("{0:N0}", fciItem.lFileSizeLeft);
                    dgvrRow.Cells[fhcvViewer.sColumnName_FileEditDateLeft].Value = fciItem.dtFileEditDateLeft.ToString(sDateDisplayForamt);
                    dgvrRow.Cells[fhcvViewer.sColumnName_FileHashLeft].Value = fciItem.sFileHashLeft;
                    dgvrRow.Cells[fhcvViewer.sColumnName_FileSizeRight].Value = String.Format("{0:N0}", fciItem.lFileSizeRight);
                    dgvrRow.Cells[fhcvViewer.sColumnName_FileEditDateRight].Value = fciItem.dtFileEditDateRight.ToString(sDateDisplayForamt);
                    dgvrRow.Cells[fhcvViewer.sColumnName_FileHashRight].Value = fciItem.sFileHashRight;
                    dgvrRow.Cells[fhcvViewer.sColumnName_FileMatch].Value = fciItem.bFileMatch;
                }

                dgvrRow.Cells[fhcvViewer.sColumnName_FileSync].Value = fciItem.fsktSyncType;
            }
            else if (wctCommandType == XProvider.DataType.WorkCommandType.GetSyncFile)
            {
                dgvrRow = fhcvViewer.lViewerRow[fciItem.iRowIndex];
                dgvrRow.Cells[fhcvViewer.sColumnName_FileSync].Value = fciItem.fsktSyncType;
            }
        }

        // 똑같은 코드 2번은 상관없는데, 코드가 너무 길어서... 쿨럭;;; - 2
        private void FileHashCheckResult_Worker_UpdateRowCellStyle(XProvider.WorkControl.FileHashCheckViewer fhcvViewer, XProvider.DataType.WorkCommandType wctCommandType, XProvider.DataModel.FileCheckItem fciItem)
        {
            DataGridViewRow dgvrRow = fhcvViewer.lViewerRow[fciItem.iRowIndex];

            // 결과값에 따른 뷰어 스타일 변경
            if (fciItem.fitItemType == XProvider.DataType.FileItemType.Match)
            {
                if (fciItem.bFileMatch == false)
                {
                    dgvrRow.DefaultCellStyle.BackColor = Color.LightBlue;

                    if (fciItem.bMatchFileSize == false)
                    {
                        dgvrRow.Cells[fhcvViewer.sColumnName_FileSizeLeft].Style.BackColor = Color.LightPink;
                        dgvrRow.Cells[fhcvViewer.sColumnName_FileSizeRight].Style.BackColor = Color.LightPink;
                    }

                    if (fciItem.bMatchFileEditDate == false)
                    {
                        dgvrRow.Cells[fhcvViewer.sColumnName_FileEditDateLeft].Style.BackColor = Color.LightPink;
                        dgvrRow.Cells[fhcvViewer.sColumnName_FileEditDateRight].Style.BackColor = Color.LightPink;
                    }

                    if (fciItem.bMatchFileHash == false)
                    {
                        dgvrRow.Cells[fhcvViewer.sColumnName_FileHashLeft].Style.BackColor = Color.LightPink;
                        dgvrRow.Cells[fhcvViewer.sColumnName_FileHashRight].Style.BackColor = Color.LightPink;
                    }
                }
                else
                {
                    if (dgvrRow.DefaultCellStyle.BackColor != Color.Empty)
                    {
                        dgvrRow.DefaultCellStyle.BackColor = Color.Empty;
                        dgvrRow.Cells[fhcvViewer.sColumnName_FileSizeLeft].Style.BackColor = Color.Empty;
                        dgvrRow.Cells[fhcvViewer.sColumnName_FileSizeRight].Style.BackColor = Color.Empty;
                        dgvrRow.Cells[fhcvViewer.sColumnName_FileEditDateLeft].Style.BackColor = Color.Empty;
                        dgvrRow.Cells[fhcvViewer.sColumnName_FileEditDateRight].Style.BackColor = Color.Empty;
                        dgvrRow.Cells[fhcvViewer.sColumnName_FileHashLeft].Style.BackColor = Color.Empty;
                        dgvrRow.Cells[fhcvViewer.sColumnName_FileHashRight].Style.BackColor = Color.Empty;
                    }
                }
            }
        }

        // 똑같은 코드 2번은 상관없는데, 코드가 너무 길어서... 쿨럭;;; - 3
        private void FileHashCheckResult_Worker_UpdateItemCount(XProvider.WorkControl.FileHashCheckViewer fhcvViewer)
        {
            TabPage tpTab = (fhcvViewer.dgvViewer.Tag as TabPage);
            string[] sTextSource;

            if (tpTab != null)
            {
                sTextSource = (tpTab.Tag as string[]);
                tpTab.Text = String.Format(
                                    (sTextSource[0] + " " + sTextSource[1]), 
                                    fhcvViewer.lViewerRow.Count,
                                    fhcvViewer.lViewerRow.Where((x) => 
                                            (Convert.ToBoolean(x.Cells[fhcvViewer.sColumnName_FileMatch].Value) == false)
                                        ).Count()
                                );
            }
        }

        private void ApplicationReset(bool bClearWorkDirectory, bool bNotifyEndDataIntegrity)
        {
            if (this.laArgs != null)
            {
                if (bNotifyEndDataIntegrity == true)
                {
                    MessageBox.Show(XProvider.ResourceValue.MainFrame.WorkMessageValue.EndDataIntegrity, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                this.laArgs = null;
                this.cwWorker = null;
            }

            if (bClearWorkDirectory == true)
            {
                this.tbLeftDirectory.Text = XProvider.AppConfig.AppSettings.sDefaultLeftDirectory;
                this.tbRightDirectory.Text = XProvider.AppConfig.AppSettings.sDefaultRightDirectory;
            }

            this.fhcvMatch.ClearRow();
            this.fhcvOnlyLeft.ClearRow();
            this.fhcvOnlyRight.ClearRow();
            this.tcCheckResultItem.SelectedIndex = 0;

            this.cbSyncDirectoryPosition.SelectedIndex = 0;
            this.cbSyncDirectoryPosition.Enabled = false;
            this.btnStartFileSync.Enabled = false;
            this.btnStartGetSyncFile.Enabled = false;

            this.tpMatchPartTab.Text = (this.tpMatchPartTab.Tag as string[])[0];
            this.tpOnlyLeftPartTab.Text = (this.tpOnlyLeftPartTab.Tag as string[])[0];
            this.tpOnlyRightPartTab.Text = (this.tpOnlyRightPartTab.Tag as string[])[0];

            this.tsslblWorkMessage.Text = XProvider.ResourceValue.MainFrame.WorkMessageValue.NowReady;
            this.tspbWorkProgress.Style = ProgressBarStyle.Blocks;
            this.tspbWorkProgress.Value = 0;
            this.tspbWorkProgress.Visible = false;
        }

        // ---------------------------------------------------------------------

        public MainFrame()
        {
            this.InitializeComponent();

            this.fhcvMatch = new XProvider.WorkControl.FileHashCheckViewer(true, true);
            this.fhcvOnlyLeft = new XProvider.WorkControl.FileHashCheckViewer(true, false);
            this.fhcvOnlyRight = new XProvider.WorkControl.FileHashCheckViewer(false, true);

            //
            this.Icon = XProvider.ResourceValue.MainFrame.ControlValue.MainFrame_Icon;
            this.Text = XProvider.ResourceValue.MainFrame.ControlValue.MainFrame;
            //
            this.lblLeftDirectory.Text = XProvider.ResourceValue.MainFrame.ControlValue.lblLeftDirectory;
            //
            this.btnLeftDirectory.Tag = this.tbLeftDirectory;
            //
            this.lblRightDirectory.Text = XProvider.ResourceValue.MainFrame.ControlValue.lblRightDirectory;
            //
            this.btnRightDirectory.Tag = this.tbRightDirectory;
            //
            this.btnStartFileHashCheck.Text = XProvider.ResourceValue.MainFrame.ControlValue.btnStartFileHashCheck;
            this.btnStartFileHashCheck.Tag = XProvider.DataType.WorkCommandType.FileHash;
            //
            this.tcCheckResultItem.SelectedIndex = 0;
            //
            this.tpMatchPartTab.Text = XProvider.ResourceValue.MainFrame.ControlValue.tpMatchPartTab;
            this.tpMatchPartTab.Tag = new string[] { this.tpMatchPartTab.Text, "({1:N0}/{0:N0})" };
            this.fhcvMatch.dgvViewer.Tag = this.tpMatchPartTab;
            this.tpMatchPartTab.Controls.Add(this.fhcvMatch.dgvViewer);
            //
            this.tpOnlyLeftPartTab.Text = XProvider.ResourceValue.MainFrame.ControlValue.tpOnlyLeftPartTab;
            this.tpOnlyLeftPartTab.Tag = new string[] { this.tpOnlyLeftPartTab.Text, "({0:N0})" };
            this.fhcvOnlyLeft.dgvViewer.Tag = this.tpOnlyLeftPartTab;
            this.tpOnlyLeftPartTab.Controls.Add(this.fhcvOnlyLeft.dgvViewer);
            //
            this.tpOnlyRightPartTab.Text = XProvider.ResourceValue.MainFrame.ControlValue.tpOnlyRightPartTab;
            this.tpOnlyRightPartTab.Tag = new string[] { this.tpOnlyRightPartTab.Text, "({0:N0})" };
            this.fhcvOnlyRight.dgvViewer.Tag = this.tpOnlyRightPartTab;
            this.tpOnlyRightPartTab.Controls.Add(this.fhcvOnlyRight.dgvViewer);
            //
            this.btnAllClear.Text = XProvider.ResourceValue.MainFrame.ControlValue.btnAllClear;
            //
            this.cbSyncDirectoryPosition.Items.Add(new XProvider.DataModel.SyncDirectoryPositionItem(XProvider.DataType.SyncDirectoryPositionType.None, XProvider.ResourceValue.MainFrame.ControlValue.cbWorkDirectoryPosition_None));
            this.cbSyncDirectoryPosition.Items.Add(new XProvider.DataModel.SyncDirectoryPositionItem(XProvider.DataType.SyncDirectoryPositionType.LeftRight, XProvider.ResourceValue.MainFrame.ControlValue.cbWorkDirectoryPosition_LeftRight));
            this.cbSyncDirectoryPosition.Items.Add(new XProvider.DataModel.SyncDirectoryPositionItem(XProvider.DataType.SyncDirectoryPositionType.RightLeft, XProvider.ResourceValue.MainFrame.ControlValue.cbWorkDirectoryPosition_RightLeft));
            this.cbSyncDirectoryPosition.SelectedIndex = 0;
            this.cbSyncDirectoryPosition.Enabled = false;
            //
            this.btnStartFileSync.Text = XProvider.ResourceValue.MainFrame.ControlValue.btnStartFileSync;
            this.btnStartFileSync.Tag = XProvider.DataType.WorkCommandType.FileSync;
            this.btnStartFileSync.Enabled = false;
            //
            this.btnStartGetSyncFile.Text = XProvider.ResourceValue.MainFrame.ControlValue.btnStartGetSyncFile;
            this.btnStartGetSyncFile.Tag = XProvider.DataType.WorkCommandType.GetSyncFile;
            this.btnStartGetSyncFile.Enabled = false;
            //
            this.gbControlBox.Text = XProvider.ResourceValue.MainFrame.ControlValue.gbControlBox;
            //
            this.tspbWorkProgress.Visible = false;
            //
            this.tsmiItem_File.Text = XProvider.ResourceValue.MainFrame.MenuControlValue.tsmiItem_File;
            this.tsmiItem_Exit.Text = XProvider.ResourceValue.MainFrame.MenuControlValue.tsmiItem_Exit;
            this.tsmiItem_Help.Text = XProvider.ResourceValue.MainFrame.MenuControlValue.tsmiItem_Help;
            this.tsmiItem_UpdateCheck.Text = XProvider.ResourceValue.MainFrame.MenuControlValue.tsmiItem_UpdateCheck;
            this.tsmiItem_EULA.Text = XProvider.ResourceValue.MainFrame.MenuControlValue.tsmiItem_EULA;
            this.tsmiItem_About.Text = XProvider.ResourceValue.MainFrame.MenuControlValue.tsmiItem_About;

            // TODO : TEMP / 기능구현 전 비활성화;;;
            this.tsmiItem_UpdateCheck.Visible = false;

            this.bTabTextUpdateItemCount = XProvider.AppConfig.AppSettings.bTabTextUpdateItemCount;
            this.bWorking = false;
        }

        private void MainFrame_Load(object sender, EventArgs e)
        {
            this.ApplicationReset(true, false);
        }

        private void MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.bWorking == true)
            {
                MessageBox.Show(XProvider.ResourceValue.MainFrame.WorkMessageValue.NowWoringExitApplication, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
            else
            {
                e.Cancel = (MessageBox.Show(XProvider.ResourceValue.MainFrame.WorkMessageValue.ExitApplicationConfirm, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No);
            }
        }

        private void btnLeftOrRightDirectory_Click(object sender, EventArgs e)
        {
            TextBox tbWorkDirectory = ((sender as Button).Tag as TextBox);
            string sCurrentDirectory;
            string sSelectedDirectory;
            bool bUpdateWorkDirectory = false;

            if (this.fbdWorkDirectory.ShowDialog() == DialogResult.OK)
            {
                sCurrentDirectory = tbWorkDirectory.Text.Trim();
                sSelectedDirectory = this.fbdWorkDirectory.SelectedPath.Trim();

                if (String.IsNullOrEmpty(sCurrentDirectory) == true)
                {
                    // 선택된 경로가 빈값이면 검색된 내용이 없다고 간주
                    // 즉, 처음이라고 간주
                    bUpdateWorkDirectory = true;
                }
                else
                {
                    // 선택된 경로가 있고 그리드뷰에 리스트가 있을시, 파일해쉬 검색을 했다고 간주하여
                    // 선택된 경로가 다른경우 아래 그리드뷰 리스트와의 무결성이 깨진다고 판단,
                    // 아래 그리드뷰 리스트를 모두 삭제 및 초기화
                    if (sCurrentDirectory.ToLower() != sSelectedDirectory.ToLower())
                    {
                        this.ApplicationReset(false, true);
                        bUpdateWorkDirectory = true;
                    }
                }

                if (bUpdateWorkDirectory == true)
                {
                    tbWorkDirectory.Text = sSelectedDirectory;
                }
            }
        }

        private void btnStartFileXCommand_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbdCopyDirectory;
            Button btnSyncCommand = (sender as Button);
            XProvider.DataType.WorkCommandType wctCommandType = (XProvider.DataType.WorkCommandType)btnSyncCommand.Tag;
            XProvider.DataType.SyncDirectoryPositionType sdptPosition = ((XProvider.DataModel.SyncDirectoryPositionItem)this.cbSyncDirectoryPosition.SelectedItem).sdptPosition;
            string sLeftDirectory = this.tbLeftDirectory.Text.Trim();
            string sRightDirectory = this.tbRightDirectory.Text.Trim();
            bool bExecute = false;
            string sSelectedCopyDiectory = String.Empty;

            // 작업의 진행여부
            if (this.bWorking == false)
            {
                // 파일해쉬 리스트 생성 작업
                if (wctCommandType == XProvider.DataType.WorkCommandType.FileHash)
                {
                    // 디렉토리가 선택되었는지, 그리고 존재하는 디렉토리인지 체크
                    if (((String.IsNullOrEmpty(sLeftDirectory) == false) && (String.IsNullOrEmpty(sRightDirectory) == false)) && ((Directory.Exists(sLeftDirectory) == true) && (Directory.Exists(sRightDirectory) == true)))
                    {
                        // 선택한 디렉토리가 같은 디렉토리인지, 그리고 하위 디렉토리에 속해지는지 체크
                        if ((sLeftDirectory != sRightDirectory) && ((sLeftDirectory.IndexOf(sRightDirectory) <= -1) && (sRightDirectory.IndexOf(sLeftDirectory) <= -1)))
                        {
                            this.ApplicationReset(false, false);

                            this.laArgs = new XProvider.DataModel.LuncherArgs()
                            {
                                wctpWorkStart = new XProvider.WorkDelegate.dgWorkCommandTypeParameter(this.WorkStartSign),
                                wctpWorkEnd = new XProvider.WorkDelegate.dgWorkCommandTypeParameter(this.WorkEndSign),
                                wctpWorkStop = new XProvider.WorkDelegate.dgWorkCommandTypeParameter(this.WorkStopSign),
                                sriSetItem = new XProvider.WorkDelegate.dgSetReadyItem(this.SetWorkingItemCount),
                                npUpdteWorkItem = new XProvider.WorkDelegate.dgNoParameter(this.UpdateWorkingItem),
                                fhcrCheckResult = new XProvider.WorkDelegate.dgFileHashCheckResult(this.FileHashCheckResult),
                                wdFileHashDirectory = new XProvider.DataModel.WorkDirectory(sLeftDirectory, sRightDirectory),
                                sdptPosition = XProvider.DataType.SyncDirectoryPositionType.None,
                                wctCommandType = XProvider.DataType.WorkCommandType.FileHash,
                                fsdSearchDate = new XProvider.DataModel.FileSearchDate(false, new DateTime(2010, 8, 1, 12, 15, 0), new DateTime(2010, 8, 1, 12, 16, 30)),
                                bSkipNoneFileExtension = XProvider.AppConfig.AppSettings.bSkipNoneFileExtension,
                                bSkipZeroFileSize = XProvider.AppConfig.AppSettings.bSkipZeroFileSize
                            };

                            this.cwWorker = new XProvider.WorkCommand.CoreWorker(this.laArgs);
                            bExecute = true;
                        }
                        else
                        {
                            MessageBox.Show(XProvider.ResourceValue.MainFrame.WorkMessageValue.MatchOrSubWorkDirectory, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show(XProvider.ResourceValue.MainFrame.WorkMessageValue.NowAllowWorkDirectory, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                // 파일싱크 및 싱크파일 복사작업
                else if ((wctCommandType == XProvider.DataType.WorkCommandType.FileSync) || (wctCommandType == XProvider.DataType.WorkCommandType.GetSyncFile))
                {
                    if ((this.cwWorker != null) && ((this.laArgs != null) && (this.laArgs.bStop == false)))
                    {
                        // 작업경로 체크 - 선택된 경로와 Args의 경로가 동일한지 체크
                        if (((String.IsNullOrEmpty(sLeftDirectory) == false) && (String.IsNullOrEmpty(sRightDirectory) == false)) && ((sLeftDirectory.ToLower() == this.laArgs.wdFileHashDirectory.sLeft.ToLower()) && (sRightDirectory.ToLower() == this.laArgs.wdFileHashDirectory.sRight.ToLower())))
                        {
                            // Args 리스트의 개수와 데이터그리드와의 리스트 갯수가 동일한지 체크
                            if (((this.fhcvMatch.dgvViewer.Rows.Count >= 0) && (this.fhcvOnlyLeft.dgvViewer.Rows.Count >= 0) && (this.fhcvOnlyRight.dgvViewer.Rows.Count >= 0)) && ((this.fhcvMatch.dgvViewer.Rows.Count == this.laArgs.lMatchItem.Count) && (this.fhcvOnlyLeft.dgvViewer.Rows.Count == this.laArgs.lOnlyLeftItem.Count) && (this.fhcvOnlyRight.dgvViewer.Rows.Count == this.laArgs.lOnlyRightItem.Count)))
                            {
                                // 메인작업 시작
                                if (sdptPosition == XProvider.DataType.SyncDirectoryPositionType.None)
                                {
                                    MessageBox.Show(XProvider.ResourceValue.MainFrame.WorkMessageValue.PleaseSelectSyncPosition, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.cbSyncDirectoryPosition.Focus();
                                }
                                else
                                {
                                    // 꼬우~ ㅋ
                                    bExecute = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show(XProvider.ResourceValue.MainFrame.WorkMessageValue.UnMatchArgsListItemCount, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else
                        {
                            MessageBox.Show(XProvider.ResourceValue.MainFrame.WorkMessageValue.UnMatchArgsWorkDirectory, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show(XProvider.ResourceValue.MainFrame.WorkMessageValue.NotExistCoreWorkerOrArgs, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                // -_-
                else
                {
                    MessageBox.Show(XProvider.ResourceValue.MainFrame.WorkMessageValue.UnSetWorkCommand, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                // 파일작업 실행!!
                if (bExecute == true)
                {
                    if ((this.cwWorker != null) && ((this.laArgs != null) && (this.laArgs.bStop == false)))
                    {
                        if (wctCommandType == XProvider.DataType.WorkCommandType.FileSync)
                        {
                            // 파일싱크인 경우 사용자 확인을 받기위함
                            bExecute = (MessageBox.Show(XProvider.ResourceValue.MainFrame.WorkMessageValue.ExecuteConfirm_FileSync, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
                        }
                        else if (wctCommandType == XProvider.DataType.WorkCommandType.GetSyncFile)
                        {
                            // 싱크파일 복사인 경우 복사 될 디렉토리 지정 및 디렉토리 확인. 그리고 사용자 확인을 받기 위함
                            bExecute = false;
                            fbdCopyDirectory = new FolderBrowserDialog();
                            sSelectedCopyDiectory = ((fbdCopyDirectory.ShowDialog() == DialogResult.OK) ? fbdCopyDirectory.SelectedPath : String.Empty);
                            fbdCopyDirectory.Dispose();

                            if (String.IsNullOrEmpty(sSelectedCopyDiectory) == false)
                            {
                                if (Directory.Exists(sSelectedCopyDiectory) == true)
                                {
                                    if ((this.laArgs.wdFileHashDirectory.sLeft.IndexOf(sSelectedCopyDiectory) <= -1) && (this.laArgs.wdFileHashDirectory.sRight.IndexOf(sSelectedCopyDiectory) <= -1))
                                    {
                                        bExecute = (MessageBox.Show(String.Format(XProvider.ResourceValue.MainFrame.WorkMessageValue.ExecuteConfirm_GetSyncFile, sSelectedCopyDiectory), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
                                    }
                                    else
                                    {
                                        MessageBox.Show(XProvider.ResourceValue.MainFrame.WorkMessageValue.CopyDirWorkDirMatchOrSub, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(XProvider.ResourceValue.MainFrame.WorkMessageValue.NotExistCopyDirectory, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                        }

                        if (bExecute == true)
                        {
                            this.laArgs.sdptPosition = sdptPosition;
                            this.laArgs.wctCommandType = wctCommandType;
                            this.laArgs.sCopyDirectory = sSelectedCopyDiectory;
                            this.bWorking = false;
                            this.cwWorker.ExecuteWorker();
                        }
                    }
                    else
                    {
                        MessageBox.Show(XProvider.ResourceValue.MainFrame.WorkMessageValue.NotExistCoreWorkerOrArgs, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                // 작업중단여부 선택
                if (MessageBox.Show(XProvider.ResourceValue.MainFrame.WorkMessageValue.StopWorking, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.cwWorker.StopWorker();
                }
            }
        }

        private void btnAllClear_Click(object sender, EventArgs e)
        {
            this.ApplicationReset(true, false);
        }

        private void tsmiItem_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiItem_EULA_Click(object sender, EventArgs e)
        {
            EULABox eulaBox = new EULABox();
            eulaBox.ShowDialog();
            eulaBox.Dispose();
        }

        private void tsmiItem_About_Click(object sender, EventArgs e)
        {
            AboutBox abBox = new AboutBox();
            abBox.ShowDialog();
            abBox.Dispose();
        }

        private void tsmiItem_UpdateCheck_Click(object sender, EventArgs e)
        {
            MessageBox.Show("업데이트 맹글어야 함!");
        }
    }
}
