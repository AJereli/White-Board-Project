using System;
using System.Windows.Forms;

namespace WB_Client
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }
        private string loginForm;
        private string passwordForm;
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void LoginTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void EnterBatton_Click(object sender, EventArgs e)
        {
            loginForm = LoginTextBox.Text;
            passwordForm = PasswordTextBox.Text;
            User MainUser = new User();
            string login = MainUser.getLogin();
            string password = MainUser.getPassword();
            if (loginForm == login && passwordForm == password)
            {
                MessageBox.Show("Заебись!");
            }
            else
            {
                MessageBox.Show("Пароль или логин введены неправильно!");
            }
        }
    }
}
