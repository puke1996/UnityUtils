using UnityEngine;

namespace Plugins.Puke.UnityUtilities.UnityUtils
{
    public class RaycastUtil : MonoBehaviour
    {
        public void Example()
        {
            // 发出定长的点射线,返回碰撞到的所有目标
            {
                Vector3 origin = default;
                Vector3 direction = default;
                float distance = default;
                var result = Physics.RaycastAll(origin, direction, distance);
            }
            // 检测敌人
            {
                Vector3 pos = default;
                float radius = default;
                int layerMask = default;
                Collider[] hitColliders = Physics.OverlapSphere(pos, radius, layerMask);
            }
        }

        /// <summary>
        /// 从屏幕发出定长的点射线,返回碰撞到的第一个目标
        /// </summary>
        public static (bool, RaycastHit) RaycastFromScreen(Camera camera, Vector3 screenPos,
            float distance = float.MaxValue) //从屏幕上的一个点发射射线到地面返回碰撞点的位置
        {
            Ray ray = camera.ScreenPointToRay(screenPos);
            var collide = Physics.Raycast(ray, out RaycastHit info, distance);
            return (collide, info);
        }

        /// <summary>
        /// 从屏幕发出定长的点射线,返回碰撞到的第一个目标
        /// </summary>
        public static (bool, RaycastHit) RaycastLayerMaskFromScreen(Camera camera, Vector3 screenPos,
            int layerMask, float distance = float.MaxValue) //从屏幕上的一个点发射射线到地面返回碰撞点的位置
        {
            Ray ray = camera.ScreenPointToRay(screenPos);
            var collide = Physics.Raycast(ray, out RaycastHit info, distance, layerMask);
            return (collide, info);
        }

        /// <summary>
        /// 从屏幕发出定长的点射线,返回碰撞到的所有目标
        /// </summary>
        public static RaycastHit[] RaycastAllFromScreen(Camera camera, Vector3 screenPos,
            float distance = float.MaxValue)
        {
            var ray = camera.ScreenPointToRay(screenPos);
            return Physics.RaycastAll(ray, distance);
        }

        /// <summary>
        /// 发出定长的点射线,返回碰撞到的第一个目标
        /// </summary>
        public static (bool, RaycastHit) Raycast(Vector3 origin, Vector3 direction, LayerMask layerMask,
            float distance = float.MaxValue)
        {
            var collide = Physics.Raycast(origin, direction, out var info, distance, layerMask);
            return (collide, info);
        }

        /// <summary>
        /// 向特定层发出定长的球形射线,返回碰撞到的第一个目标
        /// </summary>
        public static (bool, RaycastHit) SphereRaycast(Vector3 origin, float radius, Vector3 direction,
            LayerMask layerMask, float distance = float.MaxValue)
        {
            Ray ray = new Ray(origin, direction);
            bool collide = Physics.SphereCast(ray, radius, out RaycastHit info, distance, layerMask);
            return (collide, info);
        }

        /// <summary>
        /// 相机为主相机，输入设备为鼠标
        /// </summary>
        public static GameObject GetPointedGameObject()
        {
            var result = RaycastFromScreen(Camera.main, Input.mousePosition);
            if (result.Item1)
            {
                return result.Item2.collider.gameObject;
            }

            return null;
        }
    }
}