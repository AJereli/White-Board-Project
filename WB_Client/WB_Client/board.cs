﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        Mutex muteShapeList;
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
        mode == 3 - прямоугольник
        mode == 4 - эллипс
        ..........
        */
        int mode = 0;
        int idOfShape = -1; // Сюда Номер выбранного объекта в списке
        int actualThickness = 2; // Текузая ширина кисти
        static int loadMode;// Режим загрузки, новая доска или подключение к уже созданной
        static public byte[] new_shape_code = new byte[1];
        static public byte[] typeOfShape = new byte[1];
        static public string name;

        Point prevLoc;
        Color selectedColor = Color.Black; // Цвет, которы рисуют

        Point winCenter;
        Bitmap progressGif; // Анимация прогресса 
        Rectangle progressGifRect;

        public Board()
        {
            InitializeComponent();
            winCenter = new Point(Size.Width / 2, Size.Height / 2); // Текущий центр окна
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            loadMode = WB_Client.Menu.loadMode; // Режим загрузки, новая доска или подключение к уже созданной


            broadCast = new Thread(delegate () { broadcastToTheWorld(); }); // Инициализация потока для принятия изменений других пользователей.
            muteShapeList = new Mutex();
            broadCast.Priority = ThreadPriority.Lowest;
            shape_list = new List<Tuple<int, Shape>>();

            visible_graphics = this.CreateGraphics(); // Основная т.е. видимая графика.
            offScreenBmp = new Bitmap(Width, Height); // Битмап для двойной буферизации .
            graphics_buffer = Graphics.FromImage(offScreenBmp); // Графика для двойной буферизации .
            graphics_buffer.SmoothingMode = SmoothingMode.AntiAlias;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.Text = "Good idea! - " + WB_Client.Menu.name; // Заголовок окна
            this.KeyPreview = true;
            timer1.Start();
            prevLoc = new Point();
        }
        private void boardWaitingStart()
        {
            this.Enabled = false;

            //// Загрузка анимации
            progressGif = WB_Client.Properties.Resources.loadGIF as Bitmap;
            ImageAnimator.Animate(progressGif,
                                        new EventHandler(this.OnFrameChanged));
            progressGifRect = new Rectangle(winCenter, progressGif.Size);
            ////

        }
        private void boardWaitingStop()
        {
            this.Enabled = true;
            progressGifRect.Size = new Size(0, 0);
            progressGif.Dispose();
        }
        private void loadBoard()// Загрузка борда
        {
            boardWaitingStart();
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
                string[] parsed = msg.Split('+'); // Распарсить параметры
                if (parsed.Length == 6)
                {
                    switch (parsed[0])
                    {
                        case "1":
                            shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Curve()));
                            break;
                        case "2":
                            shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Line()));
                            break;
                        case "3":
                            shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new RectangleC()));
                            break;
                        case "4":
                            shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Ellipse()));
                            break;
                        default: break;
                    }
                    int id = shape_list.Count - 1;
                    shape_list[id].Item2.type = Convert.ToInt32(parsed[0]);
                    shape_list[id].Item2.penColor = Color.FromArgb(Convert.ToInt32(parsed[1]));
                    shape_list[id].Item2.thinkness = Convert.ToInt32(parsed[2]);
                    shape_list[id].Item2.matrixFromStr(parsed[5]);
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
            boardWaitingStop();

        }
        private void Board_MouseMove(object sender, MouseEventArgs e)// События, происходящие пока мышь двигается
        {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
                return;
            Thread.Sleep(2);
            if (pressed && mode != 0)
            {
                Point pt = new Point(e.X, e.Y);
                string msg = pt.X.ToString() + '+' + pt.Y.ToString() + '+' + (shape_list.Count - 1).ToString();

                try
                {
                    client.Send(Encoding.UTF8.GetBytes(msg));
                }
                catch (SocketException se)
                {
                    if (se.ErrorCode == 10054)
                    {
                        broadCast.Abort();
                        timer1.Stop();
                        Application.Exit();
                    }
                }

                shape_list[shape_list.Count - 1].Item2.points.Add(pt); // Добавляем точки в режиме рисования
            }
            else if (mode == 0 && idOfShape != -1)// Перемещаем в режиме выбора
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
                    for (int i = 1; i < shape_list[idOfShape].Item2.transform.Elements.Length; ++i)
                        transformQuery += "!" + shape_list[idOfShape].Item2.transform.Elements[i].ToString();
                    client.Send(Encoding.UTF8.GetBytes(transformQuery));

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
            try
            {
                client.Send(Encoding.UTF8.GetBytes("goodbye"));
            }
            catch (SocketException)
            {

            }


            timer1.Stop();
            Application.Exit();
        }
        private void Board_MouseDown(object sender, MouseEventArgs e)// События, когда опущенна ЛКМ
        {
            if ((e.Button & MouseButtons.Left) != MouseButtons.Left)
                return;

            muteShapeList.WaitOne();
            switch (mode)
            {
                case 0: // Выбор и перемещение
                    for (int i = 0; i < shape_list.Count; i++) // Проверяем в какую именно фигуру мы попали
                    {
                        if (shape_list[i].Item2.selected) // Если выделенно 
                        {
                            if (shape_list[i].Item2.recF[1].Contains(e.Location))// Если выбранный прямоугольник отвечает за масштабирование по оси X
                            {
                                idOfShape = i;
                                shape_list[i].Item2.resizing = 1; // Режим масштабирования по X
                            }
                            else if (shape_list[i].Item2.recF[2].Contains(e.Location))// Если выбранный прямоугольник отвечает за масштабирование по оси Y
                            {
                                idOfShape = i;
                                shape_list[i].Item2.resizing = 2;// Режим масштабирования по Y
                            }
                            else shape_list[i].Item2.selected = false; // Выключить выделение
                        }
                        if (shape_list[i].Item2.Contains(new Point(e.X, e.Y)) && idOfShape == -1) // Первое попадание в фигуру
                        {
                            shape_list[i].Item2.select_point = e.Location;
                            shape_list[i].Item2.selected = true; // Включаем выделение фигуры
                            idOfShape = i;
                        }
                    }
                    break;
                case 1: // Draw curve
                    shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Curve()));
                    break;
                case 2: // Draw line
                    shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Line()));
                    break;
                case 3: // Draw RectangleC
                    shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new RectangleC()));
                    break;
                case 4: // Draw ellipse
                    shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Ellipse()));
                    break;
                default: break;
            }
            if (mode != 0) // Отсылаем примитив
            {
                typeOfShape[0] = Convert.ToByte(mode);
                canLoadFromOther = true;
                string query =
                    typeOfShape[0].ToString() +
                    '+' + selectedColor.ToArgb().ToString() +
                    '+' + actualThickness.ToString() +
                    '+' + (shape_list.Count - 1).ToString();

                if (!pressed)
                    client.Send(Encoding.UTF8.GetBytes(query));
                pressed = true;
                int id = shape_list.Count - 1;
                shape_list[id].Item2.type = mode;
                shape_list[id].Item2.points.Add(e.Location);
                shape_list[id].Item2.penColor = selectedColor;
                shape_list[id].Item2.thinkness = actualThickness;
            }
            muteShapeList.ReleaseMutex();
        }

        private void Board_MouseUp(object sender, MouseEventArgs e) // ЛКМ поднята
        {
            if (mode == 1)
                pressed = false;
            else if (mode == 2)
                pressed = false;
            else if (mode == 4)
                pressed = false;
            else if (mode == 3)
                pressed = false;

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
                Thread.Sleep(3);
                int rec = 0;
                try
                {
                    rec = client.Receive(infoBuff); // Принимает сообщение от сервера
                }
                catch (SocketException se)
                {
                    if (se.ErrorCode == 10054)
                    {
                        Invoke((MethodInvoker)delegate ()
                        {
                            boardWaitingStart();
                        });
                        MessageBox.Show("Проблемы с подключением к серверу, приложение будет закрыто.");
                        Invoke((MethodInvoker)delegate ()
                        {
                            timer1.Stop();
                            Application.Exit();
                        });
                        return;

                    }
                }

                string msg = new string(Encoding.UTF8.GetChars(infoBuff), 0, rec);
                string[] parsed = msg.Split('+'); // Парсит сообщение на множество строк, разделение происходит по ключу, знаку +

                if (parsed[0] == "SENDME") // Запрос повторную отправку информации о фигуре
                {
                    int id = Convert.ToInt32(parsed[1]);
                    string query =
                                   shape_list[id].Item2.type.ToString() +
                                   '+' + shape_list[id].Item2.penColor.ToArgb().ToString() +
                                   '+' + shape_list[id].Item2.thinkness.ToString() +
                                   '+' + id.ToString();

                    client.Send(Encoding.UTF8.GetBytes(query));
                }
                if (parsed[0] == "CTRLZ") // Запрос на отмену последнего действия 
                {
                    int id = Convert.ToInt32(parsed[1]);
                    shape_list.Remove(shape_list[id]);
                }
                if (parsed.Length == 4) // Запрос на добавлении новой фигуры
                {

                    if (parsed[0] == "1")
                        shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Curve()));

                    if (parsed[0] == "2")
                        shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Line()));

                    if (parsed[0] == "3")
                        shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new RectangleC()));

                    if (parsed[0] == "4")
                        shape_list.Add(new Tuple<int, Shape>(shape_list.Count, new Ellipse()));
                    int id = shape_list.Count - 1;
                    shape_list[id].Item2.type = Convert.ToInt32(parsed[0]);
                    shape_list[id].Item2.penColor = Color.FromArgb(Convert.ToInt32(parsed[1]));
                    shape_list[id].Item2.thinkness = Convert.ToInt32(parsed[2]);

                }

                else if (parsed.Length == 3)
                {
                    if (parsed[0] == "0" && msg.IndexOf('!') != -1)
                    {
                        int id = Convert.ToInt32(parsed[1]);
                        shape_list[id].Item2.matrixFromStr(parsed[2]); // Формирование матрицы трансформации из строки
                    }
                    else { // Добавление новой точки в фигуру.
                        Point coords = new Point(Convert.ToInt32(parsed[0]), Convert.ToInt32(parsed[1]));
                        int id = Convert.ToInt32(parsed[2]);
                        try
                        {
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

        private void user_name_Click(object sender, EventArgs e)
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

        private void Board_Resize(object sender, EventArgs e)
        {
            visible_graphics.Dispose();
            visible_graphics = this.CreateGraphics();
            winCenter = new Point(Size.Width / 2, Size.Height / 2);
            offScreenBmp.Dispose();
            offScreenBmp = new Bitmap(Size.Width, Size.Height);
            graphics_buffer.Dispose();
            graphics_buffer = Graphics.FromImage(offScreenBmp);
            graphics_buffer.SmoothingMode = SmoothingMode.AntiAlias;

        }

        private void ellipse_Click(object sender, EventArgs e)
        {
            mode = 4;
        }

        private void rect_Click(object sender, EventArgs e)
        {
            mode = 3;
        }
        private void deleteLast()
        {
            if (shape_list.Count > 0)
            {
                shape_list.Remove(shape_list[shape_list.Count - 1]);
                client.Send(Encoding.UTF8.GetBytes("CTRLZ+" + (shape_list.Count).ToString()));
            }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Z && e.Control)
            {
                deleteLast();

            }
        }

        private void undo_Click(object sender, EventArgs e)
        {
            deleteLast();
        }
    }
}