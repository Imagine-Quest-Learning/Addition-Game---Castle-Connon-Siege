using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPageManager : MonoBehaviour
{
    public Button startButton;
    public Button InstructionButton;

    void Start()
    {
        startButton.onClick.AddListener(LoadGamePage);
        InstructionButton.onClick.AddListener(LoadInstructionScene);
    }

    void LoadGamePage()
    {
        SceneManager.LoadScene("gamepage");
    }
    void LoadInstructionScene()
    {
        SceneManager.LoadScene("instructionpage");
    }
}
