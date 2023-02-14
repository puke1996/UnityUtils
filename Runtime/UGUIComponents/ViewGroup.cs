using System.Collections.Generic;
using UnityEngine;

namespace UGUIComponents
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