using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public GameObject say_to_npc;
    [SerializeField] private QuestSystemUI _questPanel;
    public bool AssignedQuest { get; set; }//����������� �����
    public bool Helped { get; set; }

    [SerializeField] private GameObject quests; // in this GO we will created a Quest
    [SerializeField] private string questType;// ��� ������ �������� "KillWolfes" KillWolfes.cs
    private Quest Quest { get; set; }// ����� ������� ���� ����� ����� � ������ �������


    void AssignQuest()
    {
        AssignedQuest = true;
        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));// � ������ ����� �������� ������, �������� ������.
        _questPanel.ShowPanel(!Quest.Completed);
    }
    void CheckQuest()
    {
        if (Quest.Completed)
        {
            Debug.Log("Quest.Completed======" + Quest.Completed);

            _questPanel.ShowPanel(!Quest.Completed);
            Quest.GiveReward();
            Helped = true;
            AssignedQuest = false;
            //DialogueSystem.Instance.AddNewDialogue(new string[] { "Thanks for that! Here's your reward.", "More dialogue" }, name);
        }
        else
        {
            Debug.Log("You're still in the middle of helping me. Get back at it!");
            //DialogueSystem.Instance.AddNewDialogue(new string[] { "You're still in the middle of helping me. Get back at it!" }, name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Enter colission:"+"AssignedQuest= " + AssignedQuest + "Helped= " + Helped);
            if (!AssignedQuest && !Helped)// ���� ��� ������ � ��� �� ��������
            {
                Debug.Log("��� ������ � ��� �� ��������");
                say_to_npc.SetActive(true);
                //AssignQuest();
            }
            else if (AssignedQuest && !Helped)// ���� ���� ����� � ��� �� ��������
            {
                Debug.Log("����� � ��������");
                say_to_npc.SetActive(false);
                CheckQuest();
            }
            else
            {
                say_to_npc.SetActive(true);
                //DialogueSystem.Instance.AddNewDialogue(new string[] { "Thanks for that stuff that one time." }, name);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!AssignedQuest && !Helped)// ���� ��� ������ � ��� �� ��������
            {
                AssignQuest();
            }
        }
    }
}
