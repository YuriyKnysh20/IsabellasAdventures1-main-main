using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBerriesQuestStep : QuestStep
{
    private int _berriesCollected = 0;
    [SerializeField] private int _berriesToComplete = 5;

    private void OnEnable()
    {
        GameEventsManager.Instance.miscEvents.onBerryCollected += BerryCollected;// call in BerryCollect.cs
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.miscEvents.onBerryCollected -= BerryCollected;
    }

    private void BerryCollected()
    {
        if (_berriesCollected < _berriesToComplete)
        {
            _berriesCollected++;
            UpdateState();
        }
        if (_berriesCollected >= _berriesToComplete)
        {
            FinishQuestStep();
        }
        GameEventsManager.Instance.uiEvents.BerriesChanged(_berriesCollected, _berriesToComplete);
    }
    private void UpdateState()
    {
        //QuestGoalsUI.UpdateBerryUI(_berriesCollected, _berriesToComplete);

        string state = _berriesCollected.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this._berriesCollected = System.Int32.Parse(state);
        UpdateState();
    }
}
