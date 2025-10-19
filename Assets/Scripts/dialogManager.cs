using System.Collections;
using UnityEngine;
using TMPro;

public class dialogManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialogPanel;
    public TMP_Text speakerText;
    public TMP_Text bodyText;
    public GameObject continueHint;

    [Header("Typing")]
    public float charInterval = 0.02f;

    BasicMovement player;
    Dialogue current;
    int index;
    bool isTyping;
    string fullLine;

    void Awake()
    {
        player = FindFirstObjectByType<BasicMovement>();
        if (dialogPanel) dialogPanel.SetActive(false);
        if (continueHint) continueHint.SetActive(false);
    }

    public void StartDialogue(Dialogue d)
    {
        current = d;
        index = 0;

        dialogPanel.SetActive(true);
        if (player) player.SetCanMove(false);
        if (speakerText) speakerText.text = d.speaker;

        if (continueHint) continueHint.SetActive(false);
        ShowNext();
    }

    void Update()
    {
        if (!dialogPanel || !dialogPanel.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            if (isTyping)
            {
                isTyping = false;
                bodyText.text = fullLine;
                if (continueHint) continueHint.SetActive(true);
            }
            else
            {
                if (continueHint) continueHint.SetActive(false);
                ShowNext();
            }
        }
    }

    void ShowNext()
    {
        if (current == null) return;

        if (index >= current.lines.Length)
        {
            EndDialogue();
            return;
        }

        fullLine = current.lines[index++];
        StopAllCoroutines();
        StartCoroutine(TypeLine(fullLine));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        bodyText.text = "";

        if (continueHint) continueHint.SetActive(false);

        foreach (char c in line)
        {
            bodyText.text += c;
            yield return new WaitForSeconds(charInterval);
            if (!isTyping) yield break;
        }

        isTyping = false;
        if (continueHint) continueHint.SetActive(true);
    }

    void EndDialogue()
    {
        if (continueHint) continueHint.SetActive(false);
        dialogPanel.SetActive(false);
        if (player) player.SetCanMove(true);
        current = null;
    }
}
