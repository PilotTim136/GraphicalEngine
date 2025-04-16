namespace GraphicalEngine
{
    public abstract class GraphicalBehaviour
    {
        /// <summary>
        /// Called at the start of the game
        /// </summary>
        public virtual void Start() { }

        /// <summary>
        /// Called at every frame
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Called after every physics update
        /// </summary>
        public virtual void LateUpdate() { }
    }
}
