using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;

namespace Self_Avoiding_Walk
{
    class SAW
    {
        Form1 f1;
        public int walker;
        public int step;
        public SAW(Form1 f,int w,int s)
        {
            f1 = f;
            walker = w;
            step = s;
        }
        public void plot()
        {
            Random rand = new Random();
            Graphics gg = f1.CreateGraphics();
            SolidBrush sb = new SolidBrush(Color.Green);
            //int walker = int.Parse(f1.textBox1.Text);
            //int steps = int.Parse(f1.textBox2.Text);
            double[] xave = new double[step];

            Point[,] walks = new Point[walker, step];
            int[] counter = new int[walker];
            for (int i = 0; i < walker; i++)
            {
                int x = 0;
                int y = 0;
                double a;
                for (int j = 0; j < step; j++)
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
            for (int i = 0; i < step; i++)        
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
            
            f1.dataGridView1.DataSource = dt;
        }
    }
}
