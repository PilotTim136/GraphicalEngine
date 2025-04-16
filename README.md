# GraphicalEngine

The Engine is basically "ConsoleEngine", but with actual graphics, instead of console-colors.

## Installation

Install SFML.Net, clone this project (or download DLL from releases), and import to your project
> Notice: The DLL in the releases tab may be outdated for a certain amount of time.

| Type          | Version       |
| ------------- | ------------- |
| Source Code   | BETA 2.0      |
| DLL (un usable state)         | BETA 2.0      |

PLEASE USE THE DOCUMENTATION: [Docs](https://pilottim136.gitbook.io/graphicalengine/)

### a lot of stuff has been removed from this readme, and documentaries are available for all versions.

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

## License
This project is using the [GNU GPLv3](https://choosealicense.com/licenses/gpl-3.0/) license.
