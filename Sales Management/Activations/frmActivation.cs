using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using zUtilities;

namespace Sales_Management.Activations
{
    public partial class frmActivation : Form
    {
        public frmActivation()
        {
            InitializeComponent();
        }

        string x = "0";
        string serial;
        string signature;
        private void frmActivation_Load(object sender, EventArgs e)
        {
            //this.Height = 200;
            if (Properties.Settings.Default.Product_Key != "No")
            {
                serial = HardwareOp.Identifier("win32_DiskDrive", "SerialNumber").Substring(2);
                signature = HardwareOp.Identifier("win32_DiskDrive", "Signature");

                txtProductKey.Text = KeyGeneratorOp.GetUniqueKey(5) + "-" + serial.Trim() + "-"
                    + signature
                    + "-" + KeyGeneratorOp.GetUniqueKey(5);

                x = KeyGeneratorOp.GetUniqueKey(5) + "-" + serial.Trim() + "-"
                    + (Convert.ToDecimal(signature) * 272 - 159).ToString()
                    + "-" + KeyGeneratorOp.GetUniqueKey(5);

                txtActivationKeys.Text = x;
            }
            else
            {
                MessageBox.Show("البرنامج مفعل");
                new frmMain().ShowDialog();
            }
        }
        

        private void btnActivationKey_Click(object sender, EventArgs e)
        {
            x = txtActivationKeys.Text = KeyGeneratorOp.GetUniqueKey(5) + "-" + serial.Trim() + "-"
                + (Convert.ToDecimal(signature) * 272 - 159).ToString()
                + "-" + KeyGeneratorOp.GetUniqueKey(5);
        }

        private void btnActivation_Click(object sender, EventArgs e)
        {
            if (txtActivationKey.Text == "" || txtActivationKey.Text == string.Empty)
            {
                return;
            }
            if (txtActivationKey.Text == x)
            {
                Properties.Settings.Default.Product_Key = "Yes";
                Properties.Settings.Default.Save();
                MessageBox.Show("تم تفعيل البرنامج", "");
            }
            else
            {
                MessageBox.Show("كود التفعيل خطأ", "");
                return;
            }
        }

        
    }
}
