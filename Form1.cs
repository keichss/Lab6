﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6_op
{
    public partial class FormMain : Form
    {
        MyStorage storage;
        Bitmap bmp = new Bitmap(1000, 1000);
        Graphics g;
        int whichShape = 1;
        public FormMain()
        {
            InitializeComponent();

            storage = new MyStorage();
        }

        private void paintBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (storage.isCheckedStorage(e) == false)//если нажато на пустое место
            {
                storage.AllNotChecked();
                switch (whichShape)
                {
                    case 0:
                        storage.addObject(new Circle(e.X, e.Y, 50));//добавление нового круга в хранилище
                        break;
                    case 1:
                        storage.addObject(new Square(e.X, e.Y, 50));
                        break;
                    case 2:
                        storage.addObject(new Triangle(e.X, e.Y, 50));
                        break;
                }
            }
            else
                if (Control.ModifierKeys == Keys.Control)
                storage.MakeCheckedObjectStorage(e);
            else
            {
                storage.AllNotChecked();
                storage.MakeCheckedObjectStorage(e);
            }
            paintBox.Invalidate();
        }

        private void paintBox_Paint(object sender, PaintEventArgs e)
        {
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            storage.DrawAll(paintBox, g, bmp);
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Delete:
                    g = Graphics.FromImage(bmp);
                    storage.removeCheckedObject();
                    g.Clear(Color.White);
                    break;//движение по форме
                case Keys.W:
                case Keys.S:
                case Keys.A:
                case Keys.D:
                    storage.Move(e);
                    break;
                case Keys.E://размер фигуры
                case Keys.Q:
                    storage.Resize(e);
                    break;

            }
        }

        private void rbCircle_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            whichShape = rb.TabIndex;
        }

        private void panelColor_Click(object sender, EventArgs e)
        {
            Panel p = (Panel)sender;
            Color color = new Color();
            if (p == pRed)
                color = Color.Red;
            else if (p == pBlue)
                color = Color.Blue;
            else if (p == pGreen)
                color = Color.Green;
            else color = Color.White;
            storage.ColorChange(color);
        }
    }
}
