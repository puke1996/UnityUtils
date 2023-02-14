using System.Collections.Generic;
using Singleton;
using UnityEngine;

namespace RichMono
{
    [DefaultExecutionOrder(int.MinValue)]
    [RequireComponent(typeof(RichMonoSystemPartition2))]
    public class RichMonoSystem : MonoSingleton<RichMonoSystem>
    {
        private List<IRichMono> RichMonoList { get; } = new();
        internal List<IRichMono> richMonoListClone;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Create()
        {
            var gameObject = new GameObject(nameof(RichMonoSystem));
            var richMonoSystem = gameObject.AddComponent<RichMonoSystem>();
            DontDestroyOnLoad(richMonoSystem);
        }

        private void Update()
        {
            richMonoListClone = new List<IRichMono>(RichMonoList);
            for (var i = 0; i < richMonoListClone.Count; i++)
            {
                if (((MonoBehaviour) richMonoListClone[i]).isActiveAndEnabled)
                {
                    richMonoListClone[i].EarlyUpdate();
                }
            }
        }

        internal void AddRichMono(IRichMono richMono)
        {
            RichMonoList.Add(richMono);
        }

        internal void RemoveRichMono(IRichMono richMono)
        {
            RichMonoList.Remove(richMono);
        }
    }
}