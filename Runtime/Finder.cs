using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Finder的性能消耗较高，仅适用于敏捷开发
/// </summary>
public static class Finder
{
    /// <summary>
    /// 查找一个组件（可选包括非激活）
    /// </summary>
    public static T FindAnObject<T>(bool includeInactive = false) where T : UnityEngine.Object
    {
        IList<T> temp = UnityEngine.Object.FindObjectsOfType<T>(includeInactive);
        return temp.Count > 0 ? temp[0] : null;
    }

    /// <summary>
    /// 查找多个组件（可选包括非激活）
    /// </summary>
    public static IList<T> FindAllObjects<T>(bool includeInactive = false) where T : UnityEngine.Object
    {
        return UnityEngine.Object.FindObjectsOfType<T>(includeInactive);
    }

    /// <summary>
    /// 查找一个接口（可选包括非激活）
    /// 转换回T类型有点麻烦，甚至要求被转换的对象实现接口，所以返回Object
    /// </summary>
    public static object FindAnInterface<T>(bool includeInactive = false)
    {
        var allObject = FindAllObjects<MonoBehaviour>(includeInactive);
        foreach (var monoBehaviour in allObject)
        {
            if (monoBehaviour is T)
            {
                return monoBehaviour;
            }
            // // return (T) monoBehaviour;
            // // return (monoBehaviour as typeof(T));
            // return (T) Convert.ChangeType(monoBehaviour, typeof(T));
            // // return (T) Convert.ChangeType(monoBehaviour, typeof(T));
        }

        return default(T);
    }

    /// <summary>
    /// 查找多个接口（可选包括非激活）
    /// </summary>
    public static List<T> FindAllInterfaces<T>(bool includeInactive = false)
    {
        var allMono = FindAllObjects<MonoBehaviour>(includeInactive);
        var allInterface = new List<MonoBehaviour>();
        foreach (var mono in allMono)
        {
            if (mono is T)
            {
                allInterface.Add(mono);
            }
        }

        return allInterface.Cast<T>().ToList();
    }

    /// <summary>
    /// 查找一个名字（仅激活）
    /// </summary>
    public static GameObject FindActiveObjByName(string name)
    {
        return GameObject.Find(name);
    }

    // public static Transform FindChild(this Transform transform, string childName)
    // {
    //     return transform.Find(childName);
    // }
    /// <summary>
    /// 广度优先
    /// </summary>
    public static Transform FindDeepChild(this Transform root, string name)
    {
        var queue = new Queue<Transform>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var first = queue.Dequeue();
            if (first.name == name)
                return first;
            foreach (Transform transform in first)
                queue.Enqueue(transform);
        }

        return null;
    }

    /// <summary>
    /// 获取场景中的所有游戏对象
    /// </summary>
    public static List<GameObject> FindAllObjectsInScene()
    {
        UnityEngine.SceneManagement.Scene activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        GameObject[] rootObjects = activeScene.GetRootGameObjects();
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        List<GameObject> objectsInScene = new List<GameObject>();
        for (int i = 0; i < rootObjects.Length; i++)
        {
            objectsInScene.Add(rootObjects[i]);
        }

        for (int i = 0; i < allObjects.Length; i++)
        {
            if (allObjects[i].transform.root)
            {
                for (int i2 = 0; i2 < rootObjects.Length; i2++)
                {
                    if (allObjects[i].transform.root == rootObjects[i2].transform &&
                        allObjects[i] != rootObjects[i2])
                    {
                        objectsInScene.Add(allObjects[i]);
                        break;
                    }
                }
            }
        }

        return objectsInScene;
    }

    /// <summary>
    /// 查找一个名字（所有）
    /// </summary>
    public static GameObject FindGOByName(string name)
    {
        List<GameObject> gameObjects = FindAllObjectsInScene();
        foreach (var gameObj in gameObjects)
        {
            if (gameObj.name.Equals(name))
            {
                return gameObj;
            }
        }

        return null;
    }

    /// <summary>
    /// 查找挂载在名字为name的节点上的T类型组件
    /// </summary>
    public static T Find<T>(this Transform transform, string name)
    {
        return transform.FindDeepChild(name).GetComponent<T>();
    }
}