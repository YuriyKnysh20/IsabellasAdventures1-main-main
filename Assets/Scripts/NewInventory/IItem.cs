using UnityEngine;

public interface IItem
{
    string Name { get; }
    Sprite UIIcon { get; }
    int CurrentCount { get; }
    int MaxCount { get; }
}
