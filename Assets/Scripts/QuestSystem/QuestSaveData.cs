using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class QuestSaveData
{
    public string questName;
    public List<GoalSaveData> goalsData;
}
[System.Serializable]
public class GoalSaveData
{
    public int currentAmount;
    public int requiredAmount;
    public bool goalCompleted;
}
public class SaveSystem : MonoBehaviour
{
    public static void SaveQuestData(Quest quest)
    {
        QuestSaveData data = new QuestSaveData
        {
            questName = quest.QuestName,
            goalsData = new List<GoalSaveData>()
        };
        foreach (Goal goal in quest.Goals)
        {
            GoalSaveData goalData = new GoalSaveData
            {
                currentAmount = goal.CurrentAmount,
                requiredAmount = goal.RequiredAmount,
                goalCompleted = goal.GoalCompleted
            };
            data.goalsData.Add(goalData);
        }
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("QuestSaveData", json);
        PlayerPrefs.Save();
    }
    public static void LoadQuestData(Quest quest)
    {
        if (PlayerPrefs.HasKey("QuestSaveData"))
        {
            string json = PlayerPrefs.GetString("QuestSaveData");
            QuestSaveData data = JsonUtility.FromJson<QuestSaveData>(json);
            quest.QuestName = data.questName;
            for (int i = 0; i < Mathf.Min(quest.Goals.Count, data.goalsData.Count); i++)
            {
                quest.Goals[i].CurrentAmount = data.goalsData[i].currentAmount;
                quest.Goals[i].RequiredAmount = data.goalsData[i].requiredAmount;
                quest.Goals[i].GoalCompleted = data.goalsData[i].goalCompleted;
            }
        }
    }
}
