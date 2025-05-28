using UnityEngine;
using System.Linq;

public class CreatureAppearance : MonoBehaviour
{
    [SerializeField] private ShopContent _content;
    [SerializeField] private SpriteRenderer _creaturesHead;
    [SerializeField] private SpriteRenderer _creaturesColor;

    public void OnInitialize(ShopAndSkinsData shopAndSkinsData)
    {
        _creaturesHead.sprite = _content.HeadItems.FirstOrDefault(item => item.headType == shopAndSkinsData.SelectedHeadItem).item;
        _creaturesColor.color = _content.BodyColors.FirstOrDefault(item => item.bodyColorType == shopAndSkinsData.SelectedBodycolorItem).bodyColor;
    }
}
