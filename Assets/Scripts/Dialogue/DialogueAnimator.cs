using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimator : MonoBehaviour
{
    public DialogueManager dm;
    public Canvas dialogue_Canvas;
    //public GameObject say_to_npc_button;
    //public GameObject DialogueBox;


    public void OnTriggerEnter2D(Collider2D other)
    {
        dialogue_Canvas.enabled = true;

        //DialogueBox.SetActive(true);
        //say_to_npc_button.SetActive(true);

    }
    public void OnTriggerExit2D(Collider2D other)
    {
        //DialogueBox.SetActive(false);
        //say_to_npc_button.SetActive(false);

        dialogue_Canvas.enabled = false;
        
        dm.EndDialogue();
    }
}
