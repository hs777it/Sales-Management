using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sales_Management.PL.Users
{
    public partial class frmLock : DevExpress.XtraEditors.XtraForm
    {
        int move; int valX; int valY;
        public frmLock()
        {
            InitializeComponent();
        }

        private void frmLock_Load(object sender, EventArgs e)
        {
            //lblMarquee.Visible = false;
            lblUsername.Focus();
            lblUsername.Text =  Program.UserName +"gfgfh";
           // PL.Main.FrmMain.getMainForm.Enabled = false;
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != Program.UserName)
            {
                lblErr.Visible = true;
                lblErr.Text = "كلمة سر إلغاء القفل خطأ";
                return;
            }
            else
            {
               // PL.Main.FrmMain.getMainForm.Enabled = true;
                this.Close();
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            btnUnlock.PerformClick();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblDate.Text = DateTime.Now.ToShortDateString();
            lblTime.Text = DateTime.Now.ToShortTimeString();
        }

        private void frmLock_MouseDown(object sender, MouseEventArgs e)
        {
            move = 1;
            valX = e.X;
            valY = e.Y;
        }

        private void frmLock_MouseMove(object sender, MouseEventArgs e)
        {
            if (move == 1)
                this.SetDesktopLocation(MousePosition.X - valX, MousePosition.Y - valY);
        }

        private void frmLock_MouseUp(object sender, MouseEventArgs e)
        {
            move = 0;
        }
    }
}
