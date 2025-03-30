using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.0f;

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            SetAlpha(alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetAlpha(1f);
    }

    public IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            SetAlpha(alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetAlpha(0f);
    }

    void SetAlpha(float alpha)
    {
        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
    }

    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(Transition(sceneName));
    }

    private IEnumerator Transition(string sceneName)
    {
        yield return StartCoroutine(FadeIn());
        SceneManager.LoadScene(sceneName);
        yield return StartCoroutine(FadeOut());
    }
}
