using UnityEngine;

public class CreatureAppearance : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _creaturesHead;
    [SerializeField] private SpriteRenderer _creaturesColor;

    public void OnInitialize(ShopAndSkinsData shopAndSkinsData)
    {
        //тут из хедайтема передаем нужные значения
        //и то же делаем с цветом
    }
}
