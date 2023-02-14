using UnityEngine;

namespace Singleton
{
    public abstract class LazyMonoSingleton<T> : MonoBehaviour where T : LazyMonoSingleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var gameObject = new GameObject(typeof(T).Name);
                    _instance = gameObject.AddComponent<T>();
                }

                return _instance;
            }
        }
    }
}