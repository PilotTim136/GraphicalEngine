using SFML.System;

namespace GraphicalEngine
{
    public class Application
    {
        public static uint targetFPS = 60;      //target FPS
        public static float FPSf = 0;           //float FPS
        public static int FPS = 0;              //int-FPS
        public static float gravityForce = 0;   //force of the gravity
    }

    public class Time
    {
        public static float deltaTime = 0;
        public static float timeScale = 1f;

        private static SFML.System.Clock frameClock = new();
        private static Clock fpsClock = new Clock();
        private static int frameCounter = 0;


        internal static void MeasureFPS()
        {
            float rawDelta = frameClock.Restart().AsSeconds();
            deltaTime = rawDelta * timeScale;

            frameCounter++;

            if (fpsClock.ElapsedTime.AsSeconds() >= 0.25f)
            {
                Application.FPSf = frameCounter / fpsClock.ElapsedTime.AsSeconds();
                Application.FPS = (int)Math.Round(Application.FPSf);

                fpsClock.Restart();
                frameCounter = 0;
            }
        }
    }
}
