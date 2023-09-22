using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class LevelWindow : MonoBehaviour
{
    private Text levelText;
    private Image experienceBarImage;
    private LevelSystem levelSystem;



    private void Awake()
    {
        levelText = transform.Find("levelText").GetComponent<Text>();
        experienceBarImage = transform.Find("experienceBar").Find("bar").GetComponent<Image>();

        transform.Find("experience5Btn").GetComponent<Button_UI>().ClickFunc = () => levelSystem.AddExperience(5);
        transform.Find("experience50Btn").GetComponent<Button_UI>().ClickFunc = () => levelSystem.AddExperience(50);
        transform.Find("experience500Btn").GetComponent<Button_UI>().ClickFunc = () => levelSystem.AddExperience(500);
    }
    private void SetExperienceBarSize(float experienceNormalized)

    {
        experienceBarImage.fillAmount = experienceNormalized;
    }
    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = "LEVEL \n" +( levelNumber + 1);

    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
        SetLevelNumber(levelSystem.GetLevelNumber());
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());

        levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevelNumber(levelSystem.GetLevelNumber());
    }

    private void LevelSystem_OnExperienceChanged(object sender,System.EventArgs e)
    {
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());

    }


    
}
