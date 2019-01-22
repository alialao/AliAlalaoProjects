using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerdImmunity.Classes.Misc
{
    //Statistics is Singleton
    public sealed class Statistics
    {
        public List<long> deadPeoplelist = new List<long>();
        public List<long> deadImmunodeficientlist = new List<long>();
        public List<long> numberOfInfectionslist = new List<long>();
        public List<double> efficacyRatelist = new List<double>();
        public List<double> mortalityRatelist = new List<double>();
        public List<double> vaccinationRatelist = new List<double>();

        private static readonly Statistics instance = null;

        private static String timeStamp;
        public long numberOfOrcs { get; set; }
        public long numberOfPeople { get; set; }
        public long deadOrcs { get; set; }
        public long deadPeople { get; set; }
        public long immunodeficient { get; set; }
        public long deadImmunodeficient { get; set; }
        public long numberOfInfections { get; set; }
        public double efficacyRate { get; set; }
        public double mortalityRate { get; set; }
        public double vaccinationRate { get; set; }

        public short steppingType = 0;

        public bool OpenReport = true;

        private Statistics()
        {
            numberOfOrcs = 0;
            numberOfPeople = 0;
            deadOrcs = 0;
            deadPeople = 0;
            immunodeficient = 0;
            deadImmunodeficient = 0;
            numberOfInfections = 0;
        }

        static Statistics()
        {
            instance = new Statistics();
            DateTime foo = DateTime.UtcNow;
            long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
            timeStamp = Convert.ToString(unixTime);
        }
        public static Statistics GetInstance()
        {
            return instance;
        }

        public async void writeStatisticsReport()
        {
            bool retry = true;
            while (retry)
            {
                try
                {
                    using (var tw = new StreamWriter(timeStamp + "_statistics.txt", true))
                    {
                        tw.WriteLine("--------------------------New Simulation--------------------------");
                        tw.WriteLine("Number of Orcs: " + Convert.ToString(numberOfOrcs));
                        tw.WriteLine("Number of People: " + Convert.ToString(numberOfPeople));
                        tw.WriteLine("Number of dead Orcs: " + Convert.ToString(deadOrcs));
                        tw.WriteLine("Number of dead People: " + Convert.ToString(deadPeople));
                        tw.WriteLine("Number of immunodeficient: " + Convert.ToString(immunodeficient));
                        tw.WriteLine("Number of dead immunodeficient: " + Convert.ToString(deadImmunodeficient));
                        tw.WriteLine("Number of infections: " + Convert.ToString(numberOfInfections));
                    }
                   
                    if(OpenReport)
                        Process.Start(timeStamp + "_statistics.txt");
                    retry = false;
                }
                catch (Exception e)
                {
                    Thread.Sleep(10);
                }
            }
        }

        public void NewIteration()
        {
            deadPeoplelist.Add(deadPeople+deadOrcs);
            deadImmunodeficientlist.Add(deadImmunodeficient);
            numberOfInfectionslist.Add(numberOfInfections);
            efficacyRatelist.Add(efficacyRate);
            mortalityRatelist.Add(mortalityRate);
            vaccinationRatelist.Add(vaccinationRate);

            numberOfOrcs = 0;
            numberOfPeople = 0;
            deadOrcs = 0;
            deadPeople = 0;
            immunodeficient = 0;
            deadImmunodeficient = 0;
            numberOfInfections = 0;
        }

        public void Purge()
        {
            deadPeoplelist.Clear();
            deadImmunodeficientlist.Clear();
            numberOfInfectionslist.Clear();
            efficacyRatelist .Clear();
            mortalityRatelist.Clear();
            vaccinationRatelist.Clear();

            numberOfOrcs = 0;
            numberOfPeople = 0;
            deadOrcs = 0;
            deadPeople = 0;
            immunodeficient = 0;
            deadImmunodeficient = 0;
            numberOfInfections = 0;
            efficacyRate = 0;
            mortalityRate = 0;
            vaccinationRate = 0;
        }
    }
}
