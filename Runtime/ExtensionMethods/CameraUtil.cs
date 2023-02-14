using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ExtensionMethods
{
    public static class CameraUtil
    {
        // /// <summary>
        // /// https://github.com/XJINE/Unity_ImageCaptureTool/blob/master/Assets/Packages/ImageCaptureTool/ImageCaptureTool.cs
        // /// </summary>
        // /// <param name="camera"></param>
        // /// <param name="width"></param>
        // /// <param name="height"></param>
        // /// <param name="clearBack"></param>
        // /// <returns></returns>
        // public static Texture2D Capture(this Camera camera, int width, int height, bool clearBack)
        // {
        //     // // NOTE:
        //     // // Keep presets.
        //     // Color tempBackgroundColor = camera.backgroundColor;
        //     // CameraClearFlags tempClearFlags = camera.clearFlags;
        //     // RenderTexture tempTargetTexture = camera.targetTexture;
        //     // if (clearBack)
        //     // {
        //     //     camera.backgroundColor = Color.clear;
        //     //     camera.clearFlags = CameraClearFlags.SolidColor;
        //     // }
        //
        //     RenderTexture captureImageTemp;
        //     // Texture2D captureImage;
        //     captureImageTemp = RenderTexture.GetTemporary(width, height, 32, RenderTextureFormat.ARGB32);
        //     camera.targetTexture = captureImageTemp;
        //     camera.Render();
        //     RenderTexture.active = captureImageTemp;
        //     var captureImage = new Texture2D(width, height, TextureFormat.ARGB32, false);
        //     captureImage.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        //     RenderTexture.active = null;
        //     RenderTexture.ReleaseTemporary(captureImageTemp);
        //     // // NOTE:
        //     // // Reset settings.
        //     // camera.backgroundColor = tempBackgroundColor;
        //     // camera.clearFlags = tempClearFlags;
        //     // camera.targetTexture = tempTargetTexture;
        //     captureImage.Apply();
        //     return captureImage;
        // }

        // /// <summary>
        // /// Capture
        // /// </summary>
        // /// <param name="camera"></param>
        // /// <param name="width"></param>
        // /// <param name="height"></param>
        // /// <returns></returns>
        // public static Texture2D Capture(this Camera camera, int width, int height)
        // {
        //     var tempRenderTexture = RenderTexture.GetTemporary(width, height, default, RenderTextureFormat.ARGB32);
        //     camera.targetTexture = tempRenderTexture;
        //     camera.Render();
        //     RenderTexture.active = tempRenderTexture;
        //     var texture = new Texture2D(width, height, TextureFormat.ARGB32,
        //         default);
        //     texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        //     texture.Apply();
        //     // Clean
        //     camera.targetTexture = null;
        //     RenderTexture.active = null;
        //     RenderTexture.ReleaseTemporary(tempRenderTexture);
        //     return texture;
        // }

        public static Texture2D Capture(this Camera cam, int width, int height)
        {
            var rt = new RenderTexture(width, height, 16, RenderTextureFormat.ARGB32);
            rt.Create();
            cam.targetTexture = rt;
            var renderTexture = RenderTexture.active;
            RenderTexture.active = cam.targetTexture;
            cam.Render();
            var texture = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
            texture.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
            texture.Apply();
            RenderTexture.active = renderTexture;
            return texture;
        }
        // /// <summary>
        // /// Capture
        // /// </summary>
        // /// <param name="camera"></param>
        // /// <param name="width"></param>
        // /// <param name="height"></param>
        // /// <returns></returns>
        // public static Texture2D Capture(this Camera camera, int width, int height)
        // {
        //     // RenderTexture activeRenderTexture = RenderTexture.active;
        //     // RenderTexture.active = camera.targetTexture;
        //     //
        //     // camera.Render();
        //     //
        //     // Texture2D image = new Texture2D(Camera.targetTexture.width, Camera.targetTexture.height);
        //     // image.ReadPixels(new Rect(0, 0, Camera.targetTexture.width, Camera.targetTexture.height), 0, 0);
        //     // image.Apply();
        //     // RenderTexture.active = activeRenderTexture;
        //
        //
        //     // Create renderTexture
        //     var renderTexture = new RenderTexture(width, height, 0);
        //     camera.targetTexture = renderTexture;
        //     camera.Render();
        //     RenderTexture.active = renderTexture;
        //     var texture = new Texture2D(camera.targetTexture.width, camera.targetTexture.height, TextureFormat.RGBA32,
        //         default);
        //     texture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        //     texture.Apply();
        //     // Clean
        //     camera.targetTexture = null;
        //     RenderTexture.active = null;
        //     return texture;
        // }

        // private static RenderTexture _renderTexture;
        // private static Texture2D _screenShot;
        // private static Camera _camera;
        // private static int _width;
        // private static int _height;

        // public static void PrepareCaptureFrequently(this Camera camera, int width, int height)
        // {
        //     _camera = camera;
        //     _width = width;
        //     _height = height;
        //     _renderTexture = new RenderTexture(width, height, 0);
        //     _screenShot = new Texture2D(width, height, TextureFormat.RGBA32, default);
        //     camera.targetTexture = _renderTexture;
        //     RenderTexture.active = camera.targetTexture;
        // }
        //
        // public static Texture2D CaptureFrequently()
        // {
        //     // _renderTexture.
        //     // _camera.Render();
        //     // _texture2D ;
        //     _screenShot.ReadPixels(new Rect(0, 0, _width, _height), 0, 0);
        //     _screenShot.Apply();
        //     return _screenShot;
        // }
        //
        // public static void EndCaptureFrequently()
        // {
        //     _camera.targetTexture = null;
        //     RenderTexture.active = null;
        //     _camera = null;
        //     _width = 0;
        //     _height = 0;
        // }

        /// <summary>
        /// 相机截图,100000x100000像素
        /// </summary>
        public static Texture2D CaptureBig(this Camera camera)
        {
            // 创建渲染器纹理
            var renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
            camera.targetTexture = renderTexture;

            // 渲染
            camera.Render();

            // 激活
            RenderTexture.active = renderTexture;

            // 创建纹理
            var texture = new Texture2D(16384, 16384, TextureFormat.RGBA32, false);

            // 读取
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
            texture.Apply();

            // 清理
            camera.targetTexture = null;
            RenderTexture.active = null;
            Object.DestroyImmediate(renderTexture);
            return texture;
        }

        /// <summary>
        /// 将透视相机移动到从指定方向观察物体时的最佳位置，并面向物体
        /// 这是基于Bounds的粗略版，下方有基于Mesh的精确版
        /// </summary>
        public static void ObsPerspective(Camera cam, GameObject watched, Vector3 camDir, Vector2 offset)
        {
            // 获取要被照相的物体的界限
            Bounds bounds = watched.GetRendererBounds();

            // 设置相机的位置以及距离
            var frustum = bounds.GetRadius();
            var distance = GetDistanceByFrustum(cam, frustum);

            // 位置
            cam.transform.position = bounds.center + camDir.normalized * distance;

            // 旋转
            cam.transform.LookAt(bounds.center);
            // 偏移的问题还没有解决
        }

        /// <summary>
        /// 将正交相机移动到从指定方向观察物体时的最佳位置，并面向物体，名称过滤器
        /// </summary>
        public static void ObsOrthographic(this Camera cam, GameObject watched, Vector3 camDir, float scale,
            List<string> nameFilter)
        {
            // 设置为正交相机
            cam.orthographic = true;

            // 获取要被照相的物体的尺寸
            Bounds bounds = watched.GetRendererBoundsWithNameFilter(nameFilter);

            // 相机的位置设置为,照相的方向(参)加上一个固定距离(魔法值)
            cam.transform.position = bounds.center + camDir.normalized * 30f; // 与被照相的物体保持30米的距离,如果目标过大,这里就会出问题

            // 面朝相机
            cam.transform.LookAt(bounds.center);

            // 相机的正交尺寸等于目标尺寸除以目标要缩放的比例
            cam.orthographicSize = bounds.GetRadius() / scale;
        }

        /// <summary>
        /// 将正交相机移动到从指定方向观察物体的最佳位置
        /// </summary>
        public static void Watch(this Camera cam, GameObject watched, Vector3 camDir, float scale,
            List<string> nameFilter, bool shrink = false)
        {
            // Debug.Log(watched.name);
            // 设置为正交相机
            cam.orthographic = true;

            // 获取bounds
            var bounds = watched.GetRendererBounds();

            // 设置相机角度
            cam.transform.position = bounds.center + camDir.normalized * 30f;

            // 相机对着物体
            cam.transform.LookAt(bounds.center);

            // 计算每个顶点在屏幕上的视口坐标
            var vertexPoss = new List<Vector3>();

            var meshFilters = watched.GetComponentsInChildren<MeshFilter>();
            if (meshFilters != null && meshFilters.Length != 0)
            {
                foreach (var meshFilter in meshFilters)
                {
                    if (nameFilter == null || !nameFilter.Contains(meshFilter.gameObject.name))
                    {
                        var localToWorld = meshFilter.transform.localToWorldMatrix;
                        for (int i = 0; i < meshFilter.sharedMesh.vertexCount; i++)
                        {
                            vertexPoss.Add(localToWorld.MultiplyPoint3x4(meshFilter.sharedMesh.vertices[i]));
                        }
                    }
                    else
                    {
                        Debug.Log("OK");
                    }
                }
            }

            var skinnedMeshRenderers = watched.GetComponentsInChildren<SkinnedMeshRenderer>();
            if (skinnedMeshRenderers != null && skinnedMeshRenderers.Length != 0)
            {
                foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
                {
                    var localToWorld = skinnedMeshRenderer.transform.localToWorldMatrix;
                    for (int i = 0; i < skinnedMeshRenderer.sharedMesh.vertexCount; i++)
                    {
                        vertexPoss.Add(localToWorld.MultiplyPoint3x4(skinnedMeshRenderer.sharedMesh.vertices[i]));
                    }
                }
            }

            var viewportPoss = new List<Vector2>();
            foreach (var vertexPos in vertexPoss)
            {
                var viewportPos = CoordinateSystemConversionUtils.WorldToViewportPos(cam, vertexPos);
                // // Debug.Log(viewportPos);
                // // 似乎有一些离谱的顶点存在,模型不干净导致的,这些顶点不会参与形成物体的体积与面,但在统计时确实是一个顶点
                // if (viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1)
                // {
                //     viewportPoss.Add(viewportPos);
                // }
                viewportPoss.Add(viewportPos);
            }

            if (viewportPoss.Count == 0)
            {
                Debug.Log("???");
            }

            var left = viewportPoss[0].x;
            var right = viewportPoss[0].x;
            var up = viewportPoss[0].y;
            var down = viewportPoss[0].y;
            for (int i = 1; i < viewportPoss.Count; i++)
            {
                if (viewportPoss[i].x < left)
                {
                    left = viewportPoss[i].x;
                }

                if (viewportPoss[i].x > right)
                {
                    right = viewportPoss[i].x;
                }

                if (viewportPoss[i].y > up)
                {
                    up = viewportPoss[i].y;
                }

                if (viewportPoss[i].y < down)
                {
                    down = viewportPoss[i].y;
                }
            }

// for (int i = 1; i < viewportPoss.Count; i++)
// {
//     if (viewportPoss[i].x > right)
//     {
//         right = viewportPoss[i].x;
//     }
// }
// for (int i = 1; i < viewportPoss.Count; i++)
// {
//     if (viewportPoss[i].y > up)
//     {
//         up = viewportPoss[i].y;
//     }
// }
// for (int i = 1; i < viewportPoss.Count; i++)
// {
//     if (viewportPoss[i].y < down)
//     {
//         down = viewportPoss[i].y;
//     }
// }
// 计算物体中心对应的视口坐标
            var centerViewportPosX = (left + right) / 2f;
            var centerViewportPosY = (up + down) / 2f;
// 相机再移动固定距离即可
// 先计算相机高度世界单位
            var camViewHeight = cam.orthographicSize * 2f;
            var camViewWidth = camViewHeight / 9f * 16f;
            var moveX = camViewWidth * (centerViewportPosX - 0.5f);
            var moveY = camViewHeight * (centerViewportPosY - 0.5f);
            cam.transform.position += cam.transform.right.normalized * moveX;
            cam.transform.position += cam.transform.up.normalized * moveY;
// Debug.Log(watched.name);
// 视觉高度适配
// 匹配视觉高度
            var h = (up - down);
            var w = right - left;
            if (shrink)
            {
                cam.orthographicSize *= Math.Max(h, w) * scale;
            }
            else
            {
                cam.orthographicSize *= h * scale;
            }
        }

        /// <summary>
        /// 正交相机锁定旋转并聚焦物体
        /// </summary>
        /// <param name="cam"></param>
        /// <param name="watched"></param>
        /// <param name="cameraRotation"></param>
        /// <param name="scale"></param>
        /// <param name="nameFilter"></param>
        /// <param name="shrink"></param>
        public static void LockRotationFocusOrthographic(this Camera cam, GameObject watched, float scale,
            List<string> nameFilter, bool shrink = false)
        {
            // Vector3 camDir = Vector3.back;
            // Debug.Log(watched.name);
            // 设置为透视相机
            // cam.orthographic = false;
            // 获取bounds
            var bounds = watched.GetRendererBounds();
            // 设置相机角度
            cam.transform.position = bounds.center + -1f * cam.transform.forward.normalized * 30f;
            // 相机对着物体
            cam.transform.LookAt(bounds.center);
            // 计算每个顶点在屏幕上的视口坐标
            var vertexPoss = new List<Vector3>();
            var meshFilters = watched.GetComponentsInChildren<MeshFilter>();
            if (meshFilters != null && meshFilters.Length != 0)
            {
                foreach (var meshFilter in meshFilters)
                {
                    if (nameFilter == null || !nameFilter.Contains(meshFilter.gameObject.name))
                    {
                        var localToWorld = meshFilter.transform.localToWorldMatrix;
                        for (int i = 0; i < meshFilter.sharedMesh.vertexCount; i++)
                        {
                            vertexPoss.Add(localToWorld.MultiplyPoint3x4(meshFilter.sharedMesh.vertices[i]));
                        }
                    }
                    else
                    {
                        Debug.Log("OK");
                    }
                }
            }

            var skinnedMeshRenderers = watched.GetComponentsInChildren<SkinnedMeshRenderer>();
            if (skinnedMeshRenderers != null && skinnedMeshRenderers.Length != 0)
            {
                foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
                {
                    var localToWorld = skinnedMeshRenderer.transform.localToWorldMatrix;
                    for (int i = 0; i < skinnedMeshRenderer.sharedMesh.vertexCount; i++)
                    {
                        vertexPoss.Add(localToWorld.MultiplyPoint3x4(skinnedMeshRenderer.sharedMesh.vertices[i]));
                    }
                }
            }

            var viewportPoss = new List<Vector2>();
            foreach (var vertexPos in vertexPoss)
            {
                var viewportPos = CoordinateSystemConversionUtils.WorldToViewportPos(cam, vertexPos);
                // // Debug.Log(viewportPos);
                // // 似乎有一些离谱的顶点存在,模型不干净导致的,这些顶点不会参与形成物体的体积与面,但在统计时确实是一个顶点
                // if (viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1)
                // {
                //     viewportPoss.Add(viewportPos);
                // }
                viewportPoss.Add(viewportPos);
            }

            if (viewportPoss.Count == 0)
            {
                Debug.Log("???");
            }

            var left = viewportPoss[0].x;
            var right = viewportPoss[0].x;
            var up = viewportPoss[0].y;
            var down = viewportPoss[0].y;
            for (int i = 1; i < viewportPoss.Count; i++)
            {
                if (viewportPoss[i].x < left)
                {
                    left = viewportPoss[i].x;
                }

                if (viewportPoss[i].x > right)
                {
                    right = viewportPoss[i].x;
                }

                if (viewportPoss[i].y > up)
                {
                    up = viewportPoss[i].y;
                }

                if (viewportPoss[i].y < down)
                {
                    down = viewportPoss[i].y;
                }
            }

// for (int i = 1; i < viewportPoss.Count; i++)
// {
//     if (viewportPoss[i].x > right)
//     {
//         right = viewportPoss[i].x;
//     }
// }
// for (int i = 1; i < viewportPoss.Count; i++)
// {
//     if (viewportPoss[i].y > up)
//     {
//         up = viewportPoss[i].y;
//     }
// }
// for (int i = 1; i < viewportPoss.Count; i++)
// {
//     if (viewportPoss[i].y < down)
//     {
//         down = viewportPoss[i].y;
//     }
// }
// 计算物体中心对应的视口坐标
            var centerViewportPosX = (left + right) / 2f;
            var centerViewportPosY = (up + down) / 2f;
// 相机再移动固定距离即可
// 先计算相机高度世界单位
            var camViewHeight = cam.orthographicSize * 2f;
            var camViewWidth = camViewHeight / 9f * 16f;
            var moveX = camViewWidth * (centerViewportPosX - 0.5f);
            var moveY = camViewHeight * (centerViewportPosY - 0.5f);
            cam.transform.position += cam.transform.right.normalized * moveX;
            cam.transform.position += cam.transform.up.normalized * moveY;
// Debug.Log(watched.name);
// 视觉高度适配
// 匹配视觉高度
            var h = (up - down);
            var w = right - left;
            if (shrink)
            {
                cam.orthographicSize *= Math.Max(h, w) * scale;
            }
            else
            {
                cam.orthographicSize *= h * scale;
            }
        }

        /// <summary>
        /// 锁定旋转并聚焦物体
        /// </summary>
        /// <param name="cam"></param>
        /// <param name="watched"></param>
        /// <param name="distanceMultiplier"></param>
        public static void LockRotationFocus(this Camera cam, GameObject watched, float distanceMultiplier)
        {
            if (cam.orthographic)
            {
                Debug.Log("Orthographic");
                cam.LockRotationFocusOrthographic(watched, 1, null, false);
            }
            else
            {
                Debug.Log("Perspective");
                cam.LockRotationFocusPerspective(watched.GetRendererBounds(), cam.transform.forward, 1);
            }
        }

        /// <summary>
        /// 透视相机锁定旋转并聚焦物体
        /// </summary>
        /// <param name="cam"></param>
        /// <param name="bounds"></param>
        /// <param name="camForward"></param>
        /// <param name="distanceMultiplier"></param>
        public static void LockRotationFocusPerspective(this Camera cam, Bounds bounds, Vector3 camForward,
            float distanceMultiplier)
        {
            var center = bounds.center;
            var frustum = bounds.GetRadius();
            var distance = cam.GetDistanceByFrustum(frustum);
            var bestPos = center + (-camForward).normalized * distance * distanceMultiplier;
            cam.transform.position = bestPos;
        }

        /// <summary>
        /// 计算透视相机，视角大小不变，观察指定高度物体，应当保持的距离
        /// </summary>
        public static float GetDistanceByFrustum(this Camera cam, float frustum)
        {
            return (float) (frustum / Math.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad));
        }

        /// <summary>
        /// 计算透视相机，视角大小不变，观察指定距离时，所能看到的高度
        /// </summary>
        public static float GetFrustumByDistance(this Camera cam, float distance)
        {
            return (float) (distance * Math.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad));
        }
    }
}