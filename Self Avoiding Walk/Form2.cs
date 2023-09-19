using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Self_Avoiding_Walk
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            Graphics gg = this.CreateGraphics();
            SolidBrush sb = new SolidBrush(Color.Green);
            int walker = int.Parse(textBox1.Text);
            int steps = int.Parse(textBox2.Text);
            double[] xave = new double[steps];

            Point[,] walks = new Point[walker, steps];
            int[] counter = new int[walker];
            for (int i = 0; i < walker; i++)
            {
                int x = 0;
                int y = 0;
                double a;
                for (int j = 0; j < steps; j++)
                {
                    a = rand.NextDouble();

                    if (a < 0.25)
                    {
                        x = x + 1;
                    }
                    else if (a > 0.25 && a < 0.5)
                    {
                        x = x - 1;
                    }
                    else if (a > 0.5 && a < 0.75)
                    {
                        y = y + 1;
                    }
                    else if (a < 0.25)
                    {
                        y = y - 1;
                    }
                    walks[i, j] = new Point(x, y);
                    bool flag = false;
                    for (int k = 0; k < j; k++)
                    {
                        if (walks[i, j] == walks[i, k])
                        {
                            flag = true;
                        }
                    }
                    if (flag == false)
                    {
                        gg.FillEllipse(sb, 400 + x * 10, 400 - y * 10, 4, 4);
                    }
                    else
                    {
                        counter[i] = counter[i] + 1;
                    }


                }




            }
            for (int i = 0; i < steps; i++)
            {
                double sum = 0;
                for (int j = 0; j < walker; j++)
                {
                    sum = sum + Math.Pow(walks[j, i].X, 2) + Math.Pow(walks[j, i].Y, 2);
                }
                sum = sum / walker;
                gg.FillEllipse(new SolidBrush(Color.Red), 400 + i, 400 - (float)sum, 4, 4);
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("Counter");
            dt.Columns.Add("Discarding Walks");
            DataRow dr;
            for (int i = 0; i < walker; i++)
            {
                dr = dt.NewRow();
                dr[0] = i + 1;
                dr[1] = counter[i];
                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
        }
    }
}
