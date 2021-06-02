using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Imitation
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double l1 = (double)numericUpDown1.Value;
            double l2 = (double)numericUpDown2.Value;
            int m = (int)numericUpDown3.Value;
            Model mm = new Model(l1,l2);
            mm.simulate(m);
            label3.Text = mm.calcAverage().ToString();
            label4.Text = mm.KolmogorovTest().ToString();
            if (mm.KolmogorovTest() < 0.05)
            {
                label7.Text = "Yes";
            }
            else label7.Text = "No";
        }
    }
}
