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
        static public byte[] wrong_name_code = new byte[1];
        
        static public byte[] server_ok_code = new byte[1];
        static public byte[] registration_code = new byte[1];

        public Registration()
        {
            wrong_name_code[0] = 101;
            server_ok_code[0] = 0;
            registration_code[0] = 2;
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
            client.Send(registration_code);
            client.Send(Encoding.UTF8.GetBytes(loginLength.ToString()));
            client.Send(Encoding.UTF8.GetBytes(Login.Text));           
            client.Send(Encoding.UTF8.GetBytes(passwordLength.ToString()));
            client.Send(Encoding.UTF8.GetBytes(Password.Text));
            client.Send(Encoding.UTF8.GetBytes(emailLenght.ToString()));
            client.Send(Encoding.UTF8.GetBytes(Email.Text));

            client.Receive(bytes);
          

            if (bytes[0] == server_ok_code[0])
                return true;

            else if (bytes[0] == wrong_name_code[0])
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
                Close();
            }
            else
            {
                MessageBox.Show("Это имя или Em@al занято!");
            }
        }
    }
}
