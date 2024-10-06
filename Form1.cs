using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaiTapTuan5.Model;

namespace BaiTapTuan5
{
    public partial class Form1 : Form
    {
        DbStudentContent dbStudent = new DbStudentContent();
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Student> listStudent = dbStudent.Students.ToList();
            List<Faculty> listFaculty = dbStudent.Faculties.ToList();

            FillDataCBB(listFaculty);
            FillDataDGV(listStudent);
        }

        private void FillDataCBB(List<Faculty> listFaculty)
        {
            cbbKhoa.DataSource = listFaculty;
            cbbKhoa.DisplayMember = "FacultyName";
            cbbKhoa.ValueMember = "FacultyID";
        }

        private void FillDataDGV(List<Student> listStudent)
        {
            dgvDSSV.Rows.Clear();
            foreach (var student in listStudent)
            {
                int RowNEW = dgvDSSV.Rows.Add();
                dgvDSSV.Rows[RowNEW].Cells[0].Value = student.StudentID;
                dgvDSSV.Rows[RowNEW].Cells[1].Value = student.FullName;
                dgvDSSV.Rows[RowNEW].Cells[2].Value = student.Faculty.FacultyName;
                dgvDSSV.Rows[RowNEW].Cells[3].Value = student.AverageScore;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Tạo đối tượng Student mới
            Student newStudent = new Student()
            {
                StudentID = textBox1.Text,
                FullName = textBox2.Text,
                AverageScore = float.Parse(textBox3.Text),
                FacultyID = (int)cbbKhoa.SelectedValue // Lấy FacultyID từ ComboBox
            };

            // Thêm vào database
            dbStudent.Students.Add(newStudent);
            dbStudent.SaveChanges();

            // Cập nhật DataGridView
            FillDataDGV(dbStudent.Students.ToList());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvDSSV.CurrentRow != null)
            {
                // Lấy StudentID của sinh viên đang chọn
                string studentID = dgvDSSV.CurrentRow.Cells[0].Value.ToString();

                // Tìm sinh viên trong cơ sở dữ liệu
                Student studentToDelete = dbStudent.Students.FirstOrDefault(s => s.StudentID == studentID);

                if (studentToDelete != null)
                {
                    // Xóa sinh viên
                    dbStudent.Students.Remove(studentToDelete);
                    dbStudent.SaveChanges();

                    // Cập nhật DataGridView
                    FillDataDGV(dbStudent.Students.ToList());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvDSSV.CurrentRow != null)
            {
                // Lấy StudentID của sinh viên đang chọn
                string studentID = dgvDSSV.CurrentRow.Cells[0].Value.ToString();

                // Tìm sinh viên trong cơ sở dữ liệu
                Student studentToEdit = dbStudent.Students.FirstOrDefault(s => s.StudentID == studentID);

                if (studentToEdit != null)
                {
                    // Cập nhật thông tin sinh viên
                    studentToEdit.FullName = textBox2.Text;
                    studentToEdit.AverageScore = float.Parse(textBox3.Text);
                    studentToEdit.FacultyID = (int)cbbKhoa.SelectedValue;

                    // Lưu thay đổi
                    dbStudent.SaveChanges();

                    // Cập nhật DataGridView
                    FillDataDGV(dbStudent.Students.ToList());
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDSSV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy hàng hiện tại
                DataGridViewRow row = dgvDSSV.Rows[e.RowIndex];

                // Điền dữ liệu vào các TextBox
                textBox1.Text = row.Cells[0].Value.ToString();  // StudentID
                textBox2.Text = row.Cells[1].Value.ToString();  // FullName
                textBox3.Text = row.Cells[3].Value.ToString();  // AverageScore

                // Tìm FacultyID tương ứng và đặt cho ComboBox
                string facultyName = row.Cells[2].Value.ToString();  // FacultyName
                Faculty selectedFaculty = dbStudent.Faculties.FirstOrDefault(f => f.FacultyName == facultyName);
                if (selectedFaculty != null)
                {
                    cbbKhoa.SelectedValue = selectedFaculty.FacultyID;
                }
            }
        }
    }
}

