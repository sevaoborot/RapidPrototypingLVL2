﻿using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopCategoryButton _bodyColorCategoryButton;
    [SerializeField] private ShopCategoryButton _headCategoryButton;
    [SerializeField] private ShopItemsList _itemsList;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [Space]
    [SerializeField] private ShopContent _shopContent;

    private bool _isSubscribedOnBodyColorButton = false;
    private bool _isSubscribedOnHeadButton = false;
    private OpenedSkinsChecker _openedSkinChecker;
    private SelectedSkinChecker _selectedSkinChecker;
    private SkinSelector _skinSelector;
    private SkinUnlocker _skinUnlocker;
    private ShopAndSkinsData _data;

    public void OnInitialize(OpenedSkinsChecker openedSkinsChecker, SelectedSkinChecker selectedSkinChecker, SkinSelector skinSelector, SkinUnlocker skinUnlocker, ShopAndSkinsData data)
    {
        _data = data;
        _coinsText.text = _data.Coins.ToString();
        _bodyColorCategoryButton.OnInitialize();
        _headCategoryButton.OnInitialize();
        _bodyColorCategoryButton.Click += OnBodyColorCategoryClick;
        _isSubscribedOnBodyColorButton = true;
        _headCategoryButton.Click += OnHeadColorCategoryClick;
        _isSubscribedOnHeadButton = true;
        _itemsList.OnInitialize(openedSkinsChecker, selectedSkinChecker);
        _itemsList.ShopItemViewClicked += OnItemViewClicked;
        _skinSelector = skinSelector;
        _selectedSkinChecker = selectedSkinChecker;
        _openedSkinChecker = openedSkinsChecker;
        _skinUnlocker = skinUnlocker;
        OnBodyColorCategoryClick();
    }

    private void OnEnable()
    {
        if (!_isSubscribedOnBodyColorButton)
        {
            _bodyColorCategoryButton.Click += OnBodyColorCategoryClick;
            _isSubscribedOnBodyColorButton = true;
        }
        if (!_isSubscribedOnHeadButton)
        {
            _headCategoryButton.Click += OnHeadColorCategoryClick;
            _isSubscribedOnHeadButton = true;
        }
    }

    private void OnDisable()
    {
        _bodyColorCategoryButton.Click -= OnBodyColorCategoryClick;
        _isSubscribedOnBodyColorButton = false;
        _headCategoryButton.Click -= OnHeadColorCategoryClick;
        _isSubscribedOnHeadButton = false;
        _itemsList.ShopItemViewClicked -= OnItemViewClicked;
    }

    private void OnBodyColorCategoryClick()
    {
        _bodyColorCategoryButton.Select();
        _headCategoryButton.Unselect();
        _itemsList.ClearItems();
        _itemsList.ShowItems(_shopContent.BodyColors);
    }

    private void OnHeadColorCategoryClick()
    {
        _headCategoryButton.Select();
        _bodyColorCategoryButton.Unselect();
        _itemsList.ClearItems();
        _itemsList.ShowItems(_shopContent.HeadItems);
    }

    private void OnItemViewClicked(ShopItemView itemView)
    {
        IShopItem visitorItem = itemView.Item as IShopItem;
        visitorItem.Accept(_openedSkinChecker);
        visitorItem.Accept(_selectedSkinChecker);
        if (_openedSkinChecker.IsOpened)
        {
            if (_selectedSkinChecker.IsSelected)
            {
                Debug.Log("Вы нажали на выбранный скин. Этот скин сейчас установлен как основной");
            }
            visitorItem.Accept(_skinSelector);
            _itemsList.Select(itemView);
        } else
        {
            visitorItem.Accept(_skinUnlocker);
            visitorItem.Accept(_openedSkinChecker);
            if (_openedSkinChecker.IsOpened) itemView.Unlock();
            _coinsText.text = _data.Coins.ToString();
        }
    }
}
