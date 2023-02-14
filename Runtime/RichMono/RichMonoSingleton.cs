using System;

namespace RichMono
{
    public abstract class RichMonoSingleton<T> : RichMono where T : RichMonoSingleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = UnityEngine.Object.FindObjectsOfType<T>(false)[0];
                    if (_instance == null)
                    {
                        throw new Exception(typeof(T).Name);
                    }
                }

                return _instance;
            }
        }
    }
}