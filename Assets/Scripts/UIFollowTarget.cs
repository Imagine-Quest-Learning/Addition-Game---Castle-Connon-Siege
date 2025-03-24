using UnityEngine;
using UnityEngine.UI;

public class UIFollowTarget : MonoBehaviour
{
    public RectTransform uiElement;
    public Transform target;
    public Canvas canvas;
    public Vector3 offset;

    void Update()
    {
        if (target == null || uiElement == null || canvas == null) return;

        Vector3 worldPos = target.position + offset;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect, screenPos, null, out localPoint))
        {
            uiElement.anchoredPosition = localPoint;
        }
    }
}
