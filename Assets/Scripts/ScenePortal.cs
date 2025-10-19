using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    public string targetSceneName = "gamepage";
    public float fadeDuration = 0.5f;

    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;
        StartCoroutine(DoTransition(other.GetComponent<BasicMovement>()));
    }

    IEnumerator DoTransition(BasicMovement player)
    {
        if (player) player.SetCanMove(false);

        var fader = FindFirstObjectByType<ScreenFader>();
        if (fader != null) yield return fader.FadeOut(fadeDuration);

        yield return null;
        SceneManager.LoadScene(targetSceneName);

        fader = FindFirstObjectByType<ScreenFader>();
        if (fader != null) yield return fader.FadeIn(fadeDuration);

        var newPlayer = FindFirstObjectByType<BasicMovement>();
        if (newPlayer) newPlayer.SetCanMove(true);
    }
}
