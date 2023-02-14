using RichMono;
using UnityEngine;

namespace Mouse
{
    public class LastMousePosition : RichMonoSingleton<LastMousePosition>
    {
        public static Vector3 LastMousePos { get; private set; }
        public static Vector3 DeltaPos { get; private set; }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void CreateComponents()
        {
            var gameObject = new GameObject(nameof(LastMousePosition));
            gameObject.transform.localPosition = Vector3.zero;
            var lastMousePosition = gameObject.AddComponent<LastMousePosition>();
            DontDestroyOnLoad(lastMousePosition);
        }

        /// <summary>
        /// 上帧末记录
        /// </summary>
        public override void FinallyUpdate()
        {
            LastMousePos = UnityEngine.Input.mousePosition;
        }

        /// <summary>
        /// 该帧初记录
        /// </summary>
        public override void EarlyUpdate()
        {
            base.EarlyUpdate();
            DeltaPos = UnityEngine.Input.mousePosition - LastMousePos;
        }
    }
}