using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class Pause_menu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject PauseMenu;
    public AudioMixerGroup Mixer;

    private void Start()
    {
        transform.parent.Find("PauseMenu").GetChild(0).GetComponentInChildren<Toggle>().isOn  = PlayerPrefs.GetInt("MusicEnabled",1) == 1;
        transform.parent.Find("PauseMenu").GetChild(0).GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("MasterVolume", 1);
        transform.parent.Find("PauseMenu").gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Continue();
            }
            else
            {
                OnClickPause();
            }
        }
    }
    public void Continue()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void OnClickPause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameMenu");
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void ToggleMusic(bool enabled)
    {
        if (enabled)
            Mixer.audioMixer.SetFloat("MusicVolume", 0);
        else
            Mixer.audioMixer.SetFloat("MusicVolume", -80);
        PlayerPrefs.SetInt("MusicEnabled", enabled ? 1 : 0);
    }

    public void ChangeVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, volume));
        PlayerPrefs.SetFloat("MasterVolume",volume);
    }



}
