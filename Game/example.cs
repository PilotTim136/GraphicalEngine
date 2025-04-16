using GraphicalEngine;

class Example : GraphicalBehaviour
{
    GameObject obj = null!;
    float speed = 1;

    public override void Start()
    {
        //example logs
        Debug.Log("Log");
        Debug.LogWarning("Log-warning");
        Debug.LogError("Log-error");

        obj = new GameObject();
        obj.SetName("obj")
            .SetSprite("white.png") //the sprite in Game/Textures/
            .SetScale(0.1f) //the scale of the sprite
            .SetPosition(ScreenData.GetScreenCenter(obj)); //get the center of the screen for this object
    }

    public override void Update()
    {
        //keys
        if (Input.IsKeyDown(KeyCode.W))
        {
            //move the objects
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
    }
}
