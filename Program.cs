using GraphicalEngine;
using GraphicalEngine.Engine;

class Program
{
    static void Main()
    {
        GEngine.Settings s = new GEngine.Settings();
        s.screenSize = new Vector2(600, 400);

        GEngine.Init(s);
    }
}
