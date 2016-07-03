using System;
using System.Windows.Forms;

namespace WB_Client
{
    static class Program
    {
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Authorization());
            //sApplication.Run(new Menu());
            //Application.Run(new Board());
        }
    }
}
