using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;

namespace Codename.FileSyncChecker.XProvider.WorkCommand
{
    public class CoreWorker
    {
        private DataModel.LuncherArgs laArgs { get; set; }
        private BackgroundWorker bwStarter { get; set; }
        private BackgroundWorker bwMatch { get; set; }
        private BackgroundWorker bwOnlyLeft { get; set; }
        private BackgroundWorker bwOnlyRight { get; set; }
        private int iWorkEndCount { get; set; }
        
        // -----------------------------------------

        private void StartEachListWorking()
        {
            RunWorkerCompletedEventHandler rwcehCompletedEvent;

            this.bwMatch = new BackgroundWorker();
            this.bwMatch.DoWork += new DoWorkEventHandler(
                delegate(object sender, DoWorkEventArgs e)
                {
                    if (this.laArgs.wctCommandType == DataType.WorkCommandType.FileHash)
                    {
                        new FileHashChecker(this.laArgs).Start_Match();
                    }
                    else if ((this.laArgs.wctCommandType == DataType.WorkCommandType.FileSync) || (this.laArgs.wctCommandType == DataType.WorkCommandType.GetSyncFile))
                    {
                        new FileSync(this.laArgs).Start_Match();
                    }
                }
            );
            
            this.bwOnlyLeft = new BackgroundWorker();
            this.bwOnlyLeft.DoWork += new DoWorkEventHandler(
                delegate(object sender, DoWorkEventArgs e)
                {
                    if (this.laArgs.wctCommandType == DataType.WorkCommandType.FileHash)
                    {
                        new FileHashChecker(this.laArgs).Start_OnlyLeft();
                    }
                    else if ((this.laArgs.wctCommandType == DataType.WorkCommandType.FileSync) || (this.laArgs.wctCommandType == DataType.WorkCommandType.GetSyncFile))
                    {
                        new FileSync(this.laArgs).Start_OnlyLeft();
                    }
                }
            );
            
            this.bwOnlyRight = new BackgroundWorker();
            this.bwOnlyRight.DoWork += new DoWorkEventHandler(
                delegate(object sender, DoWorkEventArgs e)
                {
                    if (this.laArgs.wctCommandType == DataType.WorkCommandType.FileHash)
                    {
                        new FileHashChecker(this.laArgs).Start_OnlyRight();
                    }
                    else if ((this.laArgs.wctCommandType == DataType.WorkCommandType.FileSync) || (this.laArgs.wctCommandType == DataType.WorkCommandType.GetSyncFile))
                    {
                        new FileSync(this.laArgs).Start_OnlyRight();
                    }
                }
            );

            rwcehCompletedEvent = new RunWorkerCompletedEventHandler(
                delegate(object sender, RunWorkerCompletedEventArgs e)
                {
                    this.WorkEndSign();
                }
            );

            this.bwMatch.RunWorkerCompleted += rwcehCompletedEvent;
            this.bwOnlyLeft.RunWorkerCompleted += rwcehCompletedEvent;
            this.bwOnlyRight.RunWorkerCompleted += rwcehCompletedEvent;

            this.bwMatch.RunWorkerAsync();
            this.bwOnlyLeft.RunWorkerAsync();
            this.bwOnlyRight.RunWorkerAsync();
        }

        private void WorkEndSign()
        {
            if (this.iWorkEndCount >= 2)
            {
                if (this.laArgs.bStop == true)
                {
                    this.laArgs.wctpWorkStop(this.laArgs.wctCommandType);
                }

                this.laArgs.wctpWorkEnd(this.laArgs.wctCommandType);
                this.iWorkEndCount = 0;
                this.bwStarter.Dispose();
                this.bwMatch.Dispose();
                this.bwOnlyLeft.Dispose();
                this.bwOnlyRight.Dispose();
            }
            else
            {
                this.iWorkEndCount++;
            }
        }

        // -----------------------------------------

        public CoreWorker(DataModel.LuncherArgs laArgs)
        {
            this.laArgs = laArgs;
            this.iWorkEndCount = 0;
        }

        public void ExecuteWorker()
        {
            this.bwStarter = new BackgroundWorker();
            this.bwStarter.DoWork += new DoWorkEventHandler(
                delegate(object sender, DoWorkEventArgs e)
                {
                    this.iWorkEndCount = 0;

                    if (this.laArgs.wctCommandType == DataType.WorkCommandType.FileHash)
                    {
                        new GetWorkItem(this.laArgs).GetWorkItemList();
                    }
                    else if ((this.laArgs.wctCommandType == DataType.WorkCommandType.FileSync) || (this.laArgs.wctCommandType == DataType.WorkCommandType.GetSyncFile))
                    {
                        this.laArgs.sriSetItem(this.laArgs.iItemTotalCount);
                    }
                }
            );
            this.bwStarter.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate(object sender, RunWorkerCompletedEventArgs e)
                {
                    if (this.laArgs.wctCommandType == DataType.WorkCommandType.FileHash)
                    {
                        this.laArgs.sriSetItem(this.laArgs.iItemTotalCount);
                    }
                    else if ((this.laArgs.wctCommandType == DataType.WorkCommandType.FileSync) || (this.laArgs.wctCommandType == DataType.WorkCommandType.GetSyncFile))
                    {
                        // Empty
                    }

                    WorkerDelay.Delay();
                    this.StartEachListWorking();
                }
            );

            this.laArgs.wctpWorkStart(this.laArgs.wctCommandType);
            WorkerDelay.Delay();
            this.bwStarter.RunWorkerAsync();
            // ** 여기서부터는 비동기 명령으로 인해 "RunWorkerCompleted"가 끝난 후 동작하지 않음
        }

        public void StopWorker()
        {
            this.laArgs.StopSign();
        }
    }
}
