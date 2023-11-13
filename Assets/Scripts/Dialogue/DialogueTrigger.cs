using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager DialogueManager;
    public void TriggerDialogue()
    {
        DialogueManager.StartDialogue(dialogue);
    }
}
