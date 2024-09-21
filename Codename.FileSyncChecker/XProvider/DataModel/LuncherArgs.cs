using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codename.FileSyncChecker.XProvider.DataModel
{
    public class LuncherArgs
    {
        public WorkDelegate.dgWorkCommandTypeParameter wctpWorkStart { get; set; }
        public WorkDelegate.dgWorkCommandTypeParameter wctpWorkEnd { get; set; }
        public WorkDelegate.dgWorkCommandTypeParameter wctpWorkStop { get; set; }
        public WorkDelegate.dgSetReadyItem sriSetItem { get; set; }
        public WorkDelegate.dgNoParameter npUpdteWorkItem { get; set; }
        public WorkDelegate.dgFileHashCheckResult fhcrCheckResult { get; set; }
        public DataModel.WorkDirectory wdFileHashDirectory { get; set; }
        public DataType.SyncDirectoryPositionType sdptPosition { get; set; }
        public DataType.WorkCommandType wctCommandType { get; set; }
        public FileSearchDate fsdSearchDate { get; set; }
        public List<string> lSkipFileName { get; set; }
        public List<string> lSkipFileExtension { get; set; }
        public List<FileCheckItem> lMatchItem { get; set; }
        public List<FileCheckItem> lOnlyLeftItem { get; set; }
        public List<FileCheckItem> lOnlyRightItem { get; set; }
        public int iItemTotalCount { get; set; }
        public bool bSkipNoneFileExtension { get; set; }
        public bool bSkipZeroFileSize { get; set; }
        public bool bStop { get; set; }
        public string sCopyDirectory { get; set; }

        // --------------------------------------------------------------

        private void DefaultSetSkipItem()
        {
            foreach (string sItem in AppConfig.AppSettings.sSkipFileName)
            {
                this.lSkipFileName.Add(sItem);
            }

            foreach (string sItem in AppConfig.AppSettings.sSkipFileExtension)
            {
                this.lSkipFileExtension.Add(sItem);
            }
        }

        // --------------------------------------------------------------

        public LuncherArgs()
        {
            this.lSkipFileName = new List<string>();
            this.lSkipFileExtension = new List<string>();
            this.lMatchItem = new List<DataModel.FileCheckItem>();
            this.lOnlyLeftItem = new List<DataModel.FileCheckItem>();
            this.lOnlyRightItem = new List<DataModel.FileCheckItem>();
            this.iItemTotalCount = 0;
            this.bStop = false;
            this.DefaultSetSkipItem();
            this.sCopyDirectory = String.Empty;
        }

        public void StopSign()
        {
            this.bStop = true;
        }
    }
}
