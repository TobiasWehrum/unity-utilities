# SingletonMonoBehaviour and PersistentSingletonMonoBehaviour

SingletonMonoBehaviour can be used to provide static access to single instances like an EntityManager easily from anywhere in the code.

PersistentSingletonMonoBehaviour also provides static instance, but additionally marks the object with [`DontDestroyOnLoad`](http://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html). That is useful for objects like a MusicManager that should continue playing even when switching levels, or a PlayerData singleton that carries the name and statistics of a player between scenes.

## A word of caution

Note that the Singleton pattern has quite a few shortcomings. For example, the instance binding is hardwired in the code, which makes providing an alternative implementation for testing impossible. Unless you are using this for a prototype like a jam game, you might considering using a framework like [Zenject](https://github.com/modesttree/Zenject) or [StrangeIoC](http://strangeioc.github.io/strangeioc) instead.

## Examples

### SingletonEntityManager

If the following component is added on a game object in the scene, it could be accessed from anywhere via `SingletonEntityManager.Instance`, e.g.: `SingletonEntityManager.Instance.AddEntity(newEntity);`. This is available even before `SingletonEntityManager.Awake()` is called.

If you want to use OnDestroy(), you have to override it like shown in the example below. All other MonoBehaviour callbacks can be used as usual.

```C#
public class SingletonEntityManager : SingletonMonoBehaviour<SingletonEntityManager>
{
	List<GameObject> entities;

	public IEnumerable<GameObject> Entities
	{
		get { return entities; }
	}

	void Awake()
	{
		entities = new List<GameObject>();
	}

	// If you want to use OnDestroy(), you have to override it like this
	protected override void OnDestroy()
	{
		base.OnDestroy();
		Debug.Log("Destroyed");
	}

	public void AddEntity(GameObject entity)
	{
		entities.Add(entity);
	}

	public void RemoveEntity(GameObject entity)
	{
		entities.Remove(entity);
	}
}
```

### SingletonMusicManager

The `SingletonMusicManager` in the following example can be accessed in the same way, but it is not destroyed when the scenes switches.

You could make this SingletonMusicManager a prefab and drop it in multiple scenes that you work on. If at any time there are two `SingletonMusicManager`,
the one from the previous scene survives and the new one is destroyed. (For that reason, you should never create an `Awake()` method in a
PersistentSingletonMonoBehaviour. Instead, use `OnPersistentSingletonAwake()` because it is only called on "surviving" instances. Similarily, you shouldn't
have an `OnDestroy()` method which would be called if this is ever destroyed via `Destroy()`; instead, use `OnPersistentSingletonDestroyed()`.)

Note that `SingletonMusicManager.Instance` is only available after `SingletonMusicManager.Awake()` was called, so if you need it in another `Awake()`
call, you should put the `SingletonMusicManager` higher in the [Script Execution Order](http://docs.unity3d.com/Manual/class-ScriptExecution.html).

```C#
public class SingletonMusicManager : PersistentSingletonMonoBehaviour<SingletonMusicManager>
{
	protected override void OnPersistentSingletonAwake()
	{
		base.OnPersistentSingletonAwake();

		// Start playing the music the first time Awake() is called
		PlayMusic();
	}

	protected override void OnSceneSwitched()
	{
		base.OnSceneSwitched();

		// Fade to random song once a new scene is loaded
		FadeToRandomSong();
	}

	protected override void OnPersistentSingletonDestroyed()
	{
		base.OnPersistentSingletonDestroyed();
		
		// Stop the music when Destroy() was called on the active instance.
		StopMusic();
	}

	public void PlayMusic()
	{
		Debug.Log("Play music");
	}

	public void StopMusic()
	{
		Debug.Log("Stop music");
	}

	public void FadeToRandomSong()
	{
		Debug.Log("Fade to a random song");
	}
}
```

## Dependencies

None.