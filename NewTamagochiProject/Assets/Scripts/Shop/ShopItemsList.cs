using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemsList: MonoBehaviour
{
    public event Action<ShopItemView> ShopItemViewClicked;

    private List<ShopItemView> _shopItems;

    [SerializeField] private ShopItemView _shopItemPrefab;
    [SerializeField] private Transform _shopItemsParent;

    private OpenedSkinsChecker _openedSkinChecker;
    private SelectedSkinChecker _selectedSkinChecker;

    //мб можно сделать кастомный пул для оптимизации, чтоб не надо было всё пересоздавать

    public void OnInitialize(OpenedSkinsChecker openedSkinsChecker, SelectedSkinChecker selectedSkinChecker)
    {
        _shopItems = new List<ShopItemView>();
        _selectedSkinChecker = selectedSkinChecker;
        _openedSkinChecker = openedSkinsChecker;
    }

    public void ShowItems(IEnumerable<ShopItem> items)
    {
        foreach (ShopItem item in items)
        {
            ShopItemView itemView = GetItem(item, _shopItemsParent);
            itemView.Click += OnItemClick;

            //всё по необходімости анселектим, анхайлайтим, локаем

            itemView.UnSelect();
            itemView.Lock();

            if (item is IItem visitorItem)
            {
                visitorItem.Accept(_openedSkinChecker);
                visitorItem.Accept(_selectedSkinChecker);
            }
            if (_openedSkinChecker.IsOpened)
            {
                if (_selectedSkinChecker.IsSelected)
                {
                    itemView.Select();
                    ShopItemViewClicked?.Invoke(itemView);
                }
                itemView.Unlock();
            }
            _shopItems.Add(itemView);
        }
    }

    public void ClearItems()
    {
            foreach (ShopItemView item in _shopItems)
            { 
                //отключить хуйню, висящую на иконках
                Destroy(item.gameObject);
            }
            _shopItems.Clear();
    }

    private ShopItemView GetItem(ShopItem shopItem, Transform parentTransform)
    {
        ShopItemView instance = Instantiate(_shopItemPrefab, parentTransform);
        instance.OnInitialize(shopItem);
        return instance;
    }

    private void OnItemClick(ShopItemView item)
    {

    }
}
