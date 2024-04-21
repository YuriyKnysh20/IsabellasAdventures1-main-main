using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelWindow : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private Image experienceBarImage;
    private LevelSystem levelSystem;

    private void Start()
    {
        levelText.text = "Level " + (levelSystem.GetLevelNumber());
    }

    #region Save And Load Experience
    private void OnEnable()
    {
        LoadExperience();
    }
    public void LoadExperience()
    {
        SaveManager.Instance.LoadLevelProgress(levelSystem.Level, levelSystem.Experience);
    }

    private void OnDisable()
    {
        SaveExperience();
    }
    private void SaveExperience()
    {
        SaveManager.Instance.SaveLevelProgress(levelSystem.Level, levelSystem.Experience);
    }
    #endregion

    public void AddExp(int count)
    {
        levelSystem.AddExperience(count);
    }
    private void SetExperienceBarSize(float experienceNormalized)

    {
        experienceBarImage.fillAmount = experienceNormalized;
    }
    private void SetLevelNumber(int levelNumber)
    {
        levelText.text = "LEVEL \n" + (levelNumber + 1);
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
    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        SetExperienceBarSize(levelSystem.GetExperienceNormalized());
    }
}
