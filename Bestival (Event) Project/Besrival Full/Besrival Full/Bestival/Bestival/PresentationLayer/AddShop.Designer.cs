namespace Bestival.PresentationLayer
{
    partial class AddShop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddShop));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_Shop_Name = new System.Windows.Forms.TextBox();
            this.TB_Shop_location = new System.Windows.Forms.TextBox();
            this.BTN_Shop_Cancel = new System.Windows.Forms.Button();
            this.CB_Shop_Type = new System.Windows.Forms.ComboBox();
            this.GB_Add_Shop = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BT_Add_Shop = new System.Windows.Forms.Button();
            this.GB_Add_Shop.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Shop Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Location";
            // 
            // TB_Shop_Name
            // 
            this.TB_Shop_Name.Location = new System.Drawing.Point(145, 78);
            this.TB_Shop_Name.Name = "TB_Shop_Name";
            this.TB_Shop_Name.Size = new System.Drawing.Size(152, 20);
            this.TB_Shop_Name.TabIndex = 2;
            // 
            // TB_Shop_location
            // 
            this.TB_Shop_location.Location = new System.Drawing.Point(145, 114);
            this.TB_Shop_location.Name = "TB_Shop_location";
            this.TB_Shop_location.Size = new System.Drawing.Size(152, 20);
            this.TB_Shop_location.TabIndex = 3;
            // 
            // BTN_Shop_Cancel
            // 
            this.BTN_Shop_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTN_Shop_Cancel.Location = new System.Drawing.Point(238, 204);
            this.BTN_Shop_Cancel.Name = "BTN_Shop_Cancel";
            this.BTN_Shop_Cancel.Size = new System.Drawing.Size(152, 25);
            this.BTN_Shop_Cancel.TabIndex = 5;
            this.BTN_Shop_Cancel.Text = "Cancel";
            this.BTN_Shop_Cancel.UseVisualStyleBackColor = true;
            this.BTN_Shop_Cancel.Click += new System.EventHandler(this.BTN_Shop_Cancel_Click);
            // 
            // CB_Shop_Type
            // 
            this.CB_Shop_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Shop_Type.FormattingEnabled = true;
            this.CB_Shop_Type.Items.AddRange(new object[] {
            "Shop",
            "Stand"});
            this.CB_Shop_Type.Location = new System.Drawing.Point(145, 41);
            this.CB_Shop_Type.Name = "CB_Shop_Type";
            this.CB_Shop_Type.Size = new System.Drawing.Size(152, 21);
            this.CB_Shop_Type.TabIndex = 6;
            // 
            // GB_Add_Shop
            // 
            this.GB_Add_Shop.Controls.Add(this.CB_Shop_Type);
            this.GB_Add_Shop.Controls.Add(this.label3);
            this.GB_Add_Shop.Controls.Add(this.TB_Shop_Name);
            this.GB_Add_Shop.Controls.Add(this.TB_Shop_location);
            this.GB_Add_Shop.Controls.Add(this.label1);
            this.GB_Add_Shop.Controls.Add(this.label2);
            this.GB_Add_Shop.Location = new System.Drawing.Point(12, 12);
            this.GB_Add_Shop.Name = "GB_Add_Shop";
            this.GB_Add_Shop.Size = new System.Drawing.Size(399, 174);
            this.GB_Add_Shop.TabIndex = 7;
            this.GB_Add_Shop.TabStop = false;
            this.GB_Add_Shop.Text = "Shop Details";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Shop Type";
            // 
            // BT_Add_Shop
            // 
            this.BT_Add_Shop.Location = new System.Drawing.Point(36, 204);
            this.BT_Add_Shop.Name = "BT_Add_Shop";
            this.BT_Add_Shop.Size = new System.Drawing.Size(152, 25);
            this.BT_Add_Shop.TabIndex = 8;
            this.BT_Add_Shop.Text = "Add";
            this.BT_Add_Shop.UseVisualStyleBackColor = true;
            this.BT_Add_Shop.Click += new System.EventHandler(this.BT_Add_Shop_Click);
            // 
            // AddShop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(423, 264);
            this.ControlBox = false;
            this.Controls.Add(this.BT_Add_Shop);
            this.Controls.Add(this.BTN_Shop_Cancel);
            this.Controls.Add(this.GB_Add_Shop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddShop";
            this.Text = "AddShop";
            this.GB_Add_Shop.ResumeLayout(false);
            this.GB_Add_Shop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TB_Shop_Name;
        private System.Windows.Forms.TextBox TB_Shop_location;
        private System.Windows.Forms.Button BTN_Shop_Cancel;
        private System.Windows.Forms.ComboBox CB_Shop_Type;
        private System.Windows.Forms.GroupBox GB_Add_Shop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BT_Add_Shop;
    }
}