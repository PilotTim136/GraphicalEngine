namespace GraphicalEngine.Engine
{
    internal class Gravity
    {
        internal static void Step()
        {
            foreach(GameObject obj in GameData.objects)
            {
                if (obj.affectedByGravity)
                {
                    doStep(obj);
                }
            }
        }

        static void doStep(GameObject obj)
        {
            List<GameObject> collider = obj.CheckCollisions();
            bool colliding = checkColliding(collider);

            float gravityForce = Application.gravityForce * Time.deltaTime;
            float correctionStep = 0.05f;

            if (!colliding)
            {
                //move down (by gravity)
                obj.MoveBy(new Vector2(0, obj.velocityY * Time.deltaTime));

                collider = obj.CheckCollisions();

                //check if colliding
                colliding = checkColliding(collider);

                //if colliding, correct it
                if (colliding)
                {
                    obj.MoveBy(new Vector2(0, -obj.velocityY * Time.deltaTime));
                    correct(obj, correctionStep);
                    obj.velocityY = 0;
                }
                else
                {
                    obj.velocityY += gravityForce;
                }
                return;
            }
            correct(obj, correctionStep);
            obj.velocityY = 0;
        }

        static void correct(GameObject obj, float correctionStep)
        {
            while (!checkColliding(obj.CheckCollisions()))
            {
                obj.MoveBy(new Vector2(0, correctionStep));
            }
            obj.MoveBy(new Vector2(0, -correctionStep));
        }

        static bool checkColliding(List<GameObject> obj)
        {
            foreach (GameObject obj2 in obj)
            {
                if (obj2.interactWithGravity || (obj2.affectedByGravity && obj2.interactWithOtherGravity))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
