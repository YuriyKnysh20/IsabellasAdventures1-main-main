using Script.Enemy.EnemyWithDamage;
using UnityEngine;

public class KillGoal : Goal
{
    [SerializeField] private EnemyWithDamage _enemy;
    public int EnemyID { get; set; }
    public KillGoal(Quest quest, int enemyID, string description, bool completed, int currentAmount, int requiredAmout)

    {
        this.Quest = quest;
        this.EnemyID = enemyID;
        this.Description = description;
        this.GoalCompleted = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmout;
    }

    public override void Init()
    {
        base.Init();
        QuestEvents.OnEnemyDeath += EnemyDied;// calling in EnemyWithDamage Script

    }

    void EnemyDied(IEnemy enemy)
    {
        if (enemy.ID == this.EnemyID)
        {

            this.CurrentAmount++;
            QuestEvents.AmountUpdate(CurrentAmount, RequiredAmount);
            Evaluate();
        }
    }
}
