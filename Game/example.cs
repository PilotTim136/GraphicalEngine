using GraphicalEngine;

class Example : GraphicalBehaviour
{
    GameObject obj = null!;
    List<GameObject> fallings = new List<GameObject>();
    float speed = 1;    //player speed
    int done = 0;
    int score = 0;      //player score
    bool _forceStop = false;

    public override void Start()
    {
        //create the object-Gameobject
        obj = new GameObject();

        //loop for a certain amount of times
        int loop = 50;
        for (int i = 0; i < loop; i++)
        {
            GameObject e = new GameObject();

            //set the sprite, scale and position
            e.SetSprite("white.png")
                .SetScale(new Vector2(0.1f, 0.2f))
                .SetPosition(SetRandomPositionV(e));


            e.affectedByGravity = true; //make them affected by gravity
            e.interactWithOtherGravity = false;
            e.SetColor(GColor.Red);

            //add it to the list of falling-gameObjects
            fallings.Add(e);
        }

        GameObject floor = new GameObject();
        floor.SetSprite("white.png")
            .SetScale(new Vector2(0.5f, 0.1f))
            .SetPosition(new Vector2(ScreenData.ScreenCenter.x, ScreenData.ScreenSize.y - 5));

        floor.interactWithGravity = true; //so the floor will be affected by gravity


        //set the properties of obj (sprite, scale, position)
        obj.SetSprite("white.png")
            .SetScale(0.2f)
            .SetPosition(ScreenData.ScreenCenter);
    }

    public override void Update()
    {
        //if forcestop is triggered, return
        if (_forceStop) return;

        CheckKeys();        //check input-keys
        //Fall();           //make the falling-objects fall
        CheckYPosition();
        CheckCollision();   //check the collisions

        //when every falling (done) has been at the bottom, add score and reset done
        if (done >= fallings.Count)
        {
            score++;
            done = 0;
            Debug.Log("Score: " + score);
        }
    }

    void CheckYPosition()
    {
        int randomY = new Random().Next(0, 50); //make a random Y position it has to fall below, so y wont always be the same
        foreach (GameObject falling in fallings)
        {
            if (falling.position.y > ScreenData.ScreenSize.y + randomY)
            {
                SetRandomPosition(falling); //sets it to a random X position
                done++;                     //increments done by 1.
            }
        }
        
    }

    void CheckCollision()
    {
        //there are multiple CheckCollision()
        //CheckCollision(string) - checks the collision for a game object with a specific name
        //CheckCollision(GameObject) - checks the collision for a specific GameObject
        //CheckCollisions() - Checks collision for ALL gameobjects (returns a list of gameobjects that are colliding)

        //the one used here, checks for all gameObjects if they are colliding.
        bool collided = obj.CheckCollision();
        if (collided)
        {
            Debug.Log("Force-stop!");
            Time.timeScale = 0;
            _forceStop = true;
        }
    }

    //this was before simple-gravity was created.
    void Fall()
    {
        //loop trough EVERY fall object in fallings
        foreach (GameObject falling in fallings)
        {
            falling.MoveBy(new Vector2(0, 2f));     //move the falling by 2f (downwards)
        }
    }

    Vector2 SetRandomPositionV(GameObject falling)
    {
        Random r = new Random();
        int rX = 0;
        int rY = (int)MathF.Round(ScreenData.ScreenSize.x);
        return new Vector2(r.Next(rX, rY), -100);
    }

    void SetRandomPosition(GameObject falling)
    {
        falling.SetPosition(SetRandomPositionV(falling));
    }

    void CheckKeys()
    {
        //check if the key is down
        if (Input.IsKeyDown(KeyCode.W))
        {
            //move if the key is down
            obj.MoveBy(new Vector2(0, -speed));
        }
        if (Input.IsKeyDown(KeyCode.S))
        {
            obj.MoveBy(new Vector2(0, speed));
        }
        if (Input.IsKeyDown(KeyCode.A))
        {
            obj.MoveBy(new Vector2(-speed, 0));
        }
        if (Input.IsKeyDown(KeyCode.D))
        {
            obj.MoveBy(new Vector2(speed, 0));
        }
        if (Input.IsKeyDown(KeyCode.E))
        {
            obj.ChangeRotation(1);
        }
    }
}
