using System;
using System.Windows.Forms;

namespace WB_Client
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Authorization());
            User mainUser = new User();
            //string login = "revi4";
            //string password = "112595";
            mainUser.setLogin("revi4");
            mainUser.setPassword("112595");
        }
    }
}
