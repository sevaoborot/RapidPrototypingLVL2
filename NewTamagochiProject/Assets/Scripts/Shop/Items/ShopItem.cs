using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
    [field: SerializeField] public Sprite item { get; private set; }
    [field: SerializeField] public Sprite itemIcon { get; private set; }
    [field: SerializeField, Range(0,1000)] public int price {  get; private set; }
}
