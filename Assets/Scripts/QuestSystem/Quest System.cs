using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] private float _mincoins;
    [SerializeField] private float _maxcoins;
    [SerializeField] private float _minXP;
    [SerializeField] private float _maxXP;

    [SerializeField] private GameObject _currentTask;
    [SerializeField] private GameObject _ManaBarFromPanelPlayer;
    [SerializeField] private Text QuestText;
    [SerializeField] private int _coinsReward;


    private QuestTest _questTest;


    private int _HPReward;
    private float _lvlMultiplier;
    private int _tabletCount;
    public int TabletCount
    {
        get; private set;
    }
    private int _enemyCount;
    [SerializeField] int _tabletLeftToCollect;
    public int TabletLeftToCollect
    {
        get; set;
    }
    [SerializeField] private int _enemiesLeftToKill;
    public int EnemiesLeftToKill
    {
        get; set;
    }
    private void Start()
    {
        _tabletCount = _tabletLeftToCollect;
        _enemyCount = _enemiesLeftToKill;
        _minXP *= _lvlMultiplier;
        _maxXP *= _lvlMultiplier;
        _mincoins *= _lvlMultiplier;
        _maxcoins *= _lvlMultiplier;
    }
    private void OnEnable()
    {
        _questTest.TabletChanged += OnValueChanged;
    }
    private void OnDisable()
    {
        _questTest.TabletChanged -= OnValueChanged;
    }
    private void OnValueChanged(int _tabletLeftToCollect, int _tabletCount)
    {
        QuestText.text = $"{"_tabletLeftToCollect" + _tabletLeftToCollect} / {_tabletCount}";
    }
    public void ShowTask()
    {        //Debug.Log("ShowTask");
        #region Take PanelPlayerTransform and Place our Task Under
        // _currentTask.transform.position = _ManaBarFromPanelPlayer.transform.position - Vector3.up * 30f;
        #endregion
       // QuestText.text = "Collect";
        _currentTask.SetActive(true);
    }
    public void GenerateQuest()
    {
        GenerateReward();
        GenerateQuantityOfEnemyKillQuest();
        GenerateQuantityOfCollectItemsQuest();
    }

    private void GenerateQuantityOfEnemyKillQuest()
    {
        // Get list of enemy in map or Instantiate enemies;
    }
    private void GenerateQuantityOfCollectItemsQuest()
    {
        //Get list of Items in map or Instantiate Items;
    }
    public void GenerateReward()
    {
        // Debug.Log("GenerateReward is called");
        _coinsReward = Convert.ToInt32(UnityEngine.Random.Range(_mincoins, _maxcoins));
        _HPReward = Convert.ToInt32(UnityEngine.Random.Range(_minXP, _maxXP));
    }
}
