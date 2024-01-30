using System;
using System.Collections.Generic;
using UnityEngine;
namespace Inventory.Model
{
    [CreateAssetMenu]
    public class HealItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField] private List<ModifierData> modifiersData = new List<ModifierData>();
        //создаем список модификаций
        public string ActionName => "Consume";
        [field: SerializeField] public AudioClip actionSFX { get; private set; }
        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            foreach (ModifierData data in modifiersData)
            {
                data.statModifier.AffectCharacter(character, data.value);
                //изменяем значения у персонажа которое мы передали,на значение которое нужно изменить 
            }
            return true;
        }
    }
    public interface IDestroyableItem
    {
    }
    public interface IItemAction
    {
        public string ActionName { get; }
        public AudioClip actionSFX { get; }
        bool PerformAction(GameObject character, List<ItemParameter> itemState);
    }

    [Serializable]
    public class ModifierData
    {
        public CharacterStatModifierSO statModifier;
        public float value;
    }
}