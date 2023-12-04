using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    /*Данный метод должен работать когда игра пройдена,при нажатии на кнопку должна не начинаться игра,а перекидывать в новосозданную сцену
    CharacterChoiceScene,она ещё не готова!*/
    /*public void LoadCharacterChoiceScreen()
    {
        SceneManager.LoadScene("CharacterChoiceScene");
    }*/

    /*public void LoadTo(int level)
    {
        SceneManager.LoadScene(level);
    }*/
    public GameObject PanelControlls;// for saving quest
    public Savingcontrolchoice savingControlChoice;// for saving Control
    public void ReturnToPouseMenu()// calling when user clicked Nazad v vibore upravlenia
    {
        PanelControlls.SetActive(false);
        ChooseControl();
    }
    public void ChooseControl()
    {
        savingControlChoice.SaveControlSetting();// for saving Control 
        Debug.Log("SaveControlSetting");
    }
    public void OnClickLevels()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}