using System.Collections.Generic;
using UnityEngine;

// Flood Fill implementation for Unity3D C#
// Used in: https://www.elfgamesworks.com/2016/12/14/identify-unwanted-maze-solutions-using-flood-fill-with-unity3d/
namespace Plugins.Puke.UnityUtilities.UnityExtensionMethods
{
    public static class ImageUtils
    {
        public struct Point
        {
            public int x;
            public int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        /// <summary>
        /// https://gist.github.com/ProGM/22a615b812c5a9f1183d43b536d14a42
        /// 漫水填充法
        /// 效率极低，不建议使用
        /// </summary>
        public static void FloodFill(Texture2D maskTexture, Texture2D writeTexture, Color color, float tolerance, int x,
            int y)
        {
            var sourceColor = maskTexture.GetPixel(x, y);
            // 创建一个队列
            // var q = new Queue<Point>(maskTexture.width * maskTexture.height);
            var active = new List<Point>();
            active.Add(new Point(x, y));
            // q.Enqueue(new Point(x, y));
            var used = new List<Point>();
            int iteration = 0;
            while (active.Count > 0)
            {
                // iteration++;
                // if (iteration > 100)
                // {
                //     break;
                // }

                Debug.Log(used.Count);
                // 出列并操作
                var point = active[active.Count - 1];
                active.Remove(point);
                var currentX = point.x;
                var currentY = point.y;
                writeTexture.SetPixel(currentX, currentY, color);
                // 传销
                var newPoint = new Point(currentX + 1, currentY);
                if (!used.Contains(newPoint) && CheckValidity(maskTexture, maskTexture.width, maskTexture.height, newPoint,
                    sourceColor, tolerance))
                {
                    used.Add(point);
                    active.Add(newPoint);
                }

                newPoint = new Point(currentX - 1, currentY);
                if (!used.Contains(newPoint) && CheckValidity(maskTexture, maskTexture.width, maskTexture.height, newPoint,
                    sourceColor, tolerance))
                {
                    used.Add(point);
                    active.Add(newPoint);
                }

                newPoint = new Point(currentX, currentY + 1);
                if (!used.Contains(newPoint) && CheckValidity(maskTexture, maskTexture.width, maskTexture.height, newPoint,
                    sourceColor, tolerance))
                {
                    used.Add(point);
                    active.Add(newPoint);
                }

                newPoint = new Point(currentX, currentY - 1);
                if (!used.Contains(newPoint) && CheckValidity(maskTexture, maskTexture.width, maskTexture.height, newPoint,
                    sourceColor, tolerance))
                {
                    used.Add(point);
                    active.Add(newPoint);
                }
            }
        }

        static bool CheckValidity(Texture2D texture, int width, int height, Point p, Color sourceColor, float tolerance)
        {
            if (p.x < 0 || p.x >= width)
            {
                return false;
            }

            if (p.y < 0 || p.y >= height)
            {
                return false;
            }

            var color = texture.GetPixel(p.x, p.y);
            var distance = Mathf.Abs(color.r - sourceColor.r) + Mathf.Abs(color.g - sourceColor.g) +
                           Mathf.Abs(color.b - sourceColor.b) + Mathf.Abs(color.a - sourceColor.a);
            return distance <= tolerance;
        }
    }
}