using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codename.FileSyncChecker.XProvider.DataModel
{
    public class SyncDirectoryPositionItem
    {
        public DataType.SyncDirectoryPositionType sdptPosition { get; private set; }
        public string sDisplayText { get; private set; }

        // -------------------------------------------------------------------

        public SyncDirectoryPositionItem(DataType.SyncDirectoryPositionType wdptPosition, string sDisplayText)
        {
            this.sdptPosition = wdptPosition;
            this.sDisplayText = sDisplayText;
        }

        public override string ToString()
        {
            return this.sDisplayText;
        } 
    }
}
