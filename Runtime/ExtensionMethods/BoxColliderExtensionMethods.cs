using UnityEngine;

namespace Plugins.Puke.UnityUtilities.UnityExtensionMethods
{
    public static class BoxColliderExtensionMethods
    {
        /// <summary>
        /// Adjust the boxed collider to match the size of the gameObject
        /// </summary>
        /// <param name="boxCollider"></param>
        public static void Match(this BoxCollider boxCollider)
        {
            // Backup transform
            var bak = boxCollider.transform.Bak();
            // Reset transform
            boxCollider.transform.Reset();
            // Adjust boxCollider
            var bounds = boxCollider.gameObject.GetRendererBoundsWithNameFilter(null);
            boxCollider.size = bounds.size;
            boxCollider.center = bounds.center;
            // Restore transform
            boxCollider.transform.Restore(bak);
        }
    }
}