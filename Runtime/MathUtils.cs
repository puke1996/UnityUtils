using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Assertions;
using UnityEngine;

namespace Plugins.Puke.UnityUtilities.UnityUtils
{
    public static partial class MathUtils
    {
        public static double Variance(this IEnumerable<float> list)
        {
            var arr = list as float[] ?? list.ToArray();
            double average = arr.Sum() / arr.Count();
            var tempList = from float each in arr select (each - average) * (each - average);
            var sum = tempList.Sum();
            return sum / arr.Count();
        }

        /// <summary>
        /// 将向量限制到box内
        /// </summary>
        public static Vector3 Clamp(Vector3 aim, Vector3 min, Vector3 max)
        {
            return new Vector3(
                Mathf.Clamp(aim.x, min.x, max.x),
                Mathf.Clamp(aim.y, min.y, max.y),
                Mathf.Clamp(aim.z, min.z, max.z)
            );
        }

        /// <summary>
        /// 给两个点,获取左下角和右上角的点坐标
        /// </summary>
        public static Tuple<Vector2, Vector2> GetMinAndMax(Vector2 pos1, Vector2 pos2)
        {
            var (minX, maxX) = pos1.x < pos2.x ? (pos1.x, pos2.x) : (pos2.x, pos1.x);
            var (minY, maxY) = pos1.y < pos2.y ? (pos1.y, pos2.y) : (pos2.y, pos1.y);
            return new Tuple<Vector2, Vector2>(new Vector2(minX, minY), new Vector2(maxX, maxY));
        }

