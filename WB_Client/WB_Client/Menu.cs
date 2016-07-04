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
        
        static private int port = 8000;
        static public byte[] query_board_code = new byte[1]; //5
        static public byte[] connect_board_code = new byte[1]; //6
        static public byte[] create_board_err_code = new byte[1]; //105
        static public byte[] server_ok_code = new byte[1];
        static public byte[] connect_board_err_code = new byte[1]; //106
        public Menu()
        {
            server_ok_code[0] = 0;
            query_board_code[0] = 5;
            create_board_err_code[0] = 105;
            connect_board_code[0] = 6;
            connect_board_err_code[0] = 106;
            InitializeComponent();
        }
        private void Menu_Load(object sender, EventArgs e)//Создаем меню
        {

        }     
         private void exitingFromBoard_Click(object sender, EventArgs e) //Кнопка Exit. Работает при клике на нее
         {
             Application.Exit(); //Закрытие приложения
         }
        public bool loadingServer(int port)
        {
            byte[] bytes = new byte[1024];

            IPAddress ipAddr = IPAddress.Parse("127.1.1.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);
            Socket client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            /*int loginLength = Login.Text.Length;*/

            client.Connect(ipEndPoint);
  

            /* client.Send(Encoding.UTF8.GetBytes(loginLength.ToString()));
             client.Send(Encoding.UTF8.GetBytes(Login.Text)); */


            client.Send(connect_board_code);

            client.Receive(bytes);

            if (bytes[0] == server_ok_code[0])
                return true;

            else if (bytes[0] == connect_board_err_code[0])
                return false;
            else
                return false;
        }
        private void loadOfBoard_Click(object sender, EventArgs e)//Подсоединение к  доске(old_girlfriend). Работает при клике на нее
        {
            if (loadingServer(port))
            {
                Board F2 = new Board();
                F2.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Невозможно присоедениться!");
            }
        }
        public bool chekingServer(int port)
         {
             byte[] bytes = new byte[1024];

            IPAddress ipAddr = IPAddress.Parse("127.1.1.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);
            Socket client = Authorization.client;

           // client.Connect(ipEndPoint);

            Authorization.client.Send(query_board_code);

            client.Receive(bytes);

            if (bytes[0] == server_ok_code[0])
                return true;

            else if (bytes[0] == create_board_err_code[0])
                return false;
            else
                return false;
        }
        private void creatingOfBoard_Click(object sender, EventArgs e)//Загрузка доски.Работает при клике на нее
        {
            if (chekingServer(port)) {
                Board F2 = new Board(); //переход к чистойs доске
                F2.ShowDialog();
                this.Close();//закрываем Menu
            }
            else
            {
                MessageBox.Show("Нельзя создать доску!");
            }
        }
    }
}
