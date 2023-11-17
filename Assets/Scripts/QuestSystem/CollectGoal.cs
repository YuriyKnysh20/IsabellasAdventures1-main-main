using Script.Enemy.EnemyWithDamage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGoal : Goal
{
    public CollectGoal(Quest quest, string description, bool completed, int currentAmount, int requiredAmout)
    {
        this.Quest = quest;
        this.Description = description;
        this.GoalCompleted = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmout;
    }

    public override void Init()
    {
        base.Init();
        QuestEvents.OnBerryCollected += BerryCollected;// calling in EnemyWithDamage Script
    }

    void BerryCollected()
    {
        this.CurrentAmount++;
        QuestEvents.AmountUpdate(CurrentAmount, RequiredAmount);
        Evaluate();

    }
}
