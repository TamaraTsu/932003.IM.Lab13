using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace TsurovaTamaraLab2Currency
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //const double k = 0.02;
        int days, i;
        Random rnd = new Random();

        double price, priced;


        const double u = 0;
        const double sd = 0.015;
        const double k = u - 0.5 * sd * sd;


        private void btStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            outEuro.Text = Math.Round(price, 2) + "₽";
            outDollar.Text = Math.Round(priced, 2) + "₽";
            if (timer1.Enabled)
            {
                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();
                timer1.Stop();
            }

        }

        private void btCalculate_Click_1(object sender, EventArgs e)
        {
            timer1.Stop();
            days = 0;
            i = 0;
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            price = (double)inputPrice.Value;
            priced = (double)inputDollar.Value;

            if (!timer1.Enabled)
            {

                chart1.Series[0].Points.AddXY(days, price);
                chart1.Series[1].Points.AddXY(days, priced);
                timer1.Start();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Random random= new Random();
            double GBM;
            days++;

            if (i == 0)
            {
                outDollar.Text = "calculating.";
                outEuro.Text = "calculating.";
            }
            if (i == 1)
            {
                outDollar.Text = "calculating..";
                outEuro.Text = "calculating..";
            }
            if (i == 2)
            {
                outDollar.Text = "calculating...";
                outEuro.Text = "calculating...";
            }
            //price = price * (1 + k * (rnd.NextDouble() - 0.5));
            //priced = priced * (1 + k * (rnd.NextDouble() - 0.5));

            var a1 = random.NextDouble();
            var a2 = random.NextDouble();
            GBM = Math.Sqrt(-2.0 * Math.Log(a1)) * Math.Cos(2.0 * Math.PI * a2);


            price = price * Math.Exp(k + sd * GBM);
            priced = priced * Math.Exp(k + sd * GBM);

            chart1.Series[0].Points.AddXY(days, price);
            chart1.Series[1].Points.AddXY(days, priced);
            labDay.Text = days.ToString();
            i++;
            if (i > 3) { i = 0; }
        }
    }

}
