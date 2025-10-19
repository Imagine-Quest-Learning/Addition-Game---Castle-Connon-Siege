using System.Collections;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    public float defaultDuration = 0.5f;

    void Reset()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>(true);
    }

    public IEnumerator FadeOut(float duration = -1f)
    {
        if (duration < 0) duration = defaultDuration;
        gameObject.SetActive(true);
        float t = 0;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Clamp01(t / duration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    public IEnumerator FadeIn(float duration = -1f)
    {
        if (duration < 0) duration = defaultDuration;
        float t = 0;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = 1f - Mathf.Clamp01(t / duration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}
