using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace QuanLyKhenThuong
{
    public partial class PhanTich : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private GridControl gridControl;
        public string MaHocVien {
            get { return MaHocVien; }
            set
            {
                MaHocVien = value;
                LoadThongTinChiTiet();
            }
        }
        
        public PhanTich()
        {
            InitializeComponent();
        }
        static List<DetailCadet.HocVien> GetDataSource(string maHocVien)
        {
            DetailCadet.HocVien hocVien = new DetailCadet.HocVien();
            string query = string.Format("EXEC dbo.LayThongTinHocVienTuMaHocVien {0}", maHocVien);
            using (SqlConnection connection = new SqlConnection(Form1.connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    hocVien.ID = table.Rows[0].Field<string>((int)DetailCadet.ThongTinHocVien.HocVienID);
                    hocVien.LastName = table.Rows[0].Field<string>((int)DetailCadet.ThongTinHocVien.TenHocVien);
                    hocVien.AddressLine = table.Rows[0].Field<string>((int)DetailCadet.ThongTinHocVien.QueQuan);
                    hocVien.BirthDate = table.Rows[0].Field<DateTime>((int)DetailCadet.ThongTinHocVien.NgaySinh);
                    hocVien.Classname = table.Rows[0].Field<string>((int)DetailCadet.ThongTinHocVien.TenLop);
                    hocVien.Image = Image.FromFile(table.Rows[0].Field<string>((int)DetailCadet.ThongTinHocVien.DuongDanAnh));
                }
            }

            List<DetailCadet.HocVien> result = new List<DetailCadet.HocVien>();
            result.Add(hocVien);
            return result;
        }

        static DataTable getMarkDataWithID(string maHocVien)
        {
            string query = string.Format("EXEC dbo.LayDiemCacHocKiVoiMaHocVien {0}", maHocVien);
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

        public void LoadThongTinChiTiet()
        {
            dataLayoutControl1.DataSource = GetDataSource(MaHocVien);
            dataLayoutControl1.RetrieveFields();
        }

        private void PhanTich_Load(object sender, EventArgs e)
        {
            LoadThongTinChiTiet();
            LayoutControlGroup newGroup = dataLayoutControl1.Root.AddGroup();
            LayoutControlItem item1 = newGroup.AddItem();
            gridControl = new GridControl();
            gridControl.Dock = DockStyle.Fill;
            gridControl.DataSource = getMarkDataWithID(MaHocVien);
            item1.Control = gridControl;
            GridView gridView1 = gridControl.MainView as GridView;
            GridColumn colHocKy = gridView1.Columns["HocKy"];
            GridColumn colNam = gridView1.Columns["Nam"];
            GridColumn colDiem = gridView1.Columns["Diem"];
            colNam.GroupIndex = 0;
            colHocKy.GroupIndex = 1;
            gridView1.ExpandAllGroups();

            //Change Format Rule

            GridFormatRule gridFormatRule = new GridFormatRule();
            FormatConditionRuleIconSet formatConditionRuleIconSet = new FormatConditionRuleIconSet();
            FormatConditionIconSet iconSet = formatConditionRuleIconSet.IconSet = new FormatConditionIconSet();
            FormatConditionIconSetIcon icon1 = new FormatConditionIconSetIcon();
            FormatConditionIconSetIcon icon2 = new FormatConditionIconSetIcon();
            FormatConditionIconSetIcon icon3 = new FormatConditionIconSetIcon();
            FormatConditionIconSetIcon icon4 = new FormatConditionIconSetIcon();

            //Choose predefined icons.
            icon1.PredefinedName = "Rating4_1.png";
            icon2.PredefinedName = "Rating4_2.png";
            icon3.PredefinedName = "Rating4_3.png";
            icon4.PredefinedName = "Rating4_4.png";

            //Specify the type of threshold values.
            iconSet.ValueType = FormatConditionValueType.Number;

            //Define ranges to which icons are applied by setting threshold values.
            icon1.Value = 8; // target range: 8 <= value
            icon1.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
            icon2.Value = 7; // target range: 7 <= value < 8
            icon2.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
            icon3.Value = 5; // target range: 5 <= value < 7
            icon3.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
            icon4.Value = 0; // target range: 0 <= value < 5
            icon4.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
            //Add icons to the icon set.
            iconSet.Icons.Add(icon1);
            iconSet.Icons.Add(icon2);
            iconSet.Icons.Add(icon3);
            iconSet.Icons.Add(icon4);

            //Specify the rule type.
            gridFormatRule.Rule = formatConditionRuleIconSet;
            //Specify the column to which formatting is applied.
            gridFormatRule.Column = colDiem;
            //Add the formatting rule to the GridView.
            gridView1.FormatRules.Add(gridFormatRule);
        }
    }
}
