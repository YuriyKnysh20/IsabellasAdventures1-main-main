using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGoalsUI : MonoBehaviour
{
    //[SerializeField] private GameObject _questGoalsUI;

    [SerializeField] private GameObject BerriesUIQuest;
    [SerializeField] private GameObject KillEnemiesUIQuest;
    [SerializeField] private GameObject KillBirdsUIQuest;
    [SerializeField] private GameObject QuestRewards;
    [SerializeField] private Text textBerries;
    [SerializeField] private Image QuestCompletedSprite;
    private void OnEnable()
    {
        GameEventsManager.Instance.uiEvents.onBerriesChanged += BerriesChanged;
    }
    private void OnDisable()
    {
        GameEventsManager.Instance.uiEvents.onBerriesChanged -= BerriesChanged;
    }

    private void BerriesChanged(int _berriesCollected, int _berriesToComplete)
    {
        textBerries.text = "Собери " + _berriesToComplete + " ягод:  " + _berriesCollected.ToString() + " из " + _berriesToComplete.ToString();
        if (_berriesCollected >= _berriesToComplete)
        {
            textBerries.text = "Собрать " + _berriesToComplete + " Ягод. Задание выполнено!" + "Вернись к Лорану, чтобы забрать награду";
            QuestCompletedSprite.enabled = true;
        }
    }
    private void Start()
    {
        //BerriesUIQuest.SetActive(false);
        KillEnemiesUIQuest.SetActive(false);
        KillBirdsUIQuest.SetActive(false);
    }
    public void ShowActiveQuests()
    {
        BerriesUIQuest.SetActive(true);
    }

}
