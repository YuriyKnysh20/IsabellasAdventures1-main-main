using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

[RequireComponent(typeof(BoxCollider2D))]
public class LevelGoals : MonoBehaviour
{
    [SerializeField] private Image _LevelPassMark;
    [SerializeField] private Image _PapersCompleteMark;
    [SerializeField] private Image _QuestCompleteMark;
    [SerializeField] private GameObject _ShowLevelProgress;
    [SerializeField] private GameObject _BackgroundLevelGoals;
    [SerializeField] private Button _GoToNextLevelButton;
    [SerializeField] private Button _ReturnToLvllButton;

    [SerializeField] private bool _isLevelPassed = true;// 50% probezhal lvl, always true for UI
    [SerializeField] private bool _isPapersCompleted = false;
    [SerializeField] private bool _isquestcompleted = false;

    private const float LevelPassPercentage = 50f;// if player only probezhal lvl
    private const float OneMoreTaskCompleted = 25f;// if you complete next goal +25%
    [SerializeField] private Text _progressText;
    private void CheckGoalsForUI()
    {
        _LevelPassMark.enabled = _isLevelPassed;
        _PapersCompleteMark.enabled = _isPapersCompleted;
        _QuestCompleteMark.enabled = _isquestcompleted;
    }
    public void HidePanel()// called in unity if user click on _ReturnToLvllButton
    {
        _BackgroundLevelGoals.SetActive(false);
    }
    public void GoToNextLevel()// If user called In this button BackgroundLevelGoals been Destroyed and player can go to Next Level.
    {
        Destroy(_BackgroundLevelGoals);
        ShowLevelProgress();// show lvl progress
        StartCoroutine(WaitForNextLvl());//
    }
    private IEnumerator WaitForNextLvl()
    {
        yield return new WaitForSeconds(3);
        _ShowLevelProgress.SetActive(false);
    }
    private void ShowLevelProgress() 
    {
        UpdateProgressUI(_isLevelPassed, _isPapersCompleted, _isquestcompleted);
        _ShowLevelProgress.SetActive(true);
    }
    private void UpdateProgressUI(bool isLevelPassed, bool isPapersCompleted, bool isQuestCompleted)
    {
        if (isLevelPassed && isPapersCompleted == false && isQuestCompleted == false)
        {
            _progressText.text = $"Level Proiden, grac \n{LevelPassPercentage}%";
        }
        else if (isPapersCompleted && isQuestCompleted == false)
        {
            _progressText.text = $"You collect all Papers\n {LevelPassPercentage + OneMoreTaskCompleted}%!";
        }
        else if (isQuestCompleted && isPapersCompleted == false)
        {
            _progressText.text = $" You Help with quest, but you dont collect all Papers \n{LevelPassPercentage + OneMoreTaskCompleted}%!";
        }
        else
        {
            _progressText.text = $"All Goals Completed! {LevelPassPercentage + OneMoreTaskCompleted}%";
        }
    }
    public void SetPapersCompleted(bool value)
    {
        _isPapersCompleted = value;
    }
    public void SetQuestCompleted(bool value)
    {
        _isquestcompleted = value;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player enter collision");
            CheckGoalsForUI();// check goals and mark complete not complete before show UI
            _BackgroundLevelGoals.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_BackgroundLevelGoals != null)
                _BackgroundLevelGoals.SetActive(false);
        }
    }
}
