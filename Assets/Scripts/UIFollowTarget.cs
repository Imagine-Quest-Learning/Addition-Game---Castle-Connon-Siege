using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Makes a UI element follow a target in world space
public class UIFollowTarget : MonoBehaviour
{
    public RectTransform uiElement;
    public Transform target;
    public Canvas canvas;
    public Vector3 offset;
    public Camera worldCamera;

    Camera uiCamera;
    RectTransform canvasRect;
    bool ready;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartCoroutine(InitNextFrame());
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        ready = false;
    }

    System.Collections.IEnumerator InitNextFrame()
    {
        yield return null;
        BindCanvasAndCamera();
        ready = true;
    }

    void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        BindCanvasAndCamera();
    }

    void BindCanvasAndCamera()
    {
        if (canvas == null) canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvasRect = canvas.transform as RectTransform;
            uiCamera = (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
                ? null
                : (canvas.worldCamera != null ? canvas.worldCamera : worldCamera);
        }
        if (worldCamera == null) worldCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (!ready || target == null || uiElement == null || canvasRect == null) return;

        Vector3 worldPos = target.position + offset;
        Vector3 screenPos = (uiCamera == null)
            ? (worldCamera != null ? worldCamera.WorldToScreenPoint(worldPos) : Vector3.zero)
            : uiCamera.WorldToScreenPoint(worldPos);

        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, uiCamera, out localPoint))
            uiElement.anchoredPosition = localPoint;
    }
}
