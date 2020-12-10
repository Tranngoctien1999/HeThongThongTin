using DevExpress.XtraEditors;
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
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.Data;
using DevExpress.Data.Browsing.Design;
using DevExpress.Utils.Svg;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Filtering.Templates;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraNavBar;

namespace QuanLyKhenThuong
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public static string connectionString =
            QuanLyKhenThuong.Properties.Settings.Default["QLKTConnectionString"].ToString();
        public Form1()
        {
            InitializeComponent();
            ribbonControl.ApplicationCaption = "Quản Lý Khen Thưởng";
            ribbonControl.RibbonStyle = RibbonControlStyle.Office2019;
            pictureEditCadet.Properties.SizeMode = PictureSizeMode.Stretch;
            pictureEditCadet.Properties.OptionsMask.MaskType = PictureEditMaskType.Circle;
            InitNavBarControl();
            InitGridControlCadet(null);
        }
        void navBarControl_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            navigationFrame.SelectedPageIndex = navBarControl.Groups.IndexOf(e.Group);
        }
        void barButtonNavigation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
            navBarControl.ActiveGroup = navBarControl.Groups[barItemIndex];
        }

        void InitNavBarControl()
        {
            NavBarItemLink item = CadetsNavBarGroup.AddItem();
            item.Item.Name = "ANHTTT53";
            item.Item.Caption = "Lớp ANHTTT";
            item.Item.LinkClicked += (sender, args) => { InitGridControlCadet(item.Item.Name); };
            NavBarItemLink item1 = ManagersNavBarGroup.AddItem();
            item1.Item.Caption = "Cán Bộ Đại Đội";
        }

        void InitGridControlCadet(string maLop)
        {
            string query = "";
            if (maLop == null)
            {
                query = "EXEC dbo.LayThongTinDiemCacKiCuaHocVien";
                
            }
            else
            {
                query = string.Format("EXECUTE dbo.LayThongTinDiemCacKiCuaHocVienTheoLop {0}" , maLop);
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    gridControlCadet.DataSource = table;
                    GridColumn colID = gridViewCadet.Columns["HocVienID"];
                    GridColumn colName = gridViewCadet.Columns["TenHocVien"];
                    GridColumn colQue = gridViewCadet.Columns["QueQuan"];
                    GridColumn colNgaySinh = gridViewCadet.Columns["NgaySinh"];
                    GridColumn colTenLop = gridViewCadet.Columns["TenLop"];
                    GridColumn colHocKy = gridViewCadet.Columns["HocKy"];
                    GridColumn colHocKyId = gridViewCadet.Columns["HocKyID"];
                    GridColumn colNamHocId = gridViewCadet.Columns["NamHocID"];
                    GridColumn colTongKet = gridViewCadet.Columns["TongKet"];
                    GridColumn colNam = gridViewCadet.Columns["Nam"];
                    GridColumn colDuongDanAnh = gridViewCadet.Columns["DuongDanAnh"];
                    GridColumn colTangTruong = gridViewCadet.Columns["TangTruong"];
                    colID.Visible = false;
                    colHocKyId.Visible = false;
                    colNamHocId.Visible = false;
                    colDuongDanAnh.Visible = false;
                    colName.Caption = "Tên Học Viên";
                    colQue.Caption = "Quê Quán";
                    colNgaySinh.Caption = "Ngày Sinh";
                    colTenLop.Caption = "Lớp";
                    colHocKy.Caption = "Học Kì";
                    colTongKet.Caption = "Điểm Tổng Kết Kì";
                    colNam.Caption = "Năm Học";
                    colNam.GroupIndex = 0;
                    colHocKy.GroupIndex = 1;
                    colNgaySinh.Summary.Clear();
                    colNgaySinh.Summary.Add(SummaryItemType.Count, colNgaySinh.FieldName, "Tổng Số : {0}");
                    colNgaySinh.Summary.Add(SummaryItemType.Max, colNgaySinh.FieldName, "Độ Tuổi : <= {0:d}");
                    gridViewCadet.OptionsView.ShowFooter = true;
                    gridViewCadet.FindPanelVisible = true;
                    gridViewCadet.OptionsFind.HighlightFindResults = true;


                    //Change Format Rule

                    GridFormatRule gridFormatRule = new GridFormatRule();
                    FormatConditionRuleIconSet formatConditionRuleIconSet = new FormatConditionRuleIconSet();
                    FormatConditionIconSet iconSet = formatConditionRuleIconSet.IconSet = new FormatConditionIconSet();
                    FormatConditionIconSetIcon icon1 = new FormatConditionIconSetIcon();
                    FormatConditionIconSetIcon icon2 = new FormatConditionIconSetIcon();
                    FormatConditionIconSetIcon icon3 = new FormatConditionIconSetIcon();

                    //Choose predefined icons.
                    icon1.PredefinedName = "Arrows3_1.png";
                    icon2.PredefinedName = "Arrows3_2.png";
                    icon3.PredefinedName = "Arrows3_3.png";

                    //Specify the type of threshold values.
                    iconSet.ValueType = FormatConditionValueType.Number;

                    //Define ranges to which icons are applied by setting threshold values.
                    icon1.Value = 30; // target range: 30% <= value
                    icon1.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                    icon2.Value = 10; // target range: 10% <= value < 30%
                    icon2.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                    icon3.Value = -100; // target range: -100% <= value < 10%
                    icon3.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;

                    //Add icons to the icon set.
                    iconSet.Icons.Add(icon1);
                    iconSet.Icons.Add(icon2);
                    iconSet.Icons.Add(icon3);

                    //Specify the rule type.
                    gridFormatRule.Rule = formatConditionRuleIconSet;
                    //Specify the column to which formatting is applied.
                    gridFormatRule.Column = colTangTruong;
                    //Add the formatting rule to the GridView.
                    gridViewCadet.FormatRules.Add(gridFormatRule);
                    gridViewCadet.ExpandAllGroups();
                }
            }
        }


        private void gridViewCadet_RowClick(object sender, RowClickEventArgs e)
        {
            if ((sender as GridView).GetFocusedRowCellValue("DuongDanAnh") != null)
            {
                string duongDanAnh = (sender as GridView).GetFocusedRowCellValue("DuongDanAnh").ToString();
                if(!string.IsNullOrEmpty(duongDanAnh))
                pictureEditCadet.Image = Image.FromFile(duongDanAnh);
                labelControlName.Text = (sender as GridView).GetFocusedRowCellValue("TenHocVien").ToString();
                if(((sender as GridView).GetFocusedRowCellValue("NgaySinh") as DateTime?).HasValue)
                labelControlNgaySinh.Text = "Ngày sinh :" + 
                    ((sender as GridView).GetFocusedRowCellValue("NgaySinh") as DateTime?).Value.Date.ToString("dd/MM/yyyy");
                string ngaySinh = (sender as GridView).GetFocusedRowCellValue("TenLop").ToString();
                if (!string.IsNullOrEmpty(ngaySinh))
                {
                    labelControlLop.Text = "Lớp : " + ngaySinh;
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QuanLyViPham quanLyViPham = new QuanLyViPham();
            quanLyViPham.ShowDialog();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PhanTich phanTich = new PhanTich();
            phanTich.ShowDialog();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void gridViewCadet_DoubleClick(object sender, EventArgs e)
        {
            if ((sender as GridView).GetFocusedRowCellValue("HocVienID") != null)
            {
                string maHocVien = (sender as GridView).GetFocusedRowCellValue("HocVienID").ToString();
                if (!string.IsNullOrEmpty(maHocVien))
                {
                    DetailCadet detailCadet = new DetailCadet(maHocVien);
                    detailCadet.ShowDialog();
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QuanLyHoatDongTichCuc quanLyHoatDongTichCuc = new QuanLyHoatDongTichCuc();
            quanLyHoatDongTichCuc.ShowDialog();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            QuanLyKiLuat quanLyKiLuat = new QuanLyKiLuat();
            quanLyKiLuat.ShowDialog();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormQuanLyKhenThuong quanLyKhenThuong = new FormQuanLyKhenThuong();
            quanLyKhenThuong.ShowDialog();
        }
    }
}