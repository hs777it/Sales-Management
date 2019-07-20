using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sales_Management
{
    class codes
    {


        private void button1_Click()
        {
            SqlConnection cn = new SqlConnection(@"Server=.\SQLEXPRESS; DataBase=Library_DB; Integrated Security=true;");
            SqlCommand Cmd;
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Backup Files (*.Bak) |*.bak";

            if (sf.ShowDialog() == DialogResult.OK)
            {
                Cmd = new SqlCommand("Backup Database Library_DB To Disk='" + sf.FileName + "'", cn);
                cn.Open();
                Cmd.ExecuteNonQuery();
                cn.Close();

            }
        }

    }
}
