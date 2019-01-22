namespace HerdImmunity.Classes.Interface
{
    partial class Statistics_graph
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelRate = new System.Windows.Forms.Label();
            this.labelToll = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BorderSkin.BackColor = System.Drawing.Color.White;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.MaximumSize = new System.Drawing.Size(1000000, 10000);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "efficacyRate";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(863, 665);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // labelRate
            // 
            this.labelRate.AutoSize = true;
            this.labelRate.BackColor = System.Drawing.Color.White;
            this.labelRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRate.Location = new System.Drawing.Point(697, 608);
            this.labelRate.Name = "labelRate";
            this.labelRate.Size = new System.Drawing.Size(57, 25);
            this.labelRate.TabIndex = 2;
            this.labelRate.Text = "Rate";
            // 
            // labelToll
            // 
            this.labelToll.AutoSize = true;
            this.labelToll.BackColor = System.Drawing.Color.White;
            this.labelToll.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.labelToll.Location = new System.Drawing.Point(85, 9);
            this.labelToll.Name = "labelToll";
            this.labelToll.Size = new System.Drawing.Size(110, 26);
            this.labelToll.TabIndex = 3;
            this.labelToll.Text = "Death Toll";
            // 
            // Statistics_graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 665);
            this.Controls.Add(this.labelToll);
            this.Controls.Add(this.labelRate);
            this.Controls.Add(this.chart1);
            this.Name = "Statistics_graph";
            this.Text = "Statistics_graph";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label labelRate;
        private System.Windows.Forms.Label labelToll;
    }
}