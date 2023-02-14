using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{
    public static class BoundsExtensionMethods
    {
        /// <summary>
        /// Merge
        /// </summary>
        /// <param name="boundsList"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Bounds Merge(this IList<Bounds> boundsList)
        {
            if (boundsList.Count == 0)
            {
                throw new Exception();
            }

            var mergedBounds = boundsList[0];
            for (var i = 1; i < boundsList.Count; i++)
            {
                mergedBounds.Encapsulate(boundsList[i]);
            }

            return mergedBounds;
        }

        /// <summary>
        /// GetRadius
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public static float GetRadius(this Bounds bounds)
        {
            return (float) Math.Sqrt(Math.Pow(bounds.extents.x, 2) +
                                     Math.Pow(bounds.extents.y, 2) +
                                     Math.Pow(bounds.extents.z, 2));
        }

        /// <summary>
        /// GetDiameter
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public static float GetDiameter(this Bounds bounds)
        {
            return (float) Math.Sqrt(Math.Pow(bounds.size.x, 2) + Math.Pow(bounds.size.y, 2) +
                                     Math.Pow(bounds.size.z, 2));
        }
    }
}