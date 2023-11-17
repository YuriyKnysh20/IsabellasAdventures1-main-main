using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillWolfes : Quest
{
    private void Start()
    {
        QuestName = "Kill Enemies";
        Description = "Kill some wolves";
        ExperienceReward = 5;
        GoldReward = 10;

        Goals.Add(new KillGoal(this, 0, "Kill 5 wolves", false, 0, 5));
        
        QuestEvents.AssignUIRewards(QuestName, ExperienceReward, GoldReward);
        Goals.ForEach(Goal => Goal.Init()); // проходит циклом по всем целям и vizov метод инит
    }
}
