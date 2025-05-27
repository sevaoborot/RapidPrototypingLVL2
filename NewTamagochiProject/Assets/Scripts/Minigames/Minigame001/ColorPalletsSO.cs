using UnityEngine;

namespace minigame001
{
    [CreateAssetMenu(menuName = "Asset/Minigames/Color Pallets")]
    public class ColorPalletsSO : ScriptableObject
    {
        public ColorPallete[] easyPallets;
        public ColorPallete[] midPallets;
        public ColorPallete[] hardPallets;
    }

    [System.Serializable]
    public struct ColorPallete
    {
        public Color baseColor;
        public Color variantColor;
    }
}