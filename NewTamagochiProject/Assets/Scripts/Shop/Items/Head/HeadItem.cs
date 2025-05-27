using UnityEngine;

[CreateAssetMenu(menuName = "Asset/Shop/Head")]
public class HeadItem : ShopItem, IShopItem
{
    [field: SerializeField] public Sprite item { get; private set; }
    [field: SerializeField] public HeadItemEnum headType { get; private set; }

    public void Accept(IShopVisitor visitor)
    {
        visitor.Visit(this);
    }
}
