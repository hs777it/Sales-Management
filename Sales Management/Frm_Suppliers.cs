﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Sales_Management
{
    public partial class Frm_Suppliers : DevExpress.XtraEditors.XtraForm
    {
        public Frm_Suppliers()
        {
            InitializeComponent();
        }

        Database db = new Database();
        DataTable tbl = new DataTable();
        private void AutoNumber()
        {
            tbl.Clear();
            tbl = db.readData("select max (Sup_ID) from Suppliers", "");

            if ((tbl.Rows[0][0].ToString() == DBNull.Value.ToString()))
            {

                txtID.Text = "1";
            }
            else
            {

                txtID.Text = (Convert.ToInt32(tbl.Rows[0][0]) + 1).ToString();
            }

            txtName.Clear();
            txtNotes.Clear();
            txtPhone.Clear();
            txtSearch.Clear();
            txtAddress.Clear();
            btnAdd.Enabled = true;
            btnNew.Enabled = true;
            btnDelete.Enabled = false;
            btnDeleteAll.Enabled = false;
            btnSave.Enabled = false;

        }

        int row;
        private void ShowData()
        {
            tbl.Clear();
            tbl = db.readData("select * from Suppliers", "");

            if (tbl.Rows.Count <= 0)
            {
                MessageBox.Show("لا يوجد بيانات فى هذه الشاشه");
            }
            else
            {
                txtID.Text = tbl.Rows[row][0].ToString();
                txtName.Text = tbl.Rows[row][1].ToString();
                txtAddress.Text = tbl.Rows[row][2].ToString();
                txtPhone.Text = tbl.Rows[row][3].ToString();
                txtNotes.Text = tbl.Rows[row][4].ToString();
            }

            btnAdd.Enabled = false;
            btnNew.Enabled = true;
            btnDelete.Enabled = true;
            btnDeleteAll.Enabled = true;
            btnSave.Enabled = true;
        }

        private void Frm_Suppliers_Load(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("من فضلك ادخل اسم المورد");
                return;
            }
            db.exceuteData("insert into Suppliers Values (" + txtID.Text + " ,N'" + txtName.Text + "' ,N'" + txtAddress.Text + "'  ,N'" + txtPhone.Text + "' ,N'" + txtNotes.Text + "')", "تم الادخال بنجاح");

            AutoNumber();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            row = 0;
            ShowData();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            tbl.Clear();
            tbl = db.readData("select count(Sup_ID) from Suppliers", "");
            row = Convert.ToInt32(tbl.Rows[0][0]) - 1;
            ShowData();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tbl.Clear();
            tbl = db.readData("select count(Sup_ID) from Suppliers", "");
            if (Convert.ToInt32(tbl.Rows[0][0]) - 1 == row)
            {
                row = 0;
                ShowData();
            }
            else
            {
                row++;
                ShowData();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (row == 0)
            {
                tbl.Clear();
                tbl = db.readData("select count(Sup_ID) from Suppliers", "");
                row = Convert.ToInt32(tbl.Rows[0][0]) - 1;
                ShowData();
            }
            else
            {


                row--;
                ShowData();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AutoNumber();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            db.readData("update Suppliers set Sup_Name=N'" + txtName.Text + "' ,Sup_Address =N'" + txtAddress.Text + "' ,Sup_Phone=N'" + txtPhone.Text + "' , Notes=N'" + txtNotes.Text + "' where Sup_ID=" + txtID.Text + "", "تم تعديل البيانات بنجاح");
            AutoNumber();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.readData("delete from Suppliers where Sup_ID=" + txtID.Text + "", "تم مسح المورد بنجاح");
                AutoNumber();
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.readData("delete from Suppliers ", "تم مسح جميع البيانات بنجاح");
                AutoNumber();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable tblSearch = new DataTable();
            tblSearch.Clear();
            tblSearch = db.readData("select * from Suppliers where Sup_Name like  N'%" + txtSearch.Text + "%'   ", "");

            try
            {
                txtID.Text = tblSearch.Rows[0][0].ToString();
                txtName.Text = tblSearch.Rows[0][1].ToString();
                txtAddress.Text = tblSearch.Rows[0][2].ToString();
                txtPhone.Text = tblSearch.Rows[0][3].ToString();
                txtNotes.Text = tblSearch.Rows[0][4].ToString();
            }
            catch (Exception)
            {


            }
            btnAdd.Enabled = false;
            btnNew.Enabled = true;
            btnDelete.Enabled = true;
            btnDeleteAll.Enabled = true;
            btnSave.Enabled = true;
        }

        private void Frm_Suppliers_FormClosing(object sender, FormClosingEventArgs e)
        {
            try {
                Frm_Buy.GetFormBuy.FillSupplier();


            } catch (Exception) { }
        }
    }
}