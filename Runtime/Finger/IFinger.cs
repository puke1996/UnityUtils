using UnityEngine;

namespace Plugins.Puke.Finger
{
    public interface IFinger
    {
        public bool TouchStart { get; }
        public bool TouchMove { get; }
        public bool TouchEnd { get; }
        public Vector2 Position { get; }
    }
}