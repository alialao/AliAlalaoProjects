using Immunization.Classes.Actors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Immunization.Classes.Actors.Tests
{
    [TestClass()]
    public class PersonTests
    {

        [TestMethod()]
        public void IsSickTest()
        {
            Person examplePerson = new Person("John", 20, true, true, false, 5, 250, 350, false, null);
            Assert.AreEqual(examplePerson.IsSick(), false);

            DiseaseProperties exampleDisease = new DiseaseProperties(5, 15, 5, 15, 10, 15, 3, 5, 20, 25);
            examplePerson.AddInfection(null, exampleDisease);
            Assert.AreEqual(examplePerson.IsSick(), true);
        }



        [TestMethod()]
        public void InfectionAttemptTest()
        {// testMethod most of the time fails to create an instance of object. Repeat until exception not occuring.
            Person originInfectedPerson = new Person("John", 20, true, true, false, 5, 250, 350, false, null);
            DiseaseProperties exampleDisease = new DiseaseProperties(5, 15, 5, 15, 10, 15, 3, 5, 20, 25);
            originInfectedPerson.AddInfection(null, exampleDisease);


            Person normalPerson = new Person("John", 20, true, false, false, 5, 250, 350, false, null);
            string examplereport = normalPerson.Name + ", 0 damage taken, being infected, ";
            Assert.AreEqual(examplereport + "resisted getting sick and becoming infectious.\r\n", normalPerson.InfectionAttempt(originInfectedPerson, exampleDisease));

            //Person immunodificientPerson = new Person("John", 20, true, true, true, 5, 250, 350, false, null);
            //examplereport = immunodificientPerson.Name + ", 0 damage taken, being infected, ";
            //Assert.AreEqual(examplereport + immunodificientPerson.AddInfection(originInfectedPerson, exampleDisease), immunodificientPerson.InfectionAttempt(originInfectedPerson, exampleDisease));

            //Person vaccinatedPerson = new Person("John", 20, true, true, false, 5, 250, 350, false, null);
            //examplereport = vaccinatedPerson.Name + ", 0 damage taken, being infected, ";
            //Assert.AreEqual(examplereport + "vaccinated.\r\n", vaccinatedPerson.InfectionAttempt(originInfectedPerson, exampleDisease));
        }


        [TestMethod()]
        public void AddInfectionTest()
        {//same function to IsSickTest()
            Person normalPerson = new Person("John", 20, true, false, false, 5, 250, 350, false, null);
            DiseaseProperties exampleDisease = new DiseaseProperties(5, 15, 5, 15, 10, 15, 3, 5, 20, 25);

            Assert.AreEqual("got sick and became infectious!\r\n", normalPerson.AddInfection(null, exampleDisease));

            Assert.AreEqual(String.Empty, normalPerson.AddInfection(null, exampleDisease));
        }

        [TestMethod()]
        public void KillTest()
        {
            Person examplePerson = new Person("John", 20, true, true, false, 5, 250, 350, false, null);
            examplePerson.Kill();

            Assert.AreEqual(examplePerson.Health, 0);
        }

        [TestMethod()]
        public void InfoStringTest()
        {
            string Name = "Alex";
            double Health = 100;
            bool IsVaccinated = false;
            bool IsImmunodeficient = false;
            Person normalPerson = new Person(Name, 20, true, IsVaccinated, IsImmunodeficient, 5, 250, 350, false, null);

            string toTest = $"Name: {Name}\n\nHealth: {Health}%\nAlive: {(Health == 0 ? "No" : "Yes")}\nInfected: " + "No" +
                   $"\nVaccinated: {(IsVaccinated ? "Yes" : "No")}\nImmunodeficient: {(IsImmunodeficient ? "Yes" : "No")}";
            Assert.AreEqual(toTest, normalPerson.InfoString());

            normalPerson.AddInfection(null, new DiseaseProperties(5, 15, 5, 15, 10, 15, 3, 5, 20, 25));
            toTest = $"Name: {Name}\n\nHealth: {Health}%\nAlive: {(Health == 0 ? "No" : "Yes")}\nInfected: " + "Yes" +
                               $"\nVaccinated: {(IsVaccinated ? "Yes" : "No")}\nImmunodeficient: {(IsImmunodeficient ? "Yes" : "No")}";

            Assert.AreEqual(toTest, normalPerson.InfoString());
        }

        [TestMethod()]
        public void VaccinateEvilOrcs()
        {
            Person exampleEvilOrc = new Person("Orc", 20, true, false, false, 50, 250, 350, true, null);
            Scene scene = new Scene();
            scene.Vaccinate(exampleEvilOrc);
            bool isVaccinated = exampleEvilOrc.IsVaccinated;

            Assert.AreEqual(isVaccinated, false);
        }

        [TestMethod()]
        public void VaccinateImmunodeficient()
        {
            Person immunodeficientPerson = new Person("Mike", 20, true, false, true, 50, 250, 350, false, null);
            Scene scene = new Scene();
            scene.Vaccinate(immunodeficientPerson);
            bool isVaccinated = immunodeficientPerson.IsVaccinated;

            Assert.AreEqual(isVaccinated, false);
        }


    }
}