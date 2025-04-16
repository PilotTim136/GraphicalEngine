using SFML.Graphics;
using SFML.System;

namespace GraphicalEngine.Engine
{
    internal class OBB
    {
        public static bool checkOBBCollision(GameObject a, GameObject b)
        {
            if (a == b || a.sprite == null || b.sprite == null) return false;

            var cornersA = getTransformedCorners(a);
            var cornersB = getTransformedCorners(b);

            return !hasSeparatingAxis(cornersA, cornersB) && !hasSeparatingAxis(cornersB, cornersA);
        }

        static Vector2f[] getTransformedCorners(GameObject obj)
        {
            FloatRect bounds = obj.sprite!.GetLocalBounds();
            Transform transform = obj.sprite.Transform;

            Vector2f topLeft = transform.TransformPoint(new Vector2f(bounds.Left, bounds.Top));
            Vector2f topRight = transform.TransformPoint(new Vector2f(bounds.Left + bounds.Width, bounds.Top));
            Vector2f bottomRight = transform.TransformPoint(new Vector2f(bounds.Left + bounds.Width, bounds.Top + bounds.Height));
            Vector2f bottomLeft = transform.TransformPoint(new Vector2f(bounds.Left, bounds.Top + bounds.Height));

            return new[] { topLeft, topRight, bottomRight, bottomLeft };
        }

        static bool hasSeparatingAxis(Vector2f[] cornersA, Vector2f[] cornersB)
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2f p1 = cornersA[i];
                Vector2f p2 = cornersA[(i + 1) % 4];

                Vector2f edge = new Vector2f(p2.X - p1.X, p2.Y - p1.Y);
                Vector2f axis = new Vector2f(-edge.Y, edge.X);

                float minA = float.MaxValue, maxA = float.MinValue;
                foreach (var corner in cornersA)
                {
                    float proj = corner.X * axis.X + corner.Y * axis.Y;
                    minA = MathF.Min(minA, proj);
                    maxA = MathF.Max(maxA, proj);
                }

                float minB = float.MaxValue, maxB = float.MinValue;
                foreach (var corner in cornersB)
                {
                    float proj = corner.X * axis.X + corner.Y * axis.Y;
                    minB = MathF.Min(minB, proj);
                    maxB = MathF.Max(maxB, proj);
                }

                if (maxA < minB || maxB < minA)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
