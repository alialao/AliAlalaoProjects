using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


/*
 * Must (M)
 * •	Admin only can log in
 * •	Application displays statistic figures about the festival 
 * -	The number of participants
 * -	The number of sold tickets and available tickets (camp spot, festival)
 * -	The number of sold products, loan materials XXX
 * -	The revenue of each shop, stands XX
 * -	The net profit of the event  ...
 * Should (S)
 * •	Admin can alter participants details 
 * •	Admin can delete participants 
 * •	Admin can search based on giving Reservation Number or Date of Birth
 * Could (C)
 * •	Admin can print participants details 
*/
namespace Bestival.PresentationLayer
{
    public partial class Management : Form
    {
        BusinessLayer.Product product = new BusinessLayer.Product();
        BusinessLayer.Customer customer = new BusinessLayer.Customer();
        BusinessLayer.Shop shop = new BusinessLayer.Shop();

        public Management()
        {
            InitializeComponent();

            FillData();
        }

        public void FillData()
        {
            try
            {
                this.dGVCustomer.DataSource = customer.Customers_All();
                this.dGVShop.DataSource = shop.Shops_All(10);
                this.dGVStand.DataSource = shop.Shops_All(20);
                this.dGVProduct.DataSource = product.Product_All();

                DgvCustomer_Control("Initialize");
                DgvShop_Control("Initialize");
                DgvStand_Control("Initialize");
                DgvProduct_Control("Initialize");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void RefrishDgv()
        {
            this.dGVCustomer.DataSource = customer.Customers_All();
            this.dGVShop.DataSource = shop.Shops_All(10);
            this.dGVStand.DataSource = shop.Shops_All(20);
            this.dGVProduct.DataSource = product.Product_All();
        }
        //#################### C U S T O M E R ####################
        private void tb_CustomerId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tb_CustomerId.Multiline = false;
                btn_Customer_Search.PerformClick();
                tb_CustomerId.Focus();
            }
        }
        private void tb_CustomerId_KeyUp(object sender, KeyEventArgs e)
        {
            if (tb_CustomerId.Text.Length == 0)
            {
                this.dGVCustomer.DataSource = customer.Customers_All();
            }
        }
        private void btn_Customer_Search_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tb_CustomerId.Text))
                {
                    DataTable dt = new DataTable();
                    dt = customer.Customers_Search(Convert.ToInt32(tb_CustomerId.Text));
                    this.dGVCustomer.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Please Provide Customer ID", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.dGVCustomer.DataSource = customer.Customers_All();
                    tb_CustomerId.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btn_Customer_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("You Choose to delete selected records\n Are you sure?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = dGVCustomer.Rows.Count - 1; i >= 0; i--)
                    {
                        object Cell = dGVCustomer.Rows[i].Cells[0].Value;
                        int customer_id = Convert.ToInt32(dGVCustomer.Rows[i].Cells[1].Value);
                        if ((string)Cell == "True")
                        {
                            customer.Customer_Delete(customer_id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.dGVCustomer.DataSource = customer.Customers_All();
            DgvCustomer_Control("Edit");
        }
        private void btn_Customer_Print_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }   //TODO
        private void btn_Customer_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save the change/s?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    foreach (DataGridViewRow item in dGVCustomer.Rows)
                    {
                        object Cell = item.Cells[dGVCustomer.Columns[0].Index].Value;
                        if ((string)Cell == "True")
                        {
                            int customer_id = Convert.ToInt32(item.Cells[dGVCustomer.Columns[1].Index].Value);
                            string first_name = item.Cells[dGVCustomer.Columns[2].Index].Value.ToString();
                            string last_name = item.Cells[dGVCustomer.Columns[3].Index].Value.ToString();
                            DateTime dof = Convert.ToDateTime(item.Cells[dGVCustomer.Columns[4].Index].Value);
                            string email_address = item.Cells[dGVCustomer.Columns[5].Index].Value.ToString();
                            string phone_number = item.Cells[dGVCustomer.Columns[6].Index].Value.ToString();
                            string notes = item.Cells[dGVCustomer.Columns[8].Index].Value.ToString();

                            string Camping_camping_category = item.Cells[dGVCustomer.Columns[9].Index].Value.ToString();
                            string Camping_camping_number = item.Cells[dGVCustomer.Columns[10].Index].Value.ToString();

                            customer.Customer_Edit(customer_id, first_name, last_name, dof, email_address, phone_number, Camping_camping_category, Camping_camping_number, notes);
                        }
                    }
                    //btn_Customer_Edit.Visible = true;
                    //btn_Customer_Save.Visible = false;
                    //dGVCustomer.Columns[0].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DgvCustomer_Control("Initialize");
        }
        private void btn_Customer_Edit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You Are About to Edit records\n Are you sure?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DgvCustomer_Control("Edit");
            }
        }
        private void dGVCustomer_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in dGVCustomer.Rows)
                {
                    object Cell = item.Cells[dGVCustomer.Columns[0].Index].Value;
                    if ((string)Cell == "True")
                    {
                        item.Cells[dGVCustomer.Columns[2].Index].ReadOnly = false;
                        item.Cells[dGVCustomer.Columns[3].Index].ReadOnly = false;
                        item.Cells[dGVCustomer.Columns[4].Index].ReadOnly = false;
                        item.Cells[dGVCustomer.Columns[5].Index].ReadOnly = false;
                        item.Cells[dGVCustomer.Columns[6].Index].ReadOnly = false;

                        item.Cells[dGVCustomer.Columns[8].Index].ReadOnly = false;
                        item.Cells[dGVCustomer.Columns[9].Index].ReadOnly = false;
                        item.Cells[dGVCustomer.Columns[10].Index].ReadOnly = false;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void DgvCustomer_Control(string P_Mode)
        {
            if(P_Mode == "Initialize")
            {
                tb_CustomerId.Clear();
                this.dGVCustomer.DataSource = customer.Customers_All();

                btn_Customer_Save.Visible = false;
                btn_Customer_Delete.Enabled = false;
                btn_Customer_Edit.Visible = true;
                dGVCustomer.Columns[0].Visible = false;

                dGVCustomer.Columns[0].ReadOnly = true;     // CheckBox
                dGVCustomer.Columns[1].ReadOnly = true;     // Customer ID
                //dGVCustomer.Columns[2].ReadOnly = true;     // Reservation Number
                dGVCustomer.Columns[2].ReadOnly = true;     // First Name
                dGVCustomer.Columns[3].ReadOnly = true;     // Last Name
                dGVCustomer.Columns[4].ReadOnly = true;     // DOB
                dGVCustomer.Columns[5].ReadOnly = true;     // Email
                dGVCustomer.Columns[6].ReadOnly = true;     // Phone Number
                dGVCustomer.Columns[7].ReadOnly = true;     // Deposit
                dGVCustomer.Columns[8].ReadOnly = true;    // text
                dGVCustomer.Columns[9].ReadOnly = true;     // Camping Cat
                dGVCustomer.Columns[10].ReadOnly = true;    // Camping Numbera
            }
            if(P_Mode == "Edit")
            {
                btn_Customer_Delete.Enabled = true;
                btn_Customer_Edit.Visible = false;
                btn_Customer_Save.Visible = true;
                dGVCustomer.Columns[0].Visible = true;
                dGVCustomer.Columns[0].ReadOnly = false;

                //DataGridViewComboBoxColumn dgvCB;
                //dGVCustomer.Columns[9] = dgvCB = new DataGridViewComboBoxColumn;
            }
        }

        //#################### S H O P ####################
        private void tb_ShopId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tb_ShopId.Multiline = false;
                btn_Shop_Search.PerformClick();
                tb_ShopId.Focus();
            }
        }
        private void tb_ShopId_KeyUp(object sender, KeyEventArgs e)
        {
            if (tb_ShopId.Text.Length == 0)
            {
                this.dGVShop.DataSource = shop.Shops_All(10);
            }
        }
        private void btn_Shop_Search_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tb_ShopId.Text))
                {
                    DataTable dt = new DataTable();
                    dt = shop.Shops_Search(Convert.ToInt32(tb_ShopId.Text), 10);
                    this.dGVShop.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Please Provide Product ID", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.dGVShop.DataSource = shop.Shops_All(10);
                    tb_ShopId.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btn_Shop_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save the change/s", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    foreach (DataGridViewRow item in dGVShop.Rows)
                    {
                        object Cell = item.Cells[dGVShop.Columns[0].Index].Value;
                        if ((string)Cell == "True")
                        {
                            int shop_id = Convert.ToInt32(item.Cells[dGVShop.Columns[1].Index].Value);
                            string shop_name = item.Cells[dGVShop.Columns[2].Index].Value.ToString();
                            string location = item.Cells[dGVShop.Columns[3].Index].Value.ToString();

                            shop.Shop_Edit(shop_id, shop_name, location, 10);
                        }
                    }
                    //btn_Shop_Edit.Visible = true;
                    //btn_Shop_Save.Visible = false;
                    //dGVShop.Columns[0].ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DgvShop_Control("Initialize");
        }
        private void btn_Shop_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("You Choose to delete selected records\n Are you sure?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = dGVShop.Rows.Count - 1; i >= 0; i--)
                    {
                        object Cell = dGVShop.Rows[i].Cells[0].Value;
                        int shop_Id = Convert.ToInt32(dGVShop.Rows[i].Cells[1].Value);
                        if ((string)Cell == "True")
                        {
                            shop.Shop_Delete(shop_Id, 10);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.dGVShop.DataSource = shop.Shops_All(10);
            DgvShop_Control("Edit");
        }
        private void btn_Shop_Print_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }   //TODO
        private void btn_Shop_Add_Click(object sender, EventArgs e)
        {
            AddShop addShop = new AddShop(this);            
            addShop.ShowDialog();
        }
        private void btn_Shop_Edit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You Are About to Edit records\n Are you sure?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DgvShop_Control("Edit");

                // get max number id in datagridview
            }
        }
        private void dGVShop_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in dGVShop.Rows)
                {
                    object Cell = item.Cells[dGVShop.Columns[0].Index].Value;
                    if ((string)Cell == "True")
                    {
                        item.Cells[dGVShop.Columns[2].Index].ReadOnly = false;
                        item.Cells[dGVShop.Columns[3].Index].ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void DgvShop_Control(string P_Mode)
        {
            if (P_Mode == "Initialize")
            {
                tb_ShopId.Clear();
                this.dGVShop.DataSource = shop.Shops_All(10);

                btn_Shop_Save.Visible = false;
                btn_Shop_Delete.Enabled = false;
                btn_Shop_Edit.Visible = true;
                dGVShop.Columns[0].Visible = false;

                dGVShop.Columns[0].ReadOnly = true;
                dGVShop.Columns[1].ReadOnly = true; //have to be true
                dGVShop.Columns[2].ReadOnly = true;
                dGVShop.Columns[3].ReadOnly = true;
            }
            if (P_Mode == "Edit")
            {
                btn_Shop_Delete.Enabled = true;
                btn_Shop_Edit.Visible = false;
                btn_Shop_Save.Visible = true;
                dGVShop.Columns[0].Visible = true;
                dGVShop.Columns[0].ReadOnly = false;
            }
        }

        //#################### S T A N D ####################
        private void tb_StandId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tb_StandId.Multiline = false;
                btn_Stand_Search.PerformClick();
                tb_StandId.Focus();
            }
        }
        private void tb_StandId_KeyUp(object sender, KeyEventArgs e)
        {
            if (tb_StandId.Text.Length == 0)
            {
                this.dGVStand.DataSource = shop.Shops_All(20);
            }
        }
        private void btn_Stand_Search_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tb_StandId.Text))
                {
                    DataTable dt = new DataTable();
                    dt = shop.Shops_Search(Convert.ToInt32(tb_StandId.Text), 20);
                    this.dGVStand.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Please Provide Product ID", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.dGVStand.DataSource = shop.Shops_All(20);
                    tb_StandId.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btn_Stand_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save the change/s", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    foreach (DataGridViewRow item in dGVStand.Rows)
                    {
                        object Cell = item.Cells[dGVStand.Columns[0].Index].Value;
                        if ((string)Cell == "True")
                        {
                            int shop_id = Convert.ToInt32(item.Cells[dGVStand.Columns[1].Index].Value);
                            string shop_name = item.Cells[dGVStand.Columns[2].Index].Value.ToString();
                            string location = item.Cells[dGVStand.Columns[3].Index].Value.ToString();

                            shop.Shop_Edit(shop_id, shop_name, location, 20);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DgvStand_Control("Initialize");
        }
        private void btn_Stand_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("You Choose to delete selected records\n Are you sure?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = dGVStand.Rows.Count - 1; i >= 0; i--)
                    {
                        object Cell = dGVStand.Rows[i].Cells[0].Value;
                        int shop_Id = Convert.ToInt32(dGVStand.Rows[i].Cells[1].Value);
                        if ((string)Cell == "True")
                        {
                            shop.Shop_Delete(shop_Id, 20);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.dGVStand.DataSource = shop.Shops_All(20);
            DgvStand_Control("Edit");
        }
        private void btn_Stand_Print_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }   //TODO
        private void btn_Stand_Add_Click(object sender, EventArgs e)
        {
            AddShop addShop = new AddShop(this);
            addShop.ShowDialog();
        }
        private void btn_Stand_Edit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You Are About to Edit records\n Are you sure?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DgvStand_Control("Edit");
            }
        }
        private void dGVStand_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in dGVStand.Rows)
                {
                    object Cell = item.Cells[dGVStand.Columns[0].Index].Value;
                    if ((string)Cell == "True")
                    {
                        item.Cells[dGVStand.Columns[2].Index].ReadOnly = false;
                        item.Cells[dGVStand.Columns[3].Index].ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void DgvStand_Control(string P_Mode)
        {
            if (P_Mode == "Initialize")
            {
                tb_StandId.Clear();
                this.dGVStand.DataSource = shop.Shops_All(20);

                btn_Stand_Save.Visible = false;
                btn_Stand_Delete.Enabled = false;
                btn_Stand_Edit.Visible = true;
                dGVStand.Columns[0].Visible = false;

                dGVStand.Columns[0].ReadOnly = true;
                dGVStand.Columns[1].ReadOnly = true;
                dGVStand.Columns[2].ReadOnly = true;
                dGVStand.Columns[3].ReadOnly = true;
            }
            if (P_Mode == "Edit")
            {
                btn_Stand_Delete.Enabled = true;
                btn_Stand_Edit.Visible = false;
                btn_Stand_Save.Visible = true;
                dGVStand.Columns[0].Visible = true;
                dGVStand.Columns[0].ReadOnly = false;
            }
        }

        //#################### P R O D U C T ####################
        private void tb_ProductId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tb_ProductId.Multiline = false;
                btn_Product_Search.PerformClick();
                tb_ProductId.Focus();
            }
        }
        private void tb_ProductId_KeyUp(object sender, KeyEventArgs e)
        {
            if (tb_ProductId.Text.Length == 0)
            {
                this.dGVProduct.DataSource = product.Product_All();
            }
        }
        private void btn_product_Search_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tb_ProductId.Text))
                {
                    DataTable dt = new DataTable();
                    dt = product.Product_Searsh(Convert.ToInt32(tb_ProductId.Text));
                    this.dGVProduct.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Please Provide Product ID", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.dGVProduct.DataSource = product.Product_All();
                    tb_ProductId.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btn_Product_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save the change/s", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    foreach (DataGridViewRow item in dGVProduct.Rows)
                    {
                        object Cell = item.Cells[dGVProduct.Columns[0].Index].Value;
                        if ((string)Cell == "True")
                        {
                            int product_id = Convert.ToInt32(item.Cells[dGVProduct.Columns[1].Index].Value);
                            string product_name = item.Cells[dGVProduct.Columns[2].Index].Value.ToString();
                            double product_price = Convert.ToDouble(item.Cells[dGVProduct.Columns[3].Index].Value);
                            int quantity = Convert.ToInt32(item.Cells[dGVProduct.Columns[4].Index].Value);
                            int shop_id = Convert.ToInt32(item.Cells[dGVProduct.Columns[6].Index].Value);

                            product.Product_Edit(product_id, product_name, product_price, quantity, shop_id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DgvProduct_Control("Initialize");
        }
        private void btn_Product_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("You Choose to delete selected records\n Are you sure?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = dGVProduct.Rows.Count - 1; i >= 0; i--)
                    {
                        object Cell = dGVProduct.Rows[i].Cells[0].Value;
                        int product_id = Convert.ToInt32(dGVProduct.Rows[i].Cells[1].Value);
                        int shop_id = Convert.ToInt32(dGVProduct.Rows[i].Cells[6].Value);
                        if ((string)Cell == "True")
                        {
                            product.DeletProduct(product_id, shop_id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.dGVProduct.DataSource = product.Product_All();
            DgvProduct_Control("Edit");
        }
        private void btn_Product_Print_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } //TODO
        private void btn_Product_Add_Click(object sender, EventArgs e)
        {
            AddProduct addProduct = new AddProduct(this);
            addProduct.ShowDialog();
        }
        private void btn_Product_Edit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You Are About to Edit records\n Are you sure?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DgvProduct_Control("Edit");
            }
        }
        private void dGVProduct_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in dGVProduct.Rows)
                {
                    object Cell = item.Cells[dGVProduct.Columns[0].Index].Value;
                    if ((string)Cell == "True")
                    {
                        item.Cells[dGVProduct.Columns[2].Index].ReadOnly = false;
                        item.Cells[dGVProduct.Columns[3].Index].ReadOnly = false;
                        item.Cells[dGVProduct.Columns[4].Index].ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void DgvProduct_Control(string P_Mode)
        {
            if (P_Mode == "Initialize")
            {
                tb_ProductId.Clear();
                this.dGVProduct.DataSource = product.Product_All();

                btn_Product_Save.Visible = false;
                btn_Product_Delete.Enabled = false;
                btn_Product_Edit.Visible = true;
                dGVProduct.Columns[0].Visible = false;

                dGVProduct.Columns[0].ReadOnly = true;
                dGVProduct.Columns[1].ReadOnly = true;
                dGVProduct.Columns[2].ReadOnly = true;
                dGVProduct.Columns[3].ReadOnly = true;
                dGVProduct.Columns[4].ReadOnly = true;
                dGVProduct.Columns[5].ReadOnly = true;
                dGVProduct.Columns[6].ReadOnly = true;
                dGVProduct.Columns[7].ReadOnly = true;
            }
            if (P_Mode == "Edit")
            {
                btn_Product_Delete.Enabled = true;
                btn_Product_Edit.Visible = false;
                btn_Product_Save.Visible = true;
                dGVProduct.Columns[0].Visible = true;
                dGVProduct.Columns[0].ReadOnly = false;
            }
        }

        private void Management_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Application.OpenForms.Count == 0) Application.Exit();
        }
    }
}
