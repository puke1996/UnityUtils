using System;
using Plugins.Puke.UnityUtilities.AutoGetcomponentAttributes;
using UnityEditor;
using UnityEngine;

namespace Plugins.Puke.Engine.UnityAttributes.Editor
{
    [CustomPropertyDrawer(typeof(SelfAttribute))]
    public class SelfDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Component targetObject = null;
            try
            {
                targetObject = property.serializedObject.targetObject as Component;
                property.objectReferenceValue = targetObject.GetComponent(fieldInfo.FieldType);
                // 使字段显示且不可编辑
                GUI.enabled = false;
                EditorGUI.PropertyField(position, property, label, true);
                GUI.enabled = true;
            }
            catch (Exception e)
            {
                throw new Exception("targetObject.name is " + targetObject.name + " targetObject.gameObject.name is " +
                                    targetObject.gameObject.name);
            }
        }
    }
}