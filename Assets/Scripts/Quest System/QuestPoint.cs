using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class QuestPoint : MonoBehaviour
{
    private bool playerIsNear = false;
    [SerializeField] private QuestInfoSO questInfoForPoint;

    private string questId;

    private QuestState currentQuestState;

    private void Awake()
    {
        questId = questInfoForPoint.id;
    }
    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.onQuestStateChange += QuestStateChange;
        //GameEventsManager.Instance.inputEvents.onSubmitPressed += SubmitPressed;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.onQuestStateChange -= QuestStateChange;
        // GameEventsManager.Instance.inputEvents.onSubmitPressed -= SubmitPressed;
    }
    private void SubmitPressed()
    {
        if (!playerIsNear)
            return;
        GameEventsManager.Instance.questEvents.StartQuest(questId);
        GameEventsManager.Instance.questEvents.AdvanceQuest(questId);
        GameEventsManager.Instance.questEvents.FinishQuest(questId);
    }
    private void QuestStateChange(Quest quest)
    {
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;

        }
    }
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }

}
