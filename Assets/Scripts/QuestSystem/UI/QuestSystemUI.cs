using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystemUI : MonoBehaviour
{
    [SerializeField] private GameObject _questSystemUI;//++ pokazat skrit panel
   
    [SerializeField] private GameObject KillGoalString;
    [SerializeField] private GameObject CollectGoalString;
    [SerializeField] private Image CompleteKillGoalImg;//++
    [SerializeField] private Image CompleteCollectGoalImg;//++
    [SerializeField] private Text KillGoalTxt;//++
    [SerializeField] private Text CollectGoalTxt;//++
    [SerializeField] private Text GoldlTxt;//++
    [SerializeField] private Text ExpTxt;//++
    private void OnEnable()
    {
        QuestEvents.OnAssignUiRewards += CurrentRewardsForQuest;
        QuestEvents.IsAmountUpdate += UpdateUIAmount;
    }
    private void OnDisable()
    {
        QuestEvents.OnAssignUiRewards -= CurrentRewardsForQuest;
        QuestEvents.IsAmountUpdate -= UpdateUIAmount;
    }
    private void CurrentRewardsForQuest(string QuestName, int ExperienceReward, int GoldReward)
    {
        ExpTxt.text = ExperienceReward.ToString();
        GoldlTxt.text = GoldReward.ToString();
        #region ��������� ������� � ����� ������
        if (QuestName == "Collect Berries")
        {
            CollectGoalString.SetActive(true);
            KillGoalString.SetActive(false);
        }
        if (QuestName == "Kill Enemies")
        {
            CollectGoalString.SetActive(false);
            KillGoalString.SetActive(true);
        }
        #endregion
    }
    public void UpdateUIAmount(int currentamout, int requiredamount)
    {
        KillGoalTxt.text = "����� " + currentamout + " �� " + requiredamount;

        CollectGoalTxt.text = "������� ����  " + currentamout + " �� " + requiredamount;

        if (currentamout == requiredamount)
        {
            CompleteKillGoalImg.enabled = true;
            CompleteCollectGoalImg.enabled = true;
        }
    }
    public void ShowPanel(bool ShowPanel)
    {
        _questSystemUI.SetActive(ShowPanel);
    }
}
