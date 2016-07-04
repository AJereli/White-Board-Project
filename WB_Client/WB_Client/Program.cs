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
<<<<<<< HEAD
            Application.Run(new Authorization());
            //sApplication.Run(new Menu());
            //Application.Run(new Board());
=======
           // Application.Run(new Authorization());
            //Application.Run(new Menu());
            Application.Run(new Board());
>>>>>>> refs/remotes/origin/master
        }
    }
}
