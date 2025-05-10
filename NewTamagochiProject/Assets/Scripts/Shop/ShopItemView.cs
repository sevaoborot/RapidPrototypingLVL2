using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ShopItemView> Click;

    [SerializeField] private GameObject _itemSelected;
    [SerializeField] private Image _itemPreview;
    [SerializeField] private TextMeshProUGUI _itemPrice;
    [SerializeField] private Image _itemLock;

    public void OnInitialize(ShopItem item)
    {
        _itemPreview.sprite = item.itemIcon;
        _itemPrice.text = item.price.ToString();
        this.Lock();
    }

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

    public void Lock() => _itemLock.gameObject.SetActive(true);
    public void Unlock() => _itemLock.gameObject.SetActive(false);

    public void Select()=> _itemSelected.SetActive(true);
    public void UnSelect() => _itemSelected.SetActive(false);
}
