using UnityEngine;

namespace Plugins.Puke.UnityUtilities.AutoGetcomponentAttributes.Samples
{
    public class ChildTest : MonoBehaviour
    {
        [Child] [SerializeField] private Target _target;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}