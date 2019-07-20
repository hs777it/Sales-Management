using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using zUtilities;


namespace Sales_Management.Test
{
    public partial class frmExamples : Form
    {
        int move; int valX; int valY;
        public frmExamples()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            move = 1;
            valX = e.X;
            valY = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move == 1)
                this.SetDesktopLocation(MousePosition.X - valX, MousePosition.Y - valY);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            move = 0;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Form1_MouseDown(sender, e);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Form1_MouseMove(sender, e);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Form1_MouseUp(sender, e);
        }

        private void btnEn_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("En");
            new frmLocalizable().ShowDialog();
        }

        private void btnAr_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("Ar");
            new frmLocalizable().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           textBox1.Text = Helpers.GetProductID();
        }
    }
}