namespace Sales_Management.PL.Settings
{
    partial class frmSQLServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.sqlServerAuthentication = new System.Windows.Forms.RadioButton();
            this.windowsAuthentication = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDatabases = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbServers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.testConnectionButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnDBFileBrowse = new System.Windows.Forms.Button();
            this.txtRestoreFileLoc = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtBackupFileLoc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.passwordTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.usernameTextBox);
            this.groupBox1.Controls.Add(this.sqlServerAuthentication);
            this.groupBox1.Controls.Add(this.windowsAuthentication);
            this.groupBox1.Location = new System.Drawing.Point(22, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 132);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Authentication";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Enabled = false;
            this.passwordTextBox.Location = new System.Drawing.Point(98, 98);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(201, 20);
            this.passwordTextBox.TabIndex = 13;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Username:";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Enabled = false;
            this.usernameTextBox.Location = new System.Drawing.Point(98, 72);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(201, 20);
            this.usernameTextBox.TabIndex = 10;
            // 
            // sqlServerAuthentication
            // 
            this.sqlServerAuthentication.AutoSize = true;
            this.sqlServerAuthentication.Location = new System.Drawing.Point(9, 44);
            this.sqlServerAuthentication.Name = "sqlServerAuthentication";
            this.sqlServerAuthentication.Size = new System.Drawing.Size(173, 17);
            this.sqlServerAuthentication.TabIndex = 9;
            this.sqlServerAuthentication.Text = "Use SQL Server Authentication";
            this.sqlServerAuthentication.UseVisualStyleBackColor = true;
            this.sqlServerAuthentication.CheckedChanged += new System.EventHandler(this.sqlServerAuthentication_CheckedChanged);
            // 
            // windowsAuthentication
            // 
            this.windowsAuthentication.AutoSize = true;
            this.windowsAuthentication.Checked = true;
            this.windowsAuthentication.Location = new System.Drawing.Point(9, 21);
            this.windowsAuthentication.Name = "windowsAuthentication";
            this.windowsAuthentication.Size = new System.Drawing.Size(162, 17);
            this.windowsAuthentication.TabIndex = 8;
            this.windowsAuthentication.TabStop = true;
            this.windowsAuthentication.Text = "Use Windows Authentication";
            this.windowsAuthentication.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmbDatabases);
            this.groupBox2.Location = new System.Drawing.Point(22, 229);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 59);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Database:";
            // 
            // cmbDatabases
            // 
            this.cmbDatabases.FormattingEnabled = true;
            this.cmbDatabases.Location = new System.Drawing.Point(68, 20);
            this.cmbDatabases.Name = "cmbDatabases";
            this.cmbDatabases.Size = new System.Drawing.Size(231, 21);
            this.cmbDatabases.TabIndex = 12;
            this.cmbDatabases.DropDown += new System.EventHandler(this.cmbDatabases_DropDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbServers);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(22, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(332, 73);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SQL Server";
            // 
            // cmbServers
            // 
            this.cmbServers.FormattingEnabled = true;
            this.cmbServers.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmbServers.Location = new System.Drawing.Point(9, 41);
            this.cmbServers.Name = "cmbServers";
            this.cmbServers.Size = new System.Drawing.Size(290, 21);
            this.cmbServers.TabIndex = 1;
            this.cmbServers.DropDown += new System.EventHandler(this.cmbServers_DropDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "MSSQL Server Name:";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(267, 505);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 37;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // testConnectionButton
            // 
            this.testConnectionButton.Location = new System.Drawing.Point(31, 505);
            this.testConnectionButton.Name = "testConnectionButton";
            this.testConnectionButton.Size = new System.Drawing.Size(109, 23);
            this.testConnectionButton.TabIndex = 35;
            this.testConnectionButton.Text = "Test Connection";
            this.testConnectionButton.UseVisualStyleBackColor = true;
            this.testConnectionButton.Click += new System.EventHandler(this.testConnectionButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(146, 505);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 34;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnRestore);
            this.groupBox4.Controls.Add(this.btnDBFileBrowse);
            this.groupBox4.Controls.Add(this.txtRestoreFileLoc);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(22, 394);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(332, 81);
            this.groupBox4.TabIndex = 39;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Database Restore";
            // 
            // btnRestore
            // 
            this.btnRestore.Enabled = false;
            this.btnRestore.Location = new System.Drawing.Point(245, 43);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(75, 23);
            this.btnRestore.TabIndex = 9;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnDBFileBrowse
            // 
            this.btnDBFileBrowse.Location = new System.Drawing.Point(245, 14);
            this.btnDBFileBrowse.Name = "btnDBFileBrowse";
            this.btnDBFileBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnDBFileBrowse.TabIndex = 8;
            this.btnDBFileBrowse.Text = "Browse";
            this.btnDBFileBrowse.UseVisualStyleBackColor = true;
            this.btnDBFileBrowse.Click += new System.EventHandler(this.btnDBFileBrowse_Click);
            // 
            // txtRestoreFileLoc
            // 
            this.txtRestoreFileLoc.Location = new System.Drawing.Point(18, 38);
            this.txtRestoreFileLoc.Name = "txtRestoreFileLoc";
            this.txtRestoreFileLoc.ReadOnly = true;
            this.txtRestoreFileLoc.Size = new System.Drawing.Size(215, 20);
            this.txtRestoreFileLoc.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Backup Path:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnBackup);
            this.groupBox5.Controls.Add(this.btnBrowse);
            this.groupBox5.Controls.Add(this.txtBackupFileLoc);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Location = new System.Drawing.Point(22, 298);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(332, 80);
            this.groupBox5.TabIndex = 38;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Database Backup";
            // 
            // btnBackup
            // 
            this.btnBackup.Enabled = false;
            this.btnBackup.Location = new System.Drawing.Point(245, 46);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(75, 23);
            this.btnBackup.TabIndex = 9;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(245, 17);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 8;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtBackupFileLoc
            // 
            this.txtBackupFileLoc.Location = new System.Drawing.Point(16, 38);
            this.txtBackupFileLoc.Name = "txtBackupFileLoc";
            this.txtBackupFileLoc.ReadOnly = true;
            this.txtBackupFileLoc.Size = new System.Drawing.Size(217, 20);
            this.txtBackupFileLoc.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Location:";
            // 
            // frmSQLServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 547);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.testConnectionButton);
            this.Controls.Add(this.connectButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSQLServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SQL Server";
            this.Load += new System.EventHandler(this.frmSQLServer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.RadioButton sqlServerAuthentication;
        private System.Windows.Forms.RadioButton windowsAuthentication;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDatabases;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbServers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button testConnectionButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnDBFileBrowse;
        private System.Windows.Forms.TextBox txtRestoreFileLoc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtBackupFileLoc;
        private System.Windows.Forms.Label label5;

    }
}