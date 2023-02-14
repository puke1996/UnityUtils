using UnityEngine;

namespace Plugins.Puke.Finger
{
    public class Finger : IFinger
    {
        private readonly int _pointerId;
        private int _currentFrameCount = -1;
        private Touch _touch;

        public Finger(int pointerId)
        {
            _pointerId = pointerId;
        }

        private Touch Touch
        {
            get
            {
                if (_currentFrameCount != Time.frameCount)
                {
                    for (var i = 0; i < Input.touchCount; i++)
                    {
                        if (Input.GetTouch(i).fingerId == _pointerId)
                        {
                            _touch = Input.GetTouch(i);
                            _currentFrameCount = Time.frameCount;
                            break;
                        }
                    }
                }

                return _touch;
            }
        }

        public bool TouchStart => Touch.phase == TouchPhase.Began;
        public bool TouchMove => Touch.phase != TouchPhase.Began;
        public bool TouchEnd => Touch.phase == TouchPhase.Ended || Touch.phase == TouchPhase.Canceled;
        public Vector2 Position => Touch.position;
    }
}