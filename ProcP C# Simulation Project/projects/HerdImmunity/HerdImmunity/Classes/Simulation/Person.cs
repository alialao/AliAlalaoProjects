using SFML.Graphics;
using System;
using HerdImmunity.Classes.Misc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HerdImmunity.Classes.Actors;

namespace Immunization.Classes.Actors
{
    public class Person : Drawable
    {
        public string Name;
        public double Age;
        public bool IsMale;
        public bool IsVaccinated;
        public bool IsImmunodeficient;
        public double Susceptibility;
        public double Health = 100.0;
        public List<Infection> Infections;
        
        public bool IsAnEvilOrc;

        public double X;
        public double Y;

        public Sprite Sprite;
        public Text Label;

        private Scene parentScene;

        public Person(string name, double age, bool isMale, bool isVaccinated, bool isImmunodeficient, double susceptibility, double x, double y, bool isAnEvilOrc, Scene parentScene)
        {
            Name = name;
            Age = age;
            IsMale = isMale;
            IsVaccinated = isVaccinated;
            IsImmunodeficient = isImmunodeficient;
            Susceptibility = susceptibility;
            X = x;
            Y = y;
            IsAnEvilOrc = isAnEvilOrc;

            this.parentScene = parentScene;

            Infections = new List<Infection>();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (this.Health > 0)
            {
                Sprite.Color = Utility.GetHealthColor(this.Health);
            }
            else
            {
                Sprite.Color = Color.White;
            }
            target.Draw(Sprite, states);
            target.Draw(Label, states);

        }

        public bool IsSick()
        {
            return Infections.Count > 0;
        }

        public bool IsBeingDamaged()
        {
            return Infections.Any(o => o.Incubated);
        }

        public bool SpreadsDisease()
        {
            return Infections.Any(o => o.Contagious);
        }

        public string Tick()
        {
            string report = "";
            foreach (var infection in Infections)
            {
                infection.Tick();

                if (this.Health > 0 && infection.Incubated)
                {
                    double damage = Math.Round(Utility.GetSusceptibilityWeightedValue(infection.Type.MortalityRateMin, infection.Type.MortalityRateMax, Susceptibility)/
                                               Utility.GetSusceptibilityWeightedValue(infection.Type.TerminationPeriodMin, infection.Type.TerminationPeriodMax, Susceptibility), 2);
                    this.Health -= damage;
                    if (damage < 0.0)
                    {
                        report += Name + "," + Convert.ToString(damage * (-1)) + " damage healed. \r\n";
                    }
                    else
                    {
                        report += Name + "," + Convert.ToString(damage) + " damage taken. \r\n";
                    }

                    if (this.Health <= 1)
                    {
                        Health = 0;
                        this.onDeath();
                        report += "\t" + Name + " died! \n";

                        if (IsAnEvilOrc)
                        {
                            Statistics.GetInstance().deadOrcs++;
                        } else
                        {
                            Statistics.GetInstance().deadPeople++;
                        }
                        if (IsImmunodeficient)
                            Statistics.GetInstance().deadImmunodeficient++;
                    }    
                }

                if(infection.Contagious)
                    report += Sneeze();
            }

            return report;
        }

        private string Sneeze()
        {
            string infectionReport = "";
            foreach (var infection in Infections)
            {
                if (infection.SickDays >= infection.Type.IncubationPeriodMin &&
                    infection.SickDays <= infection.Type.TerminationPeriodMin)
                {
                    List<Person> closePeople = parentScene.GetPeopleInRange(this, ((infection.Type.InfectionRateMax + infection.Type.InfectionRateMin) / 2 / 10) * 3.5 + 5);
                    foreach (Person p2 in closePeople)
                    {
                        infectionReport += Convert.ToString(parentScene.passedDays + 1) + " \t" + p2.InfectionAttempt(this, infection.Type);
                    }
                }
            }

            return infectionReport;
        }

        public string InfectionAttempt(Person origin, DiseaseProperties infectionType)
        {
            string report = Name + ", 0 damage taken, being infected, ";

            if(this.IsVaccinated)
            {
                report += "vaccinated.\r\n";
            }
            else if(IsImmunodeficient)
            {
                report += AddInfection(origin, infectionType);
            }
            else if (Utility.RollWithSusceptibility(infectionType.InfectionRateMin, infectionType.InfectionRateMax, Susceptibility))
            {
                report += "resisted getting sick and becoming infectious.\r\n";
            }
            else
            {
                report += AddInfection(origin, infectionType);  
            }

            return report;
        }

        public string AddInfection(Person origin, DiseaseProperties infectionType)
        {
            var currentDiseases = Infections.Select(o => o.Type).ToList();

            // Only infect a person with a given disease type once
            if (!currentDiseases.Contains(infectionType))
            {
                Infections.Add(new Infection(infectionType, this));
                Statistics.GetInstance().numberOfInfections++;
                if(origin!=null)
                    parentScene.InfectionLinks.Add((origin, this));
                return "got sick and became infectious!\r\n";
            }
            return String.Empty;
        }

        public void Kill()
        {
            this.Health = 0;
            onDeath();
        }

        private void onDeath()
        {
            if (parentScene != null)
            {
                if (parentScene.GFXEnabled)
                {
                    var newSprite = new Sprite(Constants.PersonSimulationDeadTexture)
                    {
                        Position = Sprite.Position,
                        Origin = Sprite.Origin
                    };

                    Sprite = newSprite;
                }
            }
            
        }

        public string InfoString()
        {
            return $"Name: {Name}\n\nHealth: {Health}%\nAlive: {(Health == 0 ? "No" : "Yes")}\nInfected: {(IsSick() ? "Yes" : "No")}" +
                   $"\nVaccinated: {(IsVaccinated ? "Yes" : "No")}\nImmunodeficient: {(IsImmunodeficient ? "Yes" : "No")}";
        }
    }
}
