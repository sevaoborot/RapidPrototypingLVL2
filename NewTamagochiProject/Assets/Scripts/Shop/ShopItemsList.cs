using System.Collections.Generic;
using UnityEngine;

public class ShopItemsList: MonoBehaviour
{
    private List<ShopItemView> _shopItems;

    [SerializeField] private ShopItemView _shopItemPrefab;
    [SerializeField] private Transform _shopItemsParent;

    //мб можно сделать кастомный пул для оптимизации, чтоб не надо было всё пересоздавать

    public void OnInitialize()
    {
        _shopItems = new List<ShopItemView>();
    }

    public void ShowItems(IEnumerable<ShopItem> items)
    {
        foreach (ShopItem item in items)
        {
            ShopItemView itemView = GetItem(item, _shopItemsParent);

            //тут всякая хуня какую надо сделать с иконками

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
}
