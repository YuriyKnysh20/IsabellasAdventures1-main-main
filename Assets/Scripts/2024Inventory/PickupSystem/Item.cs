using Inventory.Model;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    [field: SerializeField] public ItemSO InventoryItem { get; private set; }
    [field: SerializeField] public int Quantity { get; set; } = 1;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float duration = 0.3f;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
        //получаем спрайт этого предмета
    }
    public void DestroyItem()
    {
        //когда подбираем предмет, выключаем колайдер запускаем корутину которая через
        //какое то время(duration) уничтожится
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickup());
    }
    private IEnumerator AnimateItemPickup()
    {
        //проигрываем звук подбора предмета, получаем начальный размер
        //берем нулевой размер чтобы далее его уменьшить в лерпе.
        //audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while (currentTime < duration)
        {
            //пока таймер не истечет(не станет 0), меняем размер 
            currentTime += Time.deltaTime;
            transform.localScale =
                Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        Destroy(gameObject);
    }
}