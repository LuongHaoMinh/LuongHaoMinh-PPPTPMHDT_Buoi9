using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _LuongHaoMinh_1150080066_Buoi9
{
    public partial class Form3 : Form
    {
        string strCon = @"Data Source=DESKTOP-AHL97Q6\SQL;Initial Catalog=QuanLyBanSach;Integrated Security=True";
        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;
        int vt = -1; // vị trí dòng được chọn

        public Form3()
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

        // Hiển thị dữ liệu lên DataGridView
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

        // Xóa ô nhập
        private void XoaForm()
        {
            txtMaXB.Clear();
            txtTenXB.Clear();
            txtDiaChi.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThiDuLieu();
            XoaForm();
        }

        // Khi click vào dòng trong DataGridView
        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vt = e.RowIndex;
            if (vt < 0) return;

            DataRow row = ds.Tables["tblNhaXuatBan"].Rows[vt];
            txtMaXB.Text = row["MaXB"].ToString();
            txtTenXB.Text = row["TenXB"].ToString();
            txtDiaChi.Text = row["DiaChi"].ToString();
        }

        // Nút chỉnh sửa
        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            if (vt < 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để chỉnh sửa!");
                return;
            }

            DataRow row = ds.Tables["tblNhaXuatBan"].Rows[vt];
            row.BeginEdit();
            row["MaXB"] = txtMaXB.Text.Trim();
            row["TenXB"] = txtTenXB.Text.Trim();
            row["DiaChi"] = txtDiaChi.Text.Trim();
            row.EndEdit();

            int kq = adapter.Update(ds.Tables["tblNhaXuatBan"]);

            if (kq > 0)
            {
                MessageBox.Show("Cập nhật dữ liệu thành công!");
                HienThiDuLieu();
                XoaForm();
            }
            else
            {
                MessageBox.Show("Cập nhật dữ liệu thất bại!");
            }
        }
    }
}
