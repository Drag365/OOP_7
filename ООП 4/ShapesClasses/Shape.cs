﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        protected char Colored = 'B';
        abstract public void Draw();

        virtual public void Save(StreamWriter stream)
        {
            return;
        }

        virtual public void Load(StreamReader stream)
        {
            return;
        }
        virtual public bool checkPointPosition(Point point)
        {
            double len = Math.Sqrt(Math.Pow(point.X - p.X, 2) + Math.Pow(point.Y - p.Y, 2));
            if (len < R / 2)
            {
                return true;
            }
            return false;
        }

        virtual public Graphics getG()
        {
            return g;
        }

        virtual public bool canMove(int x, int y, int width, int height)
        {
            if (p.X + R / 2 + x < width &&
                p.X - R / 2 + x > 0 &&
                p.Y + R / 2 + y < height &&
                p.Y - R / 2 + y > 0)
                return true;
            else
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

        virtual public char getColor()
        {
            return Colored;
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

        virtual public void changeColor(char Color)
        {
            Colored = Color;
        }

        ~Shape() { }
    }
}