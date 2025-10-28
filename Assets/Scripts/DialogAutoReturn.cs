using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogAutoReturnSimple : MonoBehaviour
{
    [SerializeField] string targetSceneName = "AddroomOut";
    [SerializeField] float delaySeconds = 3f;
    bool started;

    void OnEnable()
    {
        if (started) return;
        started = true;
        StartCoroutine(JumpAfterDelay());
    }

    IEnumerator JumpAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delaySeconds);

        if (Time.timeScale != 1f) Time.timeScale = 1f;

        SceneManager.LoadScene(targetSceneName, LoadSceneMode.Single);
    }
}
