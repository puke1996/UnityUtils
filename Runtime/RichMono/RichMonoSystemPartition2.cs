using UnityEngine;

namespace Plugins.Puke.RichMono
{
    [DefaultExecutionOrder(int.MaxValue)]
    internal class RichMonoSystemPartition2 : MonoBehaviour
    {
        private void LateUpdate()
        {
            for (int i = 0; i < RichMonoSystem.Instance.richMonoListClone.Count; i++)
            {
                if (!RichMonoSystem.Instance.richMonoListClone[i].IsDestroyed &&
                    ((MonoBehaviour) RichMonoSystem.Instance.richMonoListClone[i]).isActiveAndEnabled)
                {
                    RichMonoSystem.Instance.richMonoListClone[i].FinallyUpdate();
                }
            }
        }
    }
}