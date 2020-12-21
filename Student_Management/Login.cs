using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            string username;
            string password;

            txtPassword.PasswordChar = '\u25CF';
            btnLogin.Click += Login_Click;
        }

        private void Login_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            User user = new User();

            if (username == user.Username && password == user.Password)
            {
                Form1 frm1 = new Form1();
                frm1.Show();
                this.Hide();
                txtUsername.Text = null;
                txtPassword.Text = null;
            }
            else
            {
                MessageBox.Show("Invalid Username or Password", "Invalid Credential", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Text = null;
                txtPassword.Text = null;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form frm2 = new Form2();
            frm2.Show();
        }
    }
}
