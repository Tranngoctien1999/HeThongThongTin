using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace QuanLyKhenThuong
{
    public partial class FormLogin : DevExpress.XtraEditors.XtraForm
    {
        public static string connectionString =
            QuanLyKhenThuong.Properties.Settings.Default["QLKTConnectionString"].ToString();
        public FormLogin()
        {
            InitializeComponent();
        }
        private string tenNguoiDung;
        private string matKhau;
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                if (parameter != null)
                {
                    string[] listQuery = query.Split(' ');
                    int i = 0;
                    foreach (string item in listQuery)
                    {
                        if (item.Contains("@"))
                        {
                            string newItem = item.Trim(',');
                            cmd.Parameters.AddWithValue(newItem, parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            return dt;
        }
        public DataTable DataSource_Login(string _userName, string _passWork)
        {
            string query = @"exec  SP_GetInfoByUsername @USERNAME, @PASS";
            return ExecuteQuery(query, new object[] { _userName, _passWork });
        }
       
        private void getInfomationByUserName(string _userName, string _passWork)
        {
            DataTable dt = new DataTable();
            dt = DataSource_Login(_userName, _passWork);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng");
            }
            else
            {
                
                tenNguoiDung = dt.Rows[0]["TenNguoiDung"].ToString().Trim(' ');
                matKhau = dt.Rows[0]["MatKhau"].ToString().Trim(' ');
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();

            }
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
        private void simpleButtonDN_Click(object sender, EventArgs e)
        {
            string username = textEditTenDN.Text.Trim(' ');
            string password = textEditMK.Text.Trim(' ');
            string passwords = MD5Hash(password);
            getInfomationByUserName(username, passwords);
        }

        private void simpleButtonThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn chắc chắn muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                Application.Exit();
            }
        }
    }
}