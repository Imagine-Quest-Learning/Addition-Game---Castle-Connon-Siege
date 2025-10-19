using UnityEngine;
using UnityEngine.SceneManagement;

public class GamepageDiag : MonoBehaviour
{
    void Start()
    {
        Debug.Log($"[Diag] Scene={SceneManager.GetActiveScene().name}");
        Debug.Log($"[Diag] Active MainCamera: {Camera.main?.name}");
        Debug.Log($"[Diag] AudioListeners: {FindObjectsOfType<AudioListener>(true).Length}");
        var canvas = FindObjectOfType<Canvas>();
        Debug.Log($"[Diag] Canvas renderMode={canvas?.renderMode}, worldCamera={canvas?.worldCamera?.name}");
    }
}
