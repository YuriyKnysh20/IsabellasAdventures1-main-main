using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public class Quest : MonoBehaviour
{
    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public int ExperienceReward { get; set; }
    public int GoldReward { get; set; }
    public Itemm ItemReward { get; set; }
    public bool Completed { get; set; }
    [SerializeField] private GameObject _QuestRewardCanvas;
    
    public void CheckGoals()
    {
        Completed = Goals.All(g => g.GoalCompleted);// return true or false
        //Debug.Log("Completed in quest =" + Completed);
    }
    public void GiveReward()
    {
        LevelWindow levelWindow = FindObjectOfType<LevelWindow>();
        if (levelWindow != null)
            levelWindow.AddExp(ExperienceReward);
        else Debug.LogWarning("LevelWindow не найден в сцене. Награда не выдается.");

    }
    
}
