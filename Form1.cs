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
using System.Configuration;

namespace PROCEDURE
{
    public partial class Form1 : Form
    {
 
        public Form1()
        {
            InitializeComponent();
        }
        private void LayDSHS()
        {
            //khởi tạo các đối tượng sqlConnection, sqladapter, datatable
            SqlConnection conn = new SqlConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            //lấy chuồi kết nối từ app.config
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

            try
            {
                //mở chuỗi kết nối
                conn.Open();
                //khái báo đối tương sqlCommand trong SqlAdapter
                da.SelectCommand = new SqlCommand();
                //gọi thủ tục từ SQL
                da.SelectCommand.CommandText = "hienthi";
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //gán chuỗi kết nối
                da.SelectCommand.Connection = conn;
                //sử dụng phương thức fill  dể gán dữ liệu vào từ database vào sqldataAdapter
                da.Fill(dt);
                //gán dữ liệu vào datagridview
                dgvDSHS.DataSource = dt;
                //đóng chuỗi kết nối


            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            LayDSHS();
        }

        private void reset()
        {
            txtEmail.Text = "";
            txtMSHS.Text = "";
            txtSDT4.Text = "";
            txtTen.Text = "";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            reset();
        }

        private bool KTThongtin()
        {
            //kiểm tra xem txtten có bị trống
            if(txtTen.Text == "")
            {
                MessageBox.Show("vui lòng nhập tên học sinh", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTen.Focus();
                return false;
            }
            //kiểm tra xem txtSDT có bị trống
            if (txtSDT4.Text == "")
            {
                MessageBox.Show("vui lòng nhập số điện thoại", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSDT4.Focus();
                return false;
            }
            //kiểm tra xem txtmshs có bị trống
            if (txtMSHS.Text == "")
            {
                MessageBox.Show("vui lòng nhập mã học sinh", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMSHS.Focus();
                return false;
            }
            //kiểm tra xem txtten có bị trống
            if (txtEmail.Text == "")
            {
                MessageBox.Show("vui lòng nhập Email liên lạc của học sinh", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(KTThongtin())
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "themHS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = txtMSHS.Text;

                    cmd.Parameters.Add("@Ten", SqlDbType.NVarChar).Value = txtTen.Text;
                    cmd.Parameters.Add("@Mail", SqlDbType.NVarChar).Value = txtEmail.Text;
                    cmd.Parameters.Add("@mobile", SqlDbType.NVarChar).Value = txtSDT4.Text;

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSHS();
                    reset();
                    MessageBox.Show("thêm thành công học sinh!", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    


                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dgvDSHS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dgvDSHS.Rows[e.RowIndex];
            txtMSHS.Text = Convert.ToString(row.Cells["Id"].Value);
            txtTen.Text = Convert.ToString(row.Cells["Name"].Value);
            txtEmail.Text = Convert.ToString(row.Cells["Email"].Value);
            txtSDT4.Text = Convert.ToString(row.Cells["Mobile"].Value);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(txtMSHS.Text =="")
            {
                MessageBox.Show("vui lòng nhập mã học sinh cần xoá", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMSHS.Focus();
            } else
            {
                try
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "xoaHS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MaHS", SqlDbType.Int).Value = Convert.ToInt32(txtMSHS.Text);

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LayDSHS();
                    reset();
                    MessageBox.Show("thêm thành công học sinh!", "thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
