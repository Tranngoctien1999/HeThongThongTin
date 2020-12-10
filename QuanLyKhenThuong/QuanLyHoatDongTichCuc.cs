using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhenThuong
{
    public partial class QuanLyHoatDongTichCuc : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public QuanLyHoatDongTichCuc()
        {
            InitializeComponent();
            DataTable table = GetDataSource();
            gridControl.DataSource = table;
            bsiRecordsCount.Caption = "Số dữ liệu : " + table.Rows.Count;
        }
        void bbiPrintPreview_ItemClick(object sender, ItemClickEventArgs e)
        {
            gridControl.ShowRibbonPrintPreview();
        }

        DataTable GetDataSource()
        {
            string query = string.Format("EXEC dbo.LayTatCaHoatDongTichCuc");
            using (SqlConnection connection = new SqlConnection(Form1.connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }
    }
}