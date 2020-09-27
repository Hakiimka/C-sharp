using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        Tetris test;
        int col, row, level = 1;
        int[,] area;
        public Form1()
        {
            InitializeComponent();
        }

        void RefreshGrid()
        {
            for (int i = 0; i < col; i++)
            {
                dataGridView1.Columns[i].Width = 20;
                for (int j = 0; j < row; j++)
                {
                    dataGridView1.Rows[j].Height = 20;
                    dataGridView1[i, j].Value = area[j, i];
                    if (area[j, i] == 10 || area[j, i] == 15)
                        dataGridView1[i, j].Style.BackColor = Color.Gray;
                    if (area[j, i] == 1)
                        dataGridView1[i, j].Style.BackColor = Color.LightBlue;
                    if (area[j, i] == 2)
                        dataGridView1[i, j].Style.BackColor = Color.Blue;
                    if (area[j, i] == 0)
                        dataGridView1[i, j].Style.BackColor = Color.White;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (test.EndGame() == false)
            {
                test.NextStep();
                dataGridView1.ClearSelection();
                label1.Text = test.GetPoint.ToString();
                RefreshGrid();
                if (level < 24 && timer1.Interval > 10) 
                {
                    level = test.GetPoint;
                    timer1.Interval = 500 - (level*20);
                }
            }
            else
            {
                timer1.Enabled = false;
                MessageBox.Show("GAME OVER!");
            }
        }
       
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                test.LeftFig();
            }
            if (e.KeyCode == Keys.Right)
            {
                test.RightFig();
            }
            if (e.KeyCode == Keys.Up)
            {
                test.Rotate();
            }
            if(e.KeyCode == Keys.Down)
            {
                test.NextStep();
                test.NextStep();
            }
            dataGridView1.ClearSelection();
            RefreshGrid();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
                timer1.Interval = 500 - level * 20;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            test = new Tetris();
            row = test.Height;
            col = test.Width;
            area = test.Area;

            dataGridView1.ColumnCount = col;
            dataGridView1.RowCount = row;
            for(int i = 0; i < col;i++)
            {
                dataGridView1.Columns[i].Width = 20;
                for (int j = 0; j < row; j++)
                {
                    dataGridView1.Rows[j].Height = 20;
                }
            }
            test.NextFigure();
            timer1.Enabled = true;
            RefreshGrid();
        }
    }
}
