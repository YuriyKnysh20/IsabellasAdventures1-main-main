using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemiesQuestStep : QuestStep
{
    private int _enemyKilled = 0;
    [SerializeField] private int _enemiesNeedToKill = 5;

    private void OnEnable()
    {
        GameEventsManager.Instance.miscEvents.onEnemyKilled += EnemyKilled;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.miscEvents.onEnemyKilled -= EnemyKilled;
    }

    private void EnemyKilled()
    {
        if (_enemyKilled < _enemiesNeedToKill)
        {
            _enemyKilled++;
            UpdateState();
        }

        if (_enemyKilled >= _enemiesNeedToKill)
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = _enemyKilled.ToString();
        ChangeState(state);
    }
    protected override void SetQuestStepState(string state)
    {
        this._enemyKilled = System.Int32.Parse(state);
        UpdateState();
    }
}

