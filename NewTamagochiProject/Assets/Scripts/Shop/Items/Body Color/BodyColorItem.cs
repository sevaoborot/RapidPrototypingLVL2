using UnityEngine;

[CreateAssetMenu(menuName = "Asset/Shop/Body Color")]
public class BodyColorItem : ShopItem, IItem
{
    [field: SerializeField] public Color bodyColor { get; private set; }
    [field: SerializeField] public BodyColorItemEnum bodyColorType {  get; private set; }

    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