        /// <summary>
        /// 是否在矩形内
        /// </summary>
        public static bool IsInRect2D(this Vector2 target, Vector2 pos1, Vector2 pos2)
        {
            var result = GetMinAndMax(pos1, pos2);
            var min = result.Item1;
            var max = result.Item2;
            if (target.x > min.x && target.y > min.y && target.x < max.x && target.y < max.y)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 判断是否在矩形范围内,XZ平面
        /// </summary>
        /// <param name="target"></param>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <returns></returns>
        public static bool IsInRectXZ(this Vector3 target, Vector3 pos1, Vector3 pos2)
        {
            var (minX, maxX) = pos1.x < pos2.x ? (pos1.x, pos2.x) : (pos2.x, pos1.x);
            var (minZ, maxZ) = pos1.z < pos2.z ? (pos1.z, pos2.z) : (pos2.z, pos1.z);
            var min = new Vector3(minX, 0, minZ);
            var max = new Vector3(maxX, 0, maxZ);
            if (target.x > min.x && target.z > min.z && target.x < max.x && target.z < max.z)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 同一帧创建的每个随机数生成器生成的随机数都是一样的,得使用同一个随机数生成器来生成随机数
        /// </summary>
        public static readonly System.Random rand = new System.Random();

        public static T GetRandomElement<T>(this IList<T> list)
        {
            return list[RandomInt(0, list.Count)];
        }

        public static T GetRandomElement<T>(this T[,] list)
        {
            List<T> temp = new List<T>();
            foreach (var e in list)
            {
                temp.Add(e);
            }

            return temp.GetRandomElement();
        }

        /// <param name="min">包括min</param>
        /// <param name="max">不包括max</param>
        public static int RandomInt(int min, int max)
        {
            return rand.Next(min, max);
        }

        /// <summary>
        /// 包括min与max
        /// 后续如果上面没用了就换上去
        /// </summary>
        public static int RandomInt2(int min, int max)
        {
            return rand.Next(min, max + 1);
        }

        /// <summary>
        /// 包含头尾
        /// </summary>
        public static int GetRandomIntInclude(int min, int max)
        {
            return rand.Next(min, max + 1);
        }

        public static void RandomOrder(IList list)
        {
            var rand = new System.Random();
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var r = rand.Next(n + 1);
                var temp = list[r];
                list[r] = list[n];
                list[n] = temp;
            }
        }

        /// <summary>
        /// Select n element from list length equals m
        /// </summary>
        /// <param name="list"></param>
        /// <param name="n"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<T> MN<T>(this IList<T> list, int n)
        {
            if (n > list.Count)
            {
                throw new Exception("N>M");
            }

            var newList = new List<T>(list);
            var times = list.Count - n;
            for (int i = 0; i < times; i++)
            {
                var element = newList.GetRandomElement();
                newList.Remove(element);
            }

            Assert.IsTrue(n == newList.Count, list.Count + "" + n);
            return newList;
        }

        public static Vector3[] MakeSmoothCurve(Vector3[] arrayToCurve, float smoothness)
        {
            List<Vector3> points;
            List<Vector3> curvedPoints;
            int pointsLength = 0;
            int curvedLength = 0;

            if (smoothness < 1.0f) smoothness = 1.0f;

            pointsLength = arrayToCurve.Length;

            curvedLength = (pointsLength * Mathf.RoundToInt(smoothness)) - 1;
            curvedPoints = new List<Vector3>(curvedLength);

            float t = 0.0f;
            for (int pointInTimeOnCurve = 0; pointInTimeOnCurve < curvedLength + 1; pointInTimeOnCurve++)
            {
                t = Mathf.InverseLerp(0, curvedLength, pointInTimeOnCurve);

                points = new List<Vector3>(arrayToCurve);

                for (int j = pointsLength - 1; j > 0; j--)
                {
                    for (int i = 0; i < j; i++)
                    {
                        points[i] = (1 - t) * points[i] + t * points[i + 1];
                    }
                }

                curvedPoints.Add(points[0]);
            }

            return (curvedPoints.ToArray());
        }

        /// <summary>
        /// 计算正交俯视时,头顶的位置
        /// </summary>
        public static Vector3 CalcOrthogonalPos(Vector3 size, float camRotationX)
        {
            return new Vector3(0, size.y + size.z / 2 * (float) Math.Tan(MathUtils.Deg2Rad(camRotationX)), 0);
        }

        /// <summary>
        /// 计算透视俯视时,头顶的位置
        /// 返回的是相对于目标物体底部的局部坐标
        /// </summary>
        public static Vector3 CalcPerspectivePos(Vector3 size, float camRotationX)
        {
            return new Vector3(0, size.y + size.z / 2 * (float) System.Math.Tan(MathUtils.Deg2Rad(camRotationX)), 0);
        }

        /// <summary>
        /// 获取最近点，以百分比表示
        /// </summary>
        private static float GetNearestPointPercentage(CoordinateSystemConversionUtils.Line line, Vector3 pos)
        {
            var dotProduct1 = DotProduct((line.endpoint2 - line.endpoint1), (pos - line.endpoint1));
            if (dotProduct1 <= 0) return 0;
            var dotProduct2 = DotProduct((line.endpoint1 - line.endpoint2), (pos - line.endpoint2));
            if (dotProduct2 <= 0) return 1;
            var length = (line.endpoint2 - line.endpoint1).magnitude;
            var projection = dotProduct1 / length;
            var percentage = projection / length;
            return percentage;
        }

        /// <summary>
        /// 在屏幕空间进行比较
        /// 返回的是世界空间中的点
        /// </summary>
        public static Vector3 GetNearestPointOnLineInCameraSpace(CoordinateSystemConversionUtils.Line line,
            Vector3 screenPos, Camera cam)
        {
            var screenLine = CoordinateSystemConversionUtils.WorldToScreenLine(line, cam);
            var result = GetNearestPointPercentage(screenLine, (Vector2) screenPos);
            return line.endpoint1 + (line.endpoint2 - line.endpoint1) * result;
        }

        public static float GetDistanceBetweenPointAndLineInCameraSpace(Vector3 screenPos,
            CoordinateSystemConversionUtils.Line line)
        {
            // 将线条转换到屏幕空间
            var endPoint1ScreenPos = CoordinateSystemConversionUtils.WorldToScreenPos(Camera.main, line.endpoint1);
            var endPoint2ScreenPos = CoordinateSystemConversionUtils.WorldToScreenPos(Camera.main, line.endpoint2);
            var screenSpaceLine = new CoordinateSystemConversionUtils.Line(endPoint1ScreenPos, endPoint2ScreenPos);
            // 获取屏幕空间中最近的点
            var pos = GetNearestPointOnLine(screenSpaceLine, screenPos);
            // 计算距离
            return Vector3.Distance(pos, screenPos);
        }

        /// <summary>
        /// 求最近点
        /// </summary>
        public static Vector3 GetNearestPointOnLine(CoordinateSystemConversionUtils.Line line, Vector3 pos)
        {
            var dotProduct1 = DotProduct((line.endpoint2 - line.endpoint1), (pos - line.endpoint1));
            if (dotProduct1 <= 0) return line.endpoint1;
            var dotProduct2 = DotProduct((line.endpoint1 - line.endpoint2), (pos - line.endpoint2));
            if (dotProduct2 <= 0) return line.endpoint2;
            var projection = dotProduct1 / line.Length;
            return line.endpoint1 + (line.endpoint2 - line.endpoint1).normalized * projection;
        }

        // /// <summary>
        // /// 获取点和线之间的距离
        // /// </summary>
        // public static void GetDistanceBetweenPointAndLine(Vector3 pos, Line line)
        // {
        //     Unity.
        // }

        /// <summary>
        /// 点积
        /// 几何意义: v2在v1方向上的投影长度乘以v1的长度,或v1在v2方向上的投影乘以v2的长度
        /// 负数,代表大于90度
        /// 0,代表等于90度
        /// 正数,代表小于90度
        /// 满足交换律
        /// </summary>
        public static float DotProduct(Vector3 v1, Vector3 v2)
        {
            return v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
        }

        /// <summary>
        /// 叉积
        /// 几何意义: 两个向量所组成的平行四边形的面积
        /// 负数,代表v2在v1左侧
        /// 0,代表v2和v1平行
        /// 正数,代表v2在v1右侧
        /// 不满足交换律
        /// 仅适用于平面
        /// </summary>
        public static float CrossProduct(Vector2 v1, Vector2 v2)
        {
            return v1.x * v2.y - v2.x * v1.y;
        }

        /// <summary>
        /// 2维版本
        /// </summary>
        public static bool InRect(Vector2 point1, Vector2 point2, Vector2 pos)
        {
            var (minX, maxX) = point1.x < point2.x ? (point1.x, point2.x) : (point2.x, point1.x);
            var (minY, maxY) = point1.y < point2.y ? (point1.y, point2.y) : (point2.y, point1.y);
            return (pos.x > minX) && (pos.x < maxX) && (pos.y > minY) && (pos.y < maxY);
        }

        /// <summary>
        /// XZ平面俯视
        /// </summary>
        public static bool InRect(Vector3 pos, Vector3 point1, Vector3 point2)
        {
            // 先找出左下角和右上角
            var (minX, maxX) = point1.x < point2.x ? (point1.x, point2.x) : (point2.x, point1.x);
            var (minZ, maxZ) = point1.z < point2.z ? (point1.z, point2.z) : (point2.z, point1.z);
            return (pos.x > minX) && (pos.x < maxX) && (pos.z > minZ) && (pos.z < maxZ);
        }

        /// <summary>
        /// 计算45度角斜对你的盒形碰撞器的Bounds尺寸
        /// </summary>
        public static Vector3 Calc45Size(this BoxCollider boxCollider)
        {
            var x = boxCollider.size.x * boxCollider.transform.lossyScale.x;
            var y = boxCollider.size.y * boxCollider.transform.lossyScale.y;
            var z = boxCollider.size.z * boxCollider.transform.lossyScale.z;
            var d = (float) System.Math.Sqrt(x * z / System.Math.PI) * 2;
            return new Vector3(d, y, d);
        }
    }
}