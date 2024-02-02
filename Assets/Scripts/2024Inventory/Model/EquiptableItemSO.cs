using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";
        [field: SerializeField] public AudioClip actionSFX { get; private set; }
        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            // берем у Перса компонент АгентВепон, проверяем на налл
            //устанавливаем оружие, this-передаем ссылку на предмет который можно снарядить на перса
            //проверяем если айтем стейт  равен налл, даем ему список параметров по умолчанию, иначе передаем текущее состояние оружия
            //возвращаем тру, чтобы знать снарядили мы предмет или нет, иначе возвращаем фолс.
            //Например если вернет фолс можно вывести в юай юзеру сообщение что персонаж не может экипировать этот предмет
            AgentWeapon weaponSystem = character.GetComponent<AgentWeapon>();
            if (weaponSystem != null)
            {
                weaponSystem.SetWeapon(this, itemState == null ?
                    DefaultParametersList : itemState);
                return true;
            }
            return false;
        }
    }
}