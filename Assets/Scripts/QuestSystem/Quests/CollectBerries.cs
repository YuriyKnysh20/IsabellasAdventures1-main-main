using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBerries : Quest
{
    private void Start()
    {
        QuestName = "Collect Berries";
        Description = "Collect some berries";
        ExperienceReward = 5;
        GoldReward = 10;

        Goals.Add(new CollectGoal(this, "Collect 5 berries", false, 0, 5));
        QuestEvents.AssignUIRewards(QuestName, ExperienceReward, GoldReward);
        Goals.ForEach(g => g.Init());
    }
}
