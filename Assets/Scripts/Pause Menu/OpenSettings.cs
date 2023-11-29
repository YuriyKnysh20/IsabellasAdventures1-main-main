using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSettings : MonoBehaviour
{
    [SerializeField] private Transform _panelSettings;
    [SerializeField] private Savingcontrolchoice _savingControlChoice;
    private bool isEnabled = false;

    public void ButtonClick()
    {
        isEnabled = isEnabled == false;
        _panelSettings.gameObject.SetActive(isEnabled);

 #region For Save Control
        if (isEnabled)
        {
            _savingControlChoice.SaveControlSetting();
        }
        else
        {
            _savingControlChoice.LoadControlSetting();
        }
        #endregion
    }
}
