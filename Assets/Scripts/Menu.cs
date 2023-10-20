using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _back;
    [SerializeField] private AudioSource _audio;
    
    private const string MusicVolume = "MusicVolume";

    private void Start()
    {
        if (!PlayerPrefs.HasKey(MusicVolume))
        {
            _audio.volume = 1;
        }
        else
            _audio.volume = PlayerPrefs.GetFloat(MusicVolume);

        _audio.Play();
    }
    
    private void Update()
        => _audio.volume = PlayerPrefs.GetFloat(MusicVolume);

    public void LoadCharacterChoiceScreen()
    {
        //characterScreen.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void LoadCharacterScreen()
    {
    }

    public void LoadTo(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Back()
    {
        _back.interactable = true;
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