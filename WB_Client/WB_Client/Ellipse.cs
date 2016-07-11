using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace WB_Client
{
    class Ellipse : Shape
    {


        public Ellipse()
        {
            selected = false;
            resizing = -1;
            thinkness = 2;
            transform = new Matrix();
            points = new List<Point>();
            penColor = Color.Black;
            List<Shape> arl = new List<Shape>();


        }
        public Ellipse(List<Point> pnts)
        {
            selected = false;
            points = pnts;
            thinkness = 2;
            penColor = Color.Black;
        }

        override protected GraphicsPath GetPath()
        {
            var path = new GraphicsPath();

            try
            {
                if (points.Count != 0)
                {
                    path.AddEllipse(points[0].X, points[0].Y, points[points.Count - 1].X, points[points.Count - 1].Y);
                }

                path.Transform(transform);

            }
            catch (ArgumentException)
            {
                path.Reset();
            }
            return path;
        }
    }
}

