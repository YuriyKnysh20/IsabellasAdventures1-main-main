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
    public bool AssignedQuest { get; set; }//����������� �����
    public bool Helped { get; set; }
    private Quest Quest { get; set; }// ����� ������� ���� ����� ����� � ������ �������

    [SerializeField] private GameObject quests; // in this GO we will created a Quest
    [SerializeField] private string questType;// ��� ������ �������� "KillWolfes" KillWolfes.cs
    [SerializeField] private Button ClaimReward;
    [SerializeField] private GameObject _QuestRewardCanvas;
    public DialogueManager dialoguemanager;
    public Dialogue dialogue;
    private DialogueTrigger finishQuestDialogueTrigger;
    [SerializeField] public Button CompletedQuest;
    void AssignQuest()
    {
        AssignedQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));// � ������ ����� �������� ������, �������� ������.
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
        Quest.GiveReward();// ���� ������� ������
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
        // ����, ���� ������ �� ����� ������
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // ��� ����������� ���� �������

        Debug.Log("Button _completedQuest Pressed!");
        GiveReward();
        // ��� ��� ��� ���������� �������� ����� ������� ������

    }
    void OnCompletedQuestClicked()
    {
        Debug.Log("Button _completedQuest Clicked!");

        // ������ �� ������ �����, ��� ��� ����, ���� ������ ����� ������ � ��������
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!AssignedQuest && !Helped)// ���� ��� ������ � ��� �� ��������
            {
                _takeQuest.SetActive(true);
            }
            else if (AssignedQuest && !Helped)// ���� ���� ����� � ��� �� ��������
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
            if (!AssignedQuest && !Helped)// ���� ��� ������ � ��� �� ��������
            {
                AssignQuest();//��������� ����� ��� ������ � �������
            }
        }

    }
}
