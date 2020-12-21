using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Student_Management
{
    public partial class Form1 : Form
    {
        Student student;
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4O3O983;Initial Catalog=studentdb;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
            ShowData();
            btnReset.Click += Reset;
            btnInsert.Click += Insert;
            btnUpdate.Click += Update;
            btnDelete.Click += Delete;
            btnLogout.Click += Logout;
            dgvStudent.CellMouseClick += DgvDataSelector;
            txtSearchName.TextChanged += Search;
        }

        private void Logout(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?", "Are you sure?",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                this.Hide();
                Login login = new Login();
                login.Show();
            }
        }

        private void Search(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from student where s_name like '%" +
                txtSearchName.Text.ToString() + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvStudent.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                int n = dgvStudent.Rows.Add();
                dgvStudent.Rows[n].Cells[0].Value = dr[0].ToString();
                dgvStudent.Rows[n].Cells[1].Value = dr[1].ToString();
                dgvStudent.Rows[n].Cells[2].Value = dr[2].ToString();
                dgvStudent.Rows[n].Cells[4].Value = dr[4].ToString();
                dgvStudent.Rows[n].Cells[5].Value = dr[5].ToString();
                dgvStudent.Rows[n].Cells[6].Value = dr[6].ToString();
            }
            conn.Close();
        }

        private void Delete(object sender, EventArgs e)
        {
            String name = txtStuName.Text.ToString();
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete " + name + "?", "Are you sure?", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(dialogResult == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    String qry = "delete from student where s_name='" + name + "'";
                    SqlCommand sc = new SqlCommand(qry, conn);
                    int i = sc.ExecuteNonQuery();
                    if (i >= 1)
                    {
                        MessageBox.Show(i + " Student Deleted Successfully : " + name, "Deleted",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }   
                    else
                    {
                        MessageBox.Show(" Student  Deletion Failed..... ");
                    }
                    btnReset.PerformClick();
                    ShowData();
                    conn.Close();
                }
                catch (System.Exception exp)
                {
                    MessageBox.Show(" Error is  " + exp.ToString());
                }
            }     
        }

        private void DgvDataSelector(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnReset.PerformClick();
            DataGridViewRow row = dgvStudent.Rows[e.RowIndex];
            if (e.RowIndex >= 0)
            {
                txtStuName.Text = row.Cells[0].Value.ToString().Trim();
                cboGender.SelectedIndex = cboGender.FindStringExact(row.Cells[1].Value.ToString().Trim());
                txtDob.Text = row.Cells[2].Value.ToString().Trim();
                txtStuAddress.Text =row.Cells[3].Value.ToString().Trim();
                txtStuPhone.Text = row.Cells[4].Value.ToString().Trim();
                cboYear.SelectedIndex = cboYear.FindStringExact(row.Cells[5].Value.ToString().Trim());
                cboDepartment.SelectedIndex = cboDepartment.FindStringExact(row.Cells[6].Value.ToString().Trim());
            } 
        }


        private void Update(object sender, EventArgs e)
        {
            try
            {
                student = new Student();
                student.Name = txtStuName.Text;
                student.Gender = cboGender.SelectedItem.ToString();
                student.Dob = txtDob.Text;
                student.Address = txtStuAddress.Text;
                student.Phone = txtStuPhone.Text;
                student.Year = cboYear.SelectedItem.ToString();
                student.Department = cboDepartment.SelectedItem.ToString();

                string qry = "update student set gender='" + student.Gender + "', dob='" + student.Dob + "', s_address='"
                    + student.Address + "', phone='" + student.Phone + "', year='" + student.Year + "', department='" +
                     student.Department + "'where s_name='" + student.Name + "'";

                conn.Open();
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                if (i >= 1)
                {
                    MessageBox.Show(i + " Student Updated Successfully : " + student.Name);
                }      
                else
                {
                    MessageBox.Show(" Student  Updation Fail..... ");
                }

                btnReset.PerformClick();
                conn.Close();
                ShowData();
            }
            catch (System.Exception exp)
            {
                MessageBox.Show(" Error is  " + exp.ToString());
            }
        }
           
        private void Insert(object sender, EventArgs e)
        {
            try
            {
         
                student = new Student();
                student.Name = txtStuName.Text;
                student.Gender = cboGender.SelectedItem.ToString();
                student.Dob = txtDob.Text;
                student.Address = txtStuAddress.Text;
                student.Phone = txtStuPhone.Text;
                student.Year = cboYear.SelectedItem.ToString();
                student.Department = cboDepartment.SelectedItem.ToString();

                string qry = "insert into student values('" + student.Name + "','" + student.Gender + "','" + student.Dob + "','"
                    + student.Address + "','" + student.Phone + "','" + student.Year + "','" + student.Department + "')";

                conn.Open();
                SqlCommand sc = new SqlCommand(qry, conn);

                int i = sc.ExecuteNonQuery();
                if (i >= 1)
                {
                    MessageBox.Show(i + " Student Registerd Successfully : " + student.Name);
                }
                else
                {
                    MessageBox.Show(" Student is not Registered: ");
                }

                btnReset.PerformClick();
                conn.Close();
                ShowData();
            }

            catch (System.Exception exp)
            {
                MessageBox.Show(" Error is  " + exp.ToString());
            }
        }

        private void Reset(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    ((TextBox)(control)).Text = "";
                }
                if (control.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)(control)).SelectedIndex = -1;
                }
            }
            ShowData();
        }

        public void ShowData()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select * from student", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvStudent.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                int n = dgvStudent.Rows.Add();
                dgvStudent.Rows[n].Cells[0].Value = dr[0].ToString();
                dgvStudent.Rows[n].Cells[1].Value = dr[1].ToString();
                dgvStudent.Rows[n].Cells[2].Value = dr[2].ToString();
                dgvStudent.Rows[n].Cells[3].Value = dr[3].ToString();
                dgvStudent.Rows[n].Cells[4].Value = dr[4].ToString();
                dgvStudent.Rows[n].Cells[5].Value = dr[5].ToString();
                dgvStudent.Rows[n].Cells[6].Value = dr[6].ToString();
            }
        }
    }
}
