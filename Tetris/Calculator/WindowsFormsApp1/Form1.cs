using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double[] num = new double[10];
        char[] znak = new char[10];
        private double arrGenerate()
        {
            string primer = textBox1.Text;
            string number = "";
           
            int I = 0,J = 0;
            int len = primer.Length;
           double result;
            for (int i=0;i<len;i++)
            {
                if((primer[i]>='0'&& primer[i]<='9')|| primer[i]=='.')
                {
                    number += primer[i];
                }
                else
                {
                    znak[J] = primer[i];
                    J++;
                    num[I] = Convert.ToDouble(number);
                    I++;
                    number = "";
                }
            }
            num[I] = Convert.ToDouble(number);
            result = num[0];
            int sdvig = 0;
            for (int i=0;i<J;i++)
            {
                switch(znak[i])
                {
                  //  case '+':result += num[i + 1]; break;
                   // case '-': result -= num[i + 1]; break;
                    case '*':
                        num[i - sdvig] *= num[i + 1 - sdvig];
                            for (int j = i + 1 - sdvig; j < J; j++)
                            num[j] = num[j + 1];
                        sdvig++;
                        break;
                        
                    case '/': result /= num[i + 1];
                        num[i] /= num[i + 1];
                        num[i + 1] = num[i];
                        break;
                }
            }
            result = num[0];
            I = 1;
            
            for (int i=0;i<J;i++)
            {
                switch(znak[i])
                {
                    case '+':result += num[I++];break;
                    case '-': result -= num[I++];break;
                }
            }
            for (int i=0;i<10;i++)
            {
                Form1.ActiveForm.Text += " " + num[i];
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Button button = (Button)sender;
            if (button.Text =="C")
            {
                textBox1.Text = "";
            }
            else
            textBox1.Text += button.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
