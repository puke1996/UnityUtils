using UnityEngine.UI;

namespace Plugins.Puke.UnityUtilities.UnityExtensionMethods
{
    public static class HorizontalLayoutGroupExtensionMethods
    {
        /// <summary>
        /// Reset
        /// </summary>
        /// <param name="horizontalLayoutGroup"></param>
        public static void Reset(this HorizontalLayoutGroup horizontalLayoutGroup)
        {
            horizontalLayoutGroup.childControlWidth = false;
            horizontalLayoutGroup.childControlHeight = false;
            horizontalLayoutGroup.childForceExpandWidth = false;
            horizontalLayoutGroup.childForceExpandHeight = false;
            horizontalLayoutGroup.childScaleWidth = true;
            horizontalLayoutGroup.childScaleHeight = true;
        }
    }
}