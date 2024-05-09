using System.Collections;
using System.Collections.Generic;
using SGame;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SGame.AttributeSystem))]
public class AttributeSystemEditor : Editor
{

	public override void OnInspectorGUI()
	{
		var v = serializedObject.FindProperty("m_items");
		if (v != null)
		{
			var count = v.arraySize;
			if(count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					var item = v.GetArrayElementAtIndex(i);
					var nv = item.FindPropertyRelative("name");
					var name = nv.stringValue;
					GUIHelp.DrawHead(name);
					EditorGUILayout.PropertyField(item);
					GUILayout.Space(10);
				}
			}
		}
	}
}
