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

        int mode = 0;
        int idOfShape = -1;
        List<Curve> curve_list;
        Graphics m_grp;
        List<Point> tmp_points;


        public Board() {
            InitializeComponent();
            curve_list = new List<Curve>() ;
            m_grp = CreateGraphics();
            this.DoubleBuffered = true;
            m_grp.Clear(Color.White);
            timer1.Start();
            
        }
       
        
        private void Board_MouseMove(object sender, MouseEventArgs e)
        {

          
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && pressed && mode == 1)
            {
                Point pt = new Point(e.X, e.Y);
                tmp_points.Add(pt);
                curve_list[curve_list.Count - 1].points = tmp_points;
            }

            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && mode == 0)
            {
                
                if (idOfShape == -1)
                    return;
                Point stPoint = curve_list[idOfShape].select_point;
                curve_list[idOfShape].offset = new Point (e.X - stPoint.X, e.Y - stPoint.Y);
                richTextBox1.AppendText(new Point(e.X - stPoint.X, e.Y - stPoint.Y).ToString() + '\n');
                for (int i = 0; i < curve_list[idOfShape].points.Capacity; i++)
                {

                    curve_list[idOfShape].points[i].Offset(curve_list[idOfShape].offset);
                }
            }
        }
        private void Board_Load(object sender, EventArgs e)
        {
           
        }

        private void Board_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && mode == 1)
            {
                curve_list.Add(new Curve());
                pressed = true;
                tmp_points = new List<Point>();
                tmp_points.Add(e.Location);

            }


            if ((e.Button & MouseButtons.Left) == MouseButtons.Left && mode == 0)
            {
               
                for (int i = 0; i < curve_list.Count; i++)
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
       
       

        private void Board_MouseUp(object sender, MouseEventArgs e)
        {
            if (mode == 1) {
                pressed = false;
                
                richTextBox1.AppendText(tmp_points.Count.ToString() + " точек\n");
            }else if (mode == 0)
                idOfShape = -1;




        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
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
    abstract class Shape
    {
        public List<Point> points { get; set; }
        public int thinkness { get; set; }
        public Point offset { get; set; }
        public Point select_point { get; set; }
        public Color penColor { get; set; }
        protected Pen GetPen()
        {
            return new Pen(penColor, thinkness);
        }
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
           
            using (var pen = GetPen())
            using (var path = GetPath())
            {
                g.DrawPath(pen, path);
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
