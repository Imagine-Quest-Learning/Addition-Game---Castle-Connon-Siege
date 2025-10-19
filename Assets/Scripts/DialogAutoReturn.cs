using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogAutoReturn : MonoBehaviour
{
    public string targetSceneName = "AddRoom";
    public float holdSeconds = 5f;
    public bool useFade = true;
    public float fadeDuration = 0.5f;

    bool started = false;

    void OnEnable()
    {
        if (started) return;
        started = true;
        StartCoroutine(ReturnFlow());
    }

    IEnumerator ReturnFlow()
    {
        var player = FindFirstObjectByType<BasicMovement>();
        if (player) player.SetCanMove(false);

        yield return new WaitForSecondsRealtime(holdSeconds);

        if (useFade)
        {
            var fader = FindFirstObjectByType<FadeController>();
            if (fader != null)
                yield return fader.FadeIn();
        }

        SceneManager.LoadScene(targetSceneName);
    }
}
