using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    #region UIELEMENTS
    [SerializeField] private GameObject _takeQuest;
    [SerializeField] private GameObject _inProcessQuest;
    [SerializeField] private GameObject _finishQuest;
    [SerializeField] private GameObject _completedQuest;
    [SerializeField] private QuestSystemUI _questPanel;
    #endregion
    public bool AssignedQuest { get; set; }//Ќазначенный квест
    public bool Helped { get; set; }
    private Quest Quest { get; set; }// квест который дает квест гивер в журнал квестов

    [SerializeField] private GameObject quests; // in this GO we will created a Quest
    [SerializeField] private string questType;// им€ квеста например "KillWolfes" KillWolfes.cs
    [SerializeField] private Button ClaimReward;
    [SerializeField] private GameObject _QuestRewardCanvas;
    public DialogueManager dialoguemanager;
    public Dialogue dialogue;
    private DialogueTrigger finishQuestDialogueTrigger;
    [SerializeField] public Button CompletedQuest;
    void AssignQuest()
    {
        AssignedQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));// в скобки нужно передать строку, название квеста.
        _questPanel.ShowPanel(!Quest.Completed);
    }
    public void CheckQuest()
    {
        if (Quest.Completed)
        {
            Debug.Log("Quest.Completed======" + Quest.Completed);

            _questPanel.ShowPanel(!Quest.Completed);
            _finishQuest.SetActive(true);
            WaitGiveReward();
            Helped = true;
            AssignedQuest = false;
        }
        else
        {
            _inProcessQuest.SetActive(true);
        }

    }
    public void WaitGiveReward()
    {
        ClaimReward.onClick.AddListener(ButtonReward);
        CompletedQuest.onClick.AddListener(OnCompletedQuestClicked);
        StartCoroutine(WaitForButtonPress());
        //dialoguemanager.sentences.Count

    }
    public void ButtonReward()
    {
        Quest.GiveReward();// даем награду игроку
    }
    public void GiveReward()
    {
        finishQuestDialogueTrigger = _finishQuest.GetComponent<DialogueTrigger>();
        int sentencesLenght = finishQuestDialogueTrigger.dialogue.sentences.Length;
        for (int sentences = 0; sentences <= finishQuestDialogueTrigger.dialogue.sentences.Length; sentences++, sentencesLenght--)
        {
            if (sentencesLenght == 0)
            {

                _QuestRewardCanvas.SetActive(true);
            }
        }
    }
    IEnumerator WaitForButtonPress()
    {
        // ∆дем, пока кнопка не будет нажата
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // »ли используйте свое условие

        Debug.Log("Button _completedQuest Pressed!");
        GiveReward();
        // ¬аш код дл€ выполнени€ действий после нажати€ кнопки

    }
    void OnCompletedQuestClicked()
    {
        Debug.Log("Button _completedQuest Clicked!");

        // Ќичего не делаем здесь, так как ждем, пока кнопка будет нажата в корутине
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!AssignedQuest && !Helped)// если нет квеста и еще не выполнен
            {
                _takeQuest.SetActive(true);
            }
            else if (AssignedQuest && !Helped)// если есть квест и еще не выполнен
            {
                _takeQuest.SetActive(false);
                CheckQuest();
            }
            else
            {
                _completedQuest.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!AssignedQuest && !Helped)// если нет квеста и еще не выполнен
            {
                AssignQuest();//назначаем квест при выходе с колизии
            }
        }

    }
}
