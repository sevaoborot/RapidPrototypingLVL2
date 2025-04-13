using UnityEngine;

[CreateAssetMenu(menuName = "Asset/Minigames/Color Pallets")]
public class ColorPalletsSO : ScriptableObject
{
    public ColorPalette[] Pallets;
}

[System.Serializable]
public struct ColorPalette
{
    public Color baseColor;
    public Color variantColor;
}