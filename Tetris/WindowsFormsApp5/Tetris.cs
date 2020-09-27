using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp5
{
    class Tetris
    {
      private  int[,] area;
      private  int[,,] figure;
      int h, w,fig_count, f, x, y,point = 0;

        public int Height
        {
            get
            {
                return h;
            }
        }

        public int Width
        {
            get
            {
                return w;
            }
        }

        public int[,] Area
        {
            get
            {
                return area;
            }
        }


        private void GenerateFigure(string path,int x=4, int y=4, int z=7)
        {
            int enter;
            StreamReader reader = new StreamReader(path);
            for (int k = 0; k < z; k++)
            {
                for (int j = 0; j < x; j++)
                {
                    for (int i = 0; i < y; i++)
                    {
                        figure[i, j, k] = reader.Read();
                        figure[i, j, k] -= 48;
                     //   Console.Write(figure[i, j, k]);
                    }
                    enter = reader.Read();
                    enter = reader.Read();
                   // Console.WriteLine("!");
                }
               // Console.WriteLine("*");
            }
            reader.Close();
        }

        private void DownFig()
        {
            for (int i = h - 2; i >= 0; i--)
            {
                for (int j = w - 1; j >= 0; j--)
                {
                    if (area[i,j] == 1)
                    {
                        area[i, j] = 2;
                    }
                }
            }
            FireLine();
            NextFigure();
        }

        public bool NextStep()// падение фигуры в низ
        {
            bool flag = false;
            int count1, count2;
            for (int i = h - 2; i >= 0; i--)
            {
                count1 = 0;
                count2 = 0;
                for (int j = w - 1; j >= 0; j--)
                {
                    if (area[i, j] == 1)
                        count1++;
                    if (area[i, j] == 1 && area[i + 1, j] == 0)
                        count2++;
                }
                if (count1 == count2)
                {
                    flag = true;
                    for (int j = w - 1; j >= 0; j--)
                    {
                        if (area[i, j] == 1)
                        {
                            area[i + 1, j] = 1;
                            area[i, j] = 0;
                        }
                    }
                }
                else
                {
                    DownFig();
                }
            }
            if (flag)
                y++;

            return true;
        }
        public void LeftFig()
        {
            int count1, count2;
            bool flag = false;
            for (int i = 1; i <w; i++)
            {
                count1 = 0;
                count2 = 0;
                for (int j = 1; j < h; j++)
                {
                    if (area[j, i] == 1)
                        count1++;
                    if(area[j,i] == 1 && area[j,i-1] == 0)
                    {
                        count2++;
                    }
                }
                if (count1==count2)
                {
                    flag = true;
                    for (int j = 1; j < h; j++)
                    {
                        if (area[j, i] == 1)
                        {
                            area[j, i - 1] = 1;
                            area[j, i] = 0;
                        }
                    }
                }
            }
            if (flag)
            {
                x--;
            }
        }

        public void RightFig()
        {
            bool flag = false;
            int count1, count2;
            for (int i = w-1; i > 0; i--)
            {
                count1 = 0;
                count2 = 0;
                for (int j = 1; j < h; j++)
                {
                    if (area[j, i] == 1)
                        count1++;
                    if (area[j, i] == 1 && area[j, i + 1] == 0)
                    {
                        count2++;
                    }
                }
                if (count1 == count2)
                {
                    flag = true;
                    for (int j = 1; j < h; j++)
                    {
                        if (area[j, i] == 1)
                        {
                            area[j, i + 1] = 1;
                            area[j, i] = 0;
                        }
                    }
                }
            }
            if (flag)
                x++;
        }

        public void Rotate()
        {
            int[,] figArray;
            int temp;
            figArray = new int[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    figArray[i, j] = figure[j, i, f];
                }

                for (int j = 0; j < 2; j++)
                {
                    temp = figArray[i, j];
                    figArray[i, j] = figArray[i, 3 - j];
                    figArray[i, 3 - j] = temp;
                }
            }
           
            int count1 = 0, count2 = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                   
                    if (figArray[i,j] == 1)
                    {
                        count1++;
                        if ((y+i > 0 && y+i < h) && (x+j > 0 && x + j < w) && (area[y+i,x+j] != 2 && area[y + i, x + j] != 10 && area[y + i, x + j] != 15))
                        {
                            count2++;
                        }
                    }
                }
            if (count1 == count2)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        figure[i, j, f] = figArray[i, j];
                        //Console.Write(figArray[i, j]);
                        if (area[y + i, x + j] == 1)
                        {
                            area[y + i, x + j] = 0;
                        }
                        if(figArray[i,j] == 1)
                        area[y + i, x + j] = figure[i, j, f];
                    }
                    // Console.WriteLine();
                }
            }
        }

        public void NextFigure()
        {
            Random rnd = new Random();
            f = rnd.Next(fig_count);
            y = 0;
            x = w / 2 - 2;
            for (int i=0; i<4;i++)
            {
                for (int j=0;j<4;j++)
                {
                    if(area[i,j+w/2-2] != 2)
                    area[i, j + w / 2-2] = figure[i, j,f];
                }
            }
        }

        private void DeleteLine(int del)
        {
            for (int i = del; i > 0; i--)
            {
                for (int j = 1; j < w - 1; j++)
                {
                    area[i, j] = area[i - 1, j];
                    area[0, j] = 0;
                }
            }
            point++;
        }
        
        public int GetPoint
        {
            get
            {
                return point;
            }
        }

        public bool EndGame()
        {
            for (int i = 0; i<w; i++)
            {
                if (area[1, i] == 2)
                    return true;
            }
            return false;
        }

        public void FireLine()
        {
            int count = 0;
            for (int i = 0; i < h; i++)
            {
                count = 0;
                for (int j = 0; j < w; j++)
                {
                    if (area[i, j] == 2)
                        count++;
                }

                if (count == (w - 2))
                {
                    DeleteLine(i);
                }
            }
        }

        private void wall()
        {
            for (int i = 0; i < h; i++)
            {
                area[i, 0] = 15;
                area[i, w-1] = 15;
            }
            for (int i = 0; i < w; i++)
            {
                area[h - 1, i] = 10;
            }
        }
        public Tetris()
        {
            area = new int[22, 12];
            h = 22;
            w = 12;
            fig_count = 7;
            wall();
            figure = new int[4, 4, 7];
            GenerateFigure("c:\\1\\fig.txt");
        }
    }
}
