using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Script for the back button on the instruction page
public class backButtonManager : MonoBehaviour
{
    public Button backButton;

    // Add click listener to the back button at the start
    void Start()
    {
        backButton.onClick.AddListener(LoadStartPage);
    }

    // Load the "startpage" scene when the back button is clicked
    void LoadStartPage()
    {
        SceneManager.LoadScene("startpage");
    }
}
