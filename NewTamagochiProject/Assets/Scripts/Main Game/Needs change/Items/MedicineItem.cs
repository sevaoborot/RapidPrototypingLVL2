using UnityEngine;

[CreateAssetMenu(menuName = "Asset/Dropable Item/Medicine")]
public class MedicineItem : ItemSO
{
    public override void UpdateNeed(CreatureNeedsChange needsChange)
    {
        needsChange.OnMedicineDropped(itemValue);
    }
}
