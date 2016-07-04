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
using Timer = System.Timers.Timer;
using System.Runtime.InteropServices;

namespace WB_Client
{

    
    public partial class Board : Form
    {
        bool pressed = false;
        /*
        mode == 0 - режим выбора
        mode == 1 - карандаш
        ..........
        */
        int mode = 0;
       
        List<Curve> curve_list; // Список кривых !!!!! НАДО ПЕРЕДЕЛАТЬ В КОЛЛЕКЦИЮ !!!!!!!
        int idOfShape = -1; // Сюда Номер выбранного объекта в списке
        Graphics m_grp; // main_graphics

        public Board() {
            InitializeComponent();
            curve_list = new List<Curve>() ;
            m_grp = CreateGraphics();
            DoubleBuffered = true;
            m_grp.Clear(Color.White);
            timer1.Start();
            
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
                Point stPoint = curve_list[idOfShape].select_point;
                Point offset = new Point(e.X - stPoint.X, (e.Y - stPoint.Y)); // Смещение
                for (int i = 0; i < curve_list[idOfShape].points.Count; i++)// Смещение каждой точки на offset
                {
                    int x = curve_list[idOfShape].points[i].X; // Текущие координаты
                    int y = curve_list[idOfShape].points[i].Y;
                    curve_list[idOfShape].points[i] = new Point(x + offset.X, y + offset.Y);
                }
                curve_list[idOfShape].select_point = e.Location; // Новая "нулевая" точка
            }
        }
        private void Board_Load(object sender, EventArgs e)
        {
           
        }

        private void Board_MouseDown(object sender, MouseEventArgs e)// События, когда опущенна ЛКМ
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && mode == 1)
            {
                curve_list.Add(new Curve());
                pressed = true;
                curve_list[curve_list.Count - 1].points.Add(e.Location);
            }


            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && mode == 0)
            {
               
                for (int i = 0; i < curve_list.Count; i++) // Проверяем, попали мы в кривую или нет !!!!! Позже тут должны быть проверки на попадания в многоугольники
                {
                    if (curve_list[i].Contains(new Point(e.X, e.Y)))
                    {
                        curve_list[i].select_point = e.Location;
                        idOfShape = i;
                        richTextBox1.AppendText("Selected: " + idOfShape.ToString() + '\n');
                        break;
                    }
                }
               
            }

        }
       
       

        private void Board_MouseUp(object sender, MouseEventArgs e) // ЛКМ поднята
        {
            if (mode == 1) 
                pressed = false;
            else if (mode == 0)
                idOfShape = -1;




        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            m_grp.Clear(Color.White);
            foreach (var a in curve_list)
            {
                a.Draw(m_grp);
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
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
    }
    abstract class Shape // Базовый класс для фигур
    {
        public List<Point> points { get; set; } // Вершины фигуры
        public int thinkness { get; set; } // Толищна кисти для рисования
        public Point offset { get; set; } // Смещение
        public Point select_point { get; set; } // "Нулевая" точка, исп. в перемещнии фигуры
        public Color penColor { get; set; } // Цвет кистм



        protected Pen GetPen()  { return new Pen(penColor, thinkness); }
    }
    class Curve :  Shape    {
    

        public Curve()
        {
            thinkness = 2;
            points = new List<Point>();
            penColor = Color.Black;
        }
        public Curve(List<Point> pnts)
        {
            points = pnts;
            thinkness = 2;
            penColor = Color.Black;
        }
        private GraphicsPath GetPath()
        {
            var path = new GraphicsPath();
            if (points.Count != 0)
                path.AddCurve(points.ToArray());
            return path;
        }

       

        public void Draw(Graphics g)
        {
           
            g.DrawPath(GetPen(), GetPath());
            
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
