using UnityEngine;

[CreateAssetMenu(menuName = "Asset/Shop/Head")]
public class HeadItem : ShopItem
{
    [field: SerializeField] public Sprite item { get; private set; }
    [field: SerializeField] public HeadItemEnum headType { get; private set; }
}
