using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codename.FileSyncChecker.XProvider.DataModel
{
    public class WorkDirectory
    {
        public string sLeft { get; set; }
        public string sRight { get; set; }

        public WorkDirectory(string sLeft, string sRight)
        {
            this.sLeft = sLeft;
            this.sRight = sRight;
        }
    }
}
