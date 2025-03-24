using UnityEngine;
using TMPro; // Required for TextMeshPro support

public class QABoardManager : MonoBehaviour
{
    [Header("Question Settings")]
    public int a = 2;                      // First number in the addition question
    public int b = 3;                      // Second number in the addition question
    public TextMeshProUGUI questionText;   // UI Text element to display the question

    [Header("Dialog UI")]
    public GameObject dialogPanel;         // Dialog panel to display messages
    public TextMeshProUGUI dialogText;     // Text component for displaying result messages

    public static QABoardManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Compute the correct answer dynamically
    public int CorrectAnswer => a + b;

    private void Start()
    {
        // Display the question on the board at the start
        if (questionText != null)
        {
            questionText.text = $"{a} + {b} = ?";
        }

        // Hide the dialog panel at the beginning
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }
    }

    // This method checks if the soldier's answer is correct when hit by a ball
    public void CheckAnswer(int soldierAnswer)
    {
        bool isCorrect = (soldierAnswer == CorrectAnswer);

        // Set text color: Green for correct, Red for wrong
        Color messageColor = isCorrect ? Color.green : Color.red;

        // Show result message with enhanced style
        ShowDialog(isCorrect ? "Correct!" : "Wrong!", messageColor, isCorrect ? 50 : 45);

        Debug.Log(isCorrect ? "Player chose the correct answer!" : "Player chose the wrong answer!");
    }

    // Show a message with dynamic color and font size
    public void ShowDialog(string message, Color textColor, float fontSize)
    {
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(true);
        }
        if (dialogText != null)
        {
            dialogText.text = message;
            dialogText.color = textColor; // Set text color dynamically
            dialogText.fontSize = fontSize; // Adjust font size dynamically
            dialogText.fontStyle = FontStyles.Bold; // Set text to bold for better visibility
        }

        // Hide the dialog after 2 seconds
        Invoke("HideDialog", 2f);
    }

    // Hide the dialog panel
    public void HideDialog()
    {
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }
    }
}
