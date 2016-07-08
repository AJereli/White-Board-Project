using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
namespace WB_Client
{

    public partial class Authorization : Form
    {
        static public int port = 8000;
        static public byte[] wrong_pass_code = new byte[1];
        static public IPAddress ipAddr = IPAddress.Parse("127.1.1.1");
        static public IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, port);
        static public Socket client;
        static public string name;
        static public byte[] server_ok_code = new byte[1];
        static public byte[] authorize_code = new byte[1];

        
        public Authorization()
        {
            wrong_pass_code[0] = 100;
            server_ok_code[0] = 0;
            authorize_code[0] = 1;
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

        public bool authorizationServer(int port)
        {
            byte[] bytes = new byte[1024];
            client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp); 


            client.Connect(ipEndPoint);
            int loginLength = Login.Text.Length;
            int passwordLength = Password.Text.Length;
            client.Send(authorize_code);
           // client.Send(Encoding.UTF8.GetBytes(loginLength.ToString()));
            client.Send(Encoding.UTF8.GetBytes(Login.Text));
            // client.Send(Encoding.UTF8.GetBytes(passwordLength.ToString()));
            Thread.Sleep(123);
            client.Send(Encoding.UTF8.GetBytes(Password.Text));

            client.Receive(bytes);

            if (bytes[0] == server_ok_code[0])
                return true;
            else if (bytes[0] == wrong_pass_code[0])
            {
                client.Close();
                return false;
            }
            else
                return false;
            }
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            if (Login.Text.Length >= 3)
            {
                if (Password.Text.Length >= 5)
                {
            if (authorizationServer(port))
            {
                name = Login.Text;
                Menu menuShow = new Menu();            
                this.Hide();
                menuShow.Show();
                
            }
                    else
                    {
                        MessageBox.Show("Неверное имя или пароль!");
                    }
                }
                else
                {
                    MessageBox.Show("Неверное имя или пароль!");
                }
            }
            else
            {
                MessageBox.Show("Неверное имя или пароль!");
            }
        }

        private void registration_Click(object sender, EventArgs e)
        {
            Registration registrationShow = new Registration();            
            this.Hide();
            registrationShow.Show();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) //закрытие окна при нажатии клавиши "Esc"
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
