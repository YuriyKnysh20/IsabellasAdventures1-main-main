using Inventory.Model;
using System.Collections.Generic;
using UnityEngine;
public class AgentWeapon : MonoBehaviour
{
    [SerializeField] private EquippableItemSO weapon;
    [SerializeField] private InventorySO inventoryData;// чтобы дать или снять снаряжение
    [SerializeField] private List<ItemParameter> parametersToModify, itemCurrentState;// создаем лист параметров для изменения и лист состояния предмета 
    public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        //проверяем оружие которое обьявлено как параметр на нулл
        // добавляем в инвентарь это оружие, передаем кол-во(единица), и его текущее состояние после использования
        if (weapon != null)
        {
            inventoryData.AddItem(weapon, 1, itemCurrentState);
        }
        //меняем с текущего оружия на оружие которое хотим взять
        // так же меняем текущее состояние(износ) предмета, заносим структуру ItemParameter в текущий лист itemCurrentState
        this.weapon = weaponItemSO;
        this.itemCurrentState = new List<ItemParameter>(itemState);
        ModifyParameters();
    }
    private void ModifyParameters()
    {
        //перебираем параметры в параметрах для модификации
        //проверяем текущее состояние элемента если оно содержит параметр
        // выше мы присвоили itemCurrentState текущее состояние предмета который нужно экипировать
        //мы собираемся проверить индекс равняется индексу текущему состоянию этого параметра
        //создаем NewValue добавляем к текущему состоянию элемента(например износ 80) значение параметра(например износ 5)
        //устанавливаем индекс текущего состояния предмета новому параметру(структура) 
        //в которой нужно присвоить параметру элемента ссылку на параметр из модификаций, взяв у него итем параметр.
        //Пример: В DefaultParameterList Durability(Износ) и новое значение которое мы высчитали выше
        foreach (var parameter in parametersToModify)
        {
            if (itemCurrentState.Contains(parameter))
            {
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].value + parameter.value;
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    value = newValue
                };
            }
        }
    }
}