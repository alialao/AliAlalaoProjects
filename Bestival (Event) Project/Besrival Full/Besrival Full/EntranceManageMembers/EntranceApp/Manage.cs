using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntranceApp.Forms
{
    public partial class Manage : Form
    {
        AboutUs aboutus = new AboutUs();
        public Manage()
        {
            InitializeComponent();
        }

        private void lblAboutUs_Click(object sender, EventArgs e)
        {
            MessageBox.Show(aboutus.Aboutus());
        }

        private void label6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(aboutus.Aboutus());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login frmLogin = new Login();
            frmLogin.Show();
            this.Hide();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        private void Manage_Load(object sender, EventArgs e)
        {

        }

        private void btnPrintPreview_Click_1(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
