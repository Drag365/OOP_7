﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Laba4OOP.src;
using System.Security.Cryptography.X509Certificates;
using ООП_4.ShapesClasses;

namespace ООП_4
{
    class Container : Shape
    {
        private List<Shape> shapes;
        public bool ctrlPressed = false;
        public bool selectMany = false;
        public Decorator decorator;
        public Container()
        {
            shapes = new List<Shape>();
        }

        override public void Draw()
        {
            foreach (Shape shape in shapes)
                shape.Draw();
        }

        public void unSelectAll()
        {
            int i = 0;
            for (; i < shapes.Count;)
            {
                if (shapes[i] is Decorator decorator)
                {
                    if (decorator.shape is Shape shape)
                    {
                        shapes[i] = shape;
                    }
                }
                i++;
            }
        }
        public bool isSelect(Point point)
        {
            int i = 0;
            bool res = false;
            for (; i < shapes.Count;)
            {
                if (shapes[i] is Decorator == false && shapes[i].checkPointPosition(point) == true)
                {
                    decorator = new Decorator(shapes[i]);
                    shapes.Add(decorator);
                    shapes.RemoveAt(i);
                    res = true;
                    if (selectMany == false)
                        return res;
                    continue;
                }
                i++;
            }
            return res;
        }
        public void delSelected()
        {
            int i = 0;
            for (; i < shapes.Count;)
            {
                if (shapes[i] is Decorator == true)
                {
                    shapes.RemoveAt(i);
                    continue;
                }
                i++;
            }
        }
        public void delshapes()
        {
            for (int i = 0; i < shapes.Count;)
            {
                    shapes.RemoveAt(i);
            }
        }
        public void AddOrSelectShape(Shape shape)
        {
            if (ctrlPressed == false)
                unSelectAll();
            if (isSelect(shape.getPosition())) { return; }
            if (ctrlPressed == false)
            {
                decorator = new Decorator(shape);
                shapes.Add(decorator);
                shapes.Remove(shape);
            }
        }

        public void moveShape(int x, int y, int width, int height)
        {

            foreach (Shape shape in shapes)
            {
                if (shape is Decorator == true)
                {
                    if (shape.getPosition().X + shape.getRadius() / 2 + x < width &&
                        shape.getPosition().X - shape.getRadius() / 2 + x > 0 &&
                        shape.getPosition().Y + shape.getRadius() / 2 + y < height &&
                        shape.getPosition().Y - shape.getRadius() / 2 + y > 0)
                        continue;
                    else return;
                }
            }
            foreach (Shape shape in shapes)
            {
                if (shape is Decorator == true)
                {
                    shape.move(x, y);
                }
            }
            
        }

        public void upSizeShape(int s, int width, int height)
        {
            foreach (Shape shape in shapes)
            {
                if (shape is Decorator == true)
                {
                    shape.upSize(s);
                }
            }
        }

        public void downSizeShape(int s, int width, int height)
        {
            foreach (Shape shape in shapes)
            {
                if (shape is Decorator == true)
                {
                    shape.upSize(s);
                }
            }
        }
        public void changeColorShape(string Color)
        {
            foreach (Shape shape in shapes)
            {
                if (shape is Decorator == true)
                {
                    shape.changeColor(Color);
                    
                }
            }
        }
    }
}
