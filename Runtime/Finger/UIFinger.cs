using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace Plugins.Puke.Finger
{
    public sealed class UIFinger : RichMono.RichMono, IPointerDownHandler
    {
        public List<IFinger> Fingers { get; } = new List<IFinger>();

        /// <summary>
        /// 该函数执行顺序由EventSystem的执行顺序决定,默认为-1000,远早于Update
        /// 这是目前判断点击在UI上的唯一方式,其他方式难以识别UI遮挡以及部分遮挡的情况
        /// </summary>
        public void OnPointerDown(PointerEventData eventData)
        {
#if UNITY_EDITOR
            Fingers.Add(new EditorFinger());
#else
            fingers.Add(new Finger(eventData.pointerId));
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