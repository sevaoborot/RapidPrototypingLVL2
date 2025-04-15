using UnityEngine;

[CreateAssetMenu(menuName = "Asset/Dropable Item/Food")]
public class FoodItem : ItemSO
{
    public override void UpdateNeed(CreatureNeedsChange needsChange)
    {
        needsChange.OnFoodDropped(itemValue);
    }
}
