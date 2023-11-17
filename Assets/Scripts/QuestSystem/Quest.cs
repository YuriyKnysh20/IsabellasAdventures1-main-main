using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Quest : MonoBehaviour
{
   // private LevelWindow _levelWindow;
   // private LevelSystem levelSystem;

    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public int ExperienceReward { get; set; }
    public int GoldReward { get; set; }
    public Item ItemReward { get; set; }
    public bool Completed { get; set; }

    public void CheckGoals()
    {
        Completed = Goals.All(g => g.GoalCompleted);// return true or false
        Debug.Log("Completed in quest =" + Completed);
    }
    //private bool Iscomplete(Goal g)
    //{
    //    return g.GoalCompleted;
    //}
    public void GiveReward()
    {

        Debug.Log("Daem nagradu:" + "ExperienceReward\t" + ExperienceReward + "GoldReward\t" + GoldReward);
        //_levelWindow.AddExp((int)ExperienceReward);
        //levelSystem.AddExperience(ExperienceReward);
    }
}
