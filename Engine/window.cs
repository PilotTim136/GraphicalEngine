using SFML.Graphics;
using SFML.Window;
using GraphicalEngine.Engine;

namespace GraphicalEngine.window
{
    public class Window
    {
        static RenderWindow RWindow = null!;

        public Window(uint sizeX, uint sizeY, string title)
        {
            //create the window
            RWindow = new RenderWindow(new VideoMode(sizeX, sizeY), title);
            RWindow.Closed += (_, __) => RWindow.Close();

            //clear the console if something is already in it
            Console.Clear();

            //initialize the inputs
            Input.Initialize(RWindow);

            //start the game-logic
            GameLogic();
        }

        void GameLogic()
        {
            //set the framerate limit to the target FPS (usually 60)
            RWindow.SetFramerateLimit(Application.targetFPS);

            //execute Start() on EVERY script that has the subclass 'GraphicalBehaviour'
            Start();
            while (RWindow.IsOpen)
            {
                RWindow.DispatchEvents();
                Input.HandleInputs();
                Update();
                //Debug.Log($"FPS: {Application.FPS}/{Application.targetFPS} | deltaTime: {Time.deltaTime}");
                Gravity.Step();
                LateUpdate();
                Render();
                RWindow.Display();

                Time.MeasureFPS();
            }
        }

        void Render()
        {
            //clear the screen
            RWindow.Clear(Color.Black);
            //go trough every object, to draw it (if sprite exists)
            foreach (GameObject obj in GameData.objects)
            {
                if (obj.sprite != null)
                {
                    RWindow.Draw(obj.sprite);
                }
            }
        }

        void Start()
        {
            foreach(GraphicalBehaviour GB in GameData.behaviours)
            {
                GB.Start();
            }
        }
        void Update()
        {
            foreach (GraphicalBehaviour GB in GameData.behaviours)
            {
                GB.Update();
            }
        }
        void LateUpdate()
        {
            foreach (GraphicalBehaviour GB in GameData.behaviours)
            {
                GB.LateUpdate();
            }
        }
    }
}
