using GraphicalEngine;
using GraphicalEngine.Engine;

class Program
{
    static void Main()
    {
        GEngineCore.Settings s = new GEngineCore.Settings();
        s.screenSize = new Vector2(600, 400);
        s.gravitySpeed = 20;
        s.defaultDeltaSpeed = 1;

        GEngineCore.Init(s);
    }
}
