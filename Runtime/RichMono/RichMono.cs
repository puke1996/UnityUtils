using System;
using UnityEngine;

namespace Plugins.Puke.RichMono
{
    public abstract class RichMono : MonoBehaviour, IRichMono
    {
        public bool IsDestroyed { get; private set; }

        protected virtual void Start()
        {
            try
            {
                RichMonoSystem.Instance.AddRichMono(this);
            }
            catch (Exception e)
            {
                Debug.Log(GetType().Name);
                throw;
            }
        }

        private void OnDestroy()
        {
            try
            {
                IsDestroyed = true;
                RichMonoSystem.Instance.RemoveRichMono(this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // throw;
            }
        }

        public virtual void EarlyUpdate()
        {
        }

        public virtual void FinallyUpdate()
        {
        }
    }
}