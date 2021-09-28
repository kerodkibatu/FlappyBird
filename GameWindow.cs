using System;
using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace FlappyBird
{
    class GameWindow
    {
        //Initialization values
        private const int WIDTH = 1280;
        private const int HEIGHT = 720;
        private const string TITLE = "Flappy Bird";
        private const int FPS = 60;
        private RenderWindow window;






        //FPS Counter values
        List<float> fps = new List<float>(60);
        Clock clock = new Clock();
        Time previousTime;
        Time currentTime;



        Font DefaultFont = new Font(@"C:\Users\KK\source\repos\ContinuousGeneticEnviroment\Font\GOTHIC.TTF");

        //System variables
        float fpsAvg = 60;
        int timepassed = 0;

        //Bird
        int PopulationCount = 10;
        List<Bird> Birds = new List<Bird>();
        Bird Bird = new Bird();
        List<Pipe> Pipes = new List<Pipe>();
        Pipe CurrentPipe;
        public GameWindow()
        {
            var videoMode = new VideoMode(WIDTH,HEIGHT);
            window = new RenderWindow(videoMode,TITLE);
            window.SetFramerateLimit(FPS);
            window.KeyPressed += Window_KeyPressed;
            window.Closed += (sender, args) => { this.window.Close(); };

            for (int i = 0; i < PopulationCount; i++)
            {
                Birds.Add(new Bird());
            }
        }

        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Space)
            {
                Bird.Up();
            }
        }
        
        public void run()
        {
            Random h = new Random();
            Pipes.Add(new Pipe((float)h.Next(100, (int)window.Size.Y - 100), window));
            Pipes[0].isCurrent = true;
            while (this.window.IsOpen)
            {
                handleEvents();
                update();
                draw();
                CountFPS();
            }
        }
        private void update()
        {
                Bird.Update(window, RenderStates.Default);
                if (!Bird.Alive)
                {
                    Bird = new Bird();
                    Pipes.Clear();
                }
                else if (Bird.Alive)
                {
                    Bird.Distance += 0.1f;

                }
                for (int i = 0; i < Pipes.Count; i++)
                {
                    Pipes[i].Update(window);
                    if (Pipes[i].isCurrent)
                    {
                        CurrentPipe = Pipes[i];
                    }
                    if (Pipes[i].Top.GetGlobalBounds().Intersects(Bird.shape.GetGlobalBounds()) ||
                        Pipes[i].Bottom.GetGlobalBounds().Intersects(Bird.shape.GetGlobalBounds()))
                    {
                        Bird.Alive = false;
                    }
                    if (!Pipes[i].Scored && Pipes[i].Top.Position.X + 20 < Bird.shape.Position.X)
                    {
                        Bird.Score++;
                        window.SetTitle("Score: " + Bird.Score);
                        Pipes[i].Scored = true;
                        Pipes[i].isCurrent = false;
                        Pipes[i + 1].isCurrent = true;
                    }
                    if (Pipes[i].outOfScreen)
                    {
                        Pipes.RemoveAt(i);
                    }
                }
            if (timepassed == 90)
            {
                Random h = new Random();
                Pipes.Add(new Pipe((float)h.Next(100, (int)window.Size.Y - 100), window));
                timepassed = 0;
            }
        }

        private void draw()
        {
            window.Clear(new Color(100,100,100));
            window.Draw(Bird);
            for (int i = 0; i < Pipes.Count; i++)
            {
                Pipes[i].Draw(window,RenderStates.Default);
            }
            window.Display();
        }
        private void handleEvents()
        {
            this.window.DispatchEvents();
        }
        private void CountFPS()
        {
            currentTime = clock.ElapsedTime;
            fps.Add(1.0f / (currentTime.AsSeconds() - previousTime.AsSeconds())); // the asSeconds returns a float
            float sum = 0;
            foreach (var item in fps)
            {
                sum += item;
            }
            if (fps.Count > 500)
            {
                fps.RemoveRange(0, 350);
            }
            fpsAvg = sum;
            timepassed++;
            previousTime = currentTime;
        }
    }
}
