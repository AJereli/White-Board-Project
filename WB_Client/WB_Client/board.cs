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
using System.Net;
using System.Net.Sockets;
namespace WB_Client
{

    public partial class Board : Form
    {
        Socket client = Authorization.client;

        List<Curve> curve_list; // Список кривых !!!!! НАДО ПЕРЕДЕЛАТЬ В КОЛЛЕКЦИЮ !!!!!!!
        Graphics m_grp; // main_graphics

        bool pressed = false;

        /*
        mode == 0 - режим выбора
        mode == 1 - карандаш
        ..........
        */
        int mode = 0;
        int idOfShape = -1; // Сюда Номер выбранного объекта в списке
        int actualThickness = 2;

        Point prevLoc;
        Color selectedColor = Color.Black;
        public Board()
        {
            InitializeComponent();
            curve_list = new List<Curve>();
            m_grp = CreateGraphics();
            DoubleBuffered = true;
            m_grp.Clear(Color.White);
            m_grp.SmoothingMode = SmoothingMode.AntiAlias;
            timer1.Start();
            prevLoc = new Point();
        }

        private void Board_MouseMove(object sender, MouseEventArgs e)// События, происходящие пока мыши двигается
        {

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && pressed && mode == 1)
            {
                Point pt = new Point(e.X, e.Y);
                curve_list[curve_list.Count - 1].points.Add(pt); // Добавляем точки в режиме рисования
            }

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && mode == 0)// Перемещаем в режиме выбора
            {
                if (idOfShape == -1)
                    return;
                if (curve_list[idOfShape].resizing == 1 )
                {
                    if (e.Location.Y > prevLoc.Y)
                        curve_list[idOfShape].transform.Scale(1, 1.01f);
                    if (e.Location.Y < prevLoc.Y)
                        curve_list[idOfShape].transform.Scale(1, 0.99f);

                }
                else if (curve_list[idOfShape].resizing == 2)
                {
                    if (e.Location.X > prevLoc.X)
                        curve_list[idOfShape].transform.Scale(1.01f, 1);
                    if (e.Location.X < prevLoc.X)
                        curve_list[idOfShape].transform.Scale(0.99f, 1);
                }
                else
                {
                    Point stPoint = curve_list[idOfShape].select_point;
                    Point offset = new Point(e.X - stPoint.X, (e.Y - stPoint.Y)); // Смещение
                    curve_list[idOfShape].transform.Translate(offset.X, offset.Y, MatrixOrder.Append);
                    curve_list[idOfShape].select_point = e.Location; // Новая "нулевая" точка 

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
                    for (int i = 0; i < curve_list.Count; i++) // Проверяем, попали мы в кривую или нет #### Позже тут должны быть проверки на попадания в многоугольники ####
                    {
                        if (curve_list[i].selected)
                        {
                            if (curve_list[i].recF[1].Contains(e.Location))
                            {
                                idOfShape = i;
                                curve_list[i].resizing = 1;
                            }else if (curve_list[i].recF[2].Contains(e.Location))
                            {
                                idOfShape = i;
                                curve_list[i].resizing = 2;
                            }
                            else curve_list[i].selected = false;
                        }
                        if (curve_list[i].Contains(new Point(e.X, e.Y)) && idOfShape == -1) // Первое попадание в фигуру
                        {
                            curve_list[i].select_point = e.Location;
                            curve_list[i].selected = true;
                            idOfShape = i;
                            richTextBox1.AppendText("Selected: " + idOfShape.ToString() + '\n');

                        }
                    }
                    break;
                case 1: // Draw curve
                    curve_list.Add(new Curve());
                    pressed = true;
                    curve_list[curve_list.Count - 1].points.Add(e.Location);
                    curve_list[curve_list.Count - 1].penColor = selectedColor;
                    curve_list[curve_list.Count - 1].thinkness = actualThickness;
                    break;

                default: break;
            }




        }

        private void Board_MouseUp(object sender, MouseEventArgs e) // ЛКМ поднята
        {
            if (mode == 1)
                pressed = false;
            else if (mode == 0)
            {
                if (idOfShape != -1)
                    curve_list[idOfShape].resizing = -1;
                idOfShape = -1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            m_grp.Clear(Color.White);

            foreach (var a in curve_list)
            {
                a.Draw(m_grp);
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
    }

    abstract class Shape // Базовый класс для фигур
    {
        public List<Point> points { get; set; } // Вершины фигуры

        public Point offset { get; set; } // Смещение
        public Point select_point { get; set; } // "Нулевая" точка, исп. в перемещнии фигуры

        public Color penColor { get; set; } // Цвет основной кисти

        public RectangleF[] recF = new RectangleF[5]; // Вспомогательные прямоугольники
        public Pen helpRectPen = new Pen(Color.CadetBlue, 1); // Кисть для вспомогательных прямоугольников
        public Matrix transform { get; set; } // Матрица преобразований


        public bool selected { get; set; }
        public int resizing { get; set; }
        public int size = 10; // Размер прямоугольников-тригеров.   
        public int thinkness { get; set; } // Толищна кисти для рисования


        protected Pen GetPen() { return new Pen(penColor, thinkness); }
    }
    class Curve : Shape
    {


        public Curve()
        {
            selected = false;
            resizing = -1;
            thinkness = 2;
            transform = new Matrix();
            points = new List<Point>();
            penColor = Color.Black;
        }
        public Curve(List<Point> pnts)
        {
            selected = false;
            points = pnts;
            thinkness = 2;
            penColor = Color.Black;
        }

        private GraphicsPath GetPath()
        {
            var path = new GraphicsPath();

           
            try
            {
                if (points.Count != 0)
                    path.AddCurve(points.ToArray());
                path.Transform(transform);
            }
            catch (ArgumentException)
            {
                path.Reset();
            }
            return path;
        }



        public void Draw(Graphics g)
        {
            var path = GetPath();
            g.DrawPath(GetPen(), path);
            if (selected)
            {
                recF[0] = path.GetBounds(); // Вычисляем координаты вспомогательных квадратиков
                recF[1] = new RectangleF(recF[0].Location.X + recF[0].Width / 2, recF[0].Location.Y + recF[0].Height, size, size);
                recF[2] = new RectangleF(recF[0].Location.X + recF[0].Width, recF[0].Location.Y + recF[0].Height / 2, size, size);
                g.DrawRectangles(helpRectPen, recF);
            }
        }

        public bool Contains(Point p)
        {
            using (var pen = GetPen())
            using (var path = GetPath())
            {
                return path.IsOutlineVisible(p, pen);
            }
        }
    }

}
