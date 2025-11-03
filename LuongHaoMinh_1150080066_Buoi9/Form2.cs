using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _LuongHaoMinh_1150080066_Buoi9
{
    public partial class Form2 : Form
    {
        string strCon = @"Data Source=DESKTOP-AHL97Q6\SQL;Initial Catalog=QuanLyBanSach;Integrated Security=True";
        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;

        public Form2()
        {
            InitializeComponent();
        }

        // Mở kết nối
        private void MoKetNoi()
        {
            if (sqlCon == null)
                sqlCon = new SqlConnection(strCon);
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
        }

        // Đóng kết nối
        private void DongKetNoi()
        {
            if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                sqlCon.Close();
        }

        // Xóa dữ liệu nhập
        private void XoaForm()
        {
            txtMaXB.Clear();
            txtTenXB.Clear();
            txtDiaChi.Clear();
        }

        // Hiển thị dữ liệu
        private void HienThiDuLieu()
        {
            MoKetNoi();
            string query = "SELECT * FROM NhaXuatBan";
            adapter = new SqlDataAdapter(query, sqlCon);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            ds = new DataSet();
            adapter.Fill(ds, "tblNhaXuatBan");
            dgvDanhSach.DataSource = ds.Tables["tblNhaXuatBan"];
            DongKetNoi();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThiDuLieu();
            XoaForm();
        }

        // Nút thêm dữ liệu
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = ds.Tables["tblNhaXuatBan"].NewRow();
                row["MaXB"] = txtMaXB.Text.Trim();
                row["TenXB"] = txtTenXB.Text.Trim();
                row["DiaChi"] = txtDiaChi.Text.Trim();

                ds.Tables["tblNhaXuatBan"].Rows.Add(row);
                int kq = adapter.Update(ds.Tables["tblNhaXuatBan"]);

                if (kq > 0)
                {
                    MessageBox.Show("Thêm dữ liệu thành công!");
                    HienThiDuLieu();
                    XoaForm();
                }
                else
                {
                    MessageBox.Show("Thêm dữ liệu không thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
