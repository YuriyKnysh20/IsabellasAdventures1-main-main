using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private GameObject QuestSystem;
    private QuestSystem _questSystem;
    private bool _isTaskGiven;
    private void Start()
    {
        _questSystem = QuestSystem.GetComponent<QuestSystem>();
        _isTaskGiven = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_isTaskGiven == false)
            {
                Debug.Log("player colide quester");
                _questSystem.GenerateQuest();
                _isTaskGiven = true;
            }
            //Show UI dialogues
            // Debug.Log("Show UI dialogues");
            TakeTask();
        }
    }

    #region UI dialogues
    //Uipanel.SetActive=true;
    // ClickButton Yes,I take this task
    // TakeTask()
    #endregion
    private void TakeTask()
    {
        Debug.Log("TakeTaskCalled");
        //Uipanel.SetActive = false;
        _questSystem.ShowTask();
    }
    #region Generate Quest,Reward and quantity to killor collect

    #endregion
}
