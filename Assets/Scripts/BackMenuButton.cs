using UnityEngine;
using UnityEngine.SceneManagement;

// Script for the menu button on the gamepage
public class BackMenuButton : MonoBehaviour
{
    // Loads the "startpage" scene when the menu button is clicked
    public void BackToStartPage()
    {
        SceneManager.LoadScene("startpage");
    }
}
