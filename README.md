# GraphicalEngine

The Engine is basically "ConsoleEngine", but with actual graphics, instead of console-colors.

## Instalation

Install SFML.Net, clone this project (or download DLL from releases), and import to your project
> Notice: The DLL in the releases tab may be outdated for a certain amount of time.

## DLL USAGE
If you are the DLL version, please copy this code:
```cs
using GraphicalEngine;
using GraphicalEngine.Engine;

class Program
{
    static void Main()
    {
        GEngine.Settings s = new GEngine.Settings();
        s.screenSize = new Vector2(600, 400); //this is changeable

        GEngine.Init(s);
    }
}
```

## Usage

You can create a sample game with the assets the engine already provides in the source code:
<br>Game/Textures/white.png | White.png is a white square with 100x100 pixels.
<br>You can use it as a sample for testing in your game. The source code also contains a simple game, which the code is now listed below:

```cs
using GraphicalEngine;

class Example : GraphicalBehaviour
{
    GameObject obj = null!;
    float speed = 1;

    public override void Start()
    {
        Debug.Log("Log");
        Debug.LogWarning("Log-warning");
        Debug.LogError("Log-error");

        obj = new GameObject();
        obj.SetName("obj")
            .SetSprite("white.png")
            .SetScale(0.1f)
            .SetPosition(ScreenData.GetScreenCenter(obj));
    }

    public override void Update()
    {
        if (Input.IsKeyDown(KeyCode.W))
        {
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
```
> NOTE: The game-code currently provided contains comments explaining what does what in the source code.

It currently contains Input and GameObjects.
<br>You can destroy GameObjects like this:
```cs
GameObject g = new GameObject();
g.Destroy();
```
<br>**information:** ***custom texture-sprites have to be in: "./Game/Textures", otherwise the ENGINE can NOT find it.***

>NOTE: You can also change the engine-code (GameObject.cs) to remove it, or in code use "../../", which will make it to the root of the project.

>Additional NOTE: If you are on visual studio and put your game files into your path where your game code is (usually in /repos/[project]/), you will have to:
<br>Right click the image -> Properties -> set Copy to output directory to "Copy, if newer" or "Always copy".

## Technologies

- [SFML.Net](https://www.nuget.org/packages/SFML.Net) - Rendering

## Documentation

> comming soon

## Soon

I'm planning on adding audio and text

## License
This project is using the [GNU GPLv3](https://choosealicense.com/licenses/gpl-3.0/) license.
