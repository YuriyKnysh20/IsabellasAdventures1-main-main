using UnityEngine;

[CreateAssetMenu(menuName = "NewItem")]
public class AssetItem : ScriptableObject, IItem
{
    public string Name => _name;

    public Sprite UIIcon => _uiIcon;

    [SerializeField] private string _name;
    [SerializeField] private Sprite _uiIcon;
}
