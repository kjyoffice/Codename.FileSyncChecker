using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace Codename.FileSyncChecker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex mtMutex;
            string sApplicationName = "Codename.FileSyncChecker";

            mtMutex = new Mutex(false, sApplicationName);

            if (mtMutex.WaitOne(0, false) == true)
            {
                XProvider.WorkCommand.WorkerDelay.iDelay = XProvider.AppConfig.AppSettings.iWorkerDelayTime;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainFrame());
            }
            else
            {
                MessageBox.Show(XProvider.ResourceValue.WorkMessageValue.BlockApplicationMultiRun, sApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            mtMutex.Close();
            mtMutex.Dispose();
        }
    }
}
