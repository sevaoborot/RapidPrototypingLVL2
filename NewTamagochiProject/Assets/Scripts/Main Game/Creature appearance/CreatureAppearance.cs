using UnityEngine;
using System.Linq;

public class CreatureAppearance : MonoBehaviour
{
    [SerializeField] private ShopContent _content;
    [SerializeField] private SpriteRenderer _creaturesHead;
    [SerializeField] private SpriteRenderer _creaturesColor;

    public void OnInitialize(ShopAndSkinsData shopAndSkinsData)
    {
        //тут из хедайтема передаем нужные значения
        _creaturesColor.color = _content.BodyColors.FirstOrDefault(item => item.bodyColorType == shopAndSkinsData.SelectedBodycolorItem).bodyColor;
    }
}
