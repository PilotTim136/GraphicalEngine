using SFML.System;

namespace GraphicalEngine
{
#pragma warning disable CS0659
#pragma warning disable CS0661
    public struct Vector2
#pragma warning restore CS0661
#pragma warning restore CS0659
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

        public static bool operator ==(Vector2 a, Vector2 b)
        {
            return (a.x == b.x) && (a.y == b.y);
        }

        public static bool operator !=(Vector2 a, Vector2 b)
        {
            return (a.x != b.x) || (a.y != b.y);
        }

        public Vector2f ToSFML()
        {
            return new Vector2f(x, y);
        }

        public Vector2 OnlyPositive()
        {
            Vector2 v2 = this;
            if (v2.x < 0) v2.x = 0;
            if (v2.y < 0) v2.y = 0;
            return new Vector2(v2.x, v2.y);
        }

        public override string ToString()
        {
            return $"{{{x}, {y}}}";
        }

#pragma warning disable CS8765 
        public override bool Equals(object obj)
#pragma warning restore CS8765
        {
            throw new NotImplementedException();
        }
    }

    internal class V2Convert
    {
        public static Vector2 FromSFML(Vector2f v)
        {
            return new Vector2(v.X, v.Y);
        }
        public static Vector2 FromSFML(Vector2i v)
        {
            return new Vector2(v.X, v.Y);
        }
        public static Vector2 OnlyPositive(Vector2 v)
        {
            Vector2 v2 = v;
            if (v2.x < 0) v2.x = 0;
            if (v2.y < 0) v2.y = 0;
            return new Vector2(v2.x, v2.y);
        }
    }
}
