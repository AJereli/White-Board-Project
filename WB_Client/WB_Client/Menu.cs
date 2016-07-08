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
        static public Socket client = Authorization.client;
        static private int port = 8000;
        static public int loadMode;
        static public string name;
        static public byte[] query_board_code = new byte[1]; //5
        static public byte[] connect_board_code = new byte[1]; //6
        static public byte[] create_board_err_code = new byte[1]; //105
        static public byte[] server_ok_code = new byte[1];
        public Menu()
        {
            server_ok_code[0] = 0;
            query_board_code[0] = 5;
            create_board_err_code[0] = 105;
            connect_board_code[0] = 6;
            name = Authorization.name;
            InitializeComponent();
        }



        private void Menu_Load(object sender, EventArgs e)//Создаем меню
        {

        }
        private void loadOfBoard_Click(object sender, EventArgs e)
        {
            client.Send(connect_board_code);
            client.Send(Encoding.UTF8.GetBytes(UserName.Text));
            byte[] answer = new byte[16];
            client.Receive(answer);
            if (answer[0] == server_ok_code[0])
            {
                loadMode = 6;
                Board F2 = new Board(); //переход к чистойs доске
                F2.ShowDialog();

            }
            else
            {
                MessageBox.Show("Не сегодня!");
            }
        }
        private void exitingFromBoard_Click(object sender, EventArgs e) //Кнопка Exit. Работает при клике на нее
        {
            Application.Exit(); //Закрытие приложения
        }
        //запрос на создание доски 5!!!!! query_board_code = 5
        public bool chekingServer(int port)
        {
            byte[] bytes = new byte[1024];

            IPAddress ipAddr = IPAddress.Parse("127.1.1.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);


            // client.Connect(ipEndPoint);

            client.Send(query_board_code);

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
            if (chekingServer(port))
            {
                Board F2 = new Board(); //переход к чистойs доске
                loadMode = 5;
                F2.ShowDialog();
               
                this.Hide();//закрываем Menu
            }
            else
            {
                MessageBox.Show("Не сегодня!");
            }
        }
    }
}
