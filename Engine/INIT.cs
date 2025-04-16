using GraphicalEngine.window;
using System.Reflection;

namespace GraphicalEngine.Engine
{
    internal class GEngine
    {
        public struct Settings
        {
            public Vector2 screenSize;
        }

        static Window window = null!;
        static string gameTitle = "Game";

        public static void Init(Settings settings)
        {
            Vector2 screenSize = settings.screenSize;

            //do not change below
            InitConsoleTitle();
            AddBehaviours();

            //set the screenSize values
            ScreenData.SetScreenSize(screenSize);
            uint x = (uint)MathF.Round(screenSize.x);
            uint y = (uint)MathF.Round(screenSize.y);

            //initialize the window
            ScreenData._LOCK();
            window = new Window(x, y, gameTitle);
        }

        static void AddBehaviours()
        {
            //gets the GraphicalBehaviour of every class
            //this is because it has to be able to execute everything in those classes
            //such as "Start()" and "Update()"
            var types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(GraphicalBehaviour)) && !t.IsAbstract)
                .ToList();

            //loops trough every class that has the GraphicalBehaviour as subclass
            foreach (var type in types)
            {
                //and adds it to the list of behaviours
                GameData.behaviours.Add((GraphicalBehaviour)Activator.CreateInstance(type)!);
            }
        }

        static void InitConsoleTitle()
        {
            //this is just so the console-title is properly updated
            Task.Run(() =>
            {
                Task.Delay(1000).Wait();
                Console.Title = "[LOG] " + gameTitle;
            });
        }
    }
}
