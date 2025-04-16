namespace GraphicalEngine
{
    public class Debug
    {
        public static void Log(object msg)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        public static void LogWarning(object msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        public static void LogError(object msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
