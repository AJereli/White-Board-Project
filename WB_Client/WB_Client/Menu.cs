using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
namespace WB_Client
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        private void Menu_Load(object sender, EventArgs e)//Создаем меню
        {

        }
        private void loadOfBoard_Click(object sender, EventArgs e)//Создание новой доски. Работает при клике на нее
        {
                    
        }        
        private void creatingOfBoard_Click(object sender, EventArgs e)//Загрузка доски.Работает при клике на нее
        {
            Board F2 = new Board(); //переход к чистой доске
            F2.ShowDialog();
            this.Close();//закрываем Menu
         }
         private void exitingFromBoard_Click(object sender, EventArgs e) //Кнопка Exit. Работает при клике на нее
         {
             Application.Exit(); //Закрытие приложения
         }
         //запрос на создание доски 5!!!!! query_board_code = 5
         static private int port = 8000;
         static private IPAddress ipAddr = IPAddress.Parse("127.0.1.1");
         static void SendMessageFromSocket()
         {
             byte[] bytes = new byte[1024]; 
         }
    }
}
