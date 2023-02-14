using UnityEngine;

namespace RichMono
{
    public abstract class LazyRichMonoSingleton<T> : RichMono where T : LazyRichMonoSingleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var gameObject = new GameObject(typeof(T).Name);
                    var thisComponent = gameObject.AddComponent<T>();
                    _instance = thisComponent;
                }

                return _instance;
            }
        }
    }
}