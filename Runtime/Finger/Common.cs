using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Common
{
    public static bool TouchOnUI(Vector2 screenPosition)
    {
        if (EventSystem.current == null)
        {
            return false;
        }

        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = screenPosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count != 0;
    }
}