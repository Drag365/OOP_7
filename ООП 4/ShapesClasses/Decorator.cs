using Laba4OOP.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ООП_4.ShapesClasses
{
    class Decorator : Shape
    {
        protected Shape shape;
        public Decorator(Shape shape)
        {
            this.shape = shape;
        }
        public override void Draw() 
        {
            shape.Draw();
            Pen pen = new Pen(Color.Black);
            pen.Width = 2;
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawRectangle(pen, p.X - (R + 8) / 2, p.Y - (R + 8) / 2, R + 8, R + 8);
        }
    }
}
