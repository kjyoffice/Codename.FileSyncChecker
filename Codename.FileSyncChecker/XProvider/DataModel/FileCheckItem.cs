using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Codename.FileSyncChecker.XProvider.DataModel
{
    public class FileCheckItem
    {
        public DataType.FileItemType fitItemType { get; set; }
        public DataType.FileSyncKeyType fsktSyncType { get; set; }
        public DateTime dtFileEditDateLeft { get; set; }
        public DateTime dtFileEditDateRight { get; set; }
        public int iRowIndex { get; set; }
        public string sFileName { get; set; }
        public string sFileHashLeft { get; set; }
        public string sFileHashRight { get; set; }
        public long lFileSizeLeft { get; set; }
        public long lFileSizeRight { get; set; }
        public bool bFileMatch { get; set; }
        public bool bMatchFileHash { get; set; }
        public bool bMatchFileSize { get; set; }
        public bool bMatchFileEditDate { get; set; }

        // ----------------------------------------------------------------------------------

        public FileCheckItem(DataType.FileItemType fitType, string sFileName)
        {
            this.fitItemType = fitType;
            this.iRowIndex = -1;
            this.sFileName = sFileName;
            this.dtFileEditDateLeft = DateTime.MinValue;
            this.dtFileEditDateRight = DateTime.MinValue;
            this.sFileHashLeft = String.Empty;
            this.sFileHashRight = String.Empty;
            this.lFileSizeLeft = 0;
            this.lFileSizeRight = 0;
            this.bMatchFileHash = false;
            this.bMatchFileSize = false;
            this.bMatchFileEditDate = false;
            this.bFileMatch = false;
            this.fsktSyncType = DataType.FileSyncKeyType.Initialize;
        }

        public void UpdateFileHash(DataType.FilePositionType fptPosition, string sFilePath, bool bUpdateMatch)
        {
            FileInfo fiFile;
            DateTime dtEditDate = DateTime.MinValue;
            string sFileHash = String.Empty;
            long lFileSize = 0;

            if (String.IsNullOrEmpty(sFilePath) == false)
            {
                fiFile = new FileInfo(sFilePath);
                dtEditDate = fiFile.LastWriteTime;
                lFileSize = fiFile.Length;
                sFileHash = this.CreateFileHash(sFilePath);
            }
            
            if (fptPosition == DataType.FilePositionType.Left)
            {
                this.sFileHashLeft = sFileHash;
                this.dtFileEditDateLeft = dtEditDate;
                this.lFileSizeLeft = lFileSize;
            }
            else if (fptPosition == DataType.FilePositionType.Right)
            {
                this.sFileHashRight = sFileHash;
                this.dtFileEditDateRight = dtEditDate;
                this.lFileSizeRight = lFileSize;
            }

            // 파일해쉬 매치체크
            if (bUpdateMatch == true)
            {
                this.bMatchFileHash = (this.sFileHashLeft == this.sFileHashRight);

                this.bMatchFileSize = ((AppConfig.AppSettings.bFileMatchWithSize == true) ? (this.lFileSizeLeft == this.lFileSizeRight) : true);
                this.bMatchFileEditDate = ((AppConfig.AppSettings.bFileMatchWithEditDate == true) ? (this.dtFileEditDateLeft == this.dtFileEditDateRight) : true);
                
                this.bFileMatch = ((this.bMatchFileHash == true) && (this.bMatchFileSize == true) && (this.bMatchFileEditDate == true));
            }
        }

        private string CreateFileHash(string sFilePath)
        {
            string result = String.Empty;

            using (MD5 hash = MD5.Create())
            {
                byte[] btFileBuffer = File.ReadAllBytes(sFilePath);
                byte[] btHash = hash.ComputeHash(btFileBuffer);
                string sRawHash = BitConverter.ToString(btHash);

                result = Regex.Replace(sRawHash, "[^0-9A-Za-z]", string.Empty, RegexOptions.IgnoreCase).Trim().ToLower();

                hash.Clear();
            }

            return result;
        }
    }
}
