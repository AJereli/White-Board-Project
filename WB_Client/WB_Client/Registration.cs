using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace WB_Client
{
    public partial class Registration : Form
    {
        static public int port = 8000;
        static public int wrong_name_code = 101;
        static public int server_ok_code = 0;
        static public int registration_code = 2;

        public Registration()
        {
            InitializeComponent();
        }

        private void Registration_Load(object sender, EventArgs e)
        {

        }

        private void Login_TextChanged(object sender, EventArgs e)
        {

        }

        private void Email_TextChanged(object sender, EventArgs e)
        {

        }

        private void Password_TextChanged(object sender, EventArgs e)
        {

        }

        public bool registrationServer(int port)
        {
            byte[] bytes = new byte[1024];

            IPAddress ipAddr = IPAddress.Parse("127.1.1.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);
            Socket client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            client.Connect(ipEndPoint);
            int loginLength = Login.Text.Length;
            int emailLenght = Email.Text.Length;
            int passwordLength = Password.Text.Length;
            client.Send(Encoding.UTF8.GetBytes(registration_code.ToString()));
            client.Send(Encoding.UTF8.GetBytes(loginLength.ToString()));
            client.Send(Encoding.UTF8.GetBytes(Login.Text));           
            client.Send(Encoding.UTF8.GetBytes(passwordLength.ToString()));
            client.Send(Encoding.UTF8.GetBytes(Password.Text));
            client.Send(Encoding.UTF8.GetBytes(emailLenght.ToString()));
            client.Send(Encoding.UTF8.GetBytes(Email.Text));

            client.Receive(bytes);

            if (bytes[0] == server_ok_code)
                return true;

            else if (bytes[0] == wrong_name_code)
                return false;
            else
                return false;
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            if (registrationServer(port))
            {
                /*Menu menuShow = new Menu();
                menuShow.Show();
                this.Close();*/
                MessageBox.Show("Все ок");
            }
            else
            {
                MessageBox.Show("Это имя или Em@al занято!");
            }
        }
    }
}
