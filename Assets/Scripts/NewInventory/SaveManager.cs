using System.Collections.Generic;
using UnityEngine;

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
        PlayerPrefs.SetString("Items", JsonUtility.ToJson(items));
        PlayerPrefs.SetString("ItemsDatas", JsonUtility.ToJson(itemsDatas));
        PlayerPrefs.Save();
    }

    public void LoadInventory(out List<AssetItem> items, out List<ItemsData> itemsDatas)
    {
        items = new List<AssetItem>();
        itemsDatas = new List<ItemsData>();

        if (PlayerPrefs.HasKey("Items"))
        {
            string itemsJson = PlayerPrefs.GetString("Items");
            items = JsonUtility.FromJson<List<AssetItem>>(itemsJson);
        }

        if (PlayerPrefs.HasKey("ItemsDatas"))
        {
            string itemsDatasJson = PlayerPrefs.GetString("ItemsDatas");
            itemsDatas = JsonUtility.FromJson<List<ItemsData>>(itemsDatasJson);
        }
    }
}
