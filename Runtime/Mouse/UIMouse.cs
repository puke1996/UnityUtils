using UnityEngine.EventSystems;

namespace Plugins.Puke.Mouse
{
    /// <summary>
    /// 确保EventSystem的执行顺序
    /// 不要使用OnPointerUp，因为事件执行太早
    /// </summary>
    public sealed class UIMouse : RichMono.RichMono, IPointerDownHandler
    {
        public bool LeftMouseButtonDown { get; private set; }
        public bool LeftMouseButtonMove { get; private set; }
        public bool LeftMouseButtonUp { get; private set; }
        public bool MiddleMouseButtonDown { get; private set; }
        public bool MiddleMouseButtonMove { get; private set; }
        public bool MiddleMouseButtonUp { get; private set; }
        public bool RightMouseButtonDown { get; private set; }
        public bool RightMouseButtonMove { get; private set; }
        public bool RightMouseButtonUp { get; private set; }

        /// <summary>
        /// 这里也可以使用UnityEngine.Input.GetMouseButtonUp(0)加上判断是否点击到此UI
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.pointerId == -1)
            {
                LeftMouseButtonDown = true;
            }

            if (eventData.pointerId == -2)
            {
                RightMouseButtonDown = true;
            }

            if (eventData.pointerId == -3)
            {
                MiddleMouseButtonDown = true;
            }
        }

        public override void EarlyUpdate()
        {
            if (LeftMouseButtonMove && UnityEngine.Input.GetMouseButtonUp(0))
            {
                LeftMouseButtonUp = true;
            }

            if (RightMouseButtonMove && UnityEngine.Input.GetMouseButtonUp(1))
            {
                RightMouseButtonUp = true;
            }

            if (MiddleMouseButtonMove && UnityEngine.Input.GetMouseButtonUp(2))
            {
                MiddleMouseButtonUp = true;
            }
        }

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
                LeftMouseButtonMove = false;
                LeftMouseButtonUp = false;
            }

            if (MiddleMouseButtonUp)
            {
                MiddleMouseButtonMove = false;
                MiddleMouseButtonUp = false;
            }

            if (RightMouseButtonUp)
            {
                RightMouseButtonMove = false;
                RightMouseButtonUp = false;
            }
        }
    }
}