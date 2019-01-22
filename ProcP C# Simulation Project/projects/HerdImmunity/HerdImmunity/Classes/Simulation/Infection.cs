using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Immunization.Classes;
using Immunization.Classes.Actors;

namespace HerdImmunity.Classes.Actors
{
    public class Infection
    {
        public DiseaseProperties Type;
        public int SickDays;
        public bool Incubated;
        public bool Contagious;
        private Person host;
        private double incubationDelay;
        private double contagiousDelay;

        public Infection(DiseaseProperties type, Person host)
        {
            Type = type;
            SickDays = 0;
            Incubated = false;
            Contagious = false;
            this.host = host;

            incubationDelay = Utility.GetSusceptibilityWeightedValue(Type.IncubationPeriodMin, Type.IncubationPeriodMax,
                host.Susceptibility);
            contagiousDelay = Utility.GetSusceptibilityWeightedValue(Type.LatencyPeriodMin, Type.LatencyPeriodMax,
                host.Susceptibility);
        }

        public void Tick()
        {
            
            if (SickDays == Convert.ToInt32(incubationDelay))
                Incubated = true;
            if (SickDays == Convert.ToInt32(contagiousDelay))
                Contagious = true;

            SickDays++;
        }
    }
}
