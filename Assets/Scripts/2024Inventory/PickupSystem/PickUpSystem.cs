using Inventory.Model;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        //берем у колиззиии компонент ИТЕМ
        Item item = collision.GetComponent<Item>();
        
        if (item != null)// если у колизии есть компонент айтем
        {
            Debug.Log("Enter colission");
            // проверяем остаток,добавляем этот айтем и кол-во нам вернет кол-во
            // если мы добавляем элемент и вернуло 0, то все предметы добавлены
            // если же кол-во айтема 5(или больше не важно ) и мы не  можем его добавить
            // то остаток айтема сохраняем через значение которое вернула функция аддайтем(она возвращает остаток)
            int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            if (reminder == 0)
            {
                Debug.Log(" calling DestroyItem");
                item.DestroyItem();
            }


            else
                item.Quantity = reminder;
        }
    }
}