﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sales_Management.PL
{
    public partial class frmStartup : DevExpress.XtraEditors.XtraForm
    {
        public frmStartup()
        {
            InitializeComponent();
        }

        private void frmStartup_Load(object sender, EventArgs e)
        {
           this.Size = new Size(0, 0);
            new Frm_Login().Show();
        }
    }
}
