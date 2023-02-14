using UnityEngine;

public class LayerUtils : MonoBehaviour
{
    public static LayerMask LayerToLayerMask(int layer)
    {
        return LayerMask.GetMask(LayerMask.LayerToName(layer));
    }

    public static LayerMask AddLayerToLayerMask(LayerMask layerMask, int layer)
    {
        // 010+001=011
        layerMask |= 1 << layer;
        return layerMask;
    }

    public static LayerMask RemoveLayerFromLayerMask(LayerMask layerMask, int layer)
    {
        // ~001=110
        // 110&011=010
        // 011+001=010
        layerMask &= ~ (1 << layer);
        return layerMask;
    }

    public static LayerMask empty = 0;
    public static LayerMask everything = ~0;

    public static LayerMask NameToLayerMask(string name)
    {
        return LayerMask.GetMask(name);
    }

    public static string LayerToName(int layer)
    {
        return LayerMask.LayerToName(layer);
    }

    public static int NameToLayer(string name)
    {
        return LayerMask.NameToLayer(name);
    }
}