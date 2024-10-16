﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codename.FileSyncChecker.XProvider.WorkCommand
{
    public class FileHashChecker
    {
        private DataModel.LuncherArgs laArgs { get; set; }

        // ---------------------------------------

        public void FileHashCheckProcess(List<DataModel.FileCheckItem> lItemList)
        {
            string sFilePathLeft;
            string sFilePathRight;

            foreach (DataModel.FileCheckItem fciItem in lItemList)
            {
                if (this.laArgs.bStop == false)
                {
                    sFilePathLeft = string.Empty;
                    sFilePathRight = string.Empty;

                    if (fciItem.fitItemType == DataType.FileItemType.Match)
                    {
                        sFilePathLeft = (this.laArgs.wdFileHashDirectory.sLeft + fciItem.sFileName);
                        sFilePathRight = (this.laArgs.wdFileHashDirectory.sRight + fciItem.sFileName);
                    }
                    else if (fciItem.fitItemType == DataType.FileItemType.OnlyLeft)
                    {
                        sFilePathLeft = (this.laArgs.wdFileHashDirectory.sLeft + fciItem.sFileName);
                        sFilePathRight = string.Empty;
                    }
                    else if (fciItem.fitItemType == DataType.FileItemType.OnlyRight)
                    {
                        sFilePathLeft = string.Empty;
                        sFilePathRight = (this.laArgs.wdFileHashDirectory.sRight + fciItem.sFileName);
                    }

                    fciItem.UpdateFileHash(DataType.FilePositionType.Left, sFilePathLeft, false);
                    fciItem.UpdateFileHash(DataType.FilePositionType.Right, sFilePathRight, true);
                    fciItem.fsktSyncType = DataType.FileSyncKeyType.Ready;

                    this.laArgs.fhcrCheckResult(DataType.WorkCommandType.FileHash, fciItem);
                    this.laArgs.npUpdteWorkItem();
                    WorkerDelay.Delay();
                }
                else
                {
                    break;
                }
            }
        }

        // ---------------------------------------

        public FileHashChecker(DataModel.LuncherArgs laArgs)
        {
            this.laArgs = laArgs;
        }

        public void Start_Match()
        {
            this.FileHashCheckProcess(this.laArgs.lMatchItem);
        }

        public void Start_OnlyLeft()
        {
            this.FileHashCheckProcess(this.laArgs.lOnlyLeftItem);
        }

        public void Start_OnlyRight()
        {
            this.FileHashCheckProcess(this.laArgs.lOnlyRightItem);
        }
    }
}
