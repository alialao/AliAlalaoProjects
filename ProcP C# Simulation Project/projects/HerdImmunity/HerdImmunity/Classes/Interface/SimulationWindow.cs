﻿using System;
using System.Diagnostics;
using Immunization.Classes;
using Immunization.Classes.Actors;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace HerdImmunity.Classes
{
    class SimulationWindow
    {
        private readonly Clock clock;
        private readonly Scene scene;
        private bool activeSimulation;
        private readonly InfoPane infoPane;
        private bool openReports;

        public SimulationWindow(Scene scene, int simNum, bool openReports)
        {
            this.scene = scene;

            infoPane = new InfoPane(scene);
            this.openReports = openReports;

            activeSimulation = true;
            clock = new Clock();

            if (simNum == 1)
            {
                UpdateDrawablesCoordinates(this.scene);
                RenderSimulationGFX();
            }
            else
            {
                RenderSimulationNoGFX();
            }            
        }

        private void clock_Tick()
        {
            if (!scene.Simulate())
            {
                activeSimulation = false;
                if(openReports)
                    Process.Start(scene.ReportFilename);
            }
            else
            {
                infoPane.Update();
            }
        }

        private void RenderSimulationNoGFX()
        {
            clock.Restart();

            while (scene.Simulate())
            {
                //// Check if scene needs updating
                //if (activeSimulation && clock.ElapsedTime.AsMilliseconds() >= Constants.SimulationTickInterval)
                //{
                //    clock_Tick();
                //    clock.Restart();
                //}
            }
            //activeSimulation = false;
            if(openReports)
                Process.Start(scene.ReportFilename);
        }

        private void RenderSimulationGFX()
        {
            var window = new RenderWindow(new VideoMode(Constants.SimulationGFXWidth, Constants.SimulationGFXHeight + Constants.SimulationInfoPaneGFXHeight), "SimulationGFX", Styles.Default);
            window.SetVerticalSyncEnabled(true);

            window.Closed += (sender, arg) => window.Close();
            window.MouseButtonReleased += (sender, arg) => OnMouseButtonReleased(sender, arg);

            clock.Restart();
            while (window.IsOpen)
            {
                // Check if scene needs updating
                if(activeSimulation&&clock.ElapsedTime.AsMilliseconds()>=Constants.SimulationTickInterval)
                {
                    // Hold off advancing the scene if in interactive mode and no infected person yet
                    if (!(scene.Interactive&&!scene.DiseaseInPlay()))
                    {
                        clock_Tick();
                        clock.Restart();
                    } 
                }

                // Process events
                window.DispatchEvents();
                
                // Wipe the window
                window.Clear(Color.White);

                // Draw infection links
                foreach (var link in scene.InfectionLinks)
                {
                    Vertex[] line =
                    {
                        new Vertex(link.From.Sprite.Position, Color.Red),
                        new Vertex(link.To.Sprite.Position, Color.Blue)
                    };

                    window.Draw(line, PrimitiveType.Lines);
                }

                // Draw people
                foreach (Person p in scene.People)
                {
                    // Draw infection state indicator
                   
                    CircleShape indicator = new CircleShape(10);
                    indicator.OutlineThickness = 2;
                    indicator.Position = p.Sprite.Position;
                    indicator.Origin = new Vector2f(10,10);
                    if (p.IsSick())
                    {
                        indicator.OutlineColor = Constants.DiseasedColor;
                        if (p.IsImmunodeficient || (p.IsAnEvilOrc == false && p.IsVaccinated == false))
                        {
                            Debug.WriteLine(p.IsAnEvilOrc);
                            indicator.OutlineColor = Constants.ImmunodeficientOrFailedVaccinationColor;
                        }
                    }
                    window.Draw(indicator);

                    window.Draw(p);
                }
                // Draw info pane
                window.Draw(infoPane);

                // Display on screen what has been rendered to the window this loop
                window.Display();
            }

            activeSimulation = false;
        }

        private void OnMouseButtonReleased(object sender, MouseButtonEventArgs arg)
        {
            // Check if within simulation box
            if (Constants.SimulationGFXIntRect.Contains(arg.X, arg.Y))
            {
                // Check if within sprite box of any person object
                foreach (var p in scene.People)
                {
                    FloatRect rect = new FloatRect(p.Sprite.Position.X - p.Sprite.Origin.X, p.Sprite.Position.Y - p.Sprite.Origin.X, 
                        p.Sprite.GetGlobalBounds().Width, p.Sprite.GetGlobalBounds().Height);

                    if (rect.Contains(arg.X, arg.Y))
                    {
                        infoPane.WatchedPerson = p;
                        infoPane.Update();
                        scene.OnPersonClick(p);
                        return;
                    }
                }
                infoPane.WatchedPerson = null;
            }
        }

        private (float, float) TranslateCoordinatesToOnScreen(Double x, Double y)
        {
            double pointsX = Constants.SimulationSceneMaxX - Constants.SimulationSceneMinX;
            double pointsY = Constants.SimulationSceneMaxY - Constants.SimulationSceneMinY;

            double pixelsPerX = Constants.SimulationGFXWidth / pointsX;
            double pixelsPerY = Constants.SimulationGFXHeight / pointsY;

            return ((float)(int)(x * pixelsPerX), (float)(int)(y * pixelsPerY));
        }

        private void UpdateDrawablesCoordinates(Scene scene)
        {
            foreach (Person p in scene.People)
            {
                var coordinates = TranslateCoordinatesToOnScreen(p.X, p.Y);
                var spriteOffset = Constants.PersonGenerationSpritesheetSpriteSize / 2;
                p.Sprite.Position = new Vector2f(coordinates.Item1, coordinates.Item2);
                p.Sprite.Origin = new Vector2f(spriteOffset, spriteOffset);

                var labelBounds = p.Label.GetGlobalBounds();
                p.Label.Origin = new Vector2f((int)labelBounds.Width / 2, (int)labelBounds.Height / 2);
                p.Label.Position = new Vector2f((int)coordinates.Item1, (int)coordinates.Item2 - Constants.PersonGenerationSpritesheetSpriteSize);
            }
        }
    }
}
