using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace WB_Client
{
    abstract class Shape : Object // Базовый класс для фигур
    {
        public List<Point> points { get; set; } // Вершины фигуры
        public Point select_point { get; set; } // "Нулевая" точка, исп. в перемещнии фигуры

        public Color penColor { get; set; } // Цвет основной кисти

        public RectangleF[] recF = new RectangleF[5]; // Вспомогательные прямоугольники
        public Pen helpRectPen = new Pen(Color.CadetBlue, 1); // Кисть для вспомогательных прямоугольников
        public Matrix transform { get; set; } // Матрица преобразований


        public bool selected { get; set; } //Выбрана ли фигура

        public int resizing { get; set; } // Переменная для изменения размеров
        public int size = 10; // Размер прямоугольников-тригеров.   
        public int thinkness { get; set; } // Толищна кисти для рисования


        virtual public bool Contains(Point p)
        {
            using (var pen = GetPen())
            using (var path = GetPath())
            {
                return path.IsOutlineVisible(p, pen);
            }
        }

        virtual protected GraphicsPath GetPath() // Без переопределния подходит только под Curve
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
        public void matrixFromStr (string strMatrix)
        {
            string[] parsed = strMatrix.Split('!');
            float[] nm = new float[6]; // Новая матрица
            for (int i = 0; i < parsed.Length; ++i)
                nm[i] = Convert.ToSingle(parsed[i]);
            transform = new Matrix(nm[0], nm[1], nm[2], nm[3], nm[4], nm[5]);
        }
        virtual public void Draw(Graphics g)
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
        protected Pen GetPen() { return new Pen(penColor, thinkness); }
    }   
}
