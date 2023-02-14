using System;
using System.Linq;
using Plugins.Puke.UnityUtilities.AutoGetcomponentAttributes;
using UnityEditor;
using UnityEngine;

namespace Plugins.Puke.Engine.UnityAttributes.Editor
{
    [CustomPropertyDrawer(typeof(ChildAttribute))]
    public class ChildDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var customAttributeData =
                fieldInfo.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(ChildAttribute));
            // 有参
            var targetObject = property.serializedObject.targetObject as Component;
            if (customAttributeData.ConstructorArguments.Count > 0)
            {
                var childName = (string) customAttributeData.ConstructorArguments[0].Value;
                if (childName != null)
                {
                    targetObject = targetObject.transform.FindDeepChild(childName);
                    if (targetObject == null)
                    {
                        throw new Exception(property.name);
                    }
                }
            }

            property.objectReferenceValue = targetObject.GetComponentInChildren(fieldInfo.FieldType);
            // 使字段显示且不可编辑
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}