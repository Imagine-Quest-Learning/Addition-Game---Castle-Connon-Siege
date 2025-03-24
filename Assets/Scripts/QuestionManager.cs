using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [Header("Question Settings")]
    public int a = 2;       // First addend
    public int b = 3;       // Second addend

    [Header("UI Reference (Optional)")]
    public Text questionText;  // A Text component to show the question like "2 + 3 = ?"

    // The correct answer
    public int CorrectAnswer
    {
        get { return a + b; }
    }

    private void Start()
    {
        // If you want to display the question on-screen
        if (questionText != null)
        {
            questionText.text = a + " + " + b + " = ?";
        }
    }
}
