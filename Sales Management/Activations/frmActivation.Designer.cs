namespace Sales_Management.Activations
{
    partial class frmActivation
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
            this.btnActivation = new System.Windows.Forms.Button();
            this.txtActivationKey = new System.Windows.Forms.TextBox();
            this.txtProductKey = new System.Windows.Forms.TextBox();
            this.txtActivationKeys = new System.Windows.Forms.TextBox();
            this.btnActivationKey = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnActivation
            // 
            this.btnActivation.Location = new System.Drawing.Point(125, 111);
            this.btnActivation.Name = "btnActivation";
            this.btnActivation.Size = new System.Drawing.Size(242, 38);
            this.btnActivation.TabIndex = 10;
            this.btnActivation.Text = "تفعيل البرنامج";
            this.btnActivation.UseVisualStyleBackColor = true;
            this.btnActivation.Click += new System.EventHandler(this.btnActivation_Click);
            // 
            // txtActivationKey
            // 
            this.txtActivationKey.Location = new System.Drawing.Point(47, 75);
            this.txtActivationKey.Name = "txtActivationKey";
            this.txtActivationKey.Size = new System.Drawing.Size(376, 28);
            this.txtActivationKey.TabIndex = 9;
            this.txtActivationKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtProductKey
            // 
            this.txtProductKey.Location = new System.Drawing.Point(47, 31);
            this.txtProductKey.Name = "txtProductKey";
            this.txtProductKey.ReadOnly = true;
            this.txtProductKey.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtProductKey.Size = new System.Drawing.Size(376, 28);
            this.txtProductKey.TabIndex = 11;
            this.txtProductKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtActivationKeys
            // 
            this.txtActivationKeys.Location = new System.Drawing.Point(47, 171);
            this.txtActivationKeys.Name = "txtActivationKeys";
            this.txtActivationKeys.Size = new System.Drawing.Size(376, 28);
            this.txtActivationKeys.TabIndex = 12;
            this.txtActivationKeys.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnActivationKey
            // 
            this.btnActivationKey.Location = new System.Drawing.Point(116, 205);
            this.btnActivationKey.Name = "btnActivationKey";
            this.btnActivationKey.Size = new System.Drawing.Size(242, 32);
            this.btnActivationKey.TabIndex = 13;
            this.btnActivationKey.Text = "إنشاء مفتاح تفعيل";
            this.btnActivationKey.UseVisualStyleBackColor = true;
            this.btnActivationKey.Click += new System.EventHandler(this.btnActivationKey_Click);
            // 
            // frmActivation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 249);
            this.Controls.Add(this.btnActivationKey);
            this.Controls.Add(this.txtActivationKeys);
            this.Controls.Add(this.txtProductKey);
            this.Controls.Add(this.btnActivation);
            this.Controls.Add(this.txtActivationKey);
            this.Font = new System.Drawing.Font("Droid Arabic Kufi", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmActivation";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "تفعيل البرنامج";
            this.Load += new System.EventHandler(this.frmActivation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnActivation;
        internal System.Windows.Forms.TextBox txtActivationKey;
        internal System.Windows.Forms.TextBox txtProductKey;
        internal System.Windows.Forms.TextBox txtActivationKeys;
        private System.Windows.Forms.Button btnActivationKey;
    }
}