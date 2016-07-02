using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace WB_Client
{

    public partial class Authorization : Form
    {
        static private int port = 8000;
        static private int wrong_pass_code = -2;
        static private int server_ok_code = 0;

        public Authorization()
        {
            InitializeComponent();
        }

        private void Authorization_Load(object sender, EventArgs e)
        {

        }

        private void Login_TextChanged(object sender, EventArgs e)
        {

        }

        private void Password_TextChanged(object sender, EventArgs e)
        {

        }

        public bool postingServer(int port)
        {
            byte[] bytes = new byte[1024];

            IPAddress ipAddr = IPAddress.Parse("127.1.1.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);
            Socket client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            client.Connect(ipEndPoint);

            int loginLength = Login.Text.Length ;
            int passwordLength = Password.Text.Length ;
           
            client.Send(Encoding.UTF8.GetBytes(loginLength.ToString()));
            client.Send(Encoding.UTF8.GetBytes(Login.Text));
            client.Send(Encoding.UTF8.GetBytes(passwordLength.ToString()));
            client.Send(Encoding.UTF8.GetBytes(Password.Text));

            client.Receive(bytes);


           
            if (bytes[0] == server_ok_code)
                return true;
            else if (bytes[0] == wrong_pass_code)
                return false;
            else
                return false;
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            if (postingServer(port))
            {
                Menu menuShow = new Menu();
                menuShow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильное имя или пароль!");
            }
        }
    }
}
