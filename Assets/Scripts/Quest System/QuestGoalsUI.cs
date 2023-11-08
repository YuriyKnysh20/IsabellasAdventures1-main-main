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
    [SerializeField] private static Text textBerries;
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
    //public static void UpdateBerryUI(int _berriesCollected,int _berriesToComplete)
    //{

    //    textBerries.text = _berriesCollected+ "iz"+ _berriesToComplete; 
    //}
}
