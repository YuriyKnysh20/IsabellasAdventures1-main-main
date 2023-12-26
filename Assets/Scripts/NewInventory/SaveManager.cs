using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class InventoryData
{
    public List<AssetItem> items;
    public List<ItemsData> itemsDatas;
}
public class ExperienceData
{
    public int level;
    public int experience;
}
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveInventory(List<AssetItem> items, List<ItemsData> itemsDatas)
    {
        InventoryData inventoryData = new InventoryData
        {
            items = items,
            itemsDatas = itemsDatas
        };

        string json = JsonUtility.ToJson(inventoryData);
        PlayerPrefs.SetString("InventoryData", json);
        PlayerPrefs.Save();
    }

    public void LoadInventory(out List<AssetItem> items, out List<ItemsData> itemsDatas)
    {
        items = new List<AssetItem>();
        itemsDatas = new List<ItemsData>();

        if (PlayerPrefs.HasKey("InventoryData"))
        {
            string json = PlayerPrefs.GetString("InventoryData");
            InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(json);

            items = inventoryData.items;
            itemsDatas = inventoryData.itemsDatas;
        }
    }

    public void SaveLevelProgress(int level, int exp)
    {
        ExperienceData expData = new ExperienceData
        {
            level = level,
            experience = exp
        };

        string json = JsonUtility.ToJson(expData);
        PlayerPrefs.SetString("ExperienceData", json);
        PlayerPrefs.Save();
    }
    public void LoadLevelProgress(int level, int exp)
    {
        if (PlayerPrefs.HasKey("ExperienceData"))
        {
            string json = PlayerPrefs.GetString("ExperienceData");
            ExperienceData experienceData = JsonUtility.FromJson<ExperienceData>(json);

            level = experienceData.level;
            exp = experienceData.experience;
        }
    }

}
