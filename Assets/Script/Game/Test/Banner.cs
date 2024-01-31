using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Banner : MonoBehaviour
{
	Controller _page;
	UIPanel _panel;

	void Start()
	{
		var panel = _panel = GetComponent<UIPanel>();
		SceneManager.sceneLoaded += SceneManager_sceneLoaded;

	}

	private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		if (arg0.name.StartsWith("Level01"))
		{
			_panel.enabled = true;
			StartCoroutine(Create());
		}
	}

	IEnumerator Create()
	{
		yield return null;
		_page = _panel.ui.GetController("pages");
		StartCoroutine(Loop());
	}

	IEnumerator Loop()
	{
		var wait = new WaitForSeconds(5f);
		while (true)
		{
			yield return wait;
			var c = _page.selectedIndex;
			c++;
			if (c >= _page.pageCount) c = 0;
			_page.selectedIndex = c;
		}

	}


}
