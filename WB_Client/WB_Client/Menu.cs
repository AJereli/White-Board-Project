using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
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
        static public byte[] ping_of_server = new byte[1];
        static public Socket client = Authorization.client;
        public Menu()
        {
            server_ok_code[0] = 0;
            query_board_code[0] = 5;
            create_board_err_code[0] = 105;
            connect_board_code[0] = 6;
            connect_board_err_code[0] = 106;
            ping_of_server[0] = 10;
            InitializeComponent();
        }
        private void Menu_Load(object sender, EventArgs e)//Создаем меню
        {
            System.Timers.Timer myTimer = new System.Timers.Timer(10000); //Создаем таймер
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(myTimerPing);
            myTimer.Start();

        }
        public bool chekingPing(int port) //функция отправки пинга
        {
            byte[] bytes = new byte[1024];

            IPAddress ipAddr = IPAddress.Parse("127.1.1.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);

            Authorization.client.Send(ping_of_server);

            client.Receive(bytes);

            if (bytes[0] == ping_of_server[0])
                return true;

            else
                return false;
        }
        void myTimerPing(object sender, System.Timers.ElapsedEventArgs e) //посылка пинга
        {
            if (chekingPing(port))
            {
                 //видимо просто работает
            }
            else
            {
                MessageBox.Show("А сервер то упал");
                Application.Exit();
            }
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

            client.Connect(ipEndPoint);
  

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
                this.Hide();
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
                this.Hide();//закрываем Menu
            }
            else
            {
                MessageBox.Show("Нельзя создать доску!");
            }
        }
    }
}
