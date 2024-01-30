using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> inventoryItems;
        [field: SerializeField] public int Size { get; private set; } = 10;
        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;//для обновления юай, вызывается в InformAboutChange
        public void Initialize()
        {
            inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }
        public int AddItem(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            if (item.IsStackable == false)
            {
                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    // пока кол-во больше нуля и инвентарь не полный
                    // выполняем логику добавления предметов в инвентарь
                    while (quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(item, 1, itemState);
                    }
                    InformAboutChange();
                    return quantity;
                }
            }
            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;
        }
        private int AddItemToFirstFreeSlot(ItemSO item, int quantity, List<ItemParameter> itemState = null)
        {
            InventoryItem newItem = new InventoryItem//элемент который мы хочем добавить
            {
                item = item,
                quantity = quantity,
                itemState 
         = new List<ItemParameter>(itemState == null ? item.DefaultParametersList : itemState)
         //если состояние элемента налл, то возвращаем дефолтные значения
         //если не нул, то используем параметры описанные в айтемстейт и сохраняем их в лист
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                {// если слот пустой  вставляем в эту клетку елемент который хотим добавить
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }
        private bool IsInventoryFull()
        => inventoryItems.Where(item => item.IsEmpty).Any() == false;
        //получаем все предметы инвентаря где пустой айтем
        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue; // если слот пустой, итем не стакается, пропускаем итерацию цикл
                if (inventoryItems[i].item.ID == item.ID)
                {
                    // чек текущий элемент и если он равен итему который мы хотим застекать выполняем логику
                    int amountPossibleToTake =
        inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;
                    //99-70=29
                    //высчитали кол-во которые можем взять
                    //отняв от макс кол-ва количество которое уже имеем
                    //вычислили сколько предметов можно поместить в этот слот
                    if (quantity > amountPossibleToTake)
                    {
                        //если  кол-во которое нужно добавить больше чем кол-во которое можем взять
                        //то меняем кол-во в ячейке на макссайз который может поместиться в ячейке
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        // а если кол-во которое нужно добавить 10, а можем взять еще 90
                        // то добавляемв в эту ячейку 10 штук плюсуя их с теми что уже там были
                        // информируем об изменениях и возвращаем ноль.
                        inventoryItems[i] = inventoryItems[i]
                            .ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            // если же после добавления предметов в ячейку остались предметы и инвентарь не полный
            //вычисляем сколько можно добавить в новый слот
            // далее минусуем от кол-ва число которое добавлено в новую ячейку
            // и добавляем вычисленное кол-во в первый свободный слот.
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
        }
        public void RemoveItem(int itemIndex, int amount)
        {
            //проверяем есть ли этот индекс в нашем списке
            //например если пришел индекс 6, а у нас всего 5 элементов, то ничего не произойдет
            if (inventoryItems.Count > itemIndex)
            {
                if (inventoryItems[itemIndex].IsEmpty)// проверка пустой ли элемент, если элемент пустой, то мы его не можем выкинуть
                    return;
                // Например нужно удалить третий элемент, в кол-ве 5 штук. узнаем кол-во(к примеру их 70.70-5, ремайндер=65
                // проверяем кол-во оставшихся, если их меньше нуля создается пустая ячейка.
                // если больше нуля-меняем кол-во
                int reminder = inventoryItems[itemIndex].quantity - amount;
                if (reminder <= 0)
                    inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
                else
                    inventoryItems[itemIndex] = inventoryItems[itemIndex]
                        .ChangeQuantity(reminder);
                InformAboutChange();// оповещаем юай об изменениях
            }
        }
        public void AddItem(InventoryItem item)
        {
            AddItem(item.item, item.quantity);
        }
        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        //используем этот метод для обновления юай класса инвентари через контроллер
        {
            Dictionary<int, InventoryItem> returnValue =
                new Dictionary<int, InventoryItem>();

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }
        public InventoryItem GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }
        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            //передается к примеру инджекс 2 и 4,
            //присваиваем локальной переменной инв.айтем второй елемент из списка инвентори айтемс
            //ставим  второму элементу значение 4-того элемента из списка
            //ставим на четвертое место в списке элемент который был на втором месте.
            InventoryItem item1 = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            InformAboutChange();
        }
        private void InformAboutChange()
        {
            // нужно получить текущее состояние инвентаря,создаем словарь который имеет индексы
            //элементов которые изменились и инветари дата в целом.
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }
    [Serializable]
    public struct InventoryItem
    {
        public int quantity;
        public ItemSO item;
        public List<ItemParameter> itemState;
        public bool IsEmpty => item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
        {
            return new InventoryItem
            {
                item = this.item,
                quantity = newQuantity,
                itemState = new List<ItemParameter>(this.itemState)
            };
        }

        public static InventoryItem GetEmptyItem()
            => new()
            {
                item = null,
                quantity = 0,
                itemState = new List<ItemParameter>()
            };
    }
}