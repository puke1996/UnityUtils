using UnityEngine;

namespace Finger
{
    public class EditorFinger : IFinger
    {
        public bool TouchStart => Input.GetMouseButtonDown(0);

        public bool TouchMove
        {
            get
            {
                if (Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0))
                {
                    return true;
                }

                if (Input.GetMouseButtonUp(0))
                {
                    return true;
                }

                return false;
            }
        }

        public bool TouchEnd => Input.GetMouseButtonUp(0);
        public Vector2 Position => Input.mousePosition;
    }
}