using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace WB_Client
{
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
            List<Shape> arl = new List<Shape>();
           

        }
        public Curve(List<Point> pnts)
        {
            selected = false;
            points = pnts;
            thinkness = 2;
            penColor = Color.Black;
        }


    }
}
