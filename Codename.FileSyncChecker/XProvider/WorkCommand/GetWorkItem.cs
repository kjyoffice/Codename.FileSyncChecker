using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Codename.FileSyncChecker.XProvider.WorkCommand
{
    public class GetWorkItem
    {
        private DataModel.LuncherArgs laArgs { get; set; }

        // --------------------------

        private Dictionary<bool, List<string>> CreateItemList(DirectoryInfo diSource, DirectoryInfo diTarget)
        {
            Dictionary<bool, List<string>> dicResult;
            FileInfo fiTarget;
            string sFileName;
            bool bFileListIn;

            dicResult = new Dictionary<bool, List<string>>();
            dicResult.Add(true, new List<string>());
            dicResult.Add(false, new List<string>());

            foreach (FileInfo fiItem in diSource.GetFiles("*.*", SearchOption.AllDirectories))
            {
                if (this.laArgs.bStop == false)
                {
                    sFileName = fiItem.FullName.Replace(diSource.FullName, string.Empty);

                    if (
                            ((string.IsNullOrEmpty(sFileName) == false) && (sFileName != @"\")) &&
                            (
                                (this.laArgs.bSkipNoneFileExtension == false) || 
                                (
                                    (this.laArgs.bSkipNoneFileExtension == true) &&
                                    (string.IsNullOrEmpty(fiItem.Extension) == false)
                                )
                            ) &&
                            (
                                ((laArgs.lSkipFileName.Where((x) => x.ToLower() == fiItem.Name.ToLower())).Count() <= 0) &&
                                ((laArgs.lSkipFileExtension.Where((x) => x.ToLower() == fiItem.Extension.ToLower())).Count() <= 0)
                            )
                        )
                    {
                        fiTarget = new FileInfo((diTarget.FullName + sFileName));
                        bFileListIn = true;

                        // 파일의 날자검색이 활성화 경우
                        if (this.laArgs.fsdSearchDate.bEnabled == true)
                        {
                            // 현재 foreach단계의 파일에 대해서 수정일을 범위검색 함
                            bFileListIn = ((fiItem.LastWriteTime >= this.laArgs.fsdSearchDate.dtStart) && (fiItem.LastWriteTime <= this.laArgs.fsdSearchDate.dtEnd));
                            // 반대편(Target) 경로의 파일이 존재하며 위에서 수정일에 포함되었다면 반대편 파일에 대해서 수정일을 범위검색함
                            bFileListIn = (((fiTarget.Exists == true) && (bFileListIn == true)) ? ((fiTarget.LastWriteTime >= this.laArgs.fsdSearchDate.dtStart) && (fiTarget.LastWriteTime <= this.laArgs.fsdSearchDate.dtEnd)) : bFileListIn);
                        }

                        if (bFileListIn == true)
                        {
                            dicResult[fiTarget.Exists].Add(sFileName);
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            return dicResult;
        }

        // --------------------------

        public GetWorkItem(DataModel.LuncherArgs laArgs)
        {
            this.laArgs = laArgs;
        }

        public void GetWorkItemList()
        {
            DirectoryInfo diLeft = new DirectoryInfo(laArgs.wdFileHashDirectory.sLeft);
            DirectoryInfo diRight = new DirectoryInfo(laArgs.wdFileHashDirectory.sRight);
            Dictionary<bool, List<string>> dicLR;
            Dictionary<bool, List<string>> dicRL;
            FileInfo fiLeft;
            FileInfo fiRight;
            DataType.FileItemType fiType;
            bool bSkipZeroFileSize = AppConfig.AppSettings.bSkipZeroFileSize;
            bool bFileListIn;

            if ((diLeft.Exists == true) && (diRight.Exists == true))
            {
                dicLR = this.CreateItemList(diLeft, diRight);
                dicRL = this.CreateItemList(diRight, diLeft);

                // Match
                foreach (string sItem in dicLR[true])
                {
                    fiLeft = new FileInfo((diLeft.FullName + sItem));
                    fiRight = new FileInfo((diRight.FullName + sItem));
                    fiType = DataType.FileItemType.Match;
                    bFileListIn = false;

                    // 양쪽에 다 있음
                    if ((fiLeft.Exists == true) && (fiRight.Exists == true))
                    {
                        // 0바이트인지 체크하지 않으면 그냥 리스트에 등록
                        bFileListIn = !bSkipZeroFileSize;

                        // 0바이트인지 체크진행
                        if (bSkipZeroFileSize == true)
                        {
                            // 양쪽 다 파일 크기가 0보다 큼
                            if ((fiLeft.Length > 0) && (fiRight.Length > 0))
                            {
                                fiType = DataType.FileItemType.Match;
                                bFileListIn = true;
                            }
                            // 오른쪽이 0바이트임
                            else if ((fiLeft.Length > 0) && (fiRight.Length <= 0))
                            {
                                fiType = DataType.FileItemType.OnlyLeft;
                                bFileListIn = true;
                            }
                            // 왼쪽이 0바이트임
                            else if ((fiLeft.Length <= 0) && (fiRight.Length > 0))
                            {
                                fiType = DataType.FileItemType.OnlyRight;
                                bFileListIn = true;
                            }
                            //// 양쪽 모두 0바이트임
                            //else if ((fiLeft.Length <= 0) && (fiRight.Length <= 0))
                            //{
                            //}
                        }
                    }
                    // 왼쪽에만 있음
                    else if ((fiLeft.Exists == true) && (fiRight.Exists == false))
                    {
                        if ((bSkipZeroFileSize == false) || ((bSkipZeroFileSize == true) && (fiLeft.Length > 0)))
                        {
                            fiType = DataType.FileItemType.OnlyLeft;
                            bFileListIn = true;
                        }
                    }
                    // 오른쪽에만 있음
                    else if ((fiLeft.Exists == false) && (fiRight.Exists == true))
                    {
                        if ((bSkipZeroFileSize == false) || ((bSkipZeroFileSize == true) && (fiRight.Length > 0)))
                        {
                            fiType = DataType.FileItemType.OnlyRight;
                            bFileListIn = true;
                        }
                    }
                    //// 양쪽 다 없음
                    //else if ((fiLeft.Exists == false) && (fiRight.Exists == false))
                    //{
                    //}

                    if (bFileListIn == true)
                    {
                        // 일치 리스트
                        if (fiType == DataType.FileItemType.Match)
                        {
                            this.laArgs.lMatchItem.Add(new DataModel.FileCheckItem(DataType.FileItemType.Match, sItem));
                        }
                        // 왼쪽 리스트
                        else if (fiType == DataType.FileItemType.OnlyLeft)
                        {
                            dicLR[false].Add(sItem);
                        }
                        // 오른쪽 리스트
                        else if (fiType == DataType.FileItemType.OnlyRight)
                        {
                            dicRL[false].Add(sItem);
                        }
                    }
                }

                // OnlyLeft
                foreach (string sItem in dicLR[false])
                {
                    fiLeft = new FileInfo((diLeft.FullName + sItem));

                    // 파일이 존재하는지 체크
                    if (fiLeft.Exists == true)
                    {
                        // 0바이트 제외를 하지 않거나 하는경우 파일크기 체크
                        if ((bSkipZeroFileSize == false) || ((bSkipZeroFileSize == true) && (fiLeft.Length > 0)))
                        {
                            this.laArgs.lOnlyLeftItem.Add(new DataModel.FileCheckItem(DataType.FileItemType.OnlyLeft, sItem));
                        }
                    }
                }

                // OnlyRight
                foreach (string sItem in dicRL[false])
                {
                    fiRight = new FileInfo((diRight.FullName + sItem));

                    // 파일이 존재하는지 체크
                    if (fiRight.Exists == true)
                    {
                        // 0바이트 제외를 하지 않거나 하는경우 파일크기 체크
                        if ((bSkipZeroFileSize == false) || ((bSkipZeroFileSize == true) && (fiRight.Length > 0)))
                        {
                            this.laArgs.lOnlyRightItem.Add(new DataModel.FileCheckItem(DataType.FileItemType.OnlyRight, sItem));
                        }
                    }
                }

                this.laArgs.iItemTotalCount = (this.laArgs.lMatchItem.Count + this.laArgs.lOnlyLeftItem.Count + this.laArgs.lOnlyRightItem.Count);
            }
        }
    }
}
