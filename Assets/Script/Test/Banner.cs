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
		StartCoroutine(Create(5f));
	}

	IEnumerator Create(float time)
	{
		yield return new WaitForSeconds(time);
		_panel.enabled = true;
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
