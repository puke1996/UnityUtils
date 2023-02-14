using Plugins.Puke.UnityUtilities.AutoGetcomponentAttributes;
using UnityEditor;
using UnityEngine;

namespace Plugins.Puke.Engine.UnityAttributes.Editor
{
    [CustomPropertyDrawer(typeof(ParentAttribute))]
    public class ParentDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var targetObject = property.serializedObject.targetObject as Component;
            property.objectReferenceValue = targetObject.GetComponentInParent(fieldInfo.FieldType);
            // 使字段显示且不可编辑
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}