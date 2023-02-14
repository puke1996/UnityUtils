using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{
    public static class Vector2ExtensionMethods
    {
        /// <summary>
        /// GetSlope
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static float GetSlope(Vector2 v1, Vector2 v2)
        {
            return (v2.y - v1.y) / (v2.x - v1.x);
        }

        /// <summary>
        /// Vector2 to Vector3 List
        /// </summary>
        /// <param name="vector2List"></param>
        /// <returns></returns>
        public static List<Vector3> ToVector3List(this List<Vector2> vector2List)
        {
            var vector3List = new List<Vector3>();
            foreach (var vector2 in vector2List)
            {
                vector3List.Add(vector2);
            }

            return vector3List;
        }
    }
}