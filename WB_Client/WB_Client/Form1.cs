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

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void LoginTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        public bool result()
        {
            if (LoginTextBox.Text == "revi4" && PasswordTextBox.Text == "112595")
                return true;
            else
                return false;
        }

        private void EnterBatton_Click(object sender, EventArgs e)
        {
            if (result())
                Box.Text = "1";
            else
                Box.Text = "0";
        }
    }
}
