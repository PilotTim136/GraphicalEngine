using SFML.Graphics;
using SFML.Window;
using GraphicalEngine.Engine;
using GraphicalEngine.UI;

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
            if(!GEngineCore._DEBUG)
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
            try
            {
                while (RWindow.IsOpen)
                {
                    RWindow.DispatchEvents();
                    Input.HandleInputs();
                    Update();
                    Gravity.Step();
                    LateUpdate();
                    Render();
                    RWindow.Display();
                    DestroyObjects();

                    Time.MeasureFPS();
                }
            }
            catch (Exception ex)
            {
                GameData.crash = ex.ToString();
                Debug.LogError("---------------------------");
                Debug.LogError($"CRASH!\n{ex}");
                Debug.LogError("---------------------------");
                RWindow.Close();
                new CrashW();
            }
        }

        void Render()
        {
            //clear the screen
            RWindow.Clear(Color.Black);

            Vector2 mpos = Input.GetMousePosition();

            foreach (GameObject obj in GameData.objects)
            {
                if (obj.sprite != null)
                {
                    RWindow.Draw(obj.sprite);
                }
            }
            foreach(TextObject to in GameData.textObjects)
            {
                to.Draw(RWindow);
            }
            foreach(Button btn in GameData.buttons)
            {
                btn.Draw(RWindow);
                btn.updateButton(mpos);
            }
        }

        void DestroyObjects()
        {
            if (GameData.toDestroyBTNs.Count > 0)
            {
                foreach (Button btn in GameData.toDestroyBTNs)
                {
                    GameData.buttons.Remove(btn);
                }
                GameData.toDestroyBTNs.Clear();
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
