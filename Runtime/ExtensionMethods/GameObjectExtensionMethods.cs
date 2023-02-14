using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using Object = UnityEngine.Object;

namespace ExtensionMethods
{
    public static class GameObjectExtensionMethods
    {
        /// <summary>
        /// Make a copy with only the renderer
        /// Warning! Activation and deactivation will be performed during the process
        /// </summary>
        /// <param name="go"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static GameObject InstantiateRenderer(this GameObject go, Transform parent)
        {
            go.SetActive(false);
            var renderOnly = Object.Instantiate(go, parent);
            var components = renderOnly.GetComponentsInChildren<Component>();
            for (int i = components.Length - 1; i >= 0; i--)
            {
                // 不是需要保留的组件,并且是行为组件,则摧毁
                if (!(components[i] is Transform || components[i] is MeshFilter || components[i] is Renderer))
                {
                    if (components[i] is Behaviour)
                    {
                        Object.DestroyImmediate(components[i]);
                    }
                }
            }

            // Reactive
            go.SetActive(true);
            renderOnly.SetActive(true);
            return renderOnly;
        }

        /// <summary>
        /// Disable physics for this go
        /// </summary>
        /// <param name="go"></param>
        public static void DisablePhysics(this GameObject go)
        {
            var colliders = go.GetComponentsInChildren<Collider>();

            foreach (Collider collider in colliders)
            {
                collider.isTrigger = true;
            }

            var rb = go.GetComponentInChildren<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = true;
            }
        }

