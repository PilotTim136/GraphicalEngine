using GraphicalEngine.Engine;

namespace GraphicalEngine
{
    public class Debug
    {
        public static void Log(object msg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(msg);
            Console.ResetColor();
            GameData.LOG.Add($"[LOG  ]: {msg}");
        }

        public static void LogWarning(object msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);
            Console.ResetColor();
            GameData.LOG.Add($"[WARN ]: {msg}");
        }

        public static void LogError(object msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
            GameData.LOG.Add($"[ERROR]: {msg}");
        }
    }
}
