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
    public partial class AddProduct : Form
    {
        private readonly Management addProduct;
        DataAccessLayer.DataAccessLayer DAL = new DataAccessLayer.DataAccessLayer();
        BusinessLayer.Product newProduct = new BusinessLayer.Product();
        BusinessLayer.Shop shop = new BusinessLayer.Shop();


        public AddProduct(Management refresh)
        {
            InitializeComponent();
            this.Text = "Add New Product";

            addProduct = refresh;
            ShopList();
        }

        private void ShopList()
        {
            string cmdstr = @"Shop_List";
            try
            {
                DAL.OpenConnection();

                CB_Shop_Name.DisplayMember = "SHOP_NAME";
                CB_Shop_Name.ValueMember = "SHOP_ID";

                CB_Shop_Name.DataSource = DAL.GetDataTable(cmdstr);

                DAL.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BT_ADD_Click(object sender, EventArgs e)
        {
            if (TB_Product_Name.Text != string.Empty && TB_Price.Text != string.Empty && TB_Quantity.Text != string.Empty && CB_Shop_Name.SelectedIndex > -1)
            {
                string product_name = TB_Product_Name.Text;
                double product_price = Convert.ToDouble(TB_Price.Text);
                int product_type = shop.GetShopType(Convert.ToInt32(CB_Shop_Name.SelectedValue));
                MessageBox.Show(CB_Shop_Name.SelectedValue.ToString());
                int shop_id = Convert.ToInt32(CB_Shop_Name.SelectedValue);
                int quantity = Convert.ToInt32(TB_Quantity.Text);

                newProduct.Product_Add(product_name, product_price, product_type, shop_id, quantity);

                MessageBox.Show("Added Successfully", "Add New Shop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please, Enter All The Details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            TB_Product_Name.Clear();
            TB_Price.Clear();
            TB_Quantity.Clear();
            CB_Shop_Name.SelectedIndex = -1;
            addProduct.RefrishDgv();

        }
        private void BT_Cancel_Click(object sender, EventArgs e)
        {
            addProduct.RefrishDgv();
            Close();
        }
    }
}
