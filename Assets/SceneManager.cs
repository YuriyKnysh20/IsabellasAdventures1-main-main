using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuSceneManager : MonoBehaviour
{
    [SerializeField] private Button Exit;
    [SerializeField] private Button Resume;

    /*private void OnEnable()
    {
        Time.timeScale = 0;

        Exit.onClick.AddListener(DoExit);
        Resume.onClick.AddListener(DoResume);
    }

    private void OnDisable()
    {
       Resume.onClick.RemoveListener(DoResume);
       Exit.onClick.RemoveListener(DoExit);
    }*/

    public void DoExit()
    {
        SceneManager.LoadScene(0); 
    }

    public void DoResume()
    {
        Time.timeScale = 1;
            
    }
}
