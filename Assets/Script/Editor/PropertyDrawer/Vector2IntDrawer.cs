using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
[CustomPropertyDrawer(typeof(Vector2Int))]
public class Vector2IntDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label, true);
/*
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't indent child fields
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        //var xRect = new Rect(position.x, position.y, position.width / 2, position.height);
        //var yRect = new Rect(position.x + position.width / 2, position.y, position.width / 2, position.height);
        EditorGUI.PropertyField(position, property, label, true);
        // Draw fields - passs GUIContent.none to each so they are drawn without label

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
*/
    }

    /*
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Create property container element.
        var container = new VisualElement();

        // Create property fields.
        var xField = new PropertyField(property.FindPropertyRelative("xx"));
        var yField = new PropertyField(property.FindPropertyRelative("yy"));

        // Add fields to the container.
        container.Add(xField);
        container.Add(yField);

        return container;
    }
    */

}
