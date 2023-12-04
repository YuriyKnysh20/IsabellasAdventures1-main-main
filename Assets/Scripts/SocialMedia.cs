using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SocialNetworks : MonoBehaviour
{
    public void GoDiscord()
    {
        Application.OpenURL("http://discord.com/");
    }
    public void GoYoutube()
    {
        Application.OpenURL("https://www.youtube.com/");
    }
    public void GoDiscord2()
    {
        Application.OpenURL("http://discord.com/");
    }
    public void GoSomeLink()
    {
        Application.OpenURL("http://SomeLink.com/");
    }
    public void OnContinueClick()
    {
        SceneManager.LoadScene(9);
    }
}
