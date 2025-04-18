using GraphicalEngine.Engine;
using SFML.Graphics;
using SFML.System;

namespace GraphicalEngine
{
    public class TextObject
    {
        public string text { get; internal set; } = "";
        public Vector2 position { get; internal set; }
        public uint size { get; internal set; } = 24;
        public GColor color { get; internal set; } = GColor.White;
        public GColor outlineColor { get; internal set; } = GColor.Black;
        public float OutlineThickness { get; internal set; } = 0;
        public Font font { get; internal set; } = null!;

        int idx = 0;

        Text t = new Text();

        #region constructors
        public TextObject()
        {
            addAndIndex();
        }
        public TextObject(string text)
        {
            this.text = text;
            addAndIndex();
        }
        public TextObject(Vector2 position)
        {
            this.position = position;
            addAndIndex();
        }
        public TextObject(uint size)
        {
            this.size = size;
            addAndIndex();
        }
        public TextObject(string text, Vector2 position)
        {
            this.text = text;
            this.position = position;
            addAndIndex();
        }
        public TextObject(uint size, Vector2 position)
        {
            this.size = size;
            this.position = position;
            addAndIndex();
        }
        public TextObject(string text, uint size, Vector2 position)
        {
            this.text = text;
            this.size = size;
            this.position = position;
            addAndIndex();
        }
        ~TextObject()
        {
            GameData.textObjects.Remove(this);
        }
        void addAndIndex()
        {
            font = GEngine.LoadFont(IFont.roboto);

            GameData.textObjects.Add(this);
            idx = GameData.textObjects.IndexOf(this);
            updateText();
        }
        #endregion
    
        public TextObject SetText(string text)
        {
            this.text = text;
            updateText();
            return this;
        }

        /// <summary>
        /// Sets the position of the text
        /// </summary>
        public TextObject SetPosition(Vector2 position)
        {
            this.position = position;
            updateText();
            return this;
        }

        public FloatRect GetLocalBounds()
        {
            return t.GetLocalBounds();
        }

        /// <summary>
        /// Sets the size of the text
        /// </summary>
        public TextObject SetSize(uint size)
        {
            this.size = size;
            updateText();
            return this;
        }

        /// <summary>
        /// Sets the font of the text
        /// </summary>
        public TextObject SetFont(Font font)
        {
            this.font = font;
            updateText();
            return this;
        }

        /// <summary>
        /// Sets the font of the text (/Game/Fonts/...)
        /// </summary>
        /// <param name="fontPath">Path to the font (./Game/Fonts/...)</param>
        public TextObject SetFont(string fontPath)
        {
            font = new Font("./Game/Fonts/" + fontPath);
            updateText();
            return this;
        }

        /// <summary>
        /// Sets the outline color of the text
        /// </summary>
        public TextObject SetOutlineColor(GColor color)
        {
            outlineColor = color;
            updateText();
            return this;
        }

        /// <summary>
        /// Sets the outline thickness of the outline
        /// </summary>
        public TextObject SetOutlineThickness(float thickness)
        {
            OutlineThickness = thickness;
            updateText();
            return this;
        }

        /// <summary>
        /// Sets the color of the text
        /// </summary>
        /// <param name="color">Color</param>
        public TextObject SetColor(GColor color)
        {
            this.color = color;
            updateText();
            return this;
        }

        /// <summary>
        /// Destroys the text.
        /// NOTICE: not removed from memory until there is no more reference!
        /// </summary>
        public void Destroy()
        {
            GameData.textObjects.Remove(this);
        }

        public float GetWidth()
        {
            return t.GetLocalBounds().Width;
        }

        public float GetHeight()
        {
            return t.GetLocalBounds().Height;
        }

        public void SetOriginCenter()
        {
            t.Origin = new Vector2f(t.GetLocalBounds().Width / 2, t.GetLocalBounds().Height / 2);
        }

        internal void Draw(RenderTarget target)
        {
            updateText();
            target.Draw(t);
        }

        void updateText()
        {
            if(t != null)
            {
                t.Position = position.ToSFML();
                t.FillColor = color.ToSFML();
                t.CharacterSize = size;
                t.Font = font;
                t.DisplayedString = text;
                t.OutlineColor = outlineColor.ToSFML();
                t.OutlineThickness = OutlineThickness;
                //t.Origin = new Vector2f(t.GetLocalBounds().Width / 2, t.GetLocalBounds().Height / 2);
            }
        }
    }
}
