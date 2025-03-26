using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QABoardManager : MonoBehaviour
{
    [Header("Question Settings")]
    public int a = 2;
    public int b = 3;
    public TextMeshProUGUI questionText;

    [Header("Dialog UI")]
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;

    [Header("Restart Button")]
    public Button restartButton;

    public static QABoardManager Instance { get; private set; }

    private void Awake()
    {
        // Ensure only one instance of this manager exists
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (questionText != null)
            questionText.text = $"{a} + {b} = ?";

        if (dialogPanel != null)
            dialogPanel.SetActive(false);

        if (restartButton != null)
            restartButton.gameObject.SetActive(false);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);
    }

    public int CorrectAnswer => a + b;

    public void CheckAnswer(int soldierAnswer)
    {
        bool isCorrect = (soldierAnswer == CorrectAnswer);

        if (isCorrect)
            ShowDialog("Correct!", Color.green, 50, true);
        else
            ShowDialog("Wrong!", Color.red, 45, false);
    }

    public void ShowDialog(string message, Color textColor, float fontSize, bool freezeGame)
    {
        if (dialogPanel != null)
            dialogPanel.SetActive(true);

        if (dialogText != null)
        {
            dialogText.text = message;
            dialogText.color = textColor;
            dialogText.fontSize = fontSize;
        }

        CannonController.canShoot = false;

        if (freezeGame)
        {
            Time.timeScale = 0f;

            if (restartButton != null)
                StartCoroutine(ShowRestartButtonAfterDelay(2f));
        }
        else
        {
            Invoke(nameof(HideDialog), 2f);
        }
    }


    // Hides the dialog panel
    public void HideDialog()
    {
        if (dialogPanel != null)
            dialogPanel.SetActive(false);

        CannonController.canShoot = true;
    }

    // Called by the restart button to reload the current scene
    private void RestartGame()
    {
        // Resume time flow before restarting
        Time.timeScale = 1f;

        // Reload the current scene by build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private System.Collections.IEnumerator ShowRestartButtonAfterDelay(float delay)
    {
        float timePassed = 0f;
        while (timePassed < delay)
        {
            timePassed += Time.unscaledDeltaTime;
            yield return null;
        }

        if (restartButton != null)
            restartButton.gameObject.SetActive(true);
    }

}
