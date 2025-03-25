using UnityEngine;
using UnityEngine.UI;

public class ShieldAnswer : MonoBehaviour
{
    [Header("Answer on this soldier's shield")]
    public int answer = 0;

    [Header("UI Text (Optional)")]
    public Text shieldText;

    private void Start()
    {
        if (shieldText != null)
            shieldText.text = answer.ToString();
    }
}
