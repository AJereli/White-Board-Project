using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Timers.Timer;
using System.Timers;

namespace WB_Client
{
    public partial class Board : Form
    {
        public Board()
        {
            InitializeComponent();
        }
       
        
        private void Board_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Left)
            {
                //richTextBox1.AppendText( line_cnt.ToString());
               
                    
                    Graphics g = this.CreateGraphics();

                    Pen pen = new Pen(Color.Black, 2);
                    g.DrawLine(pen, old_x, old_y, e.X, e.Y);
                    line_cnt++;
                    old_x = e.X;
                    old_y = e.Y;
             
            }
           
        }
        private void Board_Load(object sender, EventArgs e)
        {
            
        }

        private void Board_MouseDown(object sender, MouseEventArgs e)
        {
            

        }

        private void Board_MouseUp(object sender, MouseEventArgs e)
        {
            lol = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
