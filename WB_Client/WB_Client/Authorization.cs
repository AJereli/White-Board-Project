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

            IPAddress ipAddr = IPAddress.Parse("192.168.1.101");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);
            Socket client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            client.Connect(ipEndPoint);

            int loginLength = Login.Text.Length;
            string loginLeght1 = Convert.ToString(loginLength);
            char[] loginLeght2 = new char[loginLength];
            byte[] loginLengthUTF8 = Encoding.UTF8.GetBytes(loginLeght2);
            byte[] loginUTF8 = Encoding.UTF8.GetBytes(Login.Text);

            int passwordLength = Password.Text.Length;
            string passwordLeght1 = Convert.ToString(passwordLength);
            char[] passwordLeght2 = new char[passwordLength];
            byte[] passwordLengthUTF8 = Encoding.UTF8.GetBytes(passwordLeght2);
            byte[] passwordUTF8 = Encoding.UTF8.GetBytes(Password.Text);

            client.Send(loginLengthUTF8);
            client.Send(loginUTF8);
            client.Send(passwordLengthUTF8);
            client.Send(passwordUTF8);

            client.Receive(bytes);

            client.Shutdown(SocketShutdown.Both);
            client.Close();

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
