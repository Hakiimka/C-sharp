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

namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap bitmap;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "all||jpeg| *.jpg | png | *.png";
            if(openFileDialog.ShowDialog()==DialogResult.OK)
            {
                bitmap = new Bitmap(openFileDialog.FileName);
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int h = bitmap.Height;
            int w = bitmap.Width;
            Color color;
            for (int i = 0; i < h; i++)
            {
                for(int j=0;j<w;j++)
                {
                    color = bitmap.GetPixel(j, i);
                    color = Color.FromArgb(color.R,color.R,color.R);
                    bitmap.SetPixel(j, i, color);
                }
                pictureBox1.Image = bitmap;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int h = bitmap.Height;
            int w = bitmap.Width;
            Color color;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    color = bitmap.GetPixel(j, i);
                    color = Color.FromArgb(color.R, 0, 0);
                    bitmap.SetPixel(j, i, color);
                }
                pictureBox1.Image = bitmap;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int h = bitmap.Height;
            int w = bitmap.Width;
            Color color;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    color = bitmap.GetPixel(j, i);
                    if (color.R < 125 && color.B < 125 && color.B < 125)
                        bitmap.SetPixel(j, i, Color.Black);
                    else
                        bitmap.SetPixel(j, i, Color.White);
                }
                pictureBox1.Image = bitmap;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int h = bitmap.Height;
            int w = bitmap.Width;
            Color color,color1;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w/2; j++)
                {
                    color = bitmap.GetPixel(j, i);
                    color1 = bitmap.GetPixel(w - j - 1, i);
                    bitmap.SetPixel(j, i, color1);
                    //color = Color.FromArgb(color.R, 0, 0);
                    bitmap.SetPixel(w-j-1,i, color);
                }
                pictureBox1.Image = bitmap;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            Bitmap logo;
            openFileDialog.Filter = "all||jpeg| *.jpg | png | *.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                logo = new Bitmap(openFileDialog.FileName);
                int h = logo.Height;
                int w = logo.Width;
                Color color;
                if(logo.Width>bitmap.Width || logo.Height > bitmap.Height)
                {
                    MessageBox.Show("Слишком большой логотип");
                }
                else
                for(int i=0;i<h;i++)
                    for(int j=0;j<w;j++)
                    {
                        color = logo.GetPixel(j, i);
                        if (color.R < 250 && color.G < 250 && color.B < 250)
                            bitmap.SetPixel(j, i, color);
                    }
                pictureBox1.Image = bitmap;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "BMP|*.bmp";
            if(saveFileDialog.ShowDialog()==DialogResult.OK)
            {
                // FileStream file = new FileStream(saveFileDialog.FileName, FileMode.Create);
                bitmap.Save(saveFileDialog.FileName);
            }
        }
    }
}
