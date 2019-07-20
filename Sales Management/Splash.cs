using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;

namespace Sales_Management
{
    public partial class Splash : SplashScreen
    {
        public Splash()
        {
            InitializeComponent();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            //marquee.Properties.ProgressViewStyle = DevExpress.XtraEditors.Controls.ProgressViewStyle.Solid;
            //marquee.Properties.EndColor = Color.SteelBlue;
            //marquee.Properties.StartColor = Color.PowderBlue;
            //marquee.Properties.LookAndFeel.SetStyle(DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat, false, false);
            lblCopyright.Text = "Copyright © 2010 - " + DateTime.Now.Year;
        }
    }
}