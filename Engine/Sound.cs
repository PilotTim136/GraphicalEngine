using SFML.Audio;

namespace GraphicalEngine
{
    public class GSound
    {
        SoundBuffer sb = null!;
        Sound so = null!;
        bool created = false;

        public uint volume { get; set; } = 100;
        public bool looped { get; set; } = false;

        public void LoadSound(SoundBuffer sb)
        {
            if (sb == null)
            {
                Debug.LogWarning("[Sound] Error! soundBuffer is null.");
                return;
            }

            try
            {
                this.sb = sb;
                so = new Sound(sb);
                created = true;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        /// <summary>
        /// Sets the font of the text (/Game/Sounds/...) [notice: unfortonatly, limitations: .wav, .ogg]
        /// </summary>
        /// <param name="path">Path to the file</param>
        public void LoadSound(string path)
        {
            sb = new SoundBuffer(path);
            so = new Sound(sb);
            created = true;
        }

        public GSound Play()
        {
            if (!created || so == null || sb == null)
            {
                Debug.LogError("This object is NOT created OR the sound / soundBuffer is null.");
                return this;
            }

            so.Play();

            return this;
        }
    }
}
