using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WB_Client
{
    public partial class Authorization : Form
    {
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

        private void Enter_Click(object sender, EventArgs e)
        {
            if (Login.Text == "revi4" && Password.Text == "11")
            {
                Menu menuShow = new Menu();
                //menuShow.Show();
                menuShow.ShowDialog();
                
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильное имя или пароль!");
            }
        }
    }
}
