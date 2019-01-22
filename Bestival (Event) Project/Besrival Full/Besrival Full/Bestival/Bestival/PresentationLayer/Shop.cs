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

namespace Bestival.PresentationLayer
{
    public partial class Shop : Form
    {
        BusinessLayer.Customer customer = new BusinessLayer.Customer();

        public Shop()
        {
            InitializeComponent();

            Form_Control_Shop("InitializeForm");
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            PresentationLayer.FormLogIN logIn = new PresentationLayer.FormLogIN();
            logIn.Show();
            this.Hide();
        }
        private void labelAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Event system by ABIO\nCreated by\nOBAIDA BOBOL\t2956160\nBRAHIM IDIKEN\t2401304\nBTHARI AYEISHA\t3000567\nALI ALALAO\t3107493");
        }
        //############################## M E T H O D ##################################
        private void Form_Control_Shop(string P_Mode)
        {
            try
            {
                if (P_Mode == "InitializeForm")
                {
                    //Clear all DataGridView 
                    dGVShop.Rows.Clear();
                    //Clear all TextBox in GroupBox GB_OrderInfo
                    foreach (Control cont in GB_Shop_OrderInfo.Controls)
                    {
                        if (cont is TextBox)
                        {
                            ((TextBox)cont).Clear();
                            ((TextBox)cont).Enabled = false;
                        }
                    }
                    foreach (Control cont in Gb_Shop_Controls.Controls)
                    {
                        if (cont is Button)
                        {
                            ((Button)cont).Enabled = false;
                        }
                    }
                    Btn_Shop_NewOrder.Enabled = true;
                    TB_Shop_CustomerID.Enabled = true;
                    Btn_Shop_Remove.Enabled = false;

                    label_Shop_Info.Text = null;

                    tb_Shop_Total.Clear();


                    TB_Shop_ProductId.BackColor = Color.LightGray;
                    TB_Shop_Quantity.BackColor = Color.LightGray;

                    TB_Shop_ProductId.Focus();
                }
                if (P_Mode == "Shopselected")
                {
                    Btn_Shop_NewOrder.Enabled = true;
                    TB_Shop_CustomerID.Enabled = true;
                }
                if (P_Mode == "ReservNumSelected" /*&& customer.Deposit > 1*/)
                {
                    Btn_Shop_NewOrder.Enabled = false;
                    Btn_Shop_Add.Enabled = true;
                    Btn_Shop_Refresh.Enabled = true;

                    TB_Shop_CustomerID.Enabled = false;
                    TB_Shop_ProductId.Enabled = true;
                    TB_Shop_Quantity.Enabled = true;

                    TB_Shop_ProductId.BackColor = SystemColors.Control;
                    TB_Shop_Quantity.BackColor = SystemColors.Control;
                }
                if (P_Mode == "DataGridViewFilled")
                {
                    if (dGVShop.Rows.Count > 0)
                    {
                        Btn_Shop_SaveInv.Enabled = true;
                        Btn_Shop_PrintInv.Enabled = true;
                        Btn_Shop_Remove.Enabled = true;
                    }
                    else
                    {
                        Btn_Shop_SaveInv.Enabled = false;
                        Btn_Shop_PrintInv.Enabled = false;
                        Btn_Shop_Remove.Enabled = false;
                    }
                }
                if (P_Mode == "Pay")
                {
                    Btn_Shop_NewOrder.Enabled = false;
                    Btn_Shop_Add.Enabled = false;
                    Btn_Shop_Remove.Enabled = false;
                    Btn_Shop_SaveInv.Enabled = false;

                    TB_Shop_CustomerID.Enabled = false;
                    TB_Shop_ProductId.Enabled = false;
                    TB_Shop_Quantity.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information); //ali
            }
        }
    }
}
