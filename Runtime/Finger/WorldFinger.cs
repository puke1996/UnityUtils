using System.Collections.Generic;
using UnityEngine;

namespace Finger
{
    public sealed class WorldFinger : RichMono.RichMono
    {
        public List<IFinger> Fingers { get; } = new List<IFinger>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void CreateComponents()
        {
            var gameObject = new GameObject(nameof(WorldFinger));
            var worldFinger = gameObject.AddComponent<WorldFinger>();
            DontDestroyOnLoad(worldFinger);
        }

        public override void EarlyUpdate()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && Common.TouchOnUI(Input.mousePosition))
            {
                Fingers.Add(new EditorFinger());
            }
#else
            for (var i = 0; i < UnityEngine.Input.touchCount; i++)
            {
                var touch = UnityEngine.Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began && Common.TouchOnUI(Input.mousePosition))
                {
                    fingers.Add(new Finger(touch.fingerId));
                }
            }
#endif
        }

        public override void FinallyUpdate()
        {
            for (var i = Fingers.Count - 1; i >= 0; i--)
            {
                if (Fingers[i].TouchEnd)
                {
                    Fingers.Remove(Fingers[i]);
                }
            }
        }
    }
}