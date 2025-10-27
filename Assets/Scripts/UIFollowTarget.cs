using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        if (uiElement) uiElement.gameObject.SetActive(false);
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
        if (!ready || uiElement == null || canvasRect == null)
        {
            return;
        }

        if (target == null || !target.gameObject.activeInHierarchy)
        {
            if (uiElement.gameObject.activeSelf) uiElement.gameObject.SetActive(false);
            return;
        }

        if (worldCamera == null) worldCamera = Camera.main;

        Vector3 worldPos = target.position + offset;

        Vector3 screenPos = (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            ? (worldCamera != null ? worldCamera.WorldToScreenPoint(worldPos) : new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f))
            : ((uiCamera != null ? uiCamera : worldCamera).WorldToScreenPoint(worldPos));

        if (screenPos.z < 0f)
        {
            if (uiElement.gameObject.activeSelf) uiElement.gameObject.SetActive(false);
            return;
        }

        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : uiCamera,
            out localPoint))
        {
            uiElement.anchoredPosition = localPoint;
            if (!uiElement.gameObject.activeSelf) uiElement.gameObject.SetActive(true);
        }
        else
        {
            if (uiElement.gameObject.activeSelf) uiElement.gameObject.SetActive(false);
        }
    }
}
