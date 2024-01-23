
using UnityEditor;
using UnityEngine;
using Unity.Mathematics;

[CustomPropertyDrawer(typeof(int2))]
public class Int2Drawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label, true);
    }
}
