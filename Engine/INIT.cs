using GraphicalEngine.window;
using SFML.Audio;
using SFML.Graphics;
using System.Reflection;

namespace GraphicalEngine.Engine
{
    public class GEngineCore
    {
        public struct Settings
        {
            public Vector2 screenSize;
            public float gravitySpeed = 20;
            public float defaultDeltaSpeed = 1;

            public Settings(){}
            public Settings(Vector2 sS, float gS, float dDS)
            {
                screenSize = sS;
                gravitySpeed = gS;
                defaultDeltaSpeed = dDS;
            }
        }

        static Window window = null!;
        static string gameTitle = "Game";
        internal static bool _DEBUG = false;

        public static void Init(Settings settings)
        {
            Vector2 screenSize = settings.screenSize;

            Application.gravityForce = settings.gravitySpeed;
            Time.timeScale = settings.defaultDeltaSpeed;

            //do not change below
            InitConsoleTitle();
            AddBehaviours();
            GEngine.setDefaultFont();

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
            var assembly = Assembly.GetEntryAssembly()!;

            var types = assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(GraphicalBehaviour)) && !t.IsAbstract)
                .ToList();

            //loops trough every class that has the GraphicalBehaviour as subclass
            foreach (var type in types)
            {
                //and adds it to the list of behaviours
                GameData.behaviours.Add((GraphicalBehaviour)Activator.CreateInstance(type)!);
            }

            assembly = Assembly.GetExecutingAssembly();
            string[] resourceNames = assembly.GetManifestResourceNames();
            foreach (string resourceName in resourceNames)
            {
                using (var stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        if (resourceName.Contains(".Textures."))
                        {
                            Texture t = new Texture(stream);
                            string nPath = resourceName.Replace($"{assembly.GetName().Name!}.Game.Textures.", "");
                            GameData.internalTextures.Add(nPath, t);
                        }
                        else if (resourceName.Contains(".Fonts."))
                        {
                            byte[] fontData;
                            using (MemoryStream ms = new MemoryStream())
                            {
                                stream.CopyTo(ms);
                                fontData = ms.ToArray();
                            }

                            Font f = new Font(new MemoryStream(fontData));
                            string nPath = resourceName.Replace($"{assembly.GetName().Name!}.Game.Fonts.", "");
                            GameData.internalFonts.Add(nPath, f);
                        }
                        else if (resourceName.Contains(".Sounds."))
                        {
                            byte[] soundData;

                            using (MemoryStream ms = new MemoryStream())
                            {
                                stream.CopyTo(ms);
                                soundData = ms.ToArray();
                            }

                            SoundBuffer buffer = new SoundBuffer(soundData);
                            if (buffer == null)
                            {
                                return;
                            }
                            string nPath = resourceName.Replace($"{assembly.GetName().Name!}.Game.Sounds.", "");
                            GameData.internalAudio.Add(nPath, buffer);
                        }
                    }
                }
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
