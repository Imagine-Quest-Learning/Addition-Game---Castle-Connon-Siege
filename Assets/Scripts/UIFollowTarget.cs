using UnityEngine;
using UnityEngine.UI;

// Makes a UI element follow a target in world space
public class UIFollowTarget : MonoBehaviour
{
    public RectTransform uiElement;
    public Transform target;
    public Canvas canvas;
    public Vector3 offset;

    void Update()
    {
        if (target == null || uiElement == null || canvas == null) return;

        // Convert world position to screen position
        Vector3 worldPos = target.position + offset;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // Convert screen position to local position in the canvas
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out localPoint))
        {
            uiElement.anchoredPosition = localPoint;
        }
    }
}
