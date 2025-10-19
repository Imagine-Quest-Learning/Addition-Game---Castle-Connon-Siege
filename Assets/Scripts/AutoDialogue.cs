using UnityEngine;

public class AutoDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    dialogManager dm;

    void Awake()
    {
        dm = FindFirstObjectByType<dialogManager>();
    }

    void Start()
    {
        if (dm != null && dialogue != null)
            dm.StartDialogue(dialogue);
        else
            Debug.LogWarning("Warning");
    }
}
