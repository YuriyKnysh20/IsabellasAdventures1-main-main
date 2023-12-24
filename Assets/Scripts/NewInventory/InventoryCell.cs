using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using static UnityEditor.Progress;
using PlayerScripts.Mana;

public class InventoryCell : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public event Action Ejecting;

    [SerializeField] private Text _nameField;
    [SerializeField] private Text _countField;
    [SerializeField] private Image _iconField;
    [SerializeField] private Bag _bag;

    private Transform _draggingParent;
    private Transform _originalParent;
    public void Init(Transform draggingParent, Bag bag)// call in inventory
    {
        _draggingParent = draggingParent;
        _originalParent = transform.parent;
        _bag = bag;
    }
    public void Renderer(ItemsData itemsDatas)
    {
        _nameField.text = itemsDatas.TypeID.ToString();
        _iconField.sprite = itemsDatas.Prefab.GetComponent<SpriteRenderer>().sprite;
        _countField.text = _bag.MoneyCount.ToString();
    }
    public void Render(IItem item)
    {
        _nameField.text = item.Name;
        _iconField.sprite = item.UIIcon;
        _countField.text = item.CurrentCount.ToString() + " / " + item.MaxCount.ToString();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent = _draggingParent;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;// object move with mouse
    }
    public void OnEndDrag(PointerEventData eventData)
    {

        if (In((RectTransform)_originalParent))
            InsertInGrid();
        else
            Inject();
    }
    private bool In(RectTransform originalParent)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(originalParent, transform.position);
    }
    public void Inject()
    {
        Ejecting?.Invoke();
    }
    private void InsertInGrid()
    {
        #region
        //когда мышка опущена, то мы ищем ближайший обьект из инвентаря
        // ищем ближайший дочерний обьект, запоминаем его индекс и с помощью функции сетсиблингиндекс
        //мы ставим наш обьект по этому индексу
        //
        int closesIndex = 0;
        for (int i = 0; i < _originalParent.transform.childCount; i++)
        {
            if (Vector3.Distance(transform.position, _originalParent.GetChild(i).position) <
                            Vector3.Distance(transform.position, _originalParent.GetChild(closesIndex).position))
            {
                closesIndex = i;
            }
        }
        transform.parent = _originalParent;
        transform.SetSiblingIndex(closesIndex);
        #endregion
    }
}
