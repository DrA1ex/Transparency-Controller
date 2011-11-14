using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TransparencyControlEx
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                MainShell();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show( ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }
            finally
            {
                if(appController != null)
                    appController.Dispose();
            }
        }

        static void MainShell()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            appController = new ApplicationController();
            Application.Run( appController );
        }

        static ApplicationController appController = null;
    }
}
