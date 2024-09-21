using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codename.FileSyncChecker.XProvider.WorkDelegate
{
    public delegate void dgFileHashCheckResult_Worker(WorkControl.FileHashCheckViewer fhcvViewer, DataType.WorkCommandType wctCommandType, DataModel.FileCheckItem fciItem);
}
