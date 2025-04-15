using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShopItemsList: MonoBehaviour
{
    private List<ShopItemView> _shopItems;

    [SerializeField] private ShopItemView _shopItemPrefab;
    [SerializeField] private Transform _shopItemsParent;

    public void ShowItems(IEnumerable<ShopItem> items)
    {
        foreach (ShopItem item in items)
        {
            ShopItemView itemView = GetItem(item, _shopItemsParent);
        }
    }

    private ShopItemView GetItem(ShopItem shopItem, Transform parentTransform)
    {
        ShopItemView instance = Instantiate(_shopItemPrefab, parentTransform);
        instance.OnInitialize(shopItem);
        return instance;
    }
}
