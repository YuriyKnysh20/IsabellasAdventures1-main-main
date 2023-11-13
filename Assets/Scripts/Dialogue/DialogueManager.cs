﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject say_to_npc;

    public Text dialogueText;
    public Text nameText;// name of quest giver

    public Animator animator;

    private Queue<string> sentences;
    private void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        say_to_npc.SetActive(false);

        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();// clear previos conversation
        foreach (string sentence in dialogue.sentences)// loop all sentenses
        {
            sentences.Enqueue(sentence);// delete from queue last sentence
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;// wait one frame
        }
    }
    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        say_to_npc.SetActive(true);
    }
}
