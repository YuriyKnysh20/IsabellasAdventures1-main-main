using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savingcontrolchoice : MonoBehaviour
{
    public GameObject arrowsControl;
    public GameObject joystickControl;

    private const string ControlPrefsKey = "ControlPrefsKey";
    private void Awake()
    {
        //arrowsControl.SetActive(true);
        //joystickControl.SetActive(false);
        LoadControlSetting();
    }

    public void SaveControlSetting()
    {
        int controlValue;
        if (arrowsControl.activeSelf)
        {
            controlValue = 1;
        }
        else
        {
            controlValue = 0;
        }
        PlayerPrefs.SetInt(ControlPrefsKey, controlValue);
        PlayerPrefs.Save();
    }

    public void LoadControlSetting()
    {
        if (PlayerPrefs.HasKey(ControlPrefsKey))
        {
            int controlValue = PlayerPrefs.GetInt(ControlPrefsKey);
            arrowsControl.SetActive(controlValue == 1);
            joystickControl.SetActive(controlValue == 0);
        }
    }
}

