using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
        }

        int[,] arr;
        private void button1_Click(object sender, EventArgs e)
        {
            int n = trackBar1.Value;
            arr = new int[n, n];
            
            dataGridView1.ColumnCount = n;
            dataGridView1.RowCount = n;
            Random rnd = new Random();
            for (int i = 0; i < n;)
            {
                int x = rnd.Next(n);
                int y = rnd.Next(n);
                if (arr[x, y] == 0)
                {
                    arr[x, y] = 9;

                    if (x > 0)
                    {
                        if (y > 0) arr[x - 1, y - 1]++;
                        if (y < n - 1) arr[x - 1, y + 1]++;
                        arr[x - 1, y]++;
                    }

                    if (x < n - 1)
                    {
                        if (y > 0) arr[x + 1, y - 1]++;
                        if (y < n - 1) arr[x + 1, y + 1]++;
                        arr[x + 1, y]++;
                    }

                    if (y > 0) arr[x, y - 1]++;
                    if (y < n - 1) arr[x, y + 1]++;

                    i++;
                }
            }

            //int max, y = 0, x = 0;
            for (int i = 0; i < n; i++)
            {
                //max = 0;
                dataGridView1.Columns[i].Width = dataGridView1.Height / n - 1;
                dataGridView1.Rows[i].Height = dataGridView1.Height / n - 1;
                for (int j = 0; j < n; j++)
                {
                    dataGridView1[j, i].Style.BackColor = Color.LightCyan;
                    //arr[i, j] = rnd.Next(50);
                    //dataGridView1[j, i].Value = arr[i, j];
                    //if (arr[i,j]>max)
                    //{
                    //    x = i;
                    //    y = j;
                    //    max = arr[i, j];
                    //}
                    //dataGridView1[y, x].Style.BackColor = Color.Yellow;
                }
            }
        }

        private int nullCell(int x, int y)
        {
            if (x < 0 || y < 0 || x > trackBar1.Value-1 || y > trackBar1.Value-1)
            {
                return 0;
            }
            else
            {
                if (arr[x, y] != 0)
                {
                    dataGridView1[y, x].Value = arr[x, y];
                    dataGridView1[y, x].Style.BackColor = Color.LightYellow;
                    return 0;
                }
                if (dataGridView1[y, x].Style.BackColor == Color.White)
                    return 0;
                dataGridView1[y, x].Style.BackColor = Color.White;
                nullCell(x - 1, y);
                nullCell(x - 1, y-1);
                nullCell(x - 1, y+1);

                nullCell(x + 1, y);
                nullCell(x + 1, y - 1);
                nullCell(x + 1, y + 1);

                nullCell(x, y - 1);
                nullCell(x, y + 1);

                return 1;
            }
        }
        int fail = 0;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            int n = trackBar1.Value;
   
            if (arr[row, col] < 9 && arr[row, col] > 0)
            {
                dataGridView1[col, row].Value = arr[row, col];
                dataGridView1[col, row].Style.BackColor = Color.LightYellow;
            }
            else if (arr[row, col] > 8)
            {
                dataGridView1[col, row].Style.BackColor = Color.Red;
               
                MessageBox.Show("ВЫ ПРОИГРАЛИ!!!");
                fail++;
            }
            else if (arr[row, col] == 0)
            {
                nullCell(row, col);
                dataGridView1[col, row].Style.BackColor = Color.White;
            }
            int count;
            count = 0;
            for(int i=0;i<n;i++)
                for(int j=0;j<n;j++)
                {
                    if(dataGridView1[j, i].Style.BackColor==Color.LightCyan)
                    {
                        count++;
                    }
                }
            if (count == 0 && fail==0)
            {
                MessageBox.Show("ВЫ ВЫИГРАЛИ");
            }
        }

        
        
        private void button2_Click(object sender, EventArgs e)
        {
            
            int n = trackBar1.Value;
            arr = new int[n, n];
            dataGridView1.ColumnCount = n;
            dataGridView1.RowCount = n;
            for (int i=0;i<n;i++)
                for (int j=0;j<n;j++)
                {
                    arr[i, j] = 0;
                    dataGridView1[j, i].Value = null;
                }
            Random rnd = new Random();
            for (int i = 0; i < n;)
            {
                int x = rnd.Next(n);
                int y = rnd.Next(n);
                if (arr[x, y] == 0)
                {
                    arr[x, y] = 9;

                    if (x > 0)
                    {
                        if (y > 0) arr[x - 1, y - 1]++;
                        if (y < n - 1) arr[x - 1, y + 1]++;
                        arr[x - 1, y]++;
                    }

                    if (x < n - 1)
                    {
                        if (y > 0) arr[x + 1, y - 1]++;
                        if (y < n - 1) arr[x + 1, y + 1]++;
                        arr[x + 1, y]++;
                    }

                    if (y > 0) arr[x, y - 1]++;
                    if (y < n - 1) arr[x, y + 1]++;

                    i++;
                }
            }

         
            for (int i = 0; i < n; i++)
            {
                
                dataGridView1.Columns[i].Width = dataGridView1.Height / n - 1;
                dataGridView1.Rows[i].Height = dataGridView1.Height / n - 1;
                for (int j = 0; j < n; j++)
                {
                    dataGridView1[j, i].Style.BackColor = Color.LightCyan;

                }
            }
        }
    }

}

