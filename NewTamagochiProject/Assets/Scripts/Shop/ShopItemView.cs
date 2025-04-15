using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour
{
    private Image _itemImage;
    private TextMeshProUGUI _itemPrice;

    public void OnInitialize(ShopItem item)
    {
        _itemImage = GetComponent<Image>();
        _itemImage.sprite = item.itemIcon;
        _itemPrice = GetComponentInChildren<TextMeshProUGUI>();
        _itemPrice.text = item.price.ToString();
    }

    //public void ShowItems(IEnumerable<ShopItem> items)
    //{
    //    foreach (ShopItem item in items)
    //    {

    //    }
    //}
}
