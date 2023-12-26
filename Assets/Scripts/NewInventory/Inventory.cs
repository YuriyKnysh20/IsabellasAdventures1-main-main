using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<AssetItem> Items;
    [SerializeField] private List<ItemsData> _itemsDatas;//assets/entities/items
    [SerializeField] private InventoryCell _inventoryCellTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _draggingparent;
    [SerializeField] private Bag _bag;
    //[SerializeField] private ItemsEjector _ejector;
    private void OnEnable()
    {
        Render(Items, _itemsDatas);
       LoadInventory();
    }

    private void OnDisable()
    {
        SaveInventory();
    }
    private void SaveInventory()
    {
        SaveManager.Instance.SaveInventory(Items, _itemsDatas);
    }

    private void LoadInventory()
    {
        SaveManager.Instance.LoadInventory(out Items, out _itemsDatas);
    }
    //public void RenderItems(List<ItemsData> itemsDatas)
    //{
    //    foreach (Transform child in _container)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    foreach (var itemData in itemsDatas)
    //    {
    //        var cell = Instantiate(_inventoryCellTemplate, _container);
    //        cell.Init(_draggingparent);
    //        cell.Renderer(itemData);

    //        cell.Ejecting += () => Destroy(cell.gameObject);// если произошел инжектинг мы удаляем этот квадратик на сцене 
    //        //cell.Ejecting += () => _ejector.EjectFromPool(item, _ejector.transform.position, _ejector.transform.right);
    //    }
    //}
    public void Render(List<AssetItem> items, List<ItemsData> itemsDatas)
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in items)
        {
            var cell = Instantiate(_inventoryCellTemplate, _container);
            cell.Init(_draggingparent, _bag);
            cell.Render(item);

            cell.Ejecting += () => Destroy(cell.gameObject);// если произошел инжектинг мы удаляем этот квадратик на сцене 
            //cell.Ejecting += () => _ejector.EjectFromPool(item, _ejector.transform.position, _ejector.transform.right);
        }
        foreach (var itemData in itemsDatas)
        {
            var cell = Instantiate(_inventoryCellTemplate, _container);
            cell.Init(_draggingparent, _bag);
            cell.Renderer(itemData);

            cell.Ejecting += () => Destroy(cell.gameObject);// если произошел инжектинг мы удаляем этот квадратик на сцене 
            //cell.Ejecting += () => _ejector.EjectFromPool(item, _ejector.transform.position, _ejector.transform.right);
        }

    }
}
