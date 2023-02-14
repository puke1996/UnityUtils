using UnityEngine;
using UnityEngine.UI;

namespace Plugins.Puke.UnityUtilities.UnityExtensionMethods
{
    public static class RectTransformExtensionMethods
    {
        /// <summary>
        /// RefreshImmediately
        /// </summary>
        /// <param name="rectTransform"></param>
        public static void ForceRebuildLayoutImmediate(this RectTransform rectTransform)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }

        /// <summary>
        /// Side
        /// </summary>
        public enum Side
        {
            Up,
            Right,
            Down,
            Left
        }

        /// <summary>
        /// Align to side
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="side"></param>
        /// <param name="widthOrHeight"></param>
        public static void AlignToSide(this RectTransform rt, Side side, float widthOrHeight)
        {
            switch (side)
            {
                case Side.Up:
                    rt.pivot = new Vector2(0.5f, 1);
                    rt.anchorMin = Vector2.up;
                    rt.anchorMax = Vector2.one;
                    rt.sizeDelta = new Vector2(0, widthOrHeight);
                    break;
                case Side.Right:
                    rt.pivot = new Vector2(1f, 0.5f);
                    rt.anchorMin = Vector2.right;
                    rt.anchorMax = Vector3.one;
                    rt.sizeDelta = new Vector2(widthOrHeight, 0);
                    break;
                case Side.Down:
                    rt.pivot = new Vector2(0.5f, 0);
                    rt.anchorMin = Vector2.zero;
                    rt.anchorMax = Vector2.right;
                    rt.sizeDelta = new Vector2(0, widthOrHeight);
                    break;
                case Side.Left:
                    rt.pivot = new Vector2(0, 0.5f);
                    rt.anchorMin = Vector2.zero;
                    rt.anchorMax = Vector2.up;
                    rt.sizeDelta = new Vector2(widthOrHeight, 0);
                    break;
            }
        }

        /// <summary>
        /// Align to corner
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="corner"></param>
        /// <param name="size"></param>
        /// <param name="offset"></param>
        public static void AlignToCorner(this RectTransform rt, Vector2 corner, Vector2 size, Vector2 offset)
        {
            rt.pivot = corner;
            rt.anchorMin = corner;
            rt.anchorMax = corner;
            rt.anchoredPosition = offset;
            rt.sizeDelta = size;
        }

        // private const Vector2 v20 = new Vector2(0, 0);
        /// <summary>
        /// Align to all sides
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="offsetMin"></param>
        /// <param name="offsetMax"></param>
        /// <returns></returns>
        public static RectTransform AlignToAllSides(this RectTransform rt, Vector2 offsetMin = default,
            Vector2 offsetMax = default)
        {
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = offsetMin;
            rt.offsetMax = offsetMax;
            return rt;
        }

        /// <summary>
        /// 设置自身anchoredPosition相对于父节点Pivot的相对坐标
        /// </summary>
        public static void SetBaseOnParentPivotPos(this RectTransform rectTransform, Vector2 pos)
        {
            var parent = ((RectTransform) rectTransform.parent);
            // 先计算自身在父节点的坐标
            var delta = rectTransform.anchorMin - ((RectTransform) rectTransform.parent).pivot;
            var x = parent.rect.size.x * delta.x;
            var y = parent.rect.size.y * delta.y;
            rectTransform.anchoredPosition = new Vector2(pos.x - x, pos.y - y);
        }
    }
}