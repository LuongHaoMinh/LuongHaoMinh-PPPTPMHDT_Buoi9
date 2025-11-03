using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _LuongHaoMinh_1150080066_Buoi9
{
    public partial class Form1 : Form
    {
        string strCon = @"Data Source=DESKTOP-AHL97Q6\SQL;Initial Catalog=QuanLyBanSach;Integrated Security=True";



        SqlConnection sqlCon = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void MoKetNoi()
        {
            if (sqlCon == null)
                sqlCon = new SqlConnection(strCon);
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
        }

        private void DongKetNoi()
        {
            if (sqlCon != null && sqlCon.State == ConnectionState.Open)
                sqlCon.Close();
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            try
            {
                MoKetNoi();
                string sql = "SELECT * FROM NhaXuatBan";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, sqlCon);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "tblNhaXuatBan");
                dgvDanhSach.DataSource = ds.Tables["tblNhaXuatBan"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                DongKetNoi();
            }
        }
    }
}
