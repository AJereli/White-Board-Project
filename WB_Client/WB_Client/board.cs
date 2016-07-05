using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Forms;
using System.Collections;
using System.Net.Sockets;

namespace WB_Client
{

    public partial class Board : Form
    {
        static public Socket client = WB_Client.Menu.client;
        
        List<Shape> shape_list; // Коллекция фигур
        Graphics m_grp; // main_graphics

        bool pressed = false;

        /*
        mode == 0 - режим выбора
        mode == 1 - карандаш
        ..........
        */
        int mode = 0;
        int idOfShape = -1; // Сюда Номер выбранного объекта в списке
        int actualThickness = 2; // Текузая ширина кисти
        static int loadMode;
        static public byte[] new_shape_code = new byte[1];
        static public byte[] typeOfShape = new byte[1];


        Point prevLoc;
        Color selectedColor = Color.Black;
        public Board()
        {
            InitializeComponent();
            shape_list = new List<Shape>();
            m_grp = CreateGraphics();
            DoubleBuffered = true;
            m_grp.Clear(Color.White);
            m_grp.SmoothingMode = SmoothingMode.AntiAlias;
            timer1.Start();
            new_shape_code[0] = 7;
            //WB_Client.Menu.ActiveForm.Close();
            
            prevLoc = new Point();
        }

        private void Board_MouseMove(object sender, MouseEventArgs e)// События, происходящие пока мыши двигается
        {

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && pressed && mode == 1)
            {
                Point pt = new Point(e.X, e.Y);
                //client.Send(pt);
                BitConverter.GetBytes(pt.X);
                string msg = pt.X.ToString() + '+' + pt.Y.ToString();
                //client.Send(Encoding.UTF8.GetBytes(msg));
                shape_list[shape_list.Count - 1].points.Add(pt); // Добавляем точки в режиме рисования
            }

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && mode == 0)// Перемещаем в режиме выбора
            {
                if (idOfShape == -1)
                    return;
                if (shape_list[idOfShape].resizing == 1 )
                {
                    if (e.Location.Y > prevLoc.Y)
                        shape_list[idOfShape].transform.Scale(1, 1.01f);
                    if (e.Location.Y < prevLoc.Y)
                        shape_list[idOfShape].transform.Scale(1, 0.99f);

                }
                else if (shape_list[idOfShape].resizing == 2)
                {
                    if (e.Location.X > prevLoc.X)
                        shape_list[idOfShape].transform.Scale(1.01f, 1);
                    if (e.Location.X < prevLoc.X)
                        shape_list[idOfShape].transform.Scale(0.99f, 1);
                }
                else
                {
                    Point stPoint = shape_list[idOfShape].select_point;
                    Point offset = new Point(e.X - stPoint.X, (e.Y - stPoint.Y)); // Смещение
                    shape_list[idOfShape].transform.Translate(offset.X, offset.Y, MatrixOrder.Append);
                    shape_list[idOfShape].select_point = e.Location; // Новая "нулевая" точка 
                    
                }
                prevLoc = e.Location;
            }
        }
        private void Board_Load(object sender, EventArgs e)
        {

        }
        private void Board_MouseDown(object sender, MouseEventArgs e)// События, когда опущенна ЛКМ
        {

            if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
                return;

            switch (mode)
            {
                case 0: // Selecting and move
                    for (int i = 0; i < shape_list.Count; i++) // Проверяем, попали мы в кривую или нет #### Позже тут должны быть проверки на попадания в многоугольники ####
                    {
                        if (shape_list[i].selected)
                        {
                            if (shape_list[i].recF[1].Contains(e.Location))
                            {
                                idOfShape = i;
                                shape_list[i].resizing = 1;
                            }else if (shape_list[i].recF[2].Contains(e.Location))
                            {
                                idOfShape = i;
                                shape_list[i].resizing = 2;
                            }
                            else shape_list[i].selected = false;
                        }
                        if (shape_list[i].Contains(new Point(e.X, e.Y)) && idOfShape == -1) // Первое попадание в фигуру
                        {
                            shape_list[i].select_point = e.Location;
                            shape_list[i].selected = true;
                            idOfShape = i;
                            
                            richTextBox1.AppendText(shape_list[idOfShape].GetType().ToString() + '\n');

                        }
                    }
                    break;
                case 1: // Draw curve
                    typeOfShape[0] = 1;
                    shape_list.Add(new Curve());
                    string query = typeOfShape[0].ToString() + '+' + shape_list.Count.ToString() + '+' + selectedColor.ToArgb().ToString() + '+' + actualThickness.ToString();
                    richTextBox1.AppendText(query + '\n');
                    client.Send(Encoding.UTF8.GetBytes(query));
                    //timerFoServ.Start();
                    
                    pressed = true;
                    shape_list[shape_list.Count - 1].points.Add(e.Location);
                    shape_list[shape_list.Count - 1].penColor = selectedColor;
                    shape_list[shape_list.Count - 1].thinkness = actualThickness;
                    break;

                default: break;
            }




        }

        private void Board_MouseUp(object sender, MouseEventArgs e) // ЛКМ поднята
        {
            if (mode == 1)
            {
               // timerFoServ.Stop();
                pressed = false;
                client.Send(Encoding.UTF8.GetBytes("END"));
              
            }
            else if (mode == 0)
            {
                if (idOfShape != -1)
                    shape_list[idOfShape].resizing = -1;
                idOfShape = -1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            m_grp.Clear(Color.White);

            foreach (var a in shape_list)
            {
                a.Draw(m_grp);
            }

        }
        private void timerFoServ_Tick(object sender, EventArgs e)
        {
            byte[] infoBuff = new byte[64];
            //int rec = client.Receive(infoBuff);
            
            
        }


        private void Select_Click(object sender, EventArgs e)
        {
            mode = 0;
        }

        private void Pen_Click(object sender, EventArgs e)
        {
            mode = 1;
        }

        private void anyColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            selectedColor = colorDialog1.Color;
        }

        private void yellow_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(WB_Client.Properties.Resources.yellow);
            selectedColor = bmp.GetPixel(15, 15);
        }

        private void blue_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(WB_Client.Properties.Resources.blue);
            selectedColor = bmp.GetPixel(15, 15);
        }

        private void green_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(WB_Client.Properties.Resources.green);
            selectedColor = bmp.GetPixel(15, 15);
        }

        private void pink_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(WB_Client.Properties.Resources.pink);
            selectedColor = bmp.GetPixel(15, 15);
        }

        private void red_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(WB_Client.Properties.Resources.red);
            selectedColor = bmp.GetPixel(15, 15);
        }

        private void black_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(WB_Client.Properties.Resources.black);
            selectedColor = bmp.GetPixel(15, 15);
        }

        private void select_thickness_Scroll(object sender, EventArgs e)
        {
            actualThickness = select_thickness.Value;
        }

       
    }

   

}
