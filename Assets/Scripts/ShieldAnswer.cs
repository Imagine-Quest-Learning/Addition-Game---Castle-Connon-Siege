using UnityEngine;
using UnityEngine.UI;

public class ShieldAnswer : MonoBehaviour
{
    [Header("Answer on this soldier's shield")]
    public int answer = 0;

    [Header("UI Text (Optional)")]
    public Text shieldText;  // If you want to display the number on the shield UI

    private void Start()
    {
        // If there's a text assigned, display the answer
        if (shieldText != null)
        {
            shieldText.text = answer.ToString();
        }
    }
}
