using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class backButtonManager : MonoBehaviour
{
    public Button backButton;

    void Start()
    {
        backButton.onClick.AddListener(LoadStartPage);
    }

    void LoadStartPage()
    {
        SceneManager.LoadScene("startpage");
    }
}
