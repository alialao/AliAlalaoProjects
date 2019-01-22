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
    public partial class AddShop : Form
    {
        private readonly Management addShop;
        BusinessLayer.Shop shop = new BusinessLayer.Shop();
        public AddShop(Management refresh)
        {
            InitializeComponent();
            this.Text = "Add New Shop";

            addShop = refresh;
        }

        private void BTN_Shop_Cancel_Click(object sender, EventArgs e)
        {
            addShop.RefrishDgv();
            Close();
        }

        private void BT_Add_Shop_Click(object sender, EventArgs e)
        {
            if (TB_Shop_Name.Text != string.Empty && TB_Shop_location.Text != string.Empty && CB_Shop_Type.SelectedIndex > -1)
            {
                if ((string)CB_Shop_Type.Text == "Shop")
                {
                    shop.Shop_Add(TB_Shop_Name.Text, TB_Shop_location.Text, 10);
                }
                if ((string)CB_Shop_Type.Text == "Stand")
                {
                    shop.Shop_Add(TB_Shop_Name.Text, TB_Shop_location.Text, 20);
                }
                MessageBox.Show("Added Successfully", "Add New Shop", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please, Enter All The Details", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            TB_Shop_Name.Clear();
            TB_Shop_location.Clear();
            CB_Shop_Type.SelectedIndex = -1;
            addShop.RefrishDgv();
        }
    }
}
