using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace lab07
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=PC832;Initial Catalog=Category;Integrated Security=True";

            try
            {
                // Sử dụng using để đảm bảo tài nguyên được giải phóng đúng cách
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open(); // Mở kết nối
                    string query = "SELECT ID, Tên, Loại FROM danhsach";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            DisplayCategory(sqlDataReader); // Hiển thị danh mục
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}"); // Hiển thị thông báo lỗi
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi không mong muốn xảy ra: {ex.Message}"); // Xử lý lỗi không xác định
            }
        }

        private void DisplayCategory(SqlDataReader reader)
        {
            lvCatetory.Items.Clear(); // Xóa danh sách cũ
            while (reader.Read())
            {
                // Tạo một mục mới cho ListView
                ListViewItem item = new ListViewItem(reader["ID"].ToString());
                item.SubItems.Add(reader["Tên"].ToString());
                item.SubItems.Add(reader["Loại"].ToString());
                lvCatetory.Items.Add(item); // Thêm mục vào ListView
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=PC832;Initial Catalog=Category;Integrated Security=True";

            // Lấy dữ liệu từ các TextBox
            string id = txtID.Text; // Giả sử bạn có một TextBox tên là txtID
            string name = txtTen.Text; // TextBox cho tên
            string type = txtLoai.Text; // TextBox cho loại

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string query = "INSERT INTO danhsach (ID, Tên, Loại) VALUES (@ID, @Tên, @Loại)";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        // Thêm tham số vào câu truy vấn
                        sqlCommand.Parameters.AddWithValue("@ID", id);
                        sqlCommand.Parameters.AddWithValue("@Tên", name);
                        sqlCommand.Parameters.AddWithValue("@Loại", type);

                        // Thực thi câu lệnh
                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Không thể thêm bản ghi.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi không mong muốn xảy ra: {ex.Message}");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
                string connectionString = "Data Source=PC832;Initial Catalog=Category;Integrated Security=True";
                // Lấy ID từ TextBox
                string idToDelete = txtID.Text; // Giả sử bạn có một TextBox tên là txtID
                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        sqlConnection.Open();
                        string query = "DELETE FROM danhsach WHERE ID = @ID";
                        using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                        {
                            // Thêm tham số vào câu truy vấn
                            sqlCommand.Parameters.AddWithValue("@ID", idToDelete);

                            // Thực thi câu lệnh
                            int rowsAffected = sqlCommand.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa thành công!");
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy bản ghi với ID đã cho.");
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Có lỗi không mong muốn xảy ra: {ex.Message}");
                }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=PC832;Initial Catalog=Category;Integrated Security=True";

            // Lấy dữ liệu từ các TextBox
            string idToUpdate = txtID.Text; // ID của bản ghi cần cập nhật
            string newName = txtTen.Text;   // Tên mới
            string newType = txtLoai.Text;   // Loại mới

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string query = "UPDATE danhsach SET Tên = @Tên, Loại = @Loại WHERE ID = @ID";
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        // Thêm tham số vào câu truy vấn
                        sqlCommand.Parameters.AddWithValue("@ID", idToUpdate);
                        sqlCommand.Parameters.AddWithValue("@Tên", newName);
                        sqlCommand.Parameters.AddWithValue("@Loại", newType);

                        // Thực thi câu lệnh
                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cập nhật thành công!");
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy bản ghi với ID đã cho.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi không mong muốn xảy ra: {ex.Message}");
            }
        }
    }
}
