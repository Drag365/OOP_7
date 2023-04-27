using Laba4OOP.src;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ООП_4.ShapesClasses;

namespace ООП_4
{
    public abstract class Command
    {
        public abstract void Execute(Shape selection);
        public abstract void Unexecute();
        public abstract Command Clone();
        public virtual void Dispose() { }
    }

    public class MoveCommand : Command
    {
        private Shape _selection;
        private int _dx;
        private int _dy;

        public MoveCommand(int dx, int dy)
        {
            _dx = dx;
            _dy = dy;
            _selection = null;
        }

        public override void Execute(Shape selection)
        {
            _selection = selection;
            if (_selection != null)
            {
                _selection.move(_dx, _dy);
            }
        }

        public override void Unexecute()
        {
            if (_selection != null)
            {
                _selection.move(-_dx, -_dy);
            }
        }

        public override Command Clone()
        {
            return new MoveCommand(_dx, _dy);
        }

        ~MoveCommand()
        {
        }
    }

    public class SizeCommand : Command
    {
        private Shape _selection;
        private int _dSize;

        public SizeCommand(int dSize)
        {
            _dSize = dSize;
            _selection = null;
        }

        public override void Execute(Shape selection)
        {
            _selection = selection;
            if (_selection != null)
            {
                _selection.upSize(_dSize);
            }
        }

        public override void Unexecute()
        {
            if (_selection != null)
            {
                _selection.upSize(-_dSize);
            }
        }

        public override Command Clone()
        {
            return new SizeCommand(_dSize);
        }

        ~SizeCommand()
        {
        }
    }

    public class ColorCommand : Command
    {
        private Shape _selection;
        private char _Color;
        private char _ColorOld;

        public ColorCommand(char Color)
        {
            _Color = Color;
            _selection = null;
        }

        public override void Execute(Shape selection)
        {
            _selection = selection;
            _ColorOld = _selection.getColor();
            if (_selection != null)
            {
                _selection.changeColor(_Color);
            }
        }

        public override void Unexecute()
        {
            if (_selection != null)
            {
                _selection.changeColor(_ColorOld);
            }
        }

        public override Command Clone()
        {
            return new ColorCommand(_Color);
        }

        ~ColorCommand()
        {
        }
    }

    public class AddCommand : Command
    {
        private List<Shape> _shapes;
        private Shape _shapeToAdd;
        public AddCommand(List<Shape> shapes)
        {
            _shapes = shapes;
        }

        public override void Execute(Shape selection)
        {
            _shapeToAdd = selection; 
            if (_shapeToAdd != null && _shapes != null)
            {
                _shapes.Add(_shapeToAdd);
            }
        }

        public override void Unexecute()
        {
            if (_shapeToAdd != null && _shapes != null)
            {
                _shapes.Remove(_shapeToAdd);
            }
        }

        public override Command Clone()
        {
            return new AddCommand(_shapes);
        }

        ~AddCommand()
        {
        }
    }

    public class UnSelectCommand : Command
    {
        private List<Shape> _shapes;
        private Shape _shapeToAdd;
        public UnSelectCommand()
        {

        }

        public override void Execute(Shape selection)
        {
            _shapeToAdd = selection;
            if (_shapeToAdd != null && _shapes != null)
            {
                _shapes.Add(_shapeToAdd);
            }
        }

        public override void Unexecute()
        {
            if (_shapeToAdd != null && _shapes != null)
            {
                _shapes.Remove(_shapeToAdd);
            }
        }

        public override Command Clone()
        {
            return new AddCommand(_shapes);
        }

        ~UnSelectCommand()
        {
        }
    }
}
