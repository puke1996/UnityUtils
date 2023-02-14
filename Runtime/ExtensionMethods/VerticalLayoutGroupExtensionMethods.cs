using UnityEngine.UI;

namespace Plugins.Puke.UnityUtilities.UnityExtensionMethods
{
    public static class VerticalLayoutGroupExtensionMethods
    {
        public static void Reset(this VerticalLayoutGroup verticalLayoutGroup)
        {
            verticalLayoutGroup.childControlWidth = false;
            verticalLayoutGroup.childControlHeight = false;
            verticalLayoutGroup.childForceExpandWidth = false;
            verticalLayoutGroup.childForceExpandHeight = false;
            verticalLayoutGroup.childScaleWidth = true;
            verticalLayoutGroup.childScaleHeight = true;
        }
    }
}