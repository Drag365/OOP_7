﻿using Laba4OOP.src;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ООП_4.Factory;
using ООП_4.ShapesClasses;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ООП_4
{
  
    public partial class Form1 : Form
    {
        Container container = new Container();// создаем контейнер хранящий круги
        public Graphics g;// создаем объект графики
        ShapeCreation Creation;// создаем объект-конвеер кругов
        Bitmap map;// создаем битмап "мап"
        Boolean ctrlpress = false;// флажок зажатия контрола
        int typeOfShape = 0;
        char Colored = 'B';
        const string filename = "D:/data.txt";
        int razWidth;
        int razHeight;
        Dictionary<char, Command> commands;
        Stack<Command> history;

        public Form1()
        {
            InitializeComponent();
            map = new Bitmap(1920, 1080);// определяем битмап
            Creation = new ShapeCreation(Graphics.FromImage(map));// определяем конвеер кругов
            
            razWidth = this.Width - paintField.Width;
            razHeight = this.Height - paintField.Height;
            history = new Stack<Command>();
            commands = new Dictionary<char, Command>();
            commands['A'] = new MoveCommand(-10, 0);
            commands['D'] = new MoveCommand(10, 0);
            commands['W'] = new MoveCommand(0, -10);
            commands['S'] = new MoveCommand(0, 10);
            commands['Q'] = new SizeCommand(-1);
            commands['E'] = new SizeCommand(1);

        }

        protected void paintField_Paint(object sender, PaintEventArgs e)// функция отрисовки кругов
        {
            Graphics.FromImage(map).Clear(Color.LightGray);
            container.Draw();
            paintField.Image = map;
            toolStrip1.Refresh();
        }

        private void paintField_MouseClick(object sender, MouseEventArgs e)//функция нажатия мышкой для добавления на поле круга или его выделения
        {
            Command command = new AddCommand(container.getShapes());
            if (container.CheckPosition(e.Location) == false && ctrlpress == false)
            {
                if (typeOfShape == 0)
                {
                    command.Execute(Creation.createCCircle(e.Location, Colored));

                }
                else if (typeOfShape == 1)
                {
                    command.Execute(Creation.createSquare(e.Location, Colored));
                }
                else if (typeOfShape == 2)
                {
                    command.Execute(Creation.createTriangle(e.Location, Colored));
                }
            }
            if (container.CheckPosition(e.Location) == true)
                container.Select(e.Location);
            history.Push(command);
            
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)//функция нажатия клавиши контрол или делит
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)// определяем зажатие клавиши контрол
            {
                ctrlpress = true;
                container.ctrlPressed = !container.ctrlPressed;
            }
            if (e.KeyCode == Keys.Delete)
            {
                Graphics.FromImage(map).Clear(Color.LightGray);
                container.delSelected();
            }

            if (e.KeyCode == Keys.A || e.KeyCode == Keys.S || e.KeyCode == Keys.D || e.KeyCode == Keys.W || e.KeyCode == Keys.E || e.KeyCode == Keys.Q)
            {
                char c = Convert.ToChar(e.KeyCode);

                Command command = commands[c];
                if (command != null)
                {
                    Command newcommand = command.Clone();
                    newcommand.Execute(container);
                    history.Push(newcommand);
                }
            }

            if (e.KeyCode == Keys.G)
            {
                container.Compose(Creation.createCGroup());
            }
            if (e.KeyCode == Keys.R)
            {
                container.unCompose();
            }

            if (e.KeyCode == Keys.R)
            {
                container.unCompose();
            }

            if (e.KeyCode == Keys.Z && history.Count > 0)
            {
                Command lastCommand = history.Pop();
                lastCommand.Unexecute();
                lastCommand.Dispose();
            }
        }

        private void CtrlCheck_CheckedChanged(object sender, EventArgs e)//если мы выделели чекбокс "контрол зажат", то меняем значение флажка 
        {
            container.ctrlPressed = !container.ctrlPressed;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)// отжатие контрола, меняем флажок
        {
            
            if ((Control.ModifierKeys & Keys.Control) != Keys.Control && ctrlpress == true)
            {
                container.ctrlPressed = !container.ctrlPressed;
                ctrlpress = !ctrlpress;
            }
        }

        private void deleteAll_Click(object sender, EventArgs e)// функция нажатия на кнопку "Удалить все"
        {
            Graphics.FromImage(map).Clear(Color.LightGray);
            container.delshapes();
        }

        private void selectAll_CheckedChanged(object sender, EventArgs e)
        {
            container.selectMany = !container.selectMany;
        }

        private void кругToolStripMenuItem_Click(object sender, EventArgs e)
        {
            typeOfShape = 0;
            toolStripDropDownButton1.Image = global::ООП_4.Properties.Resources.Circle;
            toolStrip1.Refresh();
        }

        private void квадратToolStripMenuItem_Click(object sender, EventArgs e)
        {
            typeOfShape = 1;
            toolStripDropDownButton1.Image = global::ООП_4.Properties.Resources.Square;
            toolStrip1.Refresh();
        }

        private void треугольникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            typeOfShape = 2;
            toolStripDropDownButton1.Image = global::ООП_4.Properties.Resources.Triangle;
            toolStrip1.Refresh();
        }

        private void GreenOption_Click(object sender, EventArgs e)
        {
            container.changeColor('G');
            Command newcommand = new ColorCommand('G');
            newcommand.Execute(container);
            history.Push(newcommand);
            Colored = 'G';
        }

        private void BlackOption_Click(object sender, EventArgs e)
        {
            container.changeColor('B');
            Command newcommand = new ColorCommand('B');
            newcommand.Execute(container);
            history.Push(newcommand);
            Colored = 'B';
        }

        private void PurpleOption_Click(object sender, EventArgs e)
        {
            container.changeColor('P');
            Command newcommand = new ColorCommand('P');
            newcommand.Execute(container);
            history.Push(newcommand);
            Colored = 'P';
        }

        private void BrownOption_Click(object sender, EventArgs e)
        {
            container.changeColor('R');
            Command newcommand = new ColorCommand('R');
            newcommand.Execute(container);
            history.Push(newcommand);
            Colored = 'R';
        }

        private void BlueOption_Click(object sender, EventArgs e)
        {
            container.changeColor('V');
            Command newcommand = new ColorCommand('V');
            newcommand.Execute(container);
            history.Push(newcommand);
            Colored = 'V';
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            container.Save();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            CShapeArray array = new CShapeArray(Graphics.FromImage(map));
            container.Load(array);
        }

        private void groupButton_Click(object sender, EventArgs e)
        {
            container.Compose(Creation.createCGroup());
        }

        private void unGroupButton_Click(object sender, EventArgs e)
        {
            container.unCompose();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            panel1.Width = this.Width - razWidth;
            panel1.Height = this.Height - razHeight;
            paintField.Width = this.Width - razWidth;
            paintField.Height = this.Height - razHeight;
        }
    }
    
}
