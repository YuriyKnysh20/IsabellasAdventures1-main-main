using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvents : MonoBehaviour
{
    public delegate void EnemyEventHandler(IEnemy enemy);
    public static event EnemyEventHandler OnEnemyDeath;
    public delegate void EventBerry();
    public static event EventBerry OnBerryCollected;
    public static void EnemyDied(IEnemy enemy)
    {
        OnEnemyDeath?.Invoke(enemy);
    }
    public static void BerryCollected()
    {
        OnBerryCollected?.Invoke();
    }

    #region UIevents UIevents 

    public delegate void UIevent(string QuestName, int ExperienceReward, int GoldReward);
    public static event UIevent OnAssignUiRewards;


    public static void AssignUIRewards(string QuestName,int ExperienceReward, int GoldReward)
    {
        OnAssignUiRewards?.Invoke(QuestName, ExperienceReward, GoldReward);
    }

    public delegate void UpdateUIEvent( int currentAmount, int RequiredAmount);

    public static event UpdateUIEvent IsAmountUpdate;


    public static void AmountUpdate( int currentAmount, int RequiredAmount)
    {
        IsAmountUpdate?.Invoke( currentAmount, RequiredAmount);
    }


    #endregion
}
