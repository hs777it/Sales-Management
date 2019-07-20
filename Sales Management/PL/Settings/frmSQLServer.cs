using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sales_Management.PL.Settings
{
    public partial class frmSQLServer : Form
    {
        SqlConnection conn;
        SqlCommand command;
        // SqlDataReader reader;
        string sql = "";
        //string connectionString = "";

        public frmSQLServer()
        {
            InitializeComponent();
        }
        private string GetConnectionString(bool includeDatabase)
        {
            string connectionString;
            if (this.windowsAuthentication.Checked)
                connectionString = string.Format("Server={0}; Integrated Security=SSPI;", cmbServers.Text);
            else
                connectionString = string.Format("Server={0}; User ID={1}; Password={2};", cmbServers.Text, usernameTextBox.Text, passwordTextBox.Text);

            if (includeDatabase && !string.IsNullOrEmpty(this.cmbDatabases.Text))
                connectionString += string.Format(" Database={0};", this.cmbDatabases.Text);

            return connectionString;
        }
        private void cmbServers_DropDown(object sender, EventArgs e)
        {
            if (this.cmbServers.Items.Count > 0)
                return;
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server");
            String[] instances = (String[])rk.GetValue("InstalledInstances");
            List<string> ss = new List<string>();
            if (instances.Length > 0)
            {
                foreach (String element in instances)
                {
                    if (element == "MSSQLSERVER")
                        cmbServers.Items.Add("(local)");
                    else
                        cmbServers.Items.Add(Environment.MachineName + @"\" + element);
                }
                btnBackup.Enabled = true;
                btnRestore.Enabled = true;
            }
            else
            {
                btnBackup.Enabled = false;
                btnRestore.Enabled = false;
            }

            //DataTable instances = SqlDataSourceEnumerator.Instance.GetDataSources(); 2000 - 2005
            //foreach (DataRow row in instances.Rows)
            //{
            //    string name = row["ServerName"].ToString();
            //    if (row["InstanceName"] != null && row["InstanceName"].ToString() != string.Empty)
            //        name += string.Format(@"\{0}", row["InstanceName"]);
            //    this.sqlServerComboBox.Items.Add(name);

        }

        private void sqlServerAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            usernameTextBox.Enabled =
              passwordTextBox.Enabled =
              sqlServerAuthentication.Checked;
        }

        private void testConnectionButton_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.cmbServers.Text))
            {
                MessageBox.Show("An SQL server must be specified.");
                return;
            }

            if (sqlServerAuthentication.Checked && string.IsNullOrEmpty(this.usernameTextBox.Text))
            {
                MessageBox.Show("If SQL server authentication is used, a username must be provided.");
                return;
            }

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.GetConnectionString(true)))
                {
                    sqlConnection.Open();
                    sqlConnection.Close();
                    MessageBox.Show("Connection successful!");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Connection failed: " + ex.Message);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtBackupFileLoc.Text = dlg.SelectedPath;
            }
        }

        private void btnDBFileBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Backup File(*.bak)|*.bak|All Files(*.*)|*.*";
            dlg.FilterIndex = 0;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtRestoreFileLoc.Text = dlg.FileName;
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDatabases.Text.CompareTo("") == 0)
                {
                    MessageBox.Show("Please select a Databse. ");
                    return;
                }
                conn = new SqlConnection(GetConnectionString(true));
                conn.Open();
                sql = "BACKUP DATABASE " + cmbDatabases.Text + " TO DISK ='" + txtBackupFileLoc.Text + "\\" + cmbDatabases.Text + "_" + DateTime.Now.Ticks.ToString() + ".bak'";
                command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();

                conn.Close();
                conn.Dispose();

                MessageBox.Show("Successfully Database Backup Completed. ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbDatabases.Text.CompareTo("") == 0)
                {
                    MessageBox.Show("Please select a Databse. ");
                    return;
                }
                conn = new SqlConnection(GetConnectionString(false));
                conn.Open();
                sql = "Alter Database " + cmbDatabases.Text + " Set SINGLE_USER WITH ROLLBACK IMMEDIATE;";
                sql += "Restore Database " + cmbDatabases.Text + " FROM DISK='" + txtRestoreFileLoc.Text + "' WITH REPLACE";
                command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();

                conn.Close();
                conn.Dispose();

                MessageBox.Show("Successfully Restore Database. ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbDatabases_DropDown(object sender, EventArgs e)
        {
            if (cmbServers.Text == string.Empty)
            {
                cmbDatabases.Items.Clear();
                cmbDatabases.Items.Add("");
                return;
            }
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(this.GetConnectionString(false)))
                {
                    sqlConnection.Open();
                    DataTable databaseList = sqlConnection.GetSchema("Databases");
                    sqlConnection.Close();

                    this.cmbDatabases.Items.Clear();
                    foreach (DataRow row in databaseList.Rows)
                        this.cmbDatabases.Items.Add(row["database_name"]);
                }
            }
            catch (SqlException)
            {
                this.cmbDatabases.Items.Clear();
            }
        }

        private void cmbServers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmSQLServer_Load(object sender, EventArgs e)
        {

        }
    }
}
