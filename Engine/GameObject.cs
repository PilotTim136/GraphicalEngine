using GraphicalEngine.Engine;
using SFML.Graphics;
using SFML.System;

namespace GraphicalEngine
{
    public class GameObject
    {
        int indexInData = 0;
        public string name { get; private set; } = "";
        public Vector2 position { get; private set; }
        public Vector2 scale { get; private set; } = new Vector2(1, 1);

        public Sprite? sprite { get; private set; }

        #region constructors
        public GameObject()
        {
            GameData.objects.Add(this);
            indexInData = GameData.objects.IndexOf(this);
        }
        public GameObject(string name)
        {
            this.name = name;

            GameData.objects.Add(this);
            indexInData = GameData.objects.IndexOf(this);
        }
        public GameObject(Vector2 position)
        {
            this.position = position;

            GameData.objects.Add(this);
            indexInData = GameData.objects.IndexOf(this);
        }
        public GameObject(string name, Vector2 position)
        {
            this.name = name;
            this.position = position;

            GameData.objects.Add(this);
            indexInData = GameData.objects.IndexOf(this);
        }

        ~GameObject()
        {
            GameData.objects.Remove(this);
        }
        #endregion

        /// <summary>
        /// Destroys this GameObject.
        /// Notice: does NOT get removed from memory until no reference exists.
        /// </summary>
        public void Destroy()
        {
            GameData.objects.Remove(this);
        }

        /// <summary>
        /// Set the name of the GameObject
        /// </summary>
        /// <param name="name">The new name the gameObject should have</param>
        public GameObject SetName(string name)
        {
            this.name = name;
            return this;
        }

        /// <summary>
        /// Set the position of the GameObject
        /// </summary>
        /// <param name="position">The new position the gameObject should be placed at</param>
        public GameObject SetPosition(Vector2 position)
        {
            this.position = position;
            updatePosition();
            return this;
        }

        /// <summary>
        /// Set the scale of the GameObject
        /// </summary>
        /// <param name="scale">The new scale the gameObject should be scaled</param>
        public GameObject SetScale(Vector2 scale)
        {
            this.scale = scale;
            updatePosition();
            return this;
        }
        /// <summary>
        /// Set the scale of the GameObject
        /// </summary>
        /// <param name="scale">The new scale the gameObject should be scaled</param>
        public GameObject SetScale(float scale)
        {
            this.scale = new Vector2(scale, scale);
            updatePosition();
            return this;
        }

        /// <summary>
        /// Moves by the certain values given
        /// </summary>
        /// <param name="direction">The direction it should move towards</param>
        public GameObject MoveBy(Vector2 direction)
        {
            position += direction;
            updatePosition();
            return this;
        }

        #region sprites
        /// <summary>
        /// Set the sprite (texture) to the given path. (notice: looking default for: "./Game/Textures/{path}")
        /// </summary>
        /// <param name="path">path to the sprite</param>
        public GameObject SetSprite(string path)
        {
            try
            {
                Texture texture = new Texture($"./Game/Textures/{path}");
                sprite = new Sprite(texture);
            }
            catch (Exception e)
            {
                Console.WriteLine($"[ENGINE: GameObject]: Error loading texture: " + e);
            }
            updatePosition();
            return this;
        }
        /// <summary>
        /// Set the sprite (texture) to the given texture.
        /// </summary>
        /// <param name="texture">texture of the sprite</param>
        public GameObject SetSprite(Texture texture)
        {
            sprite = new Sprite(texture);
            updatePosition();
            return this;
        }
        /// <summary>
        /// Set the sprite to the given sprite.
        /// </summary>
        /// <param name="sprite">sprite</param>
        public GameObject SetSprite(Sprite sprite)
        {
            this.sprite = sprite;
            updatePosition();
            return this;
        }
        #endregion

        void updatePosition()
        {
            if(sprite != null)
            {
                sprite.Position = V2Convert.ToSFML(position);
                sprite.Scale = V2Convert.ToSFML(scale);
            }
        }
    }
}
