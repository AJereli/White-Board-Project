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
        Thread broadCast;
        List<Tuple<int, Shape>> shape_list;// Коллекция фигур

        Graphics m_grp; // main_graphics

        bool pressed = false;
        bool canLoadFromOther = true;
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
        static public string name;

        Point prevLoc;
        Color selectedColor = Color.Black;
        public Board()
        {
            InitializeComponent();

            loadMode = WB_Client.Menu.loadMode;
            WB_Client.Menu.ActiveForm.Close();
            user_name.Text = WB_Client.Menu.name;

            shape_list = new List<Tuple<int, Shape>>();

            m_grp = CreateGraphics();

            DoubleBuffered = true;

            m_grp.SmoothingMode = SmoothingMode.HighQuality;

            timer1.Start();

            if (loadMode == 6)
            {
                loadBoard();
            }
            broadCast = new Thread(delegate () { broadcastToTheWorld(); });
            broadCast.Start();

            new_shape_code[0] = 7;


            prevLoc = new Point();
        }
        private void loadBoard()
        {


            byte[] buff = new byte[256];
            int rec = client.Receive(buff);
            string msg = new string(Encoding.UTF8.GetChars(buff), 0, rec);
            int numbOfShape = Convert.ToInt32(msg);
            for (int i = 0; i < numbOfShape; ++i)
            {
                rec = client.Receive(buff);
                msg = new string(Encoding.UTF8.GetChars(buff), 0, rec);
                string[] parsed = msg.Split('+');
                if (parsed.Length == 5)
                {
                    if (parsed[0] == "1")
                    {
                        //int id = Convert.ToInt32(parsed[2]);
                        shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Curve()));
                        shape_list[shape_list.Count - 1].Item2.penColor = Color.FromArgb(Convert.ToInt32(parsed[2]));
                        shape_list[shape_list.Count - 1].Item2.thinkness = Convert.ToInt32(parsed[3]);
                    }
                    for (int j = 0; j < Convert.ToInt32(parsed[4]); ++j)
                    {
                        rec = client.Receive(buff);
                        msg = new string(Encoding.UTF8.GetChars(buff), 0, rec);
                        parsed = msg.Split('+');
                        if (parsed.Length == 3)
                        {
                            int index = Convert.ToInt32(parsed[2]);
                            Point coords = new Point(Convert.ToInt32(parsed[0]), Convert.ToInt32(parsed[1]));

                            shape_list[index].Item2.points.Add(coords);
                        }
                    }

                }

            }


        }
        private void Board_MouseMove(object sender, MouseEventArgs e)// События, происходящие пока мыши двигается
        {

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && pressed && mode == 1)
            {
                Point pt = new Point(e.X, e.Y);
                //client.Send(pt);

                string msg = pt.X.ToString() + '+' + pt.Y.ToString() + '+' + (shape_list.Count - 1).ToString();
                client.Send(Encoding.UTF8.GetBytes(msg));
                shape_list[shape_list.Count - 1].Item2.points.Add(pt); // Добавляем точки в режиме рисования
            }

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && mode == 0)// Перемещаем в режиме выбора
            {
                if (idOfShape == -1)
                    return;

                if (shape_list[idOfShape].Item2.resizing == 1)
                {
                    if (e.Location.Y > prevLoc.Y)
                        shape_list[idOfShape].Item2.transform.Scale(1, 1.01f);
                    if (e.Location.Y < prevLoc.Y)
                        shape_list[idOfShape].Item2.transform.Scale(1, 0.99f);

                }
                else if (shape_list[idOfShape].Item2.resizing == 2)
                {
                    if (e.Location.X > prevLoc.X)
                        shape_list[idOfShape].Item2.transform.Scale(1.01f, 1);
                    if (e.Location.X < prevLoc.X)
                        shape_list[idOfShape].Item2.transform.Scale(0.99f, 1);
                }
                else
                {
                    Point stPoint = shape_list[idOfShape].Item2.select_point;
                    Point offset = new Point(e.X - stPoint.X, (e.Y - stPoint.Y)); // Смещение
                    shape_list[idOfShape].Item2.transform.Translate(offset.X, offset.Y, MatrixOrder.Append);
                    shape_list[idOfShape].Item2.select_point = e.Location; // Новая "нулевая" точка 

                }
                prevLoc = e.Location;
            }
        }
        private void Board_Load(object sender, EventArgs e)
        {
        }
        private void Board_FormClosing(object sender, FormClosingEventArgs e)
        {
            broadCast.Abort();

            Application.Exit();
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
                        if (shape_list[i].Item2.selected)
                        {
                            if (shape_list[i].Item2.recF[1].Contains(e.Location))
                            {
                                idOfShape = i;
                                shape_list[i].Item2.resizing = 1;
                            }
                            else if (shape_list[i].Item2.recF[2].Contains(e.Location))
                            {
                                idOfShape = i;
                                shape_list[i].Item2.resizing = 2;
                            }
                            else shape_list[i].Item2.selected = false;
                        }
                        if (shape_list[i].Item2.Contains(new Point(e.X, e.Y)) && idOfShape == -1) // Первое попадание в фигуру
                        {
                            shape_list[i].Item2.select_point = e.Location;
                            shape_list[i].Item2.selected = true;
                            idOfShape = i;

                            richTextBox1.AppendText(shape_list[idOfShape].GetType().ToString() + '\n');

                        }
                    }
                    break;
                case 1: // Draw curve
                    typeOfShape[0] = 1;
                    canLoadFromOther = true;
                    shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Curve()));
                    string query = typeOfShape[0].ToString() + '+' + (shape_list.Count - 1).ToString() + '+' + selectedColor.ToArgb().ToString() + '+' + actualThickness.ToString();
                    //richTextBox1.AppendText(query + '\n');
                    client.Send(Encoding.UTF8.GetBytes(query));
                    Thread.Sleep(50);
                    //timerFoServ.Start();
                    debug_lable.Text = shape_list.Count.ToString() + "Before come";
                    richTextBox1.AppendText(shape_list.Count.ToString() + "Before come\n");
                    pressed = true;
                    shape_list[shape_list.Count - 1].Item2.points.Add(e.Location);
                    shape_list[shape_list.Count - 1].Item2.penColor = selectedColor;
                    shape_list[shape_list.Count - 1].Item2.thinkness = actualThickness;
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


            }
            else if (mode == 0)
            {
                if (idOfShape != -1)
                    shape_list[idOfShape].Item2.resizing = -1;
                idOfShape = -1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {


            // m_grp.Clear(Color.White);

            for (int i = 0; i < shape_list.Count; ++i)
            {

                shape_list[i].Item2.Draw(m_grp);
            }


        }


        private void broadcastToTheWorld() /// Broadcast to the world Out of control Broadcast to the world Ready to fold
        {
            while (true)
            {
                Thread.Sleep(10);
                byte[] infoBuff = new byte[256];

                int rec = client.Receive(infoBuff);
                string msg = new string(Encoding.UTF8.GetChars(infoBuff), 0, rec);
                //richTextBox1.AppendText(rec.ToString());
                string[] parsed = msg.Split('+');
                if (parsed.Length == 4)
                {
                    if (parsed[0] == "1")
                    {
                        //int id = Convert.ToInt32(parsed[2]);
                        shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Curve()));
                        shape_list[shape_list.Count - 1].Item2.penColor = Color.FromArgb(Convert.ToInt32(parsed[2]));
                        shape_list[shape_list.Count - 1].Item2.thinkness = Convert.ToInt32(parsed[3]);

                        Invoke((MethodInvoker)delegate ()
                        {
                            debug_lable.Text = shape_list.Count.ToString() + "After come";
                            richTextBox1.AppendText(shape_list.Count.ToString() + "After come\n");
                        });
                    }


                }
                else if (parsed.Length == 3)
                {
                    int index = Convert.ToInt32(parsed[2]);

                    Point coords = new Point(Convert.ToInt32(parsed[0]), Convert.ToInt32(parsed[1]));
                    try
                    {
                        if (canLoadFromOther)
                            shape_list[index].Item2.points.Add(coords);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        richTextBox1.AppendText("WRONG IDX: " + index.ToString() + '\n');
                        for (int i = shape_list.Count; i <= index; ++i)
                        {
                            shape_list.Add(new Tuple<int, Shape>(i, new Curve()));
                        }
                        canLoadFromOther = false;
                    }

                }





            }

        }

        private void timerFoServ_Tick(object sender, EventArgs e)
        {


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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void user_name_Click(object sender, EventArgs e)
        {

        }

        private void debug_lable_Click(object sender, EventArgs e)
        {

        }
    }



}
