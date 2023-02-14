using System;
using UnityEngine;

namespace ExtensionMethods
{
    public static class ComponentExtensionMethods
    {
        /// <summary>
        /// GetChild
        /// </summary>
        /// <param name="context"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T FindChild<T>(this Component context)
        {
            var components = context.GetComponentsInChildren<T>();
            foreach (var component in components)
            {
                if ((component as Component)?.transform.gameObject != context.gameObject)
                {
                    return component;
                }
            }

            throw new Exception();
        }

        /// <summary>
        /// GetChild
        /// </summary>
        /// <param name="context"></param>
        /// <param name="childName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T FindChild<T>(this Component context, string childName)
        {
            if (string.IsNullOrEmpty(childName))
            {
                throw new Exception();
            }

            var targetTransform = context.transform.FindDeepChild(childName);
            if (targetTransform == null)
            {
                throw new Exception();
            }

            var result = targetTransform.GetComponent<T>();
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        /// <summary>
        /// GetChild
        /// </summary>
        /// <param name="context"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T GetParent<T>(this Component context)
        {
            var components = context.GetComponentsInParent<T>();
            foreach (var component in components)
            {
                if ((component as Component)?.transform.gameObject != context.gameObject)
                {
                    return component;
                }
            }

            throw new Exception();
        }
    }
}