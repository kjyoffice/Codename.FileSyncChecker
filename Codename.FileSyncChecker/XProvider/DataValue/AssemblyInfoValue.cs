using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Codename.FileSyncChecker.XProvider.DataValue
{
    public class AssemblyInfoValue
    {
        public static string sCopyright
        {
            get
            {
                return (Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0] as AssemblyCopyrightAttribute).Copyright;
            }
        }

        public static Version vVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        public static string sName
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Name;
            }
        }    
    }
}
