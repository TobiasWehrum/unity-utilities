# Unity Utilities

Over the years, I've worked on a lot of [projects](http://dragonlab.de/portfolio) and [game jam prototypes](http://blog.dragonlab.de/tag/unity) with Unity3D and there are some pieces of code that I've needed time and time again. I'm sharing them here in the hopes that they are useful for you too! It's just a few scripts for now, but expect updates with more once I clean them up.

Everything is released under the [MIT License](https://opensource.org/licenses/MIT).

If you find any bugs or have suggestions, please add an [Issue](https://github.com/TobiasWehrum/unity-utilities/issues) here or send me a mail at Tobias.Wehrum@dragonlab.de.

## Overview
* [Countdown](https://github.com/TobiasWehrum/unity-utilities/tree/master/Countdown): Useful for things like cooldowns or spawn delays. It is also helpful for tweening things by using the `PercentElapsed` property.
* [NoiseOutputValue](https://github.com/TobiasWehrum/unity-utilities/tree/master/NoiseOutputValue): Enter a range and a speed in the editor, get an output value that fluctuates over time using [Perlin Noise](http://docs.unity3d.com/ScriptReference/Mathf.PerlinNoise.html).
* [Range](https://github.com/TobiasWehrum/unity-utilities/tree/master/Range): Editable data types that takes an int/float range.  Used for things like "Spawn 2 to 4 enemies."
* [Singleton](https://github.com/TobiasWehrum/unity-utilities/tree/master/Singleton): Allows easy and convenient creation of a Singleton. Optionally makes a Singleton persist between scenes while ensuring that only one exists.

## Usage

To use the scripts, just drop them into the Assets folder of your projects. Or better yet, make an "Assets/Extensions/UnityUtitilites" folder and drop them there. Hurray for proper organisation.

You can also just use selected scripts, but you should check the "Dependencies" section in the respective folder to make sure you copy everything you need.

## Documentation

The class documentation is available [here](http://tobiaswehrum.github.io/UnityUtilities/html/annotated.html).

## Changelog
* 2015-05-09: Added the [class documentation website](http://tobiaswehrum.github.io/UnityUtilities/html/annotated.html).
* 2015-05-08: Added [Countdown](https://github.com/TobiasWehrum/unity-utilities/tree/master/Countdown), [NoiseOutputValue](https://github.com/TobiasWehrum/unity-utilities/tree/master/NoiseOutputValue), [Range](https://github.com/TobiasWehrum/unity-utilities/tree/master/Range) and [Singleton](https://github.com/TobiasWehrum/unity-utilities/tree/master/Singleton).