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
using System.Data;

namespace Student_Management
{
    public partial class Form1 : Form
    {
        List<Student> lstStudent = new List<Student>();
        int stuCount = 0;
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-4O3O983;Initial Catalog=studentdb;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
            ShowData();
            btnReset.Click += BtnReset_Click;
            btnInsert.Click += BtnInsert_Click;
            btnUpdate.MouseClick += BtnUpdate_MouseClick;
            btnDelete.Click += BtnDelete_Click;
            dgvStudent.CellMouseClick += DgvStudent_CellMouseClick;
            txtSearchName.TextChanged += TxtSearchName_TextChanged;
        }

        private void TxtSearchName_TextChanged(object sender, EventArgs e)
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                String name = txtStuName.Text.ToString();
                String qry = "delete from student where s_name='" + name + "'";
                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                if (i >= 1)
                    MessageBox.Show(i + " Student Deleted Successfully : " + name);
                else
                    MessageBox.Show(" Student  Deletion Failed..... ");
                ClearTextBoxes(this);
                ShowData();
                conn.Close();
            }
            catch (System.Exception exp)
            {
                MessageBox.Show(" Error is  " + exp.ToString());

            }
        }

        private void DgvStudent_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ClearTextBoxes(this);
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


        private void BtnUpdate_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                conn.Open();
                string name = txtStuName.Text;
                string gender = cboGender.SelectedItem.ToString();
                string dob = txtDob.Text;
                string address = txtStuAddress.Text;
                string phone = txtStuPhone.Text;
                string year = cboYear.SelectedItem.ToString();
                string department = cboDepartment.SelectedItem.ToString();

                string qry = "update student set gender='" + gender + "', dob='" + dob + "', s_address='"
                    + address + "', phone='" + phone + "', year='" + year + "', department='" +
                    department + "'where s_name='" + name +"'";

                SqlCommand sc = new SqlCommand(qry, conn);
                int i = sc.ExecuteNonQuery();
                if (i >= 1)
                {
                    MessageBox.Show(i + " Student Updated Successfully : " + name);
                }      
                else
                {
                    MessageBox.Show(" Student  Updation Fail..... ");
                }

                ClearTextBoxes(this);
                conn.Close();
                ShowData();
            }
            catch (System.Exception exp)
            {
                MessageBox.Show(" Error is  " + exp.ToString());
            }
        }
           
        private void BtnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string name = txtStuName.Text;
                string gender = cboGender.SelectedItem.ToString();
                string dob = txtDob.Text;
                string address = txtStuAddress.Text;
                string phone = txtStuPhone.Text;
                string year = cboYear.SelectedItem.ToString();
                string department = cboDepartment.SelectedItem.ToString();

                lstStudent.Add(new Student(name, gender, dob, address, phone, year, department));

                string qry = "insert into student values('" + lstStudent[stuCount].Name + "','" + lstStudent[stuCount].Gender + "','" + lstStudent[stuCount].Dob + "','" 
                    + lstStudent[stuCount].Address + "','" + lstStudent[stuCount].Phone + "','" + lstStudent[stuCount].Year + "','" + lstStudent[stuCount].Department + "')";

                SqlCommand sc = new SqlCommand(qry, conn);

                int i = sc.ExecuteNonQuery();
                if (i >= 1)
                {
                    MessageBox.Show(i + " Student Registerd Successfully : " + name);
                }
                else
                {
                    MessageBox.Show(" Student is not Registered: ");
                }

                stuCount++;
                ClearTextBoxes(this);
                conn.Close();
                ShowData();
            }

            catch (System.Exception exp)
            {
                MessageBox.Show(" Error is  " + exp.ToString());
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            this.dgvStudent.Rows.Clear();
            ClearTextBoxes(this);
            ShowData();
        }

        public void ClearTextBoxes(Form form)
        {
            foreach (Control control in form.Controls)
            {
                if (control.GetType() == typeof(TextBox))
                {
                    ((TextBox)(control)).Text = "";
                }
                if(control.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)(control)).SelectedIndex = -1;
                }
            }
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
