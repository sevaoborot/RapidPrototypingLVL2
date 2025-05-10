using UnityEngine;

[CreateAssetMenu(menuName = "Asset/Shop/Head")]
public class HeadItem : ShopItem, IItem
{
    [field: SerializeField] public Sprite item { get; private set; }
    [field: SerializeField] public HeadItemEnum headType { get; private set; }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
