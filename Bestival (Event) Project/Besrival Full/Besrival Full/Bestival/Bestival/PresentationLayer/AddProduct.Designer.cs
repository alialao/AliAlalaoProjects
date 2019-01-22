namespace Bestival.PresentationLayer
{
    partial class AddProduct
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddProduct));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_Product_Name = new System.Windows.Forms.TextBox();
            this.TB_Price = new System.Windows.Forms.TextBox();
            this.TB_Quantity = new System.Windows.Forms.TextBox();
            this.CB_Shop_Name = new System.Windows.Forms.ComboBox();
            this.BT_ADD = new System.Windows.Forms.Button();
            this.BT_Cancel = new System.Windows.Forms.Button();
            this.GB_Add_Product = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(98, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Price";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(89, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Quantity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(98, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Shop";
            // 
            // TB_Product_Name
            // 
            this.TB_Product_Name.Location = new System.Drawing.Point(156, 45);
            this.TB_Product_Name.Name = "TB_Product_Name";
            this.TB_Product_Name.Size = new System.Drawing.Size(152, 20);
            this.TB_Product_Name.TabIndex = 4;
            // 
            // TB_Price
            // 
            this.TB_Price.Location = new System.Drawing.Point(156, 76);
            this.TB_Price.Name = "TB_Price";
            this.TB_Price.Size = new System.Drawing.Size(152, 20);
            this.TB_Price.TabIndex = 5;
            // 
            // TB_Quantity
            // 
            this.TB_Quantity.Location = new System.Drawing.Point(156, 107);
            this.TB_Quantity.Name = "TB_Quantity";
            this.TB_Quantity.Size = new System.Drawing.Size(152, 20);
            this.TB_Quantity.TabIndex = 6;
            // 
            // CB_Shop_Name
            // 
            this.CB_Shop_Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Shop_Name.FormattingEnabled = true;
            this.CB_Shop_Name.Location = new System.Drawing.Point(156, 138);
            this.CB_Shop_Name.Name = "CB_Shop_Name";
            this.CB_Shop_Name.Size = new System.Drawing.Size(152, 21);
            this.CB_Shop_Name.TabIndex = 7;
            // 
            // BT_ADD
            // 
            this.BT_ADD.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BT_ADD.Location = new System.Drawing.Point(36, 204);
            this.BT_ADD.Name = "BT_ADD";
            this.BT_ADD.Size = new System.Drawing.Size(152, 25);
            this.BT_ADD.TabIndex = 8;
            this.BT_ADD.Text = "Add";
            this.BT_ADD.UseVisualStyleBackColor = true;
            this.BT_ADD.Click += new System.EventHandler(this.BT_ADD_Click);
            // 
            // BT_Cancel
            // 
            this.BT_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BT_Cancel.Location = new System.Drawing.Point(238, 204);
            this.BT_Cancel.Name = "BT_Cancel";
            this.BT_Cancel.Size = new System.Drawing.Size(152, 25);
            this.BT_Cancel.TabIndex = 9;
            this.BT_Cancel.Text = "Cancel";
            this.BT_Cancel.UseVisualStyleBackColor = true;
            this.BT_Cancel.Click += new System.EventHandler(this.BT_Cancel_Click);
            // 
            // GB_Add_Product
            // 
            this.GB_Add_Product.Location = new System.Drawing.Point(12, 12);
            this.GB_Add_Product.Name = "GB_Add_Product";
            this.GB_Add_Product.Size = new System.Drawing.Size(399, 174);
            this.GB_Add_Product.TabIndex = 10;
            this.GB_Add_Product.TabStop = false;
            this.GB_Add_Product.Text = "Product Details";
            // 
            // AddProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(423, 264);
            this.ControlBox = false;
            this.Controls.Add(this.BT_Cancel);
            this.Controls.Add(this.BT_ADD);
            this.Controls.Add(this.CB_Shop_Name);
            this.Controls.Add(this.TB_Quantity);
            this.Controls.Add(this.TB_Price);
            this.Controls.Add(this.TB_Product_Name);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GB_Add_Product);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddProduct";
            this.Text = "AddProduct";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_Product_Name;
        private System.Windows.Forms.TextBox TB_Price;
        private System.Windows.Forms.TextBox TB_Quantity;
        private System.Windows.Forms.ComboBox CB_Shop_Name;
        private System.Windows.Forms.Button BT_ADD;
        private System.Windows.Forms.Button BT_Cancel;
        private System.Windows.Forms.GroupBox GB_Add_Product;
    }
}