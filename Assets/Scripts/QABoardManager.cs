using UnityEngine;
using TMPro;

public class QABoardManager : MonoBehaviour
{
    [Header("Question Settings")]
    public int a = 2;
    public int b = 3;
    public TextMeshProUGUI questionText;

    [Header("Dialog UI")]
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;

    public static QABoardManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public int CorrectAnswer => a + b;

    private void Start()
    {
        if (questionText != null)
            questionText.text = $"{a} + {b} = ?";

        if (dialogPanel != null)
            dialogPanel.SetActive(false);
    }

    public void CheckAnswer(int soldierAnswer)
    {
        bool isCorrect = (soldierAnswer == CorrectAnswer);
        Color messageColor = isCorrect ? Color.green : Color.red;
        ShowDialog(isCorrect ? "Correct!" : "Wrong!", messageColor, isCorrect ? 50 : 45);
    }

    public void ShowDialog(string message, Color textColor, float fontSize)
    {
        if (dialogPanel != null)
            dialogPanel.SetActive(true);

        if (dialogText != null)
        {
            dialogText.text = message;
            dialogText.color = textColor;
            dialogText.fontSize = fontSize;
            dialogText.fontStyle = FontStyles.Bold;
        }

        CannonController.canShoot = false;
        Invoke(nameof(HideDialog), 2f);
    }

    public void HideDialog()
    {
        if (dialogPanel != null)
            dialogPanel.SetActive(false);

        CannonController.canShoot = true;
    }
}
