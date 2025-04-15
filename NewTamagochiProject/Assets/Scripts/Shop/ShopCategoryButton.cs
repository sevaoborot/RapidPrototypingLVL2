using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopCategoryButton : MonoBehaviour
{
    public event Action Click;

    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _unselectedColor;

    private Image _buttonImage;

    public void OnInitialize()
    {
        _buttonImage = GetComponent<Image>();
    }

    public void Select() => _buttonImage.color = _selectedColor;

    public void Unselect() => _buttonImage.color = _unselectedColor;

    public void OnClick() => Click?.Invoke();
}
