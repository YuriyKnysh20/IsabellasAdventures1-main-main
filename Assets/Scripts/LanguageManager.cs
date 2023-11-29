using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageManager : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    private const string LanguageKey = "SelectedLanguage";
    void Start()
    {
        int savedLanguageIndex = PlayerPrefs.GetInt(LanguageKey, 0);
        dropdown.value = savedLanguageIndex;
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private void OnDropdownValueChanged(int index)
    {
        PlayerPrefs.SetInt(LanguageKey, index);
        PlayerPrefs.Save();

    }
}

