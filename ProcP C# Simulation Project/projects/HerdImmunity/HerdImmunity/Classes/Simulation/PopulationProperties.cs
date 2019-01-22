using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immunization.Classes
{
    public class PopulationProperties
    {
        public int SampleSize;
        public double VaccinationRate;
        public double Immunodeficiency;
        public int Density;
        public double Cleanliness;


        public PopulationProperties(int sampleSize, double vaccinationRate, double immunodeficiency, int density, double cleanliness)
        {
            SampleSize = sampleSize;
            VaccinationRate = vaccinationRate;
            Immunodeficiency = immunodeficiency;
            Density = density;
            Cleanliness = cleanliness;
        }
    }
}
