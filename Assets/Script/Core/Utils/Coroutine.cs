using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Coroutine
{

	static public UnityEngine.Coroutine Delay(this object item, Action call, int delay)
	{
		if (call != null)
			return Mono.Start(Delay(delay, call));
		return default;
	}


	static public UnityEngine.Coroutine Call(this object item, Action call, Func<bool> condition)
	{
		if (call != null)
			return Mono.Start(Call(condition, call));
		return default;
	}

	static public UnityEngine.Coroutine Loop(this object item, Action call, Func<bool> condition, int interval = 0, int delay = 0)
	{
		if (condition == null) return default;
		return Mono.Start(Loop(condition, call, interval, delay));
	}

	static public UnityEngine.Coroutine Start(this IEnumerator enumerator)
	{
		if (enumerator != null)
			return Mono.Start(enumerator);
		return null;
	}

	static public void Wait<T>(this T wait, Action<T> call) where T : IEnumerator
	{
		if (wait != null && call != null)
		{
			Mono.Start(CoWait<T>(wait, call));
		}
	}

	static public void Wait(this Func<bool> wait, Action call = null)
	{
		if (wait != null)
		{
			Mono.Start(Call(wait, call));
		}
	}

	static public void CallWhenQuit(this Action call)
	{
		Mono.OnQuit(call);
	}

	static public IEnumerator WaitTime(this float time)
	{
		if (time > 0)
		{
			while (true)
			{
				time -= Time.deltaTime;
				if (time > 0)
					yield return null;
				else
					break;
			}
		}
		yield break;
	}

	static public void Stop(this UnityEngine.Coroutine coroutine)
	{
		if (coroutine != null)
			Mono.Stop(coroutine);
	}

	static private IEnumerator Delay(int delay, Action call)
	{
		if (delay <= 0) yield return null;
		else yield return new WaitForSeconds(delay * 0.001f);
		call?.Invoke();
	}

	static private IEnumerator Call(Func<bool> condition, Action call)
	{
		if (condition == null)
			yield return null;
		else
			yield return new WaitUntil(condition);
		call?.Invoke();
	}

	static private IEnumerator CoWait<T>(T e, Action<T> call) where T : IEnumerator
	{
		yield return e;
		call?.Invoke(e);
	}

	static private IEnumerator Loop(Func<bool> condition, Action call, int interval = 0, int delay = 0)
	{
		if (condition == null)
		{
			yield return null;
			call?.Invoke();
		}
		else
		{
			var wait = interval <= 0 ? default : new WaitForSeconds(interval * 0.001f);
			if (delay > 0)
				yield return new WaitForSeconds(delay * 0.001f);
			while (condition())
			{
				call?.Invoke();
				if (interval == 0)
					yield return null;
				else
					yield return wait;
			}
		}
	}


	class Mono : MonoBehaviour
	{
		static event Action OnQuited;

		static private Mono __;

		static public Mono Get()
		{
			if (!__)
			{
				__ = new GameObject().AddComponent<Mono>();
				__.gameObject.hideFlags = HideFlags.HideAndDontSave;
				DontDestroyOnLoad(__.gameObject);
			}
			return __;
		}

		static public UnityEngine.Coroutine Start(IEnumerator enumerator)
		{
			return Get().StartCoroutine(enumerator);
		}

		static public void Stop(UnityEngine.Coroutine coroutine)
		{
			Get().StopCoroutine(coroutine);
		}

		static public void Quit()
		{
			var c = OnQuited;
			OnQuited = null;
			c?.Invoke();
		}

		static public void OnQuit(Action action)
		{
			OnQuited -= action;
			OnQuited += action;
		}

		private void OnApplicationQuit()
		{
			Quit();
		}

	}
}
