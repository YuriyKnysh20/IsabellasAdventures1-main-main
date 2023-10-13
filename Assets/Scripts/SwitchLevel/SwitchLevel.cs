using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SwitchLevel : MonoBehaviour
{
    [Header("Выбор типа ввода") ] [Tooltip("При выборе одного типа ввода - игнорируется другой")]
    [SerializeField] private Chooseinput _inputSelection;
    
    
    [Space(3)]
    [Header("Поля для ввода")]
    [SerializeField] private int _sceneIndex;
    [SerializeField] private string _sceneName;
    
    private Button _button;

    private void Start()
    {
        _button ??= GetComponent<Button>();
        _button.onClick.AddListener(Switch);
    }
    
    public void Switch()
    {
        if (_inputSelection == Chooseinput.inputString)
        {
            SceneManager.LoadScene(_sceneName);
            Debug.Log("string");
        }

        if (_inputSelection == Chooseinput.inputIndex)
        {
            SceneManager.LoadScene(_sceneIndex);
            Debug.Log("Index");
        }
    }
}

public enum Chooseinput
{
    inputString,
    inputIndex
} 