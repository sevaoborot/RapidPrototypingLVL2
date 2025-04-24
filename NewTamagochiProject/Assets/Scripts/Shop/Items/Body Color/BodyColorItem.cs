using UnityEngine;

[CreateAssetMenu(menuName = "Asset/Shop/Body Color")]
public class BodyColorItem : ShopItem
{
    [field: SerializeField] public Color bodyColor { get; private set; }
    [field: SerializeField] public BodyColorItemEnum bodyColorType {  get; private set; }
}
