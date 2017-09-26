# Tobi's Unity Utilities

Over the years, I've worked on a lot of [projects](http://dragonlab.de/portfolio) and [game jam prototypes](http://blog.dragonlab.de/tag/unity) with Unity3D and there are some pieces of code that I've needed time and time again. I'm sharing them here in the hopes that they are useful for you too!

Everything is released under the [MIT License](https://opensource.org/licenses/MIT).

If you find any bugs or have suggestions, please add an [Issue](https://github.com/TobiasWehrum/unity-utilities/issues) here or send me a mail at Tobias.Wehrum@dragonlab.de.

## Overview
* [Countdown](https://github.com/TobiasWehrum/unity-utilities/tree/master/Countdown): Useful for things like cooldowns or spawn delays. It is also helpful for tweening things by using the `PercentElapsed` property.
* [EditorHelper](https://github.com/TobiasWehrum/unity-utilities/tree/master/EditorHelper): Gets the `[Tooltip]` attribute content of fields for editor classes. Might get more helper methods in the future.
* [LINQExtensions](https://github.com/TobiasWehrum/unity-utilities/tree/master/LINQExtensions): A collection of extension methods for `IEnumerable`, `List` and arrays.
* [MathHelper](https://github.com/TobiasWehrum/unity-utilities/tree/master/MathHelper): Helper methods for framerate-independent eased lerping, mapping and angles.
* [MeshCreator](https://github.com/TobiasWehrum/unity-utilities/tree/master/MeshCreator): Makes it more convenient to create meshes via code.
* [NoiseOutputValue](https://github.com/TobiasWehrum/unity-utilities/tree/master/NoiseOutputValue): Enter a range and a speed in the editor, get an output value that fluctuates over time using [Perlin Noise](http://docs.unity3d.com/ScriptReference/Mathf.PerlinNoise.html).
* [RandomBag](https://github.com/TobiasWehrum/unity-utilities/tree/master/RandomBag): A `RandomBag` gives you random items from a group while ensuring that in a certain interval every item was given back the same number of times.
* [Range](https://github.com/TobiasWehrum/unity-utilities/tree/master/Range): Editable data types that take an `int`/`float` range.  Used for things like "Spawn 2 to 4 enemies."
* [RollingArray](https://github.com/TobiasWehrum/unity-utilities/tree/master/RollingArray): Collection that keeps the last x elements that are added to it.
* [Singleton](https://github.com/TobiasWehrum/unity-utilities/tree/master/Singleton): Allows easy and convenient creation of a Singleton. Optionally makes a Singleton persist between scenes while ensuring that only one exists.
* [UnityHelper](https://github.com/TobiasWehrum/unity-utilities/tree/master/UnityHelper): Contains a plethora of useful extensions and helpers for Transform, GameObject, Vector2/3/4, Rect and more.
* [XmlHelper](https://github.com/TobiasWehrum/unity-utilities/tree/master/XmlHelper): Serializes data to XML strings and makes accessing optional element content and attributes in general XMLs easier.

## Usage

To use the scripts, just drop them into the Assets folder of your projects. Or better yet, make an "Assets/Extensions/TobisUnityUtitilites" folder and drop them there. Hurray for proper organisation.

You can also just use selected scripts, but you should check the "Dependencies" section in the respective folder to make sure you copy everything you need.

## Documentation

The class documentation is available [here](http://tobiaswehrum.github.io/UnityUtilities/html/annotated.html).

## Changelog
* 2016-10-23: Fixed bugs/improved [Singleton](https://github.com/TobiasWehrum/unity-utilities/tree/master/Singleton). Added EasedLerp methods for float in [MathHelper](https://github.com/TobiasWehrum/unity-utilities/tree/master/MathHelper) and Vector2, Vector3 and Color in [UnityHelper](https://github.com/TobiasWehrum/unity-utilities/tree/master/UnityHelper). Added CalculateCentroid in [UnityHelper](https://github.com/TobiasWehrum/unity-utilities/tree/master/UnityHelper) for arrays/lists of Vector2/3/4.
* 2016-10-22: Added [MathHelper](https://github.com/TobiasWehrum/unity-utilities/tree/master/MathHelper). Added randomization helper methods to [UnityHelper](https://github.com/TobiasWehrum/unity-utilities/tree/master/UnityHelper).
* 2016-07-03: Added [MeshCreator](https://github.com/TobiasWehrum/unity-utilities/tree/master/MeshCreator).
* 2016-06-19: Added [XmlHelper](https://github.com/TobiasWehrum/unity-utilities/tree/master/XmlHelper).
* 2016-06-08: Added [UnityHelper](https://github.com/TobiasWehrum/unity-utilities/tree/master/UnityHelper).
* 2016-06-05: Added [EditorHelper](https://github.com/TobiasWehrum/unity-utilities/tree/master/EditorHelper) and [RollingArray](https://github.com/TobiasWehrum/unity-utilities/tree/master/RollingArray). Added `[Tooltip]` for `NoiseOutputValue` and edited the existing `PropertyDrawer` to use tooltips.
* 2016-05-15: Added [LINQExtensions](https://github.com/TobiasWehrum/unity-utilities/tree/master/LINQExtensions) and [RandomBag](https://github.com/TobiasWehrum/unity-utilities/tree/master/RandomBag).
* 2016-05-09: Added the [class documentation website](http://tobiaswehrum.github.io/UnityUtilities/html/annotated.html).
* 2016-05-08: Added [Countdown](https://github.com/TobiasWehrum/unity-utilities/tree/master/Countdown), [NoiseOutputValue](https://github.com/TobiasWehrum/unity-utilities/tree/master/NoiseOutputValue), [Range](https://github.com/TobiasWehrum/unity-utilities/tree/master/Range) and [Singleton](https://github.com/TobiasWehrum/unity-utilities/tree/master/Singleton).
* 2017-09-26: Removed .gitignore. Update note: This will break any scene you use example scripts in and might cause one-time .meta conflicts if you track your projects with git and didn't remove the .gitignore yourself. I'm sorry.