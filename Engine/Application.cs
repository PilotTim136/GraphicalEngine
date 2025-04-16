using SFML.System;

namespace GraphicalEngine
{
    public class Application
    {
        public static uint targetFPS = 60;  //target FPS
        //public static float FPSf = 0f;    //float FPS
        public static int FPS = 0;          //int-FPS
    }

    public class Time
    {
        private static Clock fpsClock = new Clock();
        private static int frameCounter = 0;

        internal static void MeasureFPS()
        {
            frameCounter++;

            float interval = 0.25f;

            if (fpsClock.ElapsedTime.AsSeconds() >= interval)
            {
                float fpsEstimate = frameCounter / fpsClock.ElapsedTime.AsSeconds();

                //Application.FPSf = fpsEstimate;
                Application.FPS = (int)Math.Round(fpsEstimate);

                frameCounter = 0;
                fpsClock.Restart();
            }
        }
    }
}
