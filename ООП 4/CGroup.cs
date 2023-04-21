using Laba4OOP.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ООП_4.ShapesClasses;

namespace ООП_4
{
    public class CGroup : Shape
    {
        private List<Shape> shapes;

        public CGroup(Graphics g) 
        {
            this.g = g;
            shapes = new List<Shape>();
        }

        public List<Shape> getShapes()
        {
            return shapes;
        }
        public override bool checkPointPosition(Point point)
        {
            foreach (Shape shape in shapes)
            {
                if (shape.checkPointPosition(point))
                {
                    return true;
                }
            }
            return false;
        }
        public void addShape(Shape shape) { shapes.Add(shape); }
        override public void move(int x, int y)
        {
            foreach (Shape shape in shapes)
            {
                shape.move(x, y); 
            }
        }

        override public void Draw() 
        { 
            foreach (Shape shape in shapes) 
            {
                shape.Draw();
            } 
        }

        public override Graphics getG()
        {
            return g;
        }

        override public void upSize(int s)
        {
            foreach (Shape shape in shapes)
            {
                shape.upSize(s);
            }
        }

        override public void changeColor(string Color)
        {
            foreach (Shape shape in shapes)
            {
                shape.changeColor(Color);
            }
        }

        public override bool canMove(int x, int y, int width, int height)
        { 
            foreach (Shape shape in shapes)
            {
                if (shape.canMove(x, y, width, height) == false)
                    return false;
            }
            return true;

        }
        //override public int getRadius()
        //{
        //    return 100;
        //}
        ~CGroup()
        {
            for (int i = 0; i < shapes.Count;)
            {
                shapes[i] = null;
            }
        }
    }
}
