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
         private void exitingFromBoard_Click(object sender, EventArgs e) //Кнопка Exit. Работает при клике на нее
         {
             Application.Exit(); //Закрытие приложения
         }
        //запрос на создание доски 5!!!!! query_board_code = 5
        static private int port = 8000;
        static private int query_board_code = 5;
        static private int connect_board_code = 6;
        static private int create_board_err_code = 105;

        public bool chekingServer(int port)
         {
             byte[] bytes = new byte[1024];

            IPAddress ipAddr = IPAddress.Parse("127.0.1.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);
            Socket client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            client.Connect(ipEndPoint);

            client.Receive(bytes);

            if (bytes[0] == query_board_code )
                return true;

            else if (bytes[0] == create_board_err_code)
                return false;
            else
                return false;
        }
        private void creatingOfBoard_Click(object sender, EventArgs e)//Загрузка доски.Работает при клике на нее
        {
            if (chekingServer(port)) {
                Board F2 = new Board(); //переход к чистой доске
                F2.ShowDialog();
                this.Close();//закрываем Menu
            }
            else
            {
                MessageBox.Show("Не сегодня!");
            }
        }
    }
}
