using UnityEngine;

[System.Serializable]
public abstract class ItemSO : ScriptableObject
{
    [Range(1f, 50f)]
    public float itemValue;

    public abstract void UpdateNeed(CreatureNeedsChange needsChange);
}
