using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    public Quest Quest { get; set; }
    public string Description { get; set; }
    public bool GoalCompleted { get; set; }

    [SerializeField] private int _currentAmount = 0;
    public int CurrentAmount
    {
        get { return _currentAmount; }
        set { _currentAmount = value; }
    }
    public int RequiredAmount { get; set; }

    public virtual void Init()
    {
        //CurrentAmount = 0;
        // default init staff
    }

    public void Evaluate()
    {
        // Debug.Log("Evaluete is calling" + "CurrentAmount= " + CurrentAmount + "RequiredAmount=" + RequiredAmount);
        if (CurrentAmount >= RequiredAmount)
        {
            Complete();
        }
    }

    public void Complete()
    {
        GoalCompleted = true;

        Quest.CheckGoals();

        // Debug.Log("Goal marked as completed." + "Completed=  " + GoalCompleted);
    }

}
