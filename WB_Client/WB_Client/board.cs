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

        Graphics graphics_buffer; // main_graphics
        Bitmap offScreenBmp;
        Graphics visible_graphics;

        bool pressed = false;
        bool canLoadFromOther = false;
        /*
        mode == 0 - режим выбора
        mode == 1 - карандаш
        mode == 2 - линия
        ..........
        */
        int mode = 0;
        int idOfShape = -1; // Сюда Номер выбранного объекта в списке
        int actualThickness = 2; // Текузая ширина кисти
        static int loadMode;
        static public byte[] new_shape_code = new byte[1];
        static public byte[] typeOfShape = new byte[1];
        static public string name;
        static public Point mouseF;
        static public Point mouseS;
        public bool flagDown = false;

        Point prevLoc;
        Color selectedColor = Color.Black;

        Point winCenter;
        Bitmap progressGif;
        Rectangle progressGifRect;
        public Board()
        {
            InitializeComponent();
            winCenter = new Point(Size.Width / 2, Size.Height / 2);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            
            loadMode = WB_Client.Menu.loadMode;
            // WB_Client.Menu.ActiveForm.Close();
            user_name.Text = WB_Client.Menu.name;
            broadCast = new Thread(delegate () { broadcastToTheWorld(); });
            shape_list = new List<Tuple<int, Shape>>();
            mainPict.BackColor = Color.Transparent;
            //mainPict.Parent = Select;
            //m_grp = mainPict.CreateGraphics();
            visible_graphics = this.CreateGraphics();
            offScreenBmp = new Bitmap(Width, Height);
            graphics_buffer = Graphics.FromImage(offScreenBmp);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //m_grp.SmoothingMode = SmoothingMode.HighQuality;

            timer1.Start();



            prevLoc = new Point();
        }
        private void loadBoard()// Загрузка борда
        {
            this.Enabled = false;
            
            //// Загрузка анимации
            progressGif = WB_Client.Properties.Resources.loadGIF as Bitmap;
            ImageAnimator.Animate(progressGif,
                                        new EventHandler(this.OnFrameChanged));
            progressGifRect = new Rectangle(winCenter, progressGif.Size);
            ////
            byte[] ans = new byte[64]; // Буфер для подтвержения
            ans = Encoding.UTF8.GetBytes("PLS GIVE SOME PART BRO"); // Команда для подтверждения
            byte[] buff = new byte[16384 * 2];// Основной буфер для информации и фигуре
            int rec = client.Receive(buff); // Принимает количество фигур (если ноль, то принимает END)
            string msg = new string(Encoding.UTF8.GetChars(buff), 0, rec);
            if (msg == "END\0")
            {
                this.Enabled = true;
                progressGif.Dispose();
                progressGifRect.Size = new Size(0, 0);
                return;
            }

            int numbOfShape = Convert.ToInt32(msg);
            for (int i = 0; i < numbOfShape; ++i)
            {
                client.Send(ans);
                rec = client.Receive(buff); // Принимает тип фигуры и её параметры

                msg = new string(Encoding.UTF8.GetChars(buff), 0, rec);
                richTextBox1.AppendText(msg + "\n");
                string[] parsed = msg.Split('+'); // Распарсить параметры
                if (parsed.Length == 6)
                {
                    switch (parsed[0])
                    {
                        case "1":
                            shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Curve()));
                            shape_list[shape_list.Count - 1].Item2.penColor = Color.FromArgb(Convert.ToInt32(parsed[1]));
                            shape_list[shape_list.Count - 1].Item2.thinkness = Convert.ToInt32(parsed[2]);
                            shape_list[shape_list.Count - 1].Item2.matrixFromStr(parsed[5]);
                            break;
                        case "2":
                            shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Line()));
                            shape_list[shape_list.Count - 1].Item2.penColor = Color.FromArgb(Convert.ToInt32(parsed[1]));
                            shape_list[shape_list.Count - 1].Item2.thinkness = Convert.ToInt32(parsed[2]);
                            shape_list[shape_list.Count - 1].Item2.matrixFromStr(parsed[5]);
                            break;
                        default: break;
                    }
                }

                rec = client.Receive(buff);// Принимает точки фигуры
                msg = new string(Encoding.UTF8.GetChars(buff), 0, rec);
                parsed = msg.Split('-');// Парсит строку на отдельные пары вида X+Y
                for (int j = 0; j < parsed.Length; ++j)
                {
                    ///// ВИЗИУАЛИЗАЦИЯ ПРОГРЕССА
                    ImageAnimator.UpdateFrames(progressGif);
                    graphics_buffer.DrawImage(progressGif, winCenter);
                    //// -------------------

                    string[] lonelyPart = parsed[j].Split('+'); // Отдельные кординаты X Y
                    try
                    {
                        Point coords = new Point(Convert.ToInt32(lonelyPart[0]), Convert.ToInt32(lonelyPart[1]));
                        shape_list[shape_list.Count - 1].Item2.points.Add(coords);
                    }
                    catch (FormatException)
                    {
                        continue;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        continue;
                    }

                }
            }
            this.Enabled = true;
            progressGifRect.Size = new Size(0, 0);
            progressGif.Dispose();

        }
        private void Board_MouseMove(object sender, MouseEventArgs e)// События, происходящие пока мыши двигается
        {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
                return;

            if (flagDown)
            {
                mouseS.X = e.X;
                mouseS.Y = e.Y;
            }
            
            if (pressed && (mode == 1 || mode == 2))
            {
                Point pt = new Point(e.X, e.Y);
                string msg = pt.X.ToString() + '+' + pt.Y.ToString() + '+' + (shape_list.Count - 1).ToString() ;
                Thread.Sleep(1);
                client.Send(Encoding.UTF8.GetBytes(msg));
                shape_list[shape_list.Count - 1].Item2.points.Add(pt); // Добавляем точки в режиме рисования
            }else if (pressed && mode == 2)
            {
                Point pt = new Point(e.X, e.Y);
                string msg = pt.X.ToString() + '+' + pt.Y.ToString() + (shape_list.Count - 1).ToString();
                client.Send(Encoding.UTF8.GetBytes(msg));
                shape_list[shape_list.Count - 1].Item2.points.Add(pt); // Добавляем точки в режиме рисования
            }else if (mode == 0 && idOfShape != -1)// Перемещаем в режиме выбора
            {
                
                bool isChanged = false;
                
               
                if (shape_list[idOfShape].Item2.resizing == 1) // Масштабирование по Y
                {
                    if (e.Location.Y > prevLoc.Y)
                        shape_list[idOfShape].Item2.transform.Scale(1, 1.01f);
                    if (e.Location.Y < prevLoc.Y)
                        shape_list[idOfShape].Item2.transform.Scale(1, 0.99f);
                    isChanged = true;
                }
                
                else if (shape_list[idOfShape].Item2.resizing == 2)// Масштабирование по X
                {
                    if (e.Location.X > prevLoc.X)
                        shape_list[idOfShape].Item2.transform.Scale(1.01f, 1);
                    if (e.Location.X < prevLoc.X)
                        shape_list[idOfShape].Item2.transform.Scale(0.99f, 1);
                    isChanged = true;
                }
                else
                {
                    Point stPoint = shape_list[idOfShape].Item2.select_point;
                    Point offset = new Point(e.X - stPoint.X, (e.Y - stPoint.Y)); // Смещение
                    shape_list[idOfShape].Item2.transform.Translate(offset.X, offset.Y, MatrixOrder.Append);
                    shape_list[idOfShape].Item2.select_point = e.Location; // Новая "нулевая" точка 
                    isChanged = true;
                }
                
                if (isChanged)
                {
                    string transformQuery = "0" + "+" + idOfShape.ToString() + "+";
                    transformQuery += shape_list[idOfShape].Item2.transform.Elements[0].ToString();
                    richTextBox1.AppendText("matr size: " + shape_list[idOfShape].Item2.transform.Elements.Length + "\n");
                    for (int i = 1; i < shape_list[idOfShape].Item2.transform.Elements.Length; ++i)
                        transformQuery += "!" + shape_list[idOfShape].Item2.transform.Elements[i].ToString();
                    client.Send(Encoding.UTF8.GetBytes(transformQuery));
                    richTextBox1.AppendText(transformQuery + "\n");
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
            timer1.Stop();
            Application.Exit();
        }
        private void Board_MouseDown(object sender, MouseEventArgs e)// События, когда опущенна ЛКМ
        {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
                return;
            

            if (!flagDown)
            {
                mouseF.X = e.X;
                mouseF.Y = e.Y;
                mouseS.X = e.X;
                mouseS.Y = e.Y;
                
                flagDown = true;
            }

            switch (mode)
            {
                case 0: // Selecting and move
                    for (int i = 0; i < shape_list.Count; i++) // Проверяем попадание в фигуру (контур)
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
                    string query =
                        typeOfShape[0].ToString() +
                        '+' + selectedColor.ToArgb().ToString() +
                        '+' + actualThickness.ToString() +
                        '+' + (shape_list.Count - 1).ToString();

                    if (!pressed)
                        client.Send(Encoding.UTF8.GetBytes(query));


                    debug_lable.Text = shape_list.Count.ToString() + "Before come";
                    richTextBox1.AppendText(shape_list.Count.ToString() + "Before come\n");
                    pressed = true;
                    shape_list[shape_list.Count - 1].Item2.points.Add(e.Location);
                    shape_list[shape_list.Count - 1].Item2.penColor = selectedColor;
                    shape_list[shape_list.Count - 1].Item2.thinkness = actualThickness;
                    break;
                case 2: // Draw line
                    typeOfShape[0] = 2;
                    canLoadFromOther = true;
                    shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Line()));
                    string query2 = typeOfShape[0].ToString() + 
                        '+' + selectedColor.ToArgb().ToString() + 
                        '+' + actualThickness.ToString() +
                        '+' + (shape_list.Count - 1).ToString();

                    client.Send(Encoding.UTF8.GetBytes(query2));
                   
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
            if (flagDown)
            {
                mouseS.X = e.X;
                mouseS.Y = e.Y;
                flagDown = false;
            }

            if (mode == 1)
            {
                // timerFoServ.Stop();
                pressed = false;

            }
            else if (mode == 2)
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
        private void OnFrameChanged(object sender, EventArgs e)
        {
            Invalidate(progressGifRect);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            graphics_buffer.Clear(Color.White);
            for (int i = 0; i < shape_list.Count; ++i)
                shape_list[i].Item2.Draw(graphics_buffer);
            visible_graphics.DrawImage(offScreenBmp, 0, 0);
        }
        private void broadcastToTheWorld() /// Broadcast to the world Out of control Broadcast to the world Ready to fold
        {
            byte[] infoBuff = new byte[256];
            while (true)
            {
                Thread.Sleep(12);
               
                int rec = client.Receive(infoBuff);
                string msg = new string(Encoding.UTF8.GetChars(infoBuff), 0, rec);
                string[] parsed = msg.Split('+');
                if (parsed.Length == 4)
                {
                    
                    if (parsed[0] == "1")
                    {
                        shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Curve()));
                        shape_list[shape_list.Count - 1].Item2.penColor = Color.FromArgb(Convert.ToInt32(parsed[1]));
                        shape_list[shape_list.Count - 1].Item2.thinkness = Convert.ToInt32(parsed[2]);
                    }
                    if (parsed[0] == "2")
                    {
                        shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Line()));
                        shape_list[shape_list.Count - 1].Item2.penColor = Color.FromArgb(Convert.ToInt32(parsed[1]));
                        shape_list[shape_list.Count - 1].Item2.thinkness = Convert.ToInt32(parsed[2]);
                    }
                        

                   
                    Invoke((MethodInvoker)delegate ()
                    {
                        debug_lable.Text = shape_list.Count.ToString() + "After come";
                        richTextBox1.AppendText(shape_list.Count.ToString() + "After come\n");
                    });
                }

                else if (parsed.Length == 3  )
                {
                    if (parsed[0] == "0" && msg.IndexOf('!') != -1)
                    {
                        int id2 = Convert.ToInt32(parsed[1]);
                        shape_list[id2].Item2.matrixFromStr(parsed[2]);
                    }
                    else {
                        Point coords = new Point(Convert.ToInt32(parsed[0]), Convert.ToInt32(parsed[1]));
                        int id = Convert.ToInt32(parsed[2]);
                        try {
                            shape_list[id].Item2.points.Add(coords);
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            continue;
                        }
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



        private void Board_Shown(object sender, EventArgs e)
        {
            if (loadMode == 6)
                loadBoard();

            broadCast.Start();
            new_shape_code[0] = 7;
        }



        private void line_Click(object sender, EventArgs e)
        {
            mode = 2;
        }

        private void Board_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
