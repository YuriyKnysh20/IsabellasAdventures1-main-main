using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageManager : MonoBehaviour
{
    public Dropdown languageDropdown;
    void Start()
    {        // Добавьте слушатель изменения значения Dropdown
        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
    }

    void OnLanguageChanged(int index)
    {
        // Получаем выбранный язык
        string selectedLanguage = languageDropdown.options[index].text;

        // Здесь  добавить код для смены языка в вашем приложении
        // Например, использовать Localization систему или изменить тексты вручную
        Debug.Log("Selected language: " + selectedLanguage);
    }
}
