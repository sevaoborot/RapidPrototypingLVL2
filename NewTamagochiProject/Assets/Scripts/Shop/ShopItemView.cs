using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour
{
    [SerializeField] private Image _lock;
    [SerializeField] private TextMeshProUGUI _itemPrice;
    private Image _itemImage;

    public void OnInitialize(ShopItem item)
    {
        _itemImage = GetComponent<Image>();
        _itemImage.sprite = item.itemIcon;
        _itemPrice.text = item.price.ToString();
        this.Lock();
    }

    public void Lock() => _lock.gameObject.SetActive(true);

    public void Unlock() => _lock.gameObject.SetActive(false);
}
