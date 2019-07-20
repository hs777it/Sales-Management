using System;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace Sales_Management
{
    public partial class Frm_Login : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Login()
        {
            InitializeComponent();
            
            try
            {
                Thread thread = new Thread(new ThreadStart(StartSplash));
                thread.Start();
                Thread.Sleep(2000);
                thread.Abort();
               
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void StartSplash()
        {
            Application.Run(new Splash());
        }


        Database db = new Database();
        DataTable tbl = new DataTable();

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13) SendKeys.Send("{TAB}");
            if (!string.IsNullOrEmpty(txtUserName.Text.Trim()))
                if (e.KeyChar == 13) txtPassword.Focus();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
          if (e.KeyChar == 13 && !string.IsNullOrEmpty(txtPassword.Text.Trim()))
                btnLogin.PerformClick(); ;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()) || string.IsNullOrEmpty(txtPassword.Text.Trim()))
                return;

                tbl.Clear();
            if (rbtnManager.Checked == true)
                tbl = db.readData("select * from Users where User_Name=N'" + txtUserName.Text + "' and User_Password=N'" + txtPassword.Text + "' and Type=N'مدير'", "");
            else if (rbtnEmp.Checked == true)
                tbl = db.readData("select * from Users where User_Name=N'" + txtUserName.Text + "' and User_Password=N'" + txtPassword.Text + "' and Type=N'مستخدم عادى'", "");
            if (tbl.Rows.Count <= 0)
            {
                DataTable tblStock = new DataTable();
                tblStock = db.readData("select * from Stock_Data", "");
                if (tblStock.Rows.Count <= 0)
                {
                    db.exceuteData("insert into Stock_Data Values (1,N'الخزنة الرئيسية') ", "");
                }
                string type = "مدير";
                db.exceuteData("insert into Users values (1 ,N'" + Properties.Settings.Default.USERNAME + "' ,N'" + Properties.Settings.Default.USERNAME + "',N'" + type + "',1,0)", "");
                db.exceuteData("insert into User_Setting Values (1 , 1,1,1,1,1,1,1,1,1,1,1,1)", "");
                db.exceuteData("insert into User_Customer Values (1 , 1,1,1)", "");
                db.exceuteData("insert into User_Supplier Values (1 , 1,1,1)", "");
                db.exceuteData("insert into User_Buy Values (1 , 1,1)", "");
                db.exceuteData("insert into User_Sale Values (1 , 1,1,1)", "");
                db.exceuteData("insert into User_Return Values (1 , 1,1)", "");
                db.exceuteData("insert into User_StockBank Values (1 , 1,1,1,1,1,1,1,1,1)", "");
                db.exceuteData("insert into User_Emp Values (1 , 1,1,1,1,1,1,1)", "");
                db.exceuteData("insert into User_Deserved Values (1 , 1,1,1,1,1,1,1)", "");
                db.exceuteData("insert into User_Report Values (1 , 1,1,1,1,1,1)", "");
                db.exceuteData("insert into User_BackUp Values (1 , 1,1)", "");
                tbl.Clear();
                if (rbtnManager.Checked == true)
                    tbl = db.readData("select * from Users where User_Name=N'" + txtUserName.Text + "' and User_Password=N'" + txtPassword.Text + "' and Type=N'مدير'", "");
                else if (rbtnEmp.Checked == true)
                    tbl = db.readData("select * from Users where User_Name=N'" + txtUserName.Text + "' and User_Password=N'" + txtPassword.Text + "' and Type=N'مستخدم عادى'", "");

            }
            if (tbl.Rows.Count >= 1)
            {
                if (Properties.Settings.Default.Product_Key == "No")
                {
                    if (!Trial(100))
                    {
                        new Activations.frmActivation().ShowDialog();
                        return;
                    }
                }

                Properties.Settings.Default.USERNAME = txtUserName.Text;
                Properties.Settings.Default.Stock_ID = Convert.ToInt32(tbl.Rows[0][4]);
                Properties.Settings.Default.Save();

                #region Activation 1 Year
                DateTime currentDate = DateTime.Now; // activate date
                DateTime expireDate = currentDate;
                //MessageBox.Show(Properties.Settings.Default.ActivationDays+"");
                #endregion

                this.Hide();
                new frmMain().Show();
            }
            else
            {
                MessageBox.Show("كلمة السر او اسم المستخدم  خطا", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
        }

        

        

        private void Frm_Login_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.Product_Key = "Yes";
            Properties.Settings.Default.Save();

            btnServerSettings.Enabled = !Properties.Settings.Default.ServerForm;

            //string ss = @"Data Source=.\SQLEXPRESS;Integrated Security=True";
            //    script = System.IO.File.ReadAllText(Application.StartupPath + @"\sqlscript.sql");
            //    Server server = new Server(new ServerConnection(conn));
            //    server.ConnectionContext.ExecuteNonQuery(script);

            createDatabase();

        }








        bool checkDatabase()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("", conn);
            SqlDataReader dr;
            try
            {
                cmd.CommandText = "execute sys.sp_databases";
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr.GetString(0) == "Sales_System")
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }

            cmd.Dispose();
            conn.Close();
            return false;
        }

        void createDatabase()
        {
            var con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Integrated Security=True");
            if (checkDatabase() != false)
                try
                {
                    string file = System.IO.File.ReadAllText(Application.StartupPath + @"\sqlscript.sql");
                    var sqlQuery = file.Split(new[] { "GO" },StringSplitOptions.RemoveEmptyEntries);
                    
                    var cmd = new SqlCommand("",con);
                    con.Open();
                    foreach (var query in sqlQuery)
                    {
                        cmd.CommandText = query;
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
        }

        bool Trial(int NumOfTimes)
        {
            int trialNumber = Properties.Settings.Default.Trial;
            int times = trialNumber + 1;
            Properties.Settings.Default.Trial = times + 1;
            if (times >= NumOfTimes)
            {
                MessageBox.Show("الفترة التجريبية انتهت", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
            {
                int remainder = NumOfTimes - times;
                MessageBox.Show("هذه نسخة تجريبية - متبقي لك عدد مرات:" + remainder, "",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return true;
        }

        private void btnServerSettings_Click(object sender, EventArgs e)
        {
                new PL.Settings.frmServerSettings().ShowDialog();
        }

        private void Frm_Login_Shown(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }
        //References 
        //* SendKeys.Send *//
        // https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys.send


        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}