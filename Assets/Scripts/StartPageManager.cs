using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Manages the start page button interactions
public class StartPageManager : MonoBehaviour
{
    public Button startButton;
    public Button InstructionButton;

    // Assign button click listeners at the start
    void Start()
    {
        startButton.onClick.AddListener(LoadGamePage);
        InstructionButton.onClick.AddListener(LoadInstructionScene);
    }

    // Load the main game scene
    void LoadGamePage()
    {
        SceneManager.LoadScene("gamepage");
    }

    // Load the instruction scene
    void LoadInstructionScene()
    {
        SceneManager.LoadScene("instructionpage");
    }
}
