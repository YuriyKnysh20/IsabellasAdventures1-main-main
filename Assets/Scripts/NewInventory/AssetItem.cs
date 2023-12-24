using UnityEngine;

[CreateAssetMenu(menuName = "NewItem")]
public class AssetItem : ScriptableObject, IItem
{
    public string Name => _name;

    public Sprite UIIcon => _uiIcon;
    public int CurrentCount => _currentCount;

    public int MaxCount => _maxCount;

    [SerializeField] private string _name;
    [SerializeField] private Sprite _uiIcon;
    [SerializeField] private int _currentCount;
    [SerializeField] private int _maxCount;
}
