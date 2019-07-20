using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
namespace Sales_Management
{
    class Database
    {
        string srv = Properties.Settings.Default.ServerName;
        string db = Properties.Settings.Default.DatabaseName;
        string userid = Properties.Settings.Default.DatabaseUserName;
        string pwd = Properties.Settings.Default.DatabasePassword;
        //connection to database
        SqlConnection conn;
        SqlCommand cmd = new SqlCommand();
        public Database()
        {
            if (Properties.Settings.Default.Authentication == "Windows")
                conn = new SqlConnection(@"Data Source=" + srv + ";Initial Catalog=" + db + ";Integrated Security=True");
            else
                conn = new SqlConnection(@"Data Source=" + srv + ";Initial Catalog=" + db + ";Integrated Security=False;User Id=" + userid + ";Password=" + pwd + " ");
        }

        // select 
        public DataTable readData(string stmt, string message)
        {
            DataTable tbl = new DataTable();
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = stmt;
                conn.Open();
                //load data from database to tbl 
                tbl.Load(cmd.ExecuteReader());

                conn.Close();
                if (message != "")
                {
                    MessageBox.Show(message, "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return tbl;
        }

        // insert update delete 
        public bool exceuteData(string stmt, string message)
        {
            try
            {
                cmd.Connection = conn;
                cmd.CommandText = stmt;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                if (message != "")
                {
                    MessageBox.Show(message, "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                conn.Close();
            }

        }

    }
}
