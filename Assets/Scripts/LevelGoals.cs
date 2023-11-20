using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class LevelGoals : MonoBehaviour
{
    [SerializeField] Image _LevelPassMark; 
    [SerializeField] Image _PapersCompleteMark;
    [SerializeField] Image _allgoalscompletedMark;

    [SerializeField] bool _isLevelPassed;
    [SerializeField] bool _isPapersCompleted;
    [SerializeField] bool _isgoalscompleted;

    public void CheckGoals()
    {
        _LevelPassMark.enabled = _isLevelPassed;
        _PapersCompleteMark.enabled = _isPapersCompleted;
        _allgoalscompletedMark.enabled = _allgoalscompletedMark;
    }
}
