using System;
using System.Text;
using System.Text.RegularExpressions;
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

        static public string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

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
            if (Login.Text.Length >= 3)
            {
                if (Password.Text.Length >= 5)
                {
                    if (Regex.IsMatch(Email.Text, pattern, RegexOptions.IgnoreCase))//Проверка соответствия строки шаблону
                    {
            if (registrationServer(port))
            {
                MessageBox.Show("Успешно загегестрировались!");
                Menu menuShow = new Menu();
                this.Hide();
                menuShow.Show();
                        }
                        else
                        {
                            MessageBox.Show("Это имя занято!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Некорректный email!");
                    }
                }
                else
                {
                    MessageBox.Show("Минимальное количество символов в поле Password 5!");
                }
            }
            else
            {
                MessageBox.Show("Минимальное количество символов в поле Login 3!");
            }
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
