﻿namespace GraphicalEngine
{
    public class ScreenData
    {
        public static Vector2 ScreenSize { get; private set; }
        public static Vector2 ScreenCenter { get; private set; }

        static bool _LOCKED = false;

        internal static void SetScreenSize(Vector2 screenSize)
        {
            if (_LOCKED) return;
            ScreenSize = screenSize;
            ScreenCenter = new Vector2(screenSize.x / 2, screenSize.y / 2);
        }

        public static void _LOCK()
        {
            _LOCKED = true;
        }

        public static Vector2 GetTopLeft(GameObject obj)
        {
            if (obj.sprite == null) return Vector2.Zero;

            var texRect = obj.sprite.TextureRect;
            var scale = obj.sprite.Scale;

            float width = texRect.Width * scale.X;
            float height = texRect.Height * scale.Y;

            return new Vector2(width / 2f, height / 2f);
        }

        //this is with the old system
        /*public static Vector2 GetScreenCenter(GameObject obj)
        {
            if (obj.sprite == null) return Vector2.Zero;

            float halfWidth = (obj.sprite.TextureRect.Width * obj.sprite.Scale.X) / 2f;
            float halfHeight = (obj.sprite.TextureRect.Height * obj.sprite.Scale.Y) / 2f;

            return new Vector2(
                ScreenCenter.x - halfWidth,
                ScreenCenter.y - halfHeight
            );
        }*/
    }
}
