using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEditor.Progress;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private UI.UIInventoryPage inventoryUI;
        [SerializeField] private Model.InventorySO inventoryData;
        public List<InventoryItem> initialItems = new List<InventoryItem>();// дл€ заполнени€ начальных елементов
        [SerializeField] private AudioClip dropClip;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Canvas HudCanvas;
        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }
        public void OpenCloseInventory()
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
                HudCanvas.gameObject.SetActive(false);
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    // возвращает диктионари, кей=индекс айтема,валюэ структура(итем—ќ и кол-во)
                    inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                }
            }
            else
            {
                inventoryUI.Hide();
                HudCanvas.gameObject.SetActive(true);
            }

        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)// не размещаем пустые итемы
                    continue;
                inventoryData.AddItem(item);
            }
        }
        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage,
                    item.Value.quantity);
            }
        }
        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }
        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
                //л€мбда выражение означает что вызоветс€ метод когда мы нажмем эту кнопку
            }
            //ищем айтем который можно уничтожить
            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
                // на кнопке будет написано Drop и вызоветс€ метод выброса предмета 
            }
        }
        private void DropItem(int itemIndex, int quantity)
        {
            //удал€ем предмет инвентар€ по индексу указав его кол-во
            //так же отмен€ем выбор элемента в юай(скроет описание предмета и рамку что предмет выбран.
            inventoryData.RemoveItem(itemIndex, quantity);
            inventoryUI.ResetSelection();
            audioSource.PlayOneShot(dropClip);
        }
        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.itemState);
                audioSource.PlayOneShot(itemAction.actionSFX);
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    // если €чейка пуста€(там уже нет итема), снимаем выделение, скрываем панель действий, описание.
                    inventoryUI.ResetSelection();
            }
        }
        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)// если пустой мы не хотмм перет€гивать пустой предмет
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }
        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }
        private void HandleDescriptionRequest(int itemIndex)
        {
            //получаем инвентари айтем с модели
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage,
                item.name, description);
        }
        private string PrepareDescription(InventoryItem inventoryItem)
        {
            //append добавл€ет описание, передаем ему дескрипшн итема.
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();// создает новую строку
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                //получаем название параметра
                //itemstate.value состо€ние предмета, где валюе его дурабилити(долговечность)
                //дефолт параметрс показывает сколько стоит значение по дефолту
                // по итогу получаем строку примерно такую: "Durability 5/10"
                sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName} " +
                    $": {inventoryItem.itemState[i].value} / " +
                    $"{inventoryItem.item.DefaultParametersList[i].value}");
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}