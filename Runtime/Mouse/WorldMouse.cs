using Plugins.Puke.RichMono;
using UnityEngine;

namespace Plugins.Puke.Mouse
{
    public sealed class WorldMouse : RichMonoSingleton<WorldMouse>
    {
        // /// <summary>
        // /// 白名单,名字匹配,名字一致则忽略
        // /// </summary>
        // public List<GameObject> whitelist = new List<GameObject>();

        public bool LeftMouseButtonDown { get; private set; }
        public bool LeftMouseButtonMove { get; private set; }
        public bool LeftMouseButtonUp { get; private set; }
        public bool RightMouseButtonDown { get; private set; }
        public bool RightMouseButtonMove { get; private set; }
        public bool RightMouseButtonUp { get; private set; }
        public bool MiddleMouseButtonDown { get; private set; }
        public bool MiddleMouseButtonMove { get; private set; }

        public bool MiddleMouseButtonUp { get; private set; }
        public bool TouchedOnGizmo { get; set; }

        // 滚轮旋转量
        public float MouseScrollWheel
        {
            get
            {
                if (Common.TouchOnUI(Input.mousePosition))
                {
                    return Input.GetAxis("Mouse ScrollWheel");
                }

                return 0;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void CreateComponents()
        {
            var gameObject = new GameObject(nameof(WorldMouse));
            gameObject.transform.localPosition = Vector3.zero;
            var worldMouse = gameObject.AddComponent<WorldMouse>();
            DontDestroyOnLoad(worldMouse);
        }

        /// <summary>
        /// 帧初初始化字段
        /// </summary>
        public override void EarlyUpdate()
        {
            if (Input.GetMouseButtonDown(0) &&
                Common.TouchOnUI(Input.mousePosition) && !TouchedOnGizmo)
            {
                LeftMouseButtonDown = true;
            }

            if (Input.GetMouseButtonDown(1) &&
                Common.TouchOnUI(Input.mousePosition) && !TouchedOnGizmo)
            {
                RightMouseButtonDown = true;
            }

            if (Input.GetMouseButtonDown(2) &&
                Common.TouchOnUI(Input.mousePosition) && !TouchedOnGizmo)
            {
                MiddleMouseButtonDown = true;
            }

            if (LeftMouseButtonMove && Input.GetMouseButtonUp(0))
            {
                LeftMouseButtonUp = true;
            }

            if (RightMouseButtonMove && Input.GetMouseButtonUp(1))
            {
                RightMouseButtonUp = true;
            }

            if (MiddleMouseButtonMove && Input.GetMouseButtonUp(2))
            {
                MiddleMouseButtonUp = true;
            }
        }

        /// <summary>
        /// 帧末状态过渡
        /// </summary>
        public override void FinallyUpdate()
        {
            if (LeftMouseButtonDown)
            {
                LeftMouseButtonDown = false;
                LeftMouseButtonMove = true;
            }

            if (MiddleMouseButtonDown)
            {
                MiddleMouseButtonDown = false;
                MiddleMouseButtonMove = true;
            }

            if (RightMouseButtonDown)
            {
                RightMouseButtonDown = false;
                RightMouseButtonMove = true;
            }

            if (LeftMouseButtonUp)
            {
                LeftMouseButtonUp = false;
                LeftMouseButtonMove = false;
            }

            if (MiddleMouseButtonUp)
            {
                MiddleMouseButtonUp = false;
                MiddleMouseButtonMove = false;
            }

            if (RightMouseButtonUp)
            {
                RightMouseButtonUp = false;
                RightMouseButtonMove = false;
            }

            if (TouchedOnGizmo)
            {
                TouchedOnGizmo = false;
            }
        }
    }
}