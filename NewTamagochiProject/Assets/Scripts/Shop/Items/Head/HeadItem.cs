using UnityEngine;

[CreateAssetMenu(menuName = "Asset/Shop/Head")]
public class HeadItem : ShopItem
{
    [field: SerializeField] public HeadItemEnum headType { get; private set; }
}
