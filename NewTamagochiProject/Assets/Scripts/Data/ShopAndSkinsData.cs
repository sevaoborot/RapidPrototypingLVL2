using System;
using System.Collections.Generic;

[System.Serializable]
public class ShopAndSkinsData 
{
    private List<HeadItemEnum> _unlockedHeadItems;
    private List<BodyColorItemEnum> _unlockedBodycolorItems;

    private HeadItemEnum _selectedHeadItem;
    private BodyColorItemEnum _selectedBodycolorItem;

    public IEnumerable<HeadItemEnum> UnlockedHeadItems => _unlockedHeadItems;
    public IEnumerable<BodyColorItemEnum> UnlockedBodyColorItemEnums => _unlockedBodycolorItems;

    public HeadItemEnum SelectedHeadItem
    {
        get => _selectedHeadItem;
        set
        {
            if (_unlockedHeadItems.Contains(value) == false) throw new ArgumentException(nameof(value));
            _selectedHeadItem = value;
        }
    }

    public BodyColorItemEnum SelectedBodycolorItem
    {
        get => _selectedBodycolorItem;
        set
        {
            if (_unlockedBodycolorItems.Contains(value) == false) throw new ArgumentException(nameof(value));
            _selectedBodycolorItem = value;
        }
    }

    public ShopAndSkinsData()
    {
        _selectedBodycolorItem = BodyColorItemEnum.Color001;
        _selectedHeadItem = HeadItemEnum.Head001;

        _unlockedBodycolorItems = new List<BodyColorItemEnum>() { _selectedBodycolorItem };
        _unlockedHeadItems = new List<HeadItemEnum> { _selectedHeadItem };
    }

    public void OpenBodyColorItem(BodyColorItemEnum bodyColorItem)
    {
        if (_unlockedBodycolorItems.Contains(bodyColorItem)) throw new ArgumentException(nameof(bodyColorItem));
        _unlockedBodycolorItems.Add(bodyColorItem);
    }

    public void OpenHeadItem(HeadItemEnum headItem)
    {
        if (_unlockedHeadItems.Contains(headItem)) throw new ArgumentException(nameof(headItem));
        _unlockedHeadItems.Add(headItem);
    }
}
