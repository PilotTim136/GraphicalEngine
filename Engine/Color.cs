using SFML.Graphics;

namespace GraphicalEngine
{
    public struct GColor
    {
        public static readonly GColor Black = new GColor(0, 0, 0);
        public static readonly GColor White = new GColor(255, 255, 255);
        public static readonly GColor Red = new GColor(255, 0, 0);
        public static readonly GColor Green = new GColor(0, 255, 0);
        public static readonly GColor Blue = new GColor(0, 0, 255);
        public static readonly GColor Yellow = new GColor(255, 255, 0);
        public static readonly GColor Cyan = new GColor(0, 255, 255);
        public static readonly GColor Magenta = new GColor(255, 0, 255);
        public static readonly GColor Transparent = new GColor(0, 0, 0, 0);
                               
        public static readonly GColor BlackAlpha50 = new GColor(0, 0, 0, 128);
        public static readonly GColor WhiteAlpha50 = new GColor(255, 255, 255, 128);
        public static readonly GColor Gray = new GColor(128, 128, 128);
        public static readonly GColor LightGray = new GColor(211, 211, 211);
        public static readonly GColor DarkGray = new GColor(169, 169, 169);

        public byte R { get; }
        public byte G { get; }
        public byte B { get; }
        public byte A { get; }

        public GColor(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            A = 255;
        }

        public GColor(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public override string ToString()
        {
            return $"{{R:{R}, G:{G}, B:{B}, A:{A}}}";
        }

        [Obsolete]
        public Color ToSFMLColor()
        {
            return ToSFML();
        }
        public Color ToSFML()
        {
            return new Color(R, G, B, A);
        }

        public static GColor FromSFMLColor(SFML.Graphics.Color sfmlColor)
        {
            return new GColor(sfmlColor.R, sfmlColor.G, sfmlColor.B, sfmlColor.A);
        }
    }
}
