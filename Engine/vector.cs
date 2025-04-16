using SFML.System;

namespace GraphicalEngine
{
    public struct Vector2
    {
        public float x { get; set; }
        public float y { get; set; }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 Zero => new Vector2(0, 0);

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x / b.x, a.y / b.y);
        }

        public override string ToString()
        {
            return $"{{{x}, {y}}}";
        }
    }

    internal class V2Convert
    {
        public static Vector2f ToSFML(Vector2 v)
        {
            return new Vector2f(v.x, v.y);
        }

        public static Vector2 FromSFML(Vector2f v)
        {
            return new Vector2(v.X, v.Y);
        }
    }
}
