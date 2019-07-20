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
    public partial class Frm_StockBankTransfireReport : DevExpress.XtraEditors.XtraForm
    {
        public Frm_StockBankTransfireReport()
        {
            InitializeComponent();
        }
        Database db = new Database();
        DataTable tbl = new DataTable();
        private void Frm_StockBankTransfireReport_Load(object sender, EventArgs e)
        {
            DtpFrom.Text = DateTime.Now.ToShortDateString();
            DtpTo.Text = DateTime.Now.ToShortDateString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");
            tbl.Clear();
            if (rbtnAll.Checked == true)
            {
                tbl = db.readData("SELECT[Order_ID] as 'رقم العملية',[Money] as 'المبلغ' ,[Date] as 'التاريخ',[From_] as 'تحويل من',[To_] as 'تحويل الى',[Name] as 'اسم الشخص المسؤل عن التحويل' FROM  [dbo].[StockBank_Transfire] where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
            }else if (rbtnFromStocktoBank.Checked == true)
            {
                tbl = db.readData("SELECT[Order_ID] as 'رقم العملية',[Money] as 'المبلغ' ,[Date] as 'التاريخ',[From_] as 'تحويل من',[To_] as 'تحويل الى',[Name] as 'اسم الشخص المسؤل عن التحويل' FROM [dbo].[StockBank_Transfire] where To_ = N'البنك' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
                
            }else if (rbtnFromBanktoStock.Checked == true)
            {
                tbl = db.readData("SELECT[Order_ID] as 'رقم العملية',[Money] as 'المبلغ' ,[Date] as 'التاريخ',[From_] as 'تحويل من',[To_] as 'تحويل الى',[Name] as 'اسم الشخص المسؤل عن التحويل' FROM [dbo].[StockBank_Transfire] where From_ = N'البنك' and Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "' ", "");
            }
            if (tbl.Rows.Count >= 1)
            {
                DgvSearch.DataSource = tbl;
                decimal Sum = 0;
                for (int i = 0; i <= tbl.Rows.Count - 1; i++)
                {
                    Sum += Convert.ToDecimal(tbl.Rows[i][1]);
                }

                txtTotal.Text = Math.Round(Sum, 2).ToString();
            }
            else
            { txtTotal.Text = "0"; }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string date1;
            string date2;
            date1 = DtpFrom.Value.ToString("yyyy-MM-dd");
            date2 = DtpTo.Value.ToString("yyyy-MM-dd");
            if (MessageBox.Show("هل انتا متاكد من مسح البيانات", "تاكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                db.exceuteData("delete from StockBank_Transfire where Convert(date,Date ,105 ) between '" + date1 + "' and '" + date2 + "'", "تم مسح البيانات بنجاح");

            }
        }
    }
}