using System;
using System.Diagnostics;
using System.Windows.Forms;
using HerdImmunity.Classes.Misc;


namespace HerdImmunity.Classes.Interface
{
    public partial class Statistics_graph : Form
    {
        public Statistics_graph()
        {
            InitializeComponent();

            //int i = 0;
            //int l = 0;
            int u = 0;
            //foreach (var item in Statistics.GetInstance().deadPeoplelist)
            //{
            //    chart1.Series["deadPeople"].Points.AddXY(i, item);
            //    i++;
            //}
            //foreach (var item in Statistics.GetInstance().deadImmunodeficientlist)
            //{
            //    chart1.Series["deadImmunodeficient"].Points.AddXY(l, item);
            //    l++;
            //}
            //foreach (var item in Statistics.GetInstance().numberOfInfectionslist)
            //{
            //    chart1.Series["numberOfInfections"].Points.AddXY(u, item);
            //    u++;
            //}
            Debug.WriteLine(Statistics.GetInstance().steppingType);
            if(Statistics.GetInstance().steppingType == 2)
                for (int i = 0; i < Statistics.GetInstance().efficacyRatelist.Count; i++)
                {  
                    chart1.Series["efficacyRate"].Points.AddXY(Statistics.GetInstance().efficacyRatelist[i], Statistics.GetInstance().deadPeoplelist[i]);
                }

            if (Statistics.GetInstance().steppingType == 1)
                for (int i = 0; i < Statistics.GetInstance().vaccinationRatelist.Count; i++)
                {
                    Debug.WriteLine($"dead {Statistics.GetInstance().deadPeoplelist[i]} vs {Statistics.GetInstance().vaccinationRatelist[i]}");
                    chart1.Series["efficacyRate"].Points.AddXY(Statistics.GetInstance().vaccinationRatelist[i], Statistics.GetInstance().deadPeoplelist[i]);
                }

            if (Statistics.GetInstance().steppingType == 0)
                for (int i = 0; i < Statistics.GetInstance().deadPeoplelist.Count; i++)
                {
                    chart1.Series["efficacyRate"].Points.AddXY(i, Statistics.GetInstance().deadPeoplelist[i]);
                }
        }
    }
}
