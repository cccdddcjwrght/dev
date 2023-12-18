using UnityEditor;
using UnityEngine;
using Unity.Entities;

    [CustomPropertyDrawer(typeof(Unity.Entities.Entity))]
    public class EntityDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //EditorGUI.PropertyField(position, property, label, true);
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);


            // Don't indent child fields
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            //Entity e;
            //e.Version

            // Calculate rects
            var xRect = new Rect(position.x, position.y, position.width / 2, position.height);
            var yRect = new Rect(position.x + position.width / 2, position.y, position.width / 2, position.height);
            EditorGUI.PropertyField(xRect, property.FindPropertyRelative("Index"), GUIContent.none);
            EditorGUI.PropertyField(xRect, property.FindPropertyRelative("Version"), GUIContent.none);

            // Draw fields - passs GUIContent.none to each so they are drawn without label

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
        

    }


/*
public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
var x = property.FindPropertyRelative("x");
var y = property.FindPropertyRelative("y");
float LabelWidth = 50;
var labelRect = new Rect(position.x, position.y, LabelWidth, position.height);
var xRect = new Rect(position.x + LabelWidth, position.y, (position.width - LabelWidth) / 2 , position.height);
var yRect = new Rect(position.x + LabelWidth + (position.width - LabelWidth) / 2  , position.y, (position.width - LabelWidth) / 2 , position.height);

EditorGUI.LabelField(labelRect, label);
EditorGUI.PropertyField(xRect, x);
EditorGUI.PropertyField(yRect, y);
}
 */