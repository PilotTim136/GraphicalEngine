using GraphicalEngine.Engine;
using SFML.Graphics;
using SFML.Audio;

namespace GraphicalEngine
{
    public class GEngine
    {
        public static Font defaultFont { get; internal set; } = null!;

        /// <summary>
        /// Load an internal texture
        /// </summary>
        /// <param name="texture">Texture-enum</param>
        /// <returns>Texture</returns>
        public static Texture LoadTexture(ITexture texture)
        {
            string textureName = $"{texture.ToString()}.png";

            if (GameData.internalTextures.ContainsKey(textureName))
            {
                return GameData.internalTextures[textureName];
            }
            return null!;
        }

        /// <summary>
        /// Load an internal font
        /// </summary>
        /// <param name="font">font-enum</param>
        /// <returns>Font</returns>
        public static Font LoadFont(IFont font)
        {
            string fontName = font.ToString() + ".ttf";

            if (GameData.internalFonts.ContainsKey(fontName))
            {
                return GameData.internalFonts[fontName];
            }
            return null!;
        }

        /// <summary>
        /// Load an internal sound [ONLY ACCEPTS .]
        /// </summary>
        /// <param name="audio">audio-enum</param>
        /// <returns>Sound</returns>
        public static SoundBuffer LoadAudio(IAudio audio)
        {
            string audioname = audio.ToString() + ".wav";

            if (GameData.internalAudio.TryGetValue(audioname, out var buffer))
            {
                if (buffer == null)
                {
                    Debug.LogError($"SoundBuffer for '{audioname}' is NULL!");
                    return null!;
                }

                return buffer;
            }

            Debug.LogError($"'{audioname}' not found in dictionary.");
            return null!;
        }

        internal static void setDefaultFont()
        {
            defaultFont = GameData.internalFonts["roboto.ttf"];
        }
    }
    public enum ITexture
    {
        /// <summary>
        /// a 100x100 white square
        /// </summary>
        white
    }
    public enum IFont
    {
        /// <summary>
        /// sort of Sans-Serif
        /// </summary>
        roboto
    }
    public enum IAudio
    {
        /// <summary>
        /// coin collect sound
        /// </summary>
        ding
    }
}