using System;
using UnityEngine;

namespace Plugins.Puke.UnitySingleton
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var temp = FindObjectsOfType<T>(true);
                    if (temp.Length == 0)
                    {
                        throw new Exception(typeof(T).Name);
                    }

                    _instance = temp[0];
                }

                return _instance;
            }
        }
    }
}