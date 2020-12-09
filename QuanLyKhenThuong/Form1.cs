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
            item.Item.Caption = "Lớp ANHTTT";


            NavBarItemLink item1 = ManagersNavBarGroup.AddItem();
            item1.Item.Caption = "Cán Bộ Đại Đội";
        }

        void InitGridControlCadet(string maLop)
        {
            if (maLop == null)
            {
                var query = "EXEC dbo.LayThongTinDiemCacKiCuaHocVien";
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
                        colNgaySinh.Summary.Add(SummaryItemType.Count, colNgaySinh.FieldName, "Tổng Số : {0}");
                        colNgaySinh.Summary.Add(SummaryItemType.Max, colNgaySinh.FieldName, "Độ Tuổi : <= {0:d}");
                        gridViewCadet.OptionsView.ShowFooter = true;
                        gridViewCadet.FindPanelVisible = true;
                        gridViewCadet.OptionsFind.HighlightFindResults = true;
                    }
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
            }
        }
    }
}