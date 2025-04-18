using SFML.Graphics;
using SFML.Audio;
using GraphicalEngine.UI;

namespace GraphicalEngine.Engine
{
    internal class GameData
    {
        internal static List<GameObject> objects = new List<GameObject>();
        internal static List<TextObject> textObjects = new List<TextObject>();

        //UI
        internal static List<Button> buttons = new List<Button>();

        //behaviours
        internal static List<GraphicalBehaviour> behaviours = new List<GraphicalBehaviour>();

        //internal resources
        internal static Dictionary<string, Texture> internalTextures = new Dictionary<string, Texture>();
        internal static Dictionary<string, Font> internalFonts = new Dictionary<string, Font>();
        internal static Dictionary<string, SoundBuffer> internalAudio = new Dictionary<string, SoundBuffer>();

        //log
        internal static List<string> LOG = new List<string>();
        internal static string crash = "";

        //to destroy
        internal static List<Button> toDestroyBTNs = new List<Button>();
    }
}
