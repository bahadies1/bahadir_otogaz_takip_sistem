using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace login_and_Register_System
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }

        db_connection db = new db_connection();        
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" && txtPassword.Text == "" && txtComPassword.Text == "")
            {
                MessageBox.Show("Lütfen boş bırakılan alanları doldurunuz", "Kayıt Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (txtPassword.Text == txtComPassword.Text)
            {

                get_set newUser = new get_set();

                newUser.Kullaniciadi = txtUsername.Text;
                newUser.Sifre = txtPassword.Text;
                db.registerUser(newUser);

                txtUsername.Text = "";
                txtPassword.Text = "";
                txtComPassword.Text = "";

                new DashboardPage().Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Şifreler biribiri ile uyuşmuyor", "Kayıt Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtComPassword.Text = "";
                txtPassword.Focus();
            }
        }

        private void checkbxShowPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxShowPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtComPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '•';
                txtComPassword.PasswordChar = '•';
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtComPassword.Text = "";
            txtUsername.Focus();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            new LoginPage().Show();
            this.Hide();
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
