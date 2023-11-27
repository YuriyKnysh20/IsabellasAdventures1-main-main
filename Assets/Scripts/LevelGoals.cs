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
    [SerializeField] private Image _allgoalscompletedMark;
    [SerializeField] private GameObject _ShowLevelProgress;
    [SerializeField] private GameObject _BackgroundLevelGoals;

    [SerializeField] private Button _GoToNextLevelButton;
    [SerializeField] private Button _ReturnToLvllButton;
    [SerializeField] private bool _isLevelPassed = true;// 50% probezhal lvl, always true for UI
    [SerializeField] private bool _isPapersCompleted= false;
    [SerializeField] private bool _isgoalscompleted=false;


    private void CheckGoalsForUI()
    {
        _LevelPassMark.enabled = _isLevelPassed;
        _PapersCompleteMark.enabled = _isPapersCompleted;
        _allgoalscompletedMark.enabled = _isgoalscompleted;
    }
    public void HidePanel()// called in unity if user click on _ReturnToLvllButton
    {
        _BackgroundLevelGoals.SetActive(false);
    }
    public void GoToNextLevel()// If user called In this button GoTonextLevelTrigger been Destroyed and player can go to Next Level.
    {
        Destroy(gameObject);
        ShowLevelProgress();// show 
        WaitForNextLvl();//
    }
    private IEnumerator WaitForNextLvl()
    {
        yield return new WaitForSeconds(3);
    }
    private void ShowLevelProgress()
    {
        _ShowLevelProgress.SetActive(true);
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
            _BackgroundLevelGoals.SetActive(false);
        }
    }
}
