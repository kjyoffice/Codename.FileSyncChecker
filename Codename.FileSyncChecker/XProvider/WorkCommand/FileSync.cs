using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Codename.FileSyncChecker.XProvider.WorkCommand
{
    public class FileSync
    {
        private DataModel.LuncherArgs laArgs { get; set; }

        // ---------------------------------------------------------

        public void FileSyncProcess(List<DataModel.FileCheckItem> lItemList)
        {
            DataType.FileSyncCommandType fsctSyncType;
            DataType.FilePositionType fptPosition;
            FileInfo fiTarget;
            string sSource;
            string sTarget;

            foreach (DataModel.FileCheckItem fciItem in lItemList)
            {
                if (this.laArgs.bStop == false)
                {
                    // 초기화
                    sSource = String.Empty;
                    sTarget = String.Empty;
                    fsctSyncType = DataType.FileSyncCommandType.None;
                    fptPosition = DataType.FilePositionType.Left;

                    // 파일위치
                    if (fciItem.fitItemType == DataType.FileItemType.Match)
                    {
                        // 양쪽에 존재하여 틀린 파일만 해당시킴
                        if (fciItem.bFileMatch == false)
                        {
                            // 파일 일치하지 않음 - 변경대상                            
                            // 파일복사 위치에 따른 소스와 타겟경로 지정
                            if (this.laArgs.sdptPosition == DataType.SyncDirectoryPositionType.LeftRight)
                            {
                                // 오른쪽 파일을 왼쪽의 파일로 덮어씌움
                                // 소스 : 왼쪽의 파일 / 타겟 : 오른쪽의 파일
                                sSource = (this.laArgs.wdFileHashDirectory.sLeft + fciItem.sFileName);
                                sTarget = (this.laArgs.wdFileHashDirectory.sRight + fciItem.sFileName);
                                // 복사된 파일(Target)이 Right쪽이므로 Right를 선택
                                fptPosition = DataType.FilePositionType.Right;
                            }
                            else if (this.laArgs.sdptPosition == DataType.SyncDirectoryPositionType.RightLeft)
                            {
                                // 왼쪽의 파일을 오른쪽의 파일로 덮어씌움
                                // 소스 : 오른쪽의 파일 / 타겟 : 왼쪽의 파일
                                sSource = (this.laArgs.wdFileHashDirectory.sRight + fciItem.sFileName);
                                sTarget = (this.laArgs.wdFileHashDirectory.sLeft + fciItem.sFileName);
                                // 복사된 파일(Target)이 Left쪽이므로 Left를 선택
                                fptPosition = DataType.FilePositionType.Left;
                            }

                            // 파일복사진행 Sign
                            fsctSyncType = DataType.FileSyncCommandType.Copy;
                        }
                    }
                    else if (fciItem.fitItemType == DataType.FileItemType.OnlyLeft)
                    {
                        // 왼쪽에만 존재
                        if (this.laArgs.sdptPosition == DataType.SyncDirectoryPositionType.LeftRight)
                        {
                            // 왼쪽에서 오른쪽 작업으로는 왼쪽에는 있고 오른쪽에는 없으므로 왼쪽의 파일을 오른쪽으로 복사
                            sSource = (this.laArgs.wdFileHashDirectory.sLeft + fciItem.sFileName);
                            sTarget = (this.laArgs.wdFileHashDirectory.sRight + fciItem.sFileName);
                            // 파일복사진행 Sign
                            fsctSyncType = DataType.FileSyncCommandType.Copy;
                        }
                        else if (this.laArgs.sdptPosition == DataType.SyncDirectoryPositionType.RightLeft)
                        {
                            // 오른쪽에서 왼쪽 작업으로는 오른쪽 파일만 중요하며 왼쪽의 파일이 있을 필요가 없어서 파일을 삭제함
                            sSource = (this.laArgs.wdFileHashDirectory.sLeft + fciItem.sFileName);
                            sTarget = sSource;
                            // 파일삭제진행 Sign
                            fsctSyncType = DataType.FileSyncCommandType.Delete;
                        }

                        // Copy명령의 경우, 복사된 파일(Target)의 파일해쉬를 변경해야 하므로 복사된 파일의 위치를 선택
                        // ** Delete에는 해당사항 없음
                        fptPosition = DataType.FilePositionType.Right;
                    }
                    else if (fciItem.fitItemType == DataType.FileItemType.OnlyRight)
                    {
                        // 오른쪽에만 존재
                        if (this.laArgs.sdptPosition == DataType.SyncDirectoryPositionType.LeftRight)
                        {
                            // 왼쪽에서 오른쪽 작업으로는 왼쪽 파일만 중요하며 오른쪽의 파일이 있을 필요가 없어서 파일을 삭제함
                            sSource = (this.laArgs.wdFileHashDirectory.sRight + fciItem.sFileName);
                            sTarget = sSource;
                            // 파일삭제진행 Sign
                            fsctSyncType = DataType.FileSyncCommandType.Delete;
                        }
                        else if (this.laArgs.sdptPosition == DataType.SyncDirectoryPositionType.RightLeft)
                        {
                            // 오른쪽에서 왼쪽 작업으로는 오른쪽에는 있고 왼쪽에는 없으므로 오른쪽의 파일을 왼쪽으로 복사
                            sSource = (this.laArgs.wdFileHashDirectory.sRight + fciItem.sFileName);
                            sTarget = (this.laArgs.wdFileHashDirectory.sLeft + fciItem.sFileName);
                            // 파일복사진행 Sign
                            fsctSyncType = DataType.FileSyncCommandType.Copy;
                        }

                        // Copy명령의 경우, 복사된 파일(Target)의 파일해쉬를 변경해야 하므로 복사된 파일의 위치를 선택
                        // ** Delete에는 해당사항 없음
                        fptPosition = DataType.FilePositionType.Left;
                    }

                    // 실제 파일IO
                    if (fsctSyncType != DataType.FileSyncCommandType.None)
                    {
                        // ** 타겟의 파일은 실제로 존재하는 파일이겠지만, 프로그램 동작에선 있으나 없으나 신경쓰지 않아도 됨. 문제는 소스의 파일이 없으면 안됨. =_=
                        if (((String.IsNullOrEmpty(sSource) == false) && (String.IsNullOrEmpty(sTarget) == false)) && (File.Exists(sSource) == true))
                        {
                            if (fsctSyncType == DataType.FileSyncCommandType.Copy)
                            {
                                // 싱크파일 복사인 경우에는 싱크되는 디렉토리 파일에 대해 경로 재정의
                                if (this.laArgs.wctCommandType == DataType.WorkCommandType.GetSyncFile)
                                {
                                    // 타겟경로 재정의
                                    sTarget = (this.laArgs.sCopyDirectory + fciItem.sFileName);
                                }

                                // 복사작업
                                fiTarget = new FileInfo(sTarget);

                                // 타겟에 대해서는 디렉토리가 존재하지 않을 수 있으므로 디렉토리 존재여부에 따라 생성을 우선 진행
                                if (fiTarget.Directory.Exists == false)
                                {
                                    // 디렉토리 생성
                                    fiTarget.Directory.Create();
                                }

                                // 파일복사
                                File.Copy(sSource, sTarget, true);

                                // 파일싱크인 경우, 복사된 파일에 따라 일치여부 재확인
                                if (this.laArgs.wctCommandType == DataType.WorkCommandType.FileSync)
                                {
                                    // Source -> Target로 파일을 복사한 후 왼쪽에서 오른쪽이냐, 오른쪽에서 왼쪽이냐의 여부에 따라
                                    // 파일 해쉬를 변경하며 파일 일치여부를 테스트
                                    fciItem.UpdateFileHash(fptPosition, sTarget, true);
                                    fciItem.fsktSyncType = ((fciItem.bFileMatch == true) ? DataType.FileSyncKeyType.Match : DataType.FileSyncKeyType.UnMatch);

                                    // 복사Sign
                                    fciItem.fsktSyncType = DataType.FileSyncKeyType.Match;
                                }
                                else
                                {
                                    // 복사Sign
                                    fciItem.fsktSyncType = DataType.FileSyncKeyType.Copy;
                                }
                            }
                            else if (fsctSyncType == DataType.FileSyncCommandType.Delete)
                            {
                                // 파일 싱크인 경우에만 실제파일 삭제하며 그게 아니면 그냥 SKIP.
                                if (this.laArgs.wctCommandType == DataType.WorkCommandType.FileSync)
                                {
                                    // 삭제작업
                                    // ** Source의 경로를 삭제. (* Target는 필요하지 않음)
                                    File.Delete(sSource);

                                    // 삭제Sign
                                    fciItem.fsktSyncType = DataType.FileSyncKeyType.Delete;
                                }
                                else
                                {
                                    // Skip!
                                    fciItem.fsktSyncType = DataType.FileSyncKeyType.Skip;
                                }
                            }
                        }
                        else
                        {
                            // 동작하지 않음 Sign
                            fciItem.fsktSyncType = DataType.FileSyncKeyType.NotExecute;
                        }
                    }
                    else
                    {
                        // Skip!
                        fciItem.fsktSyncType = DataType.FileSyncKeyType.Skip;
                    }

                    // 변경내역 UI로 던짐 ㅋ
                    this.laArgs.fhcrCheckResult(this.laArgs.wctCommandType, fciItem);
                    this.laArgs.npUpdteWorkItem();
                    WorkerDelay.Delay();
                }
                else
                {
                    break;
                }
            }
        }

        // ---------------------------------------------------------

        public FileSync(DataModel.LuncherArgs laArgs)
        {
            this.laArgs = laArgs;
        }

        public void Start_Match()
        {
            this.FileSyncProcess(this.laArgs.lMatchItem);
        }

        public void Start_OnlyLeft()
        {
            this.FileSyncProcess(this.laArgs.lOnlyLeftItem);
        }

        public void Start_OnlyRight()
        {
            this.FileSyncProcess(this.laArgs.lOnlyRightItem);
        }
    }
}
