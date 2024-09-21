using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Codename.FileSyncChecker.XProvider.WorkCommand
{
    public class WorkerDelay
    {
        // TODO : [예외] 너무 많이 호출되는 관계로 프로그램 초기에 값을 지정함
        public static int iDelay { get; set; }

        // ------------------------------------------------------------

        public static void Delay()
        {
            if (WorkerDelay.iDelay >= 10)
            {
                Thread.Sleep(WorkerDelay.iDelay);
            }
        }
    }
}
