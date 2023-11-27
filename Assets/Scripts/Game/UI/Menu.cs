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