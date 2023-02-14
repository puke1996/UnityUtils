using System.Collections.Generic;
using UnityEngine;

namespace Plugins.Puke.UnityUtilities.UnitySimpleUIComponents
{
    public class ViewGroup : MonoBehaviour
    {
        [SerializeField] private List<GameObject> views;

        public void Open(GameObject target)
        {
            foreach (var view in views)
            {
                view.SetActive(view == target);
            }
        }
    }
}