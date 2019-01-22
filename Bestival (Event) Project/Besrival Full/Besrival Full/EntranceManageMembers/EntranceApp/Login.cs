using EntranceApp.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntranceApp
{
    public partial class Login : Form
    {
        AboutUs aboutus = new AboutUs();
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Manage frmManage = new Manage();
            frmManage.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblAboutUs_Click(object sender, EventArgs e)
        {
            MessageBox.Show(aboutus.Aboutus());
        }
    }
}
