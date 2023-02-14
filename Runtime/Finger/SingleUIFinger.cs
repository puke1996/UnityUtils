using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Finger
{
    [DefaultExecutionOrder(Int32.MinValue)]
    public sealed class SingleUIFinger : MonoBehaviour, IPointerDownHandler
    {
        public bool TouchStart => _finger != null && _finger.TouchStart;
        public bool TouchMove => _finger != null && _finger.TouchMove;
        public bool TouchEnd => _finger != null && _finger.TouchEnd;
        public Vector2 Position => _finger.Position;
        private IFinger _finger;

        public void OnPointerDown(PointerEventData eventData)
        {
#if UNITY_EDITOR
            _finger = new EditorFinger();
#else
            _finger = new Finger(eventData.pointerId);
#endif
        }

        private void Update()
        {
            if (_finger != null && _finger.TouchEnd)
            {
                _finger = null;
            }
        }
    }
}