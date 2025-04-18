using GraphicalEngine.Engine;
using SFML.Graphics;
using SFML.System;

namespace GraphicalEngine
{
    public class GameObject
    {
        int indexInData = 0;
        public string name { get; internal set; } = "";

        public Vector2 position { get; internal set; }
        public Vector2 scale { get; internal set; } = new Vector2(1, 1);
        public float rotation { get; internal set; } = 0;

        public Sprite? sprite { get; internal set; }
        Texture? texture = null;
        public GColor color { get; internal set; } = GColor.White;

        /// <summary>
        /// if the gameObject should interact with gravity
        /// </summary>
        public bool interactWithGravity { get; set; } = false;

        /// <summary>
        /// (works if affectedByGravity is true): interacts with other gravity-objects
        /// </summary>
        public bool interactWithOtherGravity { get; set; } = true;

        /// <summary>
        /// whether or not it should be affected by gravity
        /// </summary>
        public bool affectedByGravity { get; set; } = false;

        /// <summary>
        /// the Y-Velocity the gameObject falls (affectedByGravity has to be enabled)
        /// </summary>
        public float velocityY { get; set; }

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

        public void SetRotation(float rotation)
        {
            if (sprite == null) return;
            this.rotation = rotation;
            updatePosition();
        }

        public void ChangeRotation(float rotation)
        {
            if (sprite == null) return;
            this.rotation += rotation;
            updatePosition();
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
                this.texture = texture;
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
            this.texture = texture;
            sprite = new Sprite(texture);
            updatePosition();
            return this;
        }

        #endregion

        public void SetColor(GColor color)
        {
            this.color = color;
            updatePosition();
        }

        #region collisions
        /// <summary>
        /// Return true, if a collision between 2 gameObjects exist
        /// </summary>
        /// <param name="obj">Gameobejct it should check for collision</param>
        /// <returns>if a collision exists</returns>
        public bool CheckCollision(GameObject obj)
        {
            return checkCollision(obj);
        }

        /// <summary>
        /// Searches the current GameObject-list trough names.
        /// </summary>
        /// <param name="objName">name of the object it should check</param>
        /// <returns>if a collision exists</returns>
        public bool CheckCollision(string objName)
        {
            GameObject? obj = null;
            foreach(GameObject objj in GameData.objects)
            {
                if (objj.name == objName)
                {
                    obj = objj;
                    break;
                }
            }
            if (obj == null) return false;
            return checkCollision(obj);
        }

        /// <summary>
        /// checks all collisions
        /// </summary>
        /// <returns>all colliding gameobjects</returns>
        public List<GameObject> CheckCollisions()
        {
            List<GameObject> collidingObj = new List<GameObject>();
            foreach(GameObject obj in GameData.objects)
            {
                if (checkCollision(obj))
                {
                    collidingObj.Add(obj);
                }
            }
            return collidingObj;
        }

        /// <summary>
        /// checks the collision between all currently available GameObjects
        /// </summary>
        /// <returns>if anything collides</returns>
        public bool CheckCollision()
        {
            foreach (GameObject obj in GameData.objects)
            {
                if (checkCollision(obj))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        bool checkCollision(GameObject obj)
        {
            if (obj == this || obj.sprite == null || this.sprite == null)
                return false;

            return OBB.checkOBBCollision(this, obj);
        }

        void updatePosition()
        {
            if(sprite != null && texture != null)
            {
                sprite.Origin = new Vector2f(texture.Size.X / 2f, texture.Size.Y / 2f);
                sprite.Position = position.ToSFML();
                sprite.Scale = scale.ToSFML();
                sprite.Rotation = rotation;
                sprite.Color = color.ToSFML();
            }
        }

        public override string ToString()
        {
            return $@"{{name:{name}|position:{position}|scale:{scale}|rotation:{{{rotation}}}deg}}";
        }
    }
}
