using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _LuongHaoMinh_1150080066_Buoi9
{
    public partial class Form4 : Form
    {
        string strCon = @"Data Source=DESKTOP-AHL97Q6\SQL;Initial Catalog=QuanLyBanSach;Integrated Security=True";
        SqlConnection sqlCon = null;
        SqlDataAdapter adapter = null;
        DataSet ds = null;
        int vt = -1; // vị trí dòng được chọn

        public Form4()
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

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThiDuLieu();
        }

        // Sự kiện click vào DataGridView
        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vt = e.RowIndex;
        }

        // Nút Xóa dữ liệu
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (vt < 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn xóa dòng này không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    DataRow row = ds.Tables["tblNhaXuatBan"].Rows[vt];
                    row.Delete();

                    int kq = adapter.Update(ds.Tables["tblNhaXuatBan"]);
                    if (kq > 0)
                    {
                        MessageBox.Show("Xóa dữ liệu thành công!");
                        HienThiDuLieu();
                    }
                    else
                    {
                        MessageBox.Show("Xóa dữ liệu không thành công!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
    }
}
