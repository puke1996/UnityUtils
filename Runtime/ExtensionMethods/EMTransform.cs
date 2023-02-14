using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ExtensionMethods
{
    /// <summary>
    /// WorldSpace 世界坐标系
    /// LocalSpace 局部坐标系
    /// SelfSpace 自身坐标系
    /// </summary>
    public static class EMTransform
    {
        // 四元数赋值
        // transform.rotation = Quaternion.Euler(0, 0, 0);
        // 欧拉角赋值
        // transform.eulerAngles = new Vector3(0, 0, 0);
        // 四元数具有欧拉角这个属性
        // Quaternion quaternion = new Quaternion();
        // quaternion.eulerAngles

        /// <summary>
        /// 在自身坐标系中移动
        /// </summary>
        public static void MoveInSelfSpace(this Transform self, Vector3 delta)
        {
            self.position = self.TransformPoint(delta);
        }

        /// <summary>
        /// 移动到相对目标相同方向（世界空间）指定距离的位置
        /// </summary>
        public static void KeepDistFormTarget(this Transform self, Transform target, float distance)
        {
            var position = target.position;
            self.position = position + (self.position - position).normalized * distance;
        }

        /// <summary>
        /// 面向目标并保持自身上方向不变
        /// </summary>
        public static void KeepUpLookAt(this Transform self, Vector3 v3)
        {
            self.LookAt(v3, self.up);
        }

        /// <summary>
        /// 平移物体以使得bounds.min=000
        /// </summary>
        public static void SetBoundsMin000(this Transform transform)
        {
            var rendererList = transform.GetComponentsInChildren<Renderer>();
            var boundsList = rendererList.Select(renderer => renderer.bounds).ToList();
            var newBounds = boundsList.Merge();
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(transform, "位移");
#endif
            transform.position -= newBounds.min;
        }

        /// <summary>
        /// 平移物体以使得bounds.center=000
        /// </summary>
        public static void SetBoundsCenter000(this Transform transform)
        {
            IList<Renderer> rendererList = transform.GetComponentsInChildren<Renderer>();
            IList<Bounds> boundsList = rendererList.Select(renderer => renderer.bounds).ToList();
            Bounds newBounds = boundsList.Merge();
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(transform, "位移");
#endif
            transform.position -= newBounds.center;
        }

        /// <summary>
        /// 设置世界空间缩放
        /// </summary>
        public static void SetWorldScale(this Transform transform, Vector3 worldScale)
        {
            transform.localScale = Vector3.one;
            var lossyScale = transform.lossyScale;
            transform.localScale = new Vector3(worldScale.x / lossyScale.x,
                worldScale.y / lossyScale.y, worldScale.z / lossyScale.z);
        }

        /// <summary>
        /// 缩放以使得直径等于指定直径
        /// </summary>
        public static void ScaleDiameter(this Transform transform, float diameter)
        {
            var bounds = transform.gameObject.GetRendererBounds();
            transform.localScale *= (diameter / (bounds.GetDiameter()));
        }

        /// <summary>
        /// 缩放以使得高度等于指定高度
        /// </summary>
        public static void ScaleToHeight(this Transform transform, float height)
        {
            var bounds = transform.gameObject.GetRendererBounds();
            var boundsHeight = (bounds.max.y - bounds.min.y);
            transform.localScale *= (height / boundsHeight);
        }

        /// <summary>
        /// 拷贝局部坐标
        /// </summary>
        public static void CopyTransform(Transform transform, Transform toTarget)
        {
            toTarget.localPosition = transform.localPosition;
            toTarget.localScale = transform.localScale;
            toTarget.localRotation = transform.localRotation;
        }

        public delegate void TransformHandle(Transform transform);

        // /// <summary>
        // /// 对自己包括所有子节点执行一个操作
        // /// </summary>
        // public static void 递归(this Transform transform, TransformHandle transformHandle)
        // {
        //     transformHandle(transform);
        //     递归internal(transform, transformHandle);
        // }
        //
        // private static void 递归internal(Transform transform, TransformHandle transformHandle)
        // {
        //     // 遍历所有子节点
        //     for (int i = 0; i < transform.childCount; i++)
        //     {
        //         // 对每一个子节点进行处理
        //         transformHandle(transform.GetChild(i));
        //         // 然后对每一个子节点执行这个函数自己
        //         递归internal(transform.GetChild(i), transformHandle);
        //     }
        // }
        /// <summary>
        /// 平移以使得底部坐标等于指定坐标
        /// 注意,设置底部坐标再获取底部坐标,两次坐标并不完全一致,有浮点偏差
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="position"></param>
        public static void SetBottomPos(this Transform transform, Vector3 position)
        {
            Physics.SyncTransforms();
            var bounds = transform.gameObject.GetRendererBounds();
            var currentBottomPos = new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);
            var currentPosition = transform.position;
            var targetBottomPos = position;
            var targetPosition = currentPosition - currentBottomPos + targetBottomPos;
            transform.position = targetPosition;
        }

        /// <summary>
        /// 获取底部坐标
        /// 注意,设置底部坐标再获取底部坐标,两次坐标并不完全一致,有浮点偏差
        /// </summary>
        public static Vector3 GetBottomPos(this Transform transform)
        {
            Physics.SyncTransforms();
            var bounds = transform.gameObject.GetRendererBounds();
            return new Vector3(bounds.center.x, bounds.min.y, bounds.center.z); // 设置模型坐标
            // Debug.Log(VAR);
        }

        /// <summary>
        /// 设置物体的中心坐标
        /// </summary>
        public static void SetCenterPos(this Transform transform, Vector3 pos)
        {
            Bounds bounds = transform.gameObject.GetRendererBounds();
            transform.position = pos + transform.position - bounds.center;
        }

        /// <summary>
        /// 递归对自身以及所有子节点进行操作
        /// </summary>
        public static void Recursion(this Transform transform, TransformHandle transformHandle)
        {
            transformHandle(transform);
            for (int i = 0; i < transform.childCount; i++)
            {
                Recursion(transform.GetChild(i), transformHandle);
            }
        }

        /// <summary>
        /// 缓慢地朝向
        /// </summary>
        public static void LooktAtSlowly(this Transform transform, Transform target,
            float rotateSpeed)
        {
            var quaternion = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, rotateSpeed * Time.deltaTime);
        }

        public static void SmoothLookAt(this Transform transform, Transform target, float speed,
            bool local = false)
        {
            var lookDirection = target.position - transform.position;
            lookDirection.Normalize();
            if (local)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation,
                    Quaternion.LookRotation(lookDirection),
                    speed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection),
                    speed * Time.deltaTime);
            }
        }

        public static void SmoothLookAt(this Transform transform, Vector3 targetPos,
            float speed,
            bool local = false)
        {
            var lookDirection = targetPos - transform.position;
            lookDirection.Normalize();
            if (local)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation,
                    Quaternion.LookRotation(lookDirection),
                    speed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection),
                    speed * Time.deltaTime);
            }
        }

        /// <summary>
        /// 平滑看向方向
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="direction"></param>
        /// <param name="speed"></param>
        /// <param name="up"></param>
        /// <param name="local"></param>
        public static void SmoothLookDirection(this Transform transform, Vector3 direction,
            float speed,
            Vector3 up, bool local = false)
        {
            Debug.Log("SmoothLookDirection" + direction);
            direction.Normalize();
            if (local)
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation,
                    Quaternion.LookRotation(direction, up),
                    speed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(direction, up),
                    speed * Time.deltaTime);
            }
        }

        /// <summary>
        /// 缓慢地朝向
        /// </summary>
        public static void LooktAtSlowly(this Transform transform, Vector3 forward,
            float rotateSpeed)
        {
            var quaternion = Quaternion.LookRotation(forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, rotateSpeed * Time.deltaTime);
        }

        /// <summary>
        /// 递归激活光线投射目标
        /// </summary>
        public static void EnableRaycastTargetRecursively(this Transform transform, bool enable)
        {
            var images = transform.GetComponentsInChildren<Image>();
            foreach (var image in images)
            {
                image.raycastTarget = enable;
            }
        }

        /// <summary>
        /// 局部坐标备份
        /// </summary>
        public struct LocalTransformBak
        {
            public Vector3 localPos;
            public Vector3 localRotation;
            public Vector3 localScale;

            public LocalTransformBak(Vector3 localPos, Vector3 localRotation, Vector3 localScale)
            {
                this.localPos = localPos;
                this.localRotation = localRotation;
                this.localScale = localScale;
            }

            public LocalTransformBak(Transform transform)
            {
                localPos = transform.localPosition;
                localRotation = transform.localRotation.eulerAngles;
                localScale = transform.localScale;
            }
        }

        /// <summary>
        /// 备份局部坐标
        /// </summary>
        public static LocalTransformBak Bak(this Transform transform)
        {
            return new LocalTransformBak(transform);
        }

        /// <summary>
        /// 恢复局部坐标
        /// </summary>
        public static void Restore(this Transform transform, LocalTransformBak localTransformBak)
        {
            transform.localPosition = localTransformBak.localPos;
            transform.localRotation = Quaternion.Euler(localTransformBak.localRotation);
            transform.localScale = localTransformBak.localScale;
        }

        /// <summary>
        /// 重置局部坐标
        /// </summary>
        public static void Reset(this Transform transform)
        {
            transform.localPosition = default;
            transform.localRotation = default;
            transform.localScale = Vector3.one;
        }

        /// <summary>
        /// 删除所有子节点
        /// </summary>
        public static void DestroyAllChildren(this Transform transform)
        {
            var childCount = transform.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                var go = transform.GetChild(i).gameObject;
                Object.Destroy(go);
            }
        }

        /// <summary>
        /// 删除所有子节点
        /// </summary>
        public static void DestroyAllChildrenImmediately(this Transform transform)
        {
            var childCount = transform.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                var go = transform.GetChild(i).gameObject;
                Object.DestroyImmediate(go);
            }
        }
        // public static void GetIndex(this Transform transform)
        // {
        //     transform.parent.getin
        // }
    }
}