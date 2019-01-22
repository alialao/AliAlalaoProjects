using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Immunization.Classes
{
    public class DiseaseProperties
    {
        public double MortalityRateMin { get; }
        public double MortalityRateMax { get; }
        public double InfectionRateMin { get; }
        public double InfectionRateMax { get; }
        public int IncubationPeriodMin { get; }
        public int IncubationPeriodMax { get; }
        public int LatencyPeriodMin { get; }
        public int LatencyPeriodMax { get; }
        public int TerminationPeriodMin { get; }
        public int TerminationPeriodMax { get; }

        public DiseaseProperties(double mortalityRateMin, double mortalityRateMax, double infectionRateMin, double infectionRateMax, int incubationPeriodMin, int incubationPeriodMax, int latencyPeriodMin, int latencyPeriodMax, int terminationPeriodMin, int terminationPeriodMax)
        {
            MortalityRateMin = mortalityRateMin;
            MortalityRateMax = mortalityRateMax;
            InfectionRateMin = infectionRateMin;
            InfectionRateMax = infectionRateMax;
            IncubationPeriodMin = incubationPeriodMin;
            IncubationPeriodMax = incubationPeriodMax;
            LatencyPeriodMin = latencyPeriodMin;
            LatencyPeriodMax = latencyPeriodMax;
            TerminationPeriodMin = terminationPeriodMin;
            TerminationPeriodMax = terminationPeriodMax;
        }
    }

    
}
