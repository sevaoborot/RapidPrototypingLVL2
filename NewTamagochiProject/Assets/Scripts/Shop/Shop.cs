using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopCategoryButton _bodyColorCategoryButton;
    [SerializeField] private ShopCategoryButton _headCategoryButton;
    [SerializeField] private ShopItemsList _itemsList;
    [Space]
    [SerializeField] private ShopContent _shopContent;

    private bool _isSubscribedOnBodyColorButton = false;
    private bool _isSubscribedOnHeadButton = false;

    public void OnInitialize()
    {
        _bodyColorCategoryButton.OnInitialize();
        _headCategoryButton.OnInitialize();
        _bodyColorCategoryButton.Click += OnBodyColorCategoryClick;
        _isSubscribedOnBodyColorButton = true;
        _headCategoryButton.Click += OnHeadColorCategoryClick;
        _isSubscribedOnHeadButton = true;
        OnBodyColorCategoryClick();
        _itemsList.ShowItems(_shopContent.BodyColors);
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
    }

    private void OnBodyColorCategoryClick()
    {
        _bodyColorCategoryButton.Select();
        _headCategoryButton.Unselect();
    }

    private void OnHeadColorCategoryClick()
    {
        _headCategoryButton.Select();
        _bodyColorCategoryButton.Unselect();
    }
}
