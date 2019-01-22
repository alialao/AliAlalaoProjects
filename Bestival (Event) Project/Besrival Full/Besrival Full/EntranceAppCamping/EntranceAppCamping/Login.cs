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

namespace EntranceAppCamping
{
    public partial class Login : Form
    {
        private DataHelper dh;
        public Login()
        {
            InitializeComponent();
            dh = new DataHelper();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string employeeId;
            string password;
            employeeId = Convert.ToString(txtEmployeeID.Text);
            password = Convert.ToString(txtPassword.Text);
            dh.EmployeeLogIn(employeeId, password);
          

        }

        private void txtEmployeeID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}