using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sales_Management.PL.Settings
{
    public partial class frmServerSettings : Form
    {
        int move; int valX; int valY;
        public frmServerSettings()
        {
            InitializeComponent();
        }

        private void frmServerSettings_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            cmbServers.Text = Properties.Settings.Default.ServerName;
            cmbDatabases.Text = Properties.Settings.Default.DatabaseName;
            usernameTextBox.Text = Properties.Settings.Default.DatabaseUserName;
            passwordTextBox.Text = Properties.Settings.Default.DatabasePassword;
            cbxServerForm.Checked = Properties.Settings.Default.ServerForm;
        }

        private void frmServerSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void frmServerSettings_MouseDown(object sender, MouseEventArgs e)
        {
            move = 1;
            valX = e.X;
            valY = e.Y;
        }

        private void frmServerSettings_MouseMove(object sender, MouseEventArgs e)
        {
            if (move == 1)
                this.SetDesktopLocation(MousePosition.X - valX, MousePosition.Y - valY);
               // this.Location = new Point(MousePosition.X - valX, MousePosition.Y - valY);
        }

        private void frmServerSettings_MouseUp(object sender, MouseEventArgs e)
        {
            move = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.ServerName = cmbServers.Text;
            Properties.Settings.Default.DatabaseName = cmbDatabases.Text;
            Properties.Settings.Default.DatabaseUserName = usernameTextBox.Text;
            Properties.Settings.Default.DatabasePassword = passwordTextBox.Text;
            Properties.Settings.Default.ServerForm = cbxServerForm.Checked;

            if (usernameTextBox.Enabled) Properties.Settings.Default.Authentication = "SQL";
            else Properties.Settings.Default.Authentication = "Windows";
           if(MessageBox.Show("سيتم إعادة تشغيل النظام لحفظ البيانات","",MessageBoxButtons.OKCancel,MessageBoxIcon.Information) == DialogResult.OK)
            {
                Properties.Settings.Default.Save();
                Application.Restart();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtServer_TextChanged(object sender, EventArgs e)
        {
            if (txtServer.Text.Contains("\\"))
                txtUserName.Enabled = txtPassword.Enabled = false;
            else
                txtUserName.Enabled = txtPassword.Enabled = true;
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
                //foreach (String element in instances)
                //{
                //    if (element == "MSSQLSERVER")
                //        cmbServers.Items.Add("(local)");
                //    else
                //        cmbServers.Items.Add(Environment.MachineName + @"\" + element);
                //}
                foreach (String element in instances)
                {
                    if (element == "MSSQLSERVER")
                        cmbServers.Items.Add(".");
                    else
                        cmbServers.Items.Add(@".\" + element);
                }

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
    }
}