        /// <summary>
        /// Enable physics for this go
        /// </summary>
        /// <param name="go"></param>
        public static void OpenPhysics(this GameObject go)
        {
            var colliders = go.GetComponentsInChildren<Collider>();

            foreach (Collider collider in colliders)
            {
                collider.isTrigger = false;
            }

            var rb = go.GetComponentInChildren<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = false;
            }
        }

        /// <summary>
        /// 判断是否是物理对象
        /// </summary>
        public static bool NonPhysicsObj(this GameObject gameObj)
        {
            var colliders = gameObj.GetComponentsInChildren<Collider>();
            if (gameObj.GetComponentInChildren<Rigidbody>().isKinematic == false)
            {
                return false;
            }

            foreach (Collider collider in colliders)
            {
                if (collider.isTrigger == false)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Set visible/invisible
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="value"></param>
        public static void SetVisible(this GameObject gameObject, bool value)
        {
            var renderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (var renderer in renderers)
            {
                renderer.enabled = value;
            }
        }

        /// <summary>
        /// Create MeshCollider
        /// </summary>
        /// <param name="gameObject"></param>
        public static void CreateMeshCollider(this GameObject gameObject)
        {
            IList<MeshRenderer> meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (var meshRenderer in meshRenderers)
            {
                var meshCollider = meshRenderer.GetComponent<MeshCollider>();
                if (meshCollider == null)
                {
                    meshRenderer.gameObject.AddComponent<MeshCollider>();
                }
                else
                {
                    Object.Destroy(meshCollider);
                    meshRenderer.gameObject.AddComponent<MeshCollider>();
                }
            }
        }

        /// <summary>
        /// Set convex/concave
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="convex"></param>
        public static void SetConvex(this GameObject gameObject, bool convex)
        {
            var colliders = gameObject.GetComponentsInChildren<MeshCollider>();
            foreach (MeshCollider collider in colliders)
            {
                collider.convex = convex;
            }
        }

        /// <summary>
        /// Destroy components by types
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="types"></param>
        public static void DestroyComponents(this GameObject gameObject, Type[] types)
        {
            foreach (var type in types)
            {
                var components = gameObject.GetComponentsInChildren<Component>();
                for (var i = components.Length - 1; i >= 0; i--)
                {
                    if (types.Any(eachType => eachType.IsInstanceOfType(components[i])))
                    {
                        Object.DestroyImmediate(components[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Destroy components by types
        /// </summary>
        /// <param name="gameObject"></param>
        /// <typeparam name="T"></typeparam>
        public static void DestroyComponents<T>(this GameObject gameObject)
        {
            var components = gameObject.GetComponentsInChildren(typeof(T));
            for (int i = components.Length - 1; i >= 0; i--)
            {
                if (components[i] is T)
                {
#if UNITY_EDITOR
                    Object.DestroyImmediate(components[i]);
#else
                        GameObject.Destroy(components[i]);
#endif
                    break;
                }
            }
        }

        /// <summary>
        /// 替换所有材质
        /// </summary>
        public static void ReplaceAllMaterials(this GameObject gameObj, Material oldMatl, Material newMatl)
        {
            var renderers = gameObj.GetComponentsInChildren<MeshRenderer>();
            foreach (var renderer in renderers)
            {
                if (renderer.sharedMaterial == oldMatl)
                {
                    renderer.sharedMaterial = newMatl;
                }
            }
        }

        /// <summary>
        /// 获取处于激活状态的渲染器的界限的合并
        /// </summary>
        public static Bounds GetRendererBounds(this GameObject gameObject)
        {
            var renderers = gameObject.GetComponentsInChildren<Renderer>();
            var boundsList = renderers.Select(renderer => renderer.bounds).ToList();
            var mergedBounds = boundsList.Merge();
            return mergedBounds;
        }

        /// <summary>
        /// 获取处于激活状态的渲染器的界限的合并，名称过滤器
        /// </summary>
        public static Bounds GetRendererBoundsWithNameFilter(this GameObject gameObject, List<String> names)
        {
            var renderers = gameObject.GetComponentsInChildren<Renderer>();
            // 把null的情况内部处理掉,外部使用起来更方便
            if (names != null && names.Count != 0)
            {
                renderers = renderers.Where(renderer => !names.Contains(renderer.name)).ToArray();
            }

            var boundsList = renderers.Select(renderer => renderer.bounds).ToList();
            var newBounds = boundsList.Merge();
            return newBounds;
        }

        /// <summary>
        /// 获取处于激活状态的网格界限的合并
        /// </summary>
        public static Bounds GetMeshFilterMergeBounds(this GameObject gameObject)
        {
            var meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();
            var boundsList = meshFilters.Select(meshFilter => meshFilter.sharedMesh.bounds).ToList();
            var newBounds = boundsList.Merge();
            return newBounds;
        }

        /// <summary>
        /// 清理
        /// 当将三维软件的场景直接导入到Unity作为模型使用时
        /// 这些模型通常还包含了灯光，相机等组件，并且位置偏离较远
        /// 使用此函数可以初步进行清理
        /// </summary>
        public static GameObject ProcessNativeModel(this GameObject gameObject, float size)
        {
            // 先缩放,再移动
            gameObject.transform.ScaleDiameter(size);
            gameObject.transform.SetBoundsCenter000();
            gameObject.DestroyComponents<Camera>();
            gameObject.DestroyComponents<LookAtConstraint>();
            gameObject.DestroyComponents<Light>();
            return gameObject;
        }

        /// <summary>
        /// Get center position
        /// </summary>
        /// <param name="gameObjects"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static Vector3 GetCenter(this List<GameObject> gameObjects)
        {
            if (gameObjects.Count == 0) throw new Exception();
            var sum = Vector3.zero;
            foreach (GameObject gameObj in gameObjects)
            {
                sum += gameObj.transform.position;
            }

            return sum / gameObjects.Count;
        }

        /// <summary>
        /// 计算物体的视觉中心
        /// </summary>
        public static Vector3 Center(this GameObject gameObject)
        {
            var rendererList = gameObject.GetComponentsInChildren<Renderer>();
            var boundsList = rendererList.Select(renderer => renderer.bounds).ToList();
            var newBounds = boundsList.Merge();
            return newBounds.center;
        }

        /// <summary>
        /// 替换指定游戏对象的所有材质
        /// </summary>
        public static void ReplaceAllMaterials(this GameObject gameObject, Material material)
        {
            var allRenderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (var renderer in allRenderers)
            {
                renderer.material = material;
            }
        }

        /// <summary>
        /// DestroyAll
        /// </summary>
        /// <param name="gameObjects"></param>
        public static void DestroyGameObjectList(this IList<GameObject> gameObjects)
        {
            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                Object.Destroy(gameObjects[i]);
            }
        }
    }
}