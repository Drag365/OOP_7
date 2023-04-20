using Laba4OOP.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ООП_4.ShapesClasses
{
    public class Decorator : Shape
    {
        public Shape shape;
        public Decorator(Shape shape)
        {
            this.shape = shape;

        }
        override public void Draw()
        {
            shape.Draw();

            Pen pen = new Pen(Color.Black);
            pen.Width = 2;
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            shape.g.DrawRectangle(pen, shape.getPosition().X - (shape.getRadius() + 8) / 2, shape.getPosition().Y - (shape.getRadius() + 8) / 2, shape.getRadius() + 8, shape.getRadius() + 8);
        }

        override public bool checkPointPosition(Point point)
        {
            return shape.checkPointPosition(point);
        }

        override public Point getPosition()
        {
            return shape.getPosition();
        }

        override public int getRadius()
        {
            return shape.getRadius();
        }

        override public void move(int x, int y)
        {
            shape.move(x, y);
        }

        override public void upSize(int s)
        {
            shape.upSize(s);
        }

        override public void changeColor(string Color)
        {
            shape.changeColor(Color);
        }
    }
}
