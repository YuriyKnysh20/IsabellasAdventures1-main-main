using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageManager : MonoBehaviour
{
    public Dropdown languageDropdown;
    void Start()
    {        // �������� ��������� ��������� �������� Dropdown
        languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
    }

    void OnLanguageChanged(int index)
    {
        // �������� ��������� ����
        string selectedLanguage = languageDropdown.options[index].text;

        // �����  �������� ��� ��� ����� ����� � ����� ����������
        // ��������, ������������ Localization ������� ��� �������� ������ �������
        Debug.Log("Selected language: " + selectedLanguage);
    }
}
