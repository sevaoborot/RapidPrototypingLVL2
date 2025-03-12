using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My Assets/Dialouges Data")]
public class SO_Dialouges : ScriptableObject
{
    [SerializeField] private DialougesData[] _dialougesData;

    public IReadOnlyList<DialougesData> DialougesData => _dialougesData;
}

[System.Serializable]
public struct DialougesData
{
    [Multiline(4)]
    public string dialougeText;
    public Sprite dialougeNPCSprite;
}