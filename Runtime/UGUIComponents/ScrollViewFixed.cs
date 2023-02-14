using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UGUIComponents
{
    /// <summary>
    /// 使滚动视图可配置关闭拖拽功能
    /// 如果要对滚动视图内部的UI进行拖拽操作,依然需要关闭滚动视图组件
    /// </summary>
    public class ScrollViewFixed : ScrollRect
    {
        public bool draggable;

        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (draggable)
            {
                base.OnBeginDrag(eventData);
            }
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (draggable)
            {
                base.OnBeginDrag(eventData);
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (draggable)
            {
                base.OnBeginDrag(eventData);
            }
        }
    }
}