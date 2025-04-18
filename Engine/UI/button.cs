using GraphicalEngine.Engine;
using SFML.Graphics;
using SFML.System;

namespace GraphicalEngine.UI
{
    public class Button
    {
        public Vector2 position { get; set; }
        public Vector2 size { get; set; } = new Vector2(100, 40);
        public string text { get; set; } = "";
        public GColor buttonColor { get; set; } = GColor.White;
        public GColor textColor { get; set; } = GColor.Black;
        public Font font { get; set; } = null!;

        bool clickedBefore = false;

        Action? onClick = null;
        Action? onHold = null;

        RectangleShape shape = new RectangleShape();
        Text sfText = new Text();

        #region constructors
        public Button()
        {
            doGeneralStuff();
        }
        public Button(string text)
        {
            this.text = text;
            doGeneralStuff();
        }
        public Button(Vector2 position)
        {
            this.position = position;
            doGeneralStuff();
        }
        public Button(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;
            doGeneralStuff();
        }
        public Button(string text, Vector2 position, Vector2 size)
        {
            this.text = text;
            this.position = position;
            this.size = size;
            doGeneralStuff();
        }

        ~Button()
        {
            Destroy();
        }
        #endregion

        internal void doGeneralStuff()
        {
            GameData.buttons.Add(this);
            font = GEngine.defaultFont;
            updateShape();
        }
        internal void updateShape()
        {
            shape.Position  = position.ToSFML();
            shape.Size      = size.ToSFML();
            shape.FillColor = buttonColor.ToSFML();
            shape.Origin    = new Vector2f(shape.Size.X / 2, shape.Size.Y / 2);

            if (font == null) Debug.Log("font is (currently) null | " + GEngine.defaultFont);
            if (font == null) font = GEngine.defaultFont;
            if (font != null)
            {
                sfText.Font             = font;
                sfText.CharacterSize    = 24;
                sfText.FillColor        = textColor.ToSFML();
                sfText.DisplayedString  = text;

                FloatRect tb = sfText.GetLocalBounds();
                sfText.Origin = new Vector2f(
                    tb.Left + tb.Width / 2f,
                    tb.Top + tb.Height / 2f
                );
                sfText.Position = shape.Position;
            }
        }

        /// <summary>
        /// action when the button is clicked
        /// </summary>
        /// <param name="onClick">code when clicked</param>
        public void OnClick(Action onClick)
        {
            this.onClick = onClick;
        }

        /// <summary>
        /// action when the button is held
        /// </summary>
        /// <param name="onHold">code when held</param>
        public void OnHold(Action onHold)
        {
            this.onHold = onHold;
        }

        public void Destroy()
        {
            if (!GameData.toDestroyBTNs.Contains(this))
                GameData.toDestroyBTNs.Add(this);
        }

        internal void forceDestroy()
        {
            GameData.toDestroyBTNs.Remove(this);
            GameData.buttons.Remove(this);
        }

        internal RectangleShape GetShape()
        {
            updateShape();
            return shape;
        }

        internal void updateButton(Vector2 mousePosition)
        {
            bool imbd = Input.IsMouseButtonDown(MouseButton.Left);                  //is mouse button down
            bool iia = shape.GetGlobalBounds().Contains(mousePosition.ToSFML());    //is in area

            if (imbd && iia)
            {
                if (!clickedBefore)
                {
                    onClick?.Invoke();
                    return;
                }
                clickedBefore = true;
                onHold?.Invoke();
            }
            else if (!imbd)
            {
                clickedBefore = false;
            }
        }
        internal void Draw(RenderWindow window)
        {
            updateShape();
            window.Draw(shape);
            window.Draw(sfText);
        }
    }
}
