using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogPanel; // A panel or canvas for the dialog
    public Text dialogText;        // Text to show the message

    public void ShowDialog(string message)
    {
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(true);
        }
        if (dialogText != null)
        {
            dialogText.text = message;
        }
    }

    public void HideDialog()
    {
        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }
    }
}
