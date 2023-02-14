using UnityEngine;

namespace Finger
{
    public sealed class SingleWorldFinger : RichMono.RichMono
    {
        private IFinger _finger;
        public bool FingerStart => _finger != null && _finger.TouchStart;
        public bool FingerMove => _finger != null && _finger.TouchMove;
        public bool FingerEnd => _finger != null && _finger.TouchEnd;
        public Vector2 FingerPos => _finger.Position;

        public override void EarlyUpdate()
        {
#if UNITY_EDITOR
            // 如果鼠标左键点下并且不与UI交互
            if (Input.GetMouseButtonDown(0) && Common.TouchOnUI(Input.mousePosition))
            {
                _finger = new EditorFinger();
            }
#else
            if (_finger == null)
            {
                for (var i = 0; i < Input.touchCount; i++)
                {
                    var touch = Input.GetTouch(i);
                    if (touch.phase == TouchPhase.Began && Common.TouchOnUI(Input.mousePosition))
                    {
                        _finger = new Finger(touch.fingerId);
                        break;
                    }
                }
            }
#endif
        }

        public override void FinallyUpdate()
        {
            if (FingerEnd)
            {
                _finger = null;
            }
        }

        private void OnDisable()
        {
            _finger = null;
        }
    }
}