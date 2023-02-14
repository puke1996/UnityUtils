using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{
    public static class ListUtil
    {
        /// <summary>
        /// Remove null
        /// </summary>
        /// <param name="list"></param>
        public static void RemoveNull(this IList list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] == null)
                {
                    list.RemoveAt(i);
                }
            }
        }

        // public static List<Vector3> ToVector3List(this List<Vector2> vector2s)
        // {
        //     var vector3s = new List<Vector3>();
        //     foreach (var vector2 in vector2s)
        //     {
        //         vector3s.Add(vector2);
        //     }
        //
        //     return vector3s;
        // }

        public static List<Vector2> ToVector2List(this List<Vector3> vector3s)
        {
            var vector2s = new List<Vector2>();
            foreach (var vector3 in vector3s)
            {
                vector2s.Add(vector3);
            }

            return vector2s;
        }
    }
}