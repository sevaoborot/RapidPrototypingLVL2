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
        _itemsList.OnInitialize();
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
}
