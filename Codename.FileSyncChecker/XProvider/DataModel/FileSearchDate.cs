using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codename.FileSyncChecker.XProvider.DataModel
{
    public class FileSearchDate
    {
        public DateTime dtStart { get; set; }
        public DateTime dtEnd { get; set; }
        public bool bEnabled { get; set; }

        // ----------------------------------------------

        public FileSearchDate(bool bEnabled, DateTime dtStart, DateTime dtEnd)
        {
            this.dtStart = dtStart;
            this.dtEnd = dtEnd;
            this.bEnabled = bEnabled;
        }
    }
}
