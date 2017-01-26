using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;

namespace Asys
{
    public partial class AsysGraphicEditor : Form
    {
        Color paintcolor;
        bool choose = false;
        bool draw = false;
        int x, y, lx, ly = 0;
        Item currItem;

        public AsysGraphicEditor()
        {
            InitializeComponent();
        }

        public enum Item
        {
            Rectangle, Ellipse, Line, Text, Brush, Pencil, eraser, ColorPicker
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            choose = true;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            choose = false;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (choose)
            {
                Bitmap bmp = (Bitmap)pictureBox2.Image.Clone();
                paintcolor = bmp.GetPixel(e.X, e.Y);
                red.Value = paintcolor.R;
                green.Value = paintcolor.G;
                blue.Value = paintcolor.B;
                alpha.Value = paintcolor.A;
                redval.Text = paintcolor.R.ToString();
                greenval.Text = paintcolor.G.ToString();
                blueval.Text = paintcolor.B.ToString();
                alphaval.Text = paintcolor.A.ToString();
                pictureBox3.BackColor = paintcolor;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            draw = true;
            x = e.X;
            y = e.Y;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            draw = false;
            lx = e.X;
            ly = e.Y;

            if (currItem == Item.Line)
            {
                Graphics g = pictureBox1.CreateGraphics();
                g.DrawLine(new Pen(new SolidBrush(paintcolor)), new Point(x, y), new Point(lx, ly));
                g.Dispose();

            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                Graphics g = pictureBox1.CreateGraphics();
                switch (currItem)
                {
                    case Item.Rectangle:
                        g.FillRectangle(new SolidBrush(paintcolor), x, y, e.X - x, e.Y - y);
                        break;
                    case Item.Ellipse:
                        g.FillEllipse(new SolidBrush(paintcolor), x, y, e.X - x, e.Y - y);
                        break;
                    case Item.Brush:
                        g.FillEllipse(new SolidBrush(paintcolor), e.X - x + x, e.Y - y + y, Convert.ToInt32(toolStripTextBox1.Text), Convert.ToInt32(toolStripTextBox1.Text));
                        break;
                    case Item.Pencil:
                        g.FillEllipse(new SolidBrush(paintcolor), e.X - x + x, e.Y - y + y, 5, 5);
                        break;
                    case Item.eraser:
                        g.FillEllipse(new SolidBrush(pictureBox1.BackColor), e.X - x + x, e.Y - y + y, Convert.ToInt32(toolStripTextBox1.Text), Convert.ToInt32(toolStripTextBox1.Text));
                        break;
                }
                g.Dispose();
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            currItem = Item.Brush;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            currItem = Item.Rectangle;
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            currItem = Item.Ellipse;
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            currItem = Item.Pencil;
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            currItem = Item.eraser;
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            currItem = Item.Line;
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            currItem = Item.ColorPicker;
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            currItem = Item.Text;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (currItem == Item.ColorPicker)
            {
                Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                Graphics g = Graphics.FromImage(bmp);
                Rectangle rect = pictureBox1.RectangleToScreen(pictureBox1.ClientRectangle);
                g.CopyFromScreen(rect.Location, Point.Empty, pictureBox1.Size);
                g.Dispose();
                paintcolor = bmp.GetPixel(e.X, e.Y);
                pictureBox3.BackColor = paintcolor;
                red.Value = paintcolor.R;
                green.Value = paintcolor.G;
                blue.Value = paintcolor.B;
                alpha.Value = paintcolor.A;
                redval.Text = paintcolor.R.ToString();
                greenval.Text = paintcolor.G.ToString();
                blueval.Text = paintcolor.B.ToString();
                alphaval.Text = paintcolor.A.ToString();
                bmp.Dispose();
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            if (currItem == Item.Text)
            {
                if (toolStripComboBox4.Text == "Regular")
                {
                    g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(paintcolor), new PointF(x, y));
                }
                else if (toolStripComboBox4.Text == "Bold")
                {
                    g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(paintcolor), new PointF(x, y));
                }
                else if (toolStripComboBox4.Text == "Underline")
                {
                    g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(paintcolor), new PointF(x, y));
                }
                else if (toolStripComboBox4.Text == "Strikeout")
                {
                    g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(x, y));
                }
                else if (toolStripComboBox4.Text == "Italic")
                {
                    g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(paintcolor), new PointF(x, y));
                }
                if (toolStripComboBox1.Text == "SE")
                {
                    if (toolStripComboBox4.Text == "Regular")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(Color.Gray), new PointF(x + 5, y + 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Bold")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(Color.Gray), new PointF(x + 5, y + 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Underline")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(Color.Gray), new PointF(x + 5, y + 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Strikeout")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(Color.Gray), new PointF(x + 5, y + 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Italic")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(Color.Gray), new PointF(x + 5, y + 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                }
                else if (toolStripComboBox1.Text == "SW")
                {
                    if (toolStripComboBox4.Text == "Regular")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(Color.Gray), new PointF(x - 5, y + 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Bold")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(Color.Gray), new PointF(x - 5, y + 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Underline")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(Color.Gray), new PointF(x - 5, y + 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Strikeout")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(Color.Gray), new PointF(x - 5, y + 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Italic")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(Color.Gray), new PointF(x - 5, y + 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                }
                else if (toolStripComboBox1.Text == "NE")
                {
                    if (toolStripComboBox4.Text == "Regular")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(Color.Gray), new PointF(x + 5, y - 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Bold")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(Color.Gray), new PointF(x + 5, y - 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Underline")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(Color.Gray), new PointF(x + 5, y - 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Strikeout")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(Color.Gray), new PointF(x + 5, y - 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Italic")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(Color.Gray), new PointF(x + 5, y - 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                }
                else if (toolStripComboBox1.Text == "NW")
                {
                    if (toolStripComboBox4.Text == "Regular")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(Color.Gray), new PointF(x - 5, y - 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Bold")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(Color.Gray), new PointF(x - 5, y - 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Underline")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(Color.Gray), new PointF(x - 5, y - 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Strikeout")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(Color.Gray), new PointF(x - 5, y - 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Italic")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(Color.Gray), new PointF(x - 5, y - 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                }
                else if (toolStripComboBox1.Text == "S")
                {
                    if (toolStripComboBox4.Text == "Regular")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(Color.Gray), new PointF(x, y + 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Bold")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(Color.Gray), new PointF(x, y + 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Underline")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(Color.Gray), new PointF(x, y + 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Strikeout")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(Color.Gray), new PointF(x, y + 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Italic")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(Color.Gray), new PointF(x, y + 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                }
                else if (toolStripComboBox1.Text == "N")
                {
                    if (toolStripComboBox4.Text == "Regular")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(Color.Gray), new PointF(x, y - 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Bold")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(Color.Gray), new PointF(x, y - 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Underline")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(Color.Gray), new PointF(x, y - 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Strikeout")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(Color.Gray), new PointF(x, y - 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Italic")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(Color.Gray), new PointF(x, y - 5));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                }
                else if (toolStripComboBox1.Text == "W")
                {
                    if (toolStripComboBox4.Text == "Regular")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(Color.Gray), new PointF(x - 5, y));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Bold")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(Color.Gray), new PointF(x - 5, y));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Underline")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(Color.Gray), new PointF(x - 5, y));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Strikeout")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(Color.Gray), new PointF(x - 5, y));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Italic")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(Color.Gray), new PointF(x - 5, y));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                }
                else if (toolStripComboBox1.Text == "E")
                {
                    if (toolStripComboBox4.Text == "Regular")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(Color.Gray), new PointF(x + 5, y));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Regular), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Bold")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(Color.Gray), new PointF(x + 5, y));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Bold), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Underline")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(Color.Gray), new PointF(x + 5, y));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Underline), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Strikeout")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(Color.Gray), new PointF(x + 5, y));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Strikeout), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                    else if (toolStripComboBox4.Text == "Italic")
                    {
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(Color.Gray), new PointF(x + 5, y));
                        g.DrawString(toolStripTextBox2.Text, new Font(toolStripComboBox2.Text, Convert.ToInt32(toolStripComboBox3.Text), FontStyle.Italic), new SolidBrush(paintcolor), new PointF(x, y));
                    }
                }
                g.Dispose();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            pictureBox1.Image = null;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            pictureBox1.Image = null;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Png files|*.png|jpeg files|*jpg|bitmaps|*.bmp";
            if (o.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox1.Image = (Image)Image.FromFile(o.FileName).Clone();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            Rectangle rect = pictureBox1.RectangleToScreen(pictureBox1.ClientRectangle);
            g.CopyFromScreen(rect.Location, Point.Empty, pictureBox1.Size);
            g.Dispose();
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "Png files|*.png|jpeg files|*jpg|bitmaps|*.bmp";
            if (s.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(s.FileName))
                {
                    File.Delete(s.FileName);
                }
                if (s.FileName.Contains(".jpg"))
                {
                    bmp.Save(s.FileName, ImageFormat.Jpeg);
                }
                else if (s.FileName.Contains(".png"))
                {
                    bmp.Save(s.FileName, ImageFormat.Png);
                }
                else if (s.FileName.Contains(".bmp"))
                {
                    bmp.Save(s.FileName, ImageFormat.Bmp);
                }
            }
        }

        private void AsysGraphicEditor_Load(object sender, EventArgs e)
        {
            FontFamily[] family = FontFamily.Families;
            foreach (FontFamily font in family)
            {
                toolStripComboBox2.Items.Add(font.GetName(1).ToString());
            }
        }

        private void red_Scroll(object sender, EventArgs e)
        {
            paintcolor = Color.FromArgb(alpha.Value, red.Value, green.Value, blue.Value);
            pictureBox3.BackColor = paintcolor;
            redval.Text = "R: " + paintcolor.R.ToString();
        }

        private void green_Scroll(object sender, EventArgs e)
        {
            paintcolor = Color.FromArgb(alpha.Value, red.Value, green.Value, blue.Value);
            pictureBox3.BackColor = paintcolor;
            greenval.Text = "G: " + paintcolor.G.ToString();
        }

        private void blue_Scroll(object sender, EventArgs e)
        {
            paintcolor = Color.FromArgb(alpha.Value, red.Value, green.Value, blue.Value);
            pictureBox3.BackColor = paintcolor;
            blueval.Text = "B: " + paintcolor.B.ToString();
        }

        private void alpha_Scroll(object sender, EventArgs e)
        {
            paintcolor = Color.FromArgb(alpha.Value, red.Value, green.Value, blue.Value);
            pictureBox3.BackColor = paintcolor;
            alphaval.Text = "A: " + paintcolor.A.ToString();
        }

    }
}
