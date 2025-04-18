using SFML.Graphics;
using SFML.Window;

namespace GraphicalEngine.Engine
{
    internal class CrashW
    {
        RenderWindow window;
        Vector2 screenVec = new Vector2(400, 100);
        bool logFileCreated = false;
        int timer = 0;
        int waitTime = 5;
        readonly string title = "The game crashed!";
        Vector2 screenCenter;

        public CrashW()
        {
            screenCenter = new Vector2(screenVec.x / 2, screenVec.y / 2);
            window = new RenderWindow(new VideoMode((uint)screenVec.x, (uint)screenVec.y),
                title,
                Styles.Titlebar | Styles.Close);
            Task.Run(createFile);
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(new Color(64, 64, 64));
                R();
                window.Display();
            }
        }

        void R()
        {
            //heading text
            TextObject heading = new TextObject();
            heading.SetText("CRASHED");
            heading.SetOutlineColor(GColor.Black);
            heading.SetOutlineThickness(2f);
            heading.SetSize(50);
            heading.SetOriginCenter();
            heading.SetPosition(new Vector2(screenCenter.x, 20));

            TextObject information = new TextObject();
            information.SetText(logFileCreated ? "Log file was created." : "Creating log file...");
            information.SetPosition(new Vector2(screenCenter.x, 70));
            information.SetOriginCenter();


            //draw
            heading.Draw(window);
            information.Draw(window);
        }

        async Task createFile()
        {
            Directory.CreateDirectory("./Game/");
            Directory.CreateDirectory("./Game/Logs/");
            string cLog = "";
            foreach(string line in GameData.LOG)
            {
                cLog += line + "\n";
            }
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            File.WriteAllText($"./Game/logs/log-{timestamp}.txt", cLog);
            logFileCreated = true;
            while(timer != waitTime)
            {
                window.SetTitle($"{title} ({waitTime - timer})");
                await Task.Delay(1000);
                timer++;
            }
            window.Close();
        }
    }
}
