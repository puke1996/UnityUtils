using System;
using UnityEngine;

namespace Plugins.Puke.UnityUtilities.UnityUtils
{
    public class CoordinateSystemConversionUtils : MonoBehaviour
    {
        public static Vector3 LocalToWorldPos(Transform localSpace, Vector3 localPos)
        {
            if (localSpace == null)
            {
                throw new Exception("Local Space Not Exist");
            }

            return localSpace.TransformPoint(localPos);
        }

        public static Vector3 WorldToLocalPos(Transform localSpace, Vector3 worldPos)
        {
            return localSpace.InverseTransformPoint(worldPos);
        }

        public static Vector3 WorldToScreenPos(Camera cam, Vector3 worldPos)
        {
            return cam.WorldToScreenPoint(worldPos);
        }

        public static Vector3 ScreenToWorldPos(Camera cam, Vector3 screenPos, float depth)
        {
            return cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, depth));
        }

        public static Vector3 WorldToViewportPos(Camera cam, Vector3 worldPos)
        {
            return cam.WorldToViewportPoint(worldPos);
        }

        public static Vector3 ViewportToWorldPos(Camera cam, Vector3 viewportPos)
        {
            return cam.ViewportToWorldPoint(viewportPos);
        }

        public static Vector3 ViewportToScreenPos(Camera cam, Vector3 viewportPos)
        {
            return cam.ViewportToScreenPoint(viewportPos);
        }

        public static Vector3 ScreenToViewportPos(Camera cam, Vector3 screenPos)
        {
            return cam.ScreenToViewportPoint(screenPos);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="screenPos"></param>
        /// <param name="uiCam">UI相机，如果是ScreenSpaceOverride，那么uiCam=null</param>
        /// <returns></returns>
        public static Vector2 ScreenToUIPosPivot(RectTransform rt, Vector3 screenPos, Camera uiCam)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, screenPos, uiCam, out var uiPos);
            return uiPos;
        }

        /// <summary>
        /// 以UI的左下角计算的坐标
        /// cam是ui的cam,如果是覆盖模式UIcam就是null
        /// </summary>
        public static Vector2 ScreenToUIPosLeftDown(RectTransform rt, Vector3 screenPos, Camera uiCam)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, screenPos, uiCam, out var uiPos);
            // 计算锚点的坐标
            var pivotPos = new Vector2(rt.pivot.x * rt.rect.size.x, rt.pivot.y * rt.rect.size.y);
            return uiPos + pivotPos;
        }

        /// <summary>
        /// 以UI的中心计算的坐标
        /// cam是ui的cam,如果是覆盖模式UIcam就是null
        /// </summary>
        public static Vector2 ScreenToUIPosCenter(RectTransform rt, Vector3 screenPos, Camera uiCam)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, screenPos, uiCam, out var uiPos);
            // 计算锚点的坐标
            var pivotPos = new Vector2(rt.pivot.x * rt.rect.size.x, rt.pivot.y * rt.rect.size.y);
            return uiPos + pivotPos - new Vector2(rt.rect.size.x, rt.rect.size.y) * 0.5f;
        }

        // /// <summary>
        // /// UI转UI坐标
        // /// </summary>
        // public static void UIToUIPos()
        // {
        //     
        // }
        // public static Vector2 UI2CanvasPos(RectTransform canvas, RectTransform target)
        // {
        //     var screenPos = World2ScreenPos(null, target.transform.position);
        //     Screen2UIPos(screenPos)
        //     return canvas.InverseTransformPoint(target.position);
        // }

        /// <summary>
        /// 将世界空间中的直线转换到屏幕空间
        /// </summary>
        public static Line WorldToScreenLine(Line line, Camera cam)
        {
            var endpoint1 = WorldToScreenPos(cam, line.endpoint1);
            var endpoint2 = WorldToScreenPos(cam, line.endpoint2);
            return new Line(endpoint1, endpoint2);
        }

        public static Vector3 LocalToWorldVec(Transform localSpace, Vector3 localVec)
        {
            return localSpace.TransformVector(localVec);
        }

        public static Vector3 WorldToLocalVec(Transform localSpace, Vector3 worldVec)
        {
            return localSpace.InverseTransformVector(worldVec);
        }

        public class Line
        {
            public Vector3 endpoint1;
            public Vector3 endpoint2;

            public Line(Vector3 endpoint1, Vector3 endpoint2)
            {
                this.endpoint1 = endpoint1;
                this.endpoint2 = endpoint2;
            }

            public float Length => (endpoint2 - endpoint1).magnitude;
        }
    }
}