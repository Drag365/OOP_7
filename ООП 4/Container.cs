using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Laba4OOP.src;
using System.Security.Cryptography.X509Certificates;
using ООП_4.ShapesClasses;
using System.IO;
using System.Security.Cryptography;
using ООП_4.Factory;

namespace ООП_4
{
    class Container : Shape
    {
        const string filename = "D:/data.txt";
        StreamWriter stream = null;
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

        public List<Shape> returnSelected()
        {
            List<Shape> selShapes = new List<Shape>();
            foreach (Shape shape in shapes) {
                if (shape is Decorator)
                {
                    selShapes.Add(shape as Decorator);
                }
            }
            return selShapes;
        }
        public void Save()
        {
            try
            {
                stream = new StreamWriter(filename);
                stream.WriteLine("{0}", shapes.Count);

                foreach (Shape shape in shapes)
                {
                    shape.Save(stream);
                }
                

            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        public void Load(CShapeArray array)
        {
            array.LoadShapes(filename);
            shapes = array.getList();
        }
        public void Compose(CGroup group)
        {
            int i = 0;
            for (; i < shapes.Count;)
            {
                if (shapes[i] is Decorator decorator)
                {
                    if (decorator.shape is Shape shape)
                    {
                        group.addShape(shape);
                        shapes.RemoveAt(i);
                        continue;
                    }
                }
                i++;
            }
            decorator = new Decorator(group);
            shapes.Add(decorator);
        }

        public void unCompose()
        {
            int i = 0;
            int j = shapes.Count;
            for (; i < j;)
            {
                if (shapes[i] is Decorator decorator)
                {
                    if (decorator.shape is CGroup group)
                    {
                        foreach (Shape innerShape in group.getShapes())
                        {
                            Decorator dec = new Decorator(innerShape);
                            shapes.Add(dec);
                        }
                        j--;
                        shapes.RemoveAt(i);
                        continue;
                    }
                }
                i++;
            }
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

        virtual public bool CheckPosition(Point p)
        {
            int i = 0;
            for (; i < shapes.Count;)
            {
                if (shapes[i].checkPointPosition(p) == true)
                {
                    return true;
                }
                i++;
            }
            return false;

        }
        public void Select(Point point)
        {
            if (ctrlPressed == false)
                unSelectAll();
            int i = 0;
            for (; i < shapes.Count;)
            {
                if (shapes[i] is Decorator == false && shapes[i].checkPointPosition(point) == true)
                {
                    decorator = new Decorator(shapes[i]);
                    shapes.Insert(i, decorator);
                    shapes.RemoveAt(i + 1);
                    if (selectMany == true)
                        continue;
                }
                i++;
            }
        }
        //public bool isSelect(Point point)
        //{
        //    int i = 0;
        //    bool res = false;
        //    for (; i < shapes.Count;)
        //    {
        //        if (shapes[i] is Decorator == false && shapes[i].checkPointPosition(point) == true)
        //        {
        //            decorator = new Decorator(shapes[i]);
        //            shapes.Insert(i, decorator);
        //            shapes.RemoveAt(i + 1);
        //            res = true;
        //            if (selectMany == false)
        //                return res;
        //            continue;
        //        }
        //        i++;
        //    }
        //    return res;
        //}
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

        public void Add(Shape shape)
        {
            decorator = new Decorator(shape);
            shapes.Add(decorator);
            shapes.Remove(shape);
        }

        public List<Shape> getShapes()
        {
            return shapes;
        }


        override public void move(int x, int y)
        {



            foreach (Shape shape in shapes)
            {

                if (shape is Decorator == true)
                {
                    shape.move(x, y);
                }
            }

        }

        override public void upSize(int s)
        {
            foreach (Shape shape in shapes)
            {
                if (shape is Decorator == true)
                {
                    shape.upSize(s);
                }
            }
        }

        override public void changeColor(char Color)
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
