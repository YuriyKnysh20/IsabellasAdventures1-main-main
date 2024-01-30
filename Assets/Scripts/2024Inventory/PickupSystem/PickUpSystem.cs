using Inventory.Model;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        //����� � ��������� ��������� ����
        Item item = collision.GetComponent<Item>();
        
        if (item != null)// ���� � ������� ���� ��������� �����
        {
            Debug.Log("Enter colission");
            // ��������� �������,��������� ���� ����� � ���-�� ��� ������ ���-��
            // ���� �� ��������� ������� � ������� 0, �� ��� �������� ���������
            // ���� �� ���-�� ������ 5(��� ������ �� ����� ) � �� ��  ����� ��� ��������
            // �� ������� ������ ��������� ����� �������� ������� ������� ������� ��������(��� ���������� �������)
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