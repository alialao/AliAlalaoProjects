using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bestival.PresentationLayer
{
    public partial class FormLogIN : Form
    {

        BusinessLayer.Login log = new BusinessLayer.Login();
        public FormLogIN()
        {
            InitializeComponent();
            Pobulate_Shop_List();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtEmployeeID.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                DataTable dt = log.LogIn(txtEmployeeID.Text, txtPassword.Text);
                if (dt.Rows.Count > 0)
                {
                    //MessageBox.Show("Login success !", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PresentationLayer.Shop shops = new PresentationLayer.Shop();
                    shops.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("The Employee Id or the Password you’ve entered is incorrect !", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtEmployeeID.Clear();
                    txtPassword.Clear();
                    txtEmployeeID.Focus();
                }
            }
            else
                MessageBox.Show("Please Fill the Employee ID And the Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void lblAboutUs_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Event system by ABIO\nCreated by\nOBAIDA BOBOL\t2956160\nBRAHIM IDIKEN\t2401304\nBTHARI AYEISHA\t3000567\nALI ALALAO\t3107493");
        }
        private void Pobulate_Shop_List()
        {
            DataAccessLayer.DataAccessLayer DAL = new DataAccessLayer.DataAccessLayer();
            string cmdstr = @"Shop_List";
            try
            {
                //DAL.OpenConnection();

                cbShop_Name.DisplayMember = "SHOP_NAME";
                cbShop_Name.ValueMember = "SHOP_ID";
                //
                cbShop_Name.DataSource = DAL.SelectData(cmdstr, null);

                //DAL.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
