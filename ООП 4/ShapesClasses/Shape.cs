using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba4OOP.src
{
    abstract public class Shape
    {
        public Graphics g;
        protected Point p;
        protected int R = 40;
        protected string Colored = "Blue";
        abstract public void Draw();
        virtual public bool checkPointPosition(Point point)
        {
            double len = Math.Sqrt(Math.Pow(point.X - p.X, 2) + Math.Pow(point.Y - p.Y, 2));
            if (len < R / 2)
            {
                return true;
            }
            return false;
        }

        virtual public Point getPosition()
        {
            return p;
        }

        virtual public int getRadius()
        {
            return R;
        }

        virtual public void move(int x, int y)
        {
            p.X += x;
            p.Y += y;
        }

        virtual public void upSize(int s)
        {
            R += s;
        }

        virtual public void changeColor(string Color)
        {
            Colored = Color;
        }
    }
}