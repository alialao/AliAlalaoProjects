using System;
using System.Diagnostics;
using Immunization.Classes;
using System.IO;
using HerdImmunity.Classes;
using IniParser;
using IniParser.Model;
using System.Threading.Tasks;
using System.Windows.Forms;
using HerdImmunity.Classes.Interface;
using HerdImmunity.Classes.Misc;
using System.IO.Compression;
using System.Linq;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Immunization
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonBegin_Click(object sender, EventArgs e)
        {
            PopulationProperties population = new PopulationProperties(Convert.ToInt16(numericUpDownPopulationSize.Value),
                Convert.ToDouble(numericUpDownPopulationVaccinationRate.Value), Convert.ToDouble(numericUpDownPopulationImmunodeficiency.Value),
                Convert.ToInt16(numericUpDownPopulationDensity.Value), Convert.ToDouble(numericUpDownPopulationCleanliness.Value));

            VaccineProperties vaccination = new VaccineProperties(Convert.ToDouble(numericUpDownVaccineEfficacyRateMin.Value),
                Convert.ToDouble(numericUpDownVaccineEfficacyRateMax.Value), Convert.ToDouble(numericUpDownVaccineMortalityRateMin.Value),
                Convert.ToDouble(numericUpDownVaccineMortalityRateMax.Value));

            Statistics.GetInstance().OpenReport = checkBoxTextReports.Checked;
            Statistics.GetInstance().Purge();

            for (int i = 0; i < numericUpDownSimulationNum.Value; i++)
            {
                Scene scene;
                // Substitute chance values when stepping is enabled
                if (radioButtonStepPopulationVaccination.Checked)
                {
                    population.VaccinationRate = Utility.GetStep(i, Convert.ToInt32(numericUpDownSimulationNum.Value));
                    Statistics.GetInstance().steppingType = 1;
                    scene = new Scene(population, getDisease(), vaccination, numericUpDownSimulationNum.Value == 1, (i + 1).ToString(), false, checkBoxInteractive.Checked);
                }
                else if (radioButtonStepVaccineEfficacy.Checked)
                {
                    vaccination.EfficacyRateMin = vaccination.EfficacyRateMax = Utility.GetStep(i, Convert.ToInt32(numericUpDownSimulationNum.Value));
                    Statistics.GetInstance().steppingType = 2;
                    scene = new Scene(population, getDisease(), vaccination, numericUpDownSimulationNum.Value == 1, (i + 1).ToString(), true, checkBoxInteractive.Checked);
                }
                else
                {
                    Statistics.GetInstance().steppingType = 0;
                    scene = new Scene(population, getDisease(), vaccination, numericUpDownSimulationNum.Value == 1, (i + 1).ToString(), false, checkBoxInteractive.Checked);
                }

                // Start simulation in a new thread - otherwise the UI may stop responding
                Task.Factory.StartNew(() =>
                {
                    new SimulationWindow(scene, Convert.ToInt32(numericUpDownSimulationNum.Value), checkBoxTextReports.Checked);
                }).Wait();

            }
            if (numericUpDownSimulationNum.Value != 1)
            {
                Statistics_graph statistics_graph = new Statistics_graph();
                statistics_graph.Show();
            }
            DateTime foo = DateTime.UtcNow;
            long unixTime = ((DateTimeOffset)foo).ToUnixTimeSeconds();
            String timeStamp = Convert.ToString(unixTime);
            using (var tw = new StreamWriter(timeStamp + "_visualization.stat", true))
            {
                String line = "";
                foreach (double i in Statistics.GetInstance().efficacyRatelist)
                {
                    line += Convert.ToString(i) + ",";
                }
                tw.WriteLine(line.Remove(line.Length - 1));
                line = "";
                foreach (int i in Statistics.GetInstance().deadImmunodeficientlist)
                {
                    line += Convert.ToString(i) + ",";
                }
                tw.WriteLine(line.Remove(line.Length - 1));
                line = "";
                foreach (double i in Statistics.GetInstance().mortalityRatelist)
                {
                    line += Convert.ToString(i) + ",";
                }
                tw.WriteLine(line.Remove(line.Length - 1));
                line = "";
                foreach (int i in Statistics.GetInstance().deadPeoplelist)
                {
                    line += Convert.ToString(i) + ",";
                }
                tw.WriteLine(line.Remove(line.Length - 1));
                line = "";
                foreach (int i in Statistics.GetInstance().numberOfInfectionslist)
                {
                    line += Convert.ToString(i) + ",";
                }
                tw.WriteLine(line.Remove(line.Length - 1));
            }
        }
        

        private void buttonPopulationSave_Click(object sender, EventArgs e)
        {
            var parser = new FileIniDataParser();
            parser.Parser.Configuration.ThrowExceptionsOnError = false;
            IniData data = new IniData();

            data["Sample"]["Size"] = numericUpDownPopulationSize.Text;
            data["Sample"]["VaccinationRate"] = Utility.FromNumericControlValue(numericUpDownPopulationVaccinationRate.Value);
            data["Sample"]["Immunodeficiency"] = Utility.FromNumericControlValue(numericUpDownPopulationImmunodeficiency.Value);
            data["Sample"]["Density"] = Utility.FromNumericControlValue(numericUpDownPopulationDensity.Value);
            data["Sample"]["Cleanliness"] = Utility.FromNumericControlValue(numericUpDownPopulationCleanliness.Value);

            parser.WriteFile($"{Constants.INIPopulationsPath}/{comboBoxPopulation.Text}.ini", data);

            setStatus($"Saved '{comboBoxPopulation.Text}' to file.");

            RefreshComboBoxes();
        }

        private void comboBoxPopulation_SelectedIndexChanged(object sender, EventArgs e)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile($"{Constants.INIPopulationsPath}/{comboBoxPopulation.Text}.ini");

            numericUpDownPopulationSize.Value = Utility.ToNumericControlValue(data["Sample"]["Size"]);
            numericUpDownPopulationVaccinationRate.Value = Utility.ToNumericControlValue(data["Sample"]["VaccinationRate"]);
            numericUpDownPopulationImmunodeficiency.Value = Utility.ToNumericControlValue(data["Sample"]["Immunodeficiency"]);
            numericUpDownPopulationDensity.Value = Utility.ToNumericControlValue(data["Sample"]["Density"]);
            numericUpDownPopulationCleanliness.Value = Utility.ToNumericControlValue(data["Sample"]["Cleanliness"]);

            if (comboBoxPopulation.Focused)
                setStatus($"Loaded '{comboBoxPopulation.Text}' from file.");
        }

        private void buttonDiseaseSave_Click(object sender, EventArgs e)
        {
            var parser = new FileIniDataParser();
            IniData data = new IniData();

            data["Disease"]["MortalityRateMin"] = Utility.FromNumericControlValue(numericUpDownDiseaseMortalityRateMin.Value); 
            data["Disease"]["MortalityRateMax"] = Utility.FromNumericControlValue(numericUpDownDiseaseMortalityRateMax.Value);
            data["Disease"]["InfectionRateMin"] = Utility.FromNumericControlValue(numericUpDownDiseaseInfectionRateMin.Value);
            data["Disease"]["InfectionRateMax"] = Utility.FromNumericControlValue(numericUpDownDiseaseInfectionRateMax.Value);
            data["Disease"]["IncubationPeriodMin"] = Utility.FromNumericControlValue(numericUpDownDiseaseIncubationPeriodMin.Value);
            data["Disease"]["IncubationPeriodMax"] = Utility.FromNumericControlValue(numericUpDownDiseaseIncubationPeriodMax.Value);
            data["Disease"]["LatencyPeriodMin"] = Utility.FromNumericControlValue(numericUpDownDiseaseLatencyPeriodMin.Value);
            data["Disease"]["LatencyPeriodMax"] = Utility.FromNumericControlValue(numericUpDownDiseaseLatencyPeriodMax.Value);
            data["Disease"]["TerminationPeriodMin"] = Utility.FromNumericControlValue(numericUpDownDiseaseTerminationPeriodMin.Value);
            data["Disease"]["TerminationPeriodMax"] = Utility.FromNumericControlValue(numericUpDownDiseaseTerminationPeriodMax.Value);

            data["Vaccine"]["MortalityRateMin"] = Utility.FromNumericControlValue(numericUpDownVaccineMortalityRateMin.Value);
            data["Vaccine"]["MortalityRateMax"] = Utility.FromNumericControlValue(numericUpDownVaccineMortalityRateMax.Value);
            data["Vaccine"]["EfficacyRateMin"] = Utility.FromNumericControlValue(numericUpDownVaccineEfficacyRateMin.Value);
            data["Vaccine"]["EfficacyRateMax"] = Utility.FromNumericControlValue(numericUpDownVaccineEfficacyRateMax.Value);

            parser.WriteFile($"{Constants.INIDiseasesPath}/{comboBoxDisease.Text}.ini", data);

            setStatus($"Saved '{comboBoxDisease.Text}' to file.");

            RefreshComboBoxes();
        }

        private void comboBoxDisease_SelectedIndexChanged(object sender, EventArgs e)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile($"{Constants.INIDiseasesPath}/{comboBoxDisease.Text}.ini");

            numericUpDownDiseaseMortalityRateMin.Value = Utility.ToNumericControlValue(data["Disease"]["MortalityRateMin"]);
            numericUpDownDiseaseMortalityRateMax.Value = Utility.ToNumericControlValue(data["Disease"]["MortalityRateMax"]);
            numericUpDownDiseaseInfectionRateMin.Value = Utility.ToNumericControlValue(data["Disease"]["InfectionRateMin"]);
            numericUpDownDiseaseInfectionRateMax.Value = Utility.ToNumericControlValue(data["Disease"]["InfectionRateMax"]);
            numericUpDownDiseaseIncubationPeriodMin.Value = Utility.ToNumericControlValue(data["Disease"]["IncubationPeriodMin"]);
            numericUpDownDiseaseIncubationPeriodMax.Value = Utility.ToNumericControlValue(data["Disease"]["IncubationPeriodMax"]);
            numericUpDownDiseaseLatencyPeriodMin.Value = Utility.ToNumericControlValue(data["Disease"]["LatencyPeriodMin"]);
            numericUpDownDiseaseLatencyPeriodMax.Value = Utility.ToNumericControlValue(data["Disease"]["LatencyPeriodMax"]);
            numericUpDownDiseaseTerminationPeriodMin.Value = Utility.ToNumericControlValue(data["Disease"]["TerminationPeriodMin"]);
            numericUpDownDiseaseTerminationPeriodMax.Value = Utility.ToNumericControlValue(data["Disease"]["TerminationPeriodMax"]);

            numericUpDownVaccineMortalityRateMin.Value = Utility.ToNumericControlValue(data["Vaccine"]["MortalityRateMin"]);
            numericUpDownVaccineMortalityRateMax.Value = Utility.ToNumericControlValue(data["Vaccine"]["MortalityRateMax"]);
            numericUpDownVaccineEfficacyRateMin.Value = Utility.ToNumericControlValue(data["Vaccine"]["EfficacyRateMin"]);
            numericUpDownVaccineEfficacyRateMax.Value = Utility.ToNumericControlValue(data["Vaccine"]["EfficacyRateMax"]);

            if (comboBoxDisease.Focused)
                setStatus($"Loaded '{comboBoxDisease.Text}' from file.");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var (populationConfigCount, diseaseConfigCount) = RefreshComboBoxes(); 

            setStatus($"Found {populationConfigCount} population file{(populationConfigCount != 1 ? "s" : "")} and {diseaseConfigCount} disease file{(diseaseConfigCount != 1 ? "s" : "")}.");

            // Load files on start
            if (populationConfigCount > 0)
                comboBoxPopulation.SelectedIndex = 0;
            if(diseaseConfigCount > 0)
                comboBoxDisease.SelectedIndex = 0;
        }

        private (int, int) RefreshComboBoxes()
        {
            int populationConfigCount = 0, diseaseConfigCount = 0;

            comboBoxPopulation.Items.Clear();
            foreach (string file in Directory.EnumerateFiles(Constants.INIPopulationsPath, "*.ini"))
            {
                comboBoxPopulation.Items.Add(Path.GetFileNameWithoutExtension((file)));
                populationConfigCount++;
            }

            comboBoxDisease.Items.Clear();
            foreach (string file in Directory.EnumerateFiles(Constants.INIDiseasesPath, "*.ini"))
            {
                comboBoxDisease.Items.Add(Path.GetFileNameWithoutExtension((file)));
                diseaseConfigCount++;
            }

            return (populationConfigCount, diseaseConfigCount);
        }

        private void setStatus(String text)
        {
            labelStatus.Text = $"[{DateTime.Now.ToString("hh:mm:ss")}] {text}";
        }

        private DiseaseProperties getDisease()
        {
            double mortalityRateMin = Convert.ToDouble(numericUpDownDiseaseMortalityRateMin.Text);
            double mortalityRateMax = Convert.ToDouble(numericUpDownDiseaseMortalityRateMax.Text);
            double infectionRateMin = Convert.ToDouble(numericUpDownDiseaseInfectionRateMin.Text);
            double infectionRateMax = Convert.ToDouble(numericUpDownDiseaseInfectionRateMax.Text);
            int incubationPeriodMin = Convert.ToInt32(numericUpDownDiseaseIncubationPeriodMin.Text);
            int incubationPeriodMax = Convert.ToInt32(numericUpDownDiseaseIncubationPeriodMax.Text);
            int latencyPeriodMin = Convert.ToInt32(numericUpDownDiseaseLatencyPeriodMin.Text);
            int latencyPeriodMax = Convert.ToInt32(numericUpDownDiseaseLatencyPeriodMax.Text);
            int terminationPeriodMin = Convert.ToInt32(numericUpDownDiseaseTerminationPeriodMin.Text);
            int terminationPeriodMax = Convert.ToInt32(numericUpDownDiseaseTerminationPeriodMax.Text);

            return new DiseaseProperties(mortalityRateMin, mortalityRateMax, infectionRateMin, infectionRateMax, incubationPeriodMin, incubationPeriodMax, latencyPeriodMin, latencyPeriodMax, terminationPeriodMin, terminationPeriodMax);
        }

        private void radioButtonStepPopulationVaccination_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownPopulationVaccinationRate.Enabled = !radioButtonStepPopulationVaccination.Checked;
        }

        private void radioButtonStepVaccineEfficacy_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDownVaccineEfficacyRateMin.Enabled = !radioButtonStepVaccineEfficacy.Checked;
            numericUpDownVaccineEfficacyRateMax.Enabled = !radioButtonStepVaccineEfficacy.Checked;
        }

        private void radioButtonNoStepping_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButtonNoStepping.Checked) return;
            numericUpDownVaccineEfficacyRateMax.Enabled = true;
            numericUpDownVaccineEfficacyRateMin.Enabled = true;
            numericUpDownPopulationVaccinationRate.Enabled = true;
        }

        private void buttonLoadResult_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "STAT files|*.stat";
            theDialog.InitialDirectory = Application.ExecutablePath;
            //theDialog.InitialDirectory = @"C:\Users\Kaloyan\Documents\ProcP\projects\HerdImmunity\HerdImmunity\bin\Debug";
            if (theDialog.ShowDialog() == DialogResult.OK)
            if ((myStream = theDialog.OpenFile()) != null)
            {
                using (var reader = new StreamReader(myStream))
                {
                    int i = 0;
                    
                    while (!reader.EndOfStream)
                    {
                        switch (i)
                        {
                            case 0:
                                foreach (String s in reader.ReadLine().Split(','))
                                {
                                    Statistics.GetInstance().efficacyRatelist.Add(Convert.ToDouble(s));
                                }
                                break;
                            case 1:
                                foreach (String s in reader.ReadLine().Split(','))
                                {
                                    Statistics.GetInstance().deadImmunodeficientlist.Add(Convert.ToInt32(s));
                                }
                                break;
                            case 2:
                                foreach (String s in reader.ReadLine().Split(','))
                                {
                                    Statistics.GetInstance().mortalityRatelist.Add(Convert.ToDouble(s));
                                }
                                break;
                            case 3:
                                foreach (String s in reader.ReadLine().Split(','))
                                {
                                    Statistics.GetInstance().deadPeoplelist.Add(Convert.ToInt32(s));
                                }
                                break;
                            case 4:
                                foreach (String s in reader.ReadLine().Split(','))
                                {
                                    Statistics.GetInstance().numberOfInfectionslist.Add(Convert.ToInt32(s));
                                }
                                break;
                        }
                        i++;
                    }

                    Statistics_graph statistics_graph = new Statistics_graph();
                    statistics_graph.Show();
                }
            }
        }

        private void numericUpDownSimulationNum_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownSimulationNum.Value == 1)
            {
                radioButtonNoStepping.Checked = true;
                checkBoxInteractive.Enabled = true;
            }
            else
            {
                checkBoxInteractive.Checked = false;
                checkBoxInteractive.Enabled = false;
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var populationPath = Directory.GetCurrentDirectory() + "\\" + Constants.INIPopulationsPath;
                var diseasesPath = Directory.GetCurrentDirectory() + "\\" + Constants.INIDiseasesPath;
                
                if (!Utility.IsDirectoryEmpty(populationPath))
                   ZipFile.CreateFromDirectory(populationPath, dialog.FileName+"\\"+"HerdImmunity - PopulationExport.hipop");
                if (!Utility.IsDirectoryEmpty(diseasesPath))
                    ZipFile.CreateFromDirectory(diseasesPath, dialog.FileName + "\\" + "HerdImmunity - DiseaseExport.hidis");

                setStatus($"Exported to {dialog.FileName}.");
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var populationPath = Directory.GetCurrentDirectory() + "\\" + Constants.INIPopulationsPath;
                var diseasesPath = Directory.GetCurrentDirectory() + "\\" + Constants.INIDiseasesPath;

                int i = 0;

                var populationFiles = Directory.GetFiles(dialog.FileName, "*.hipop", SearchOption.TopDirectoryOnly);
                foreach (var file in populationFiles)
                {
                    try
                    {
                        ZipFile.ExtractToDirectory(file, populationPath);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show(
                            "An error has occured while importing population files. Imported files may already exist.");
                    }

                    i++;
                }

                var diseaseFiles = Directory.GetFiles(dialog.FileName, "*.hidis", SearchOption.TopDirectoryOnly);
                foreach (var file in diseaseFiles)
                {
                    try
                    {
                        ZipFile.ExtractToDirectory(file, diseasesPath);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show(
                            "An error has occured while importing disease files. Imported files may already exist.");
                    }

                    i++;
                }

                RefreshComboBoxes();
                setStatus($"Imported {i} files from {dialog.FileName}.");
            }
        }
    }
}
