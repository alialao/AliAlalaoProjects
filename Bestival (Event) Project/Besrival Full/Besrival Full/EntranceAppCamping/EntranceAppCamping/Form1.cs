using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntranceAppCamping
{
    public partial class Form1 : Form
    {
        private DataHelper dh;
        public Form1()
        {
            InitializeComponent();
            dh = new DataHelper();
        }

        private void txtSID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Login loginPage = new Login();
            loginPage.Show();
        }

        private void btnCheckStatus_Click(object sender, EventArgs e)
        {
            string rsv_number = Convert.ToString(txtSID.Text);
           
               
                foreach (Customers cust in dh.GetCustomers(rsv_number))
                {
                    
                    this.lbStatus.Items.Add(cust.ToString());
                  
                }
           

        }

        private void dataGridViewMembers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
