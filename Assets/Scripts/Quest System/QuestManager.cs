using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> _questMap;
    private void Awake()
    {
        _questMap = CreateQuestMap();
        // Quest quest = GetQuestById("CollectCoinsQuest");// in folder Resources/Quests find on scriptable object ID
    }

    private void OnEnable()
    {
        GameEventsManager.Instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.Instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.Instance.questEvents.onFinishQuest += FinishQuest;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.Instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.Instance.questEvents.onFinishQuest -= FinishQuest;
    }

    private void Start()
    {
        foreach (Quest quest in _questMap.Values)
        {
            GameEventsManager.Instance.questEvents.QuestStateChange(quest);
        }
    }

    private void StartQuest(string id)
    {
        Debug.Log("Start Quest:" + id);
    }
    private void AdvanceQuest(string id)
    {
        Debug.Log("Advance Quest:" + id);
    }
    private void FinishQuest(string id)
    {
        Debug.Log("Finish Quest:" + id);
    }


    private Dictionary<string, Quest> CreateQuestMap()
    {
        // load all QuestInfoSO scriptable objects in folder Resources/Quests
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        // Create the quest map
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map" + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }
        return idToQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = _questMap[id];
        if (quest == null)
        {
            Debug.LogError("ID not found in the Quest Map:" + id);
        }
        return quest;
    }
}
