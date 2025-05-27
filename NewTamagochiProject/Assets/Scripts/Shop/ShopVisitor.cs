using System.Linq;

public interface IShopItem
{
    public void Accept(IShopVisitor visitor);
}

public interface IShopVisitor
{
    public void Visit(BodyColorItem item);
    public void Visit(HeadItem item);
}

public class SkinUnlocker : IShopVisitor
{
    private ShopAndSkinsData _data;

    public SkinUnlocker(ShopAndSkinsData data) => _data = data;

    public void Visit(BodyColorItem item)
    {
        if (_data.Coins >= item.price)
        {
            _data.Coins = _data.Coins - item.price;
            _data.OpenBodyColorItem(item.bodyColorType);
        }
    }

    public void Visit(HeadItem item)
    {
        if (_data.Coins >= item.price)
        {
            _data.Coins = _data.Coins - item.price;
            _data.OpenHeadItem(item.headType);
        }
    }
}

public class OpenedSkinsChecker : IShopVisitor
{
    public bool IsOpened { get; private set; }

    private ShopAndSkinsData _data;

    public OpenedSkinsChecker(ShopAndSkinsData data) => _data = data;

    public void Visit(BodyColorItem item)
        => IsOpened = _data.UnlockedBodyColorItems.Contains(item.bodyColorType);
    public void Visit(HeadItem item)
        => IsOpened = _data.UnlockedHeadItems.Contains(item.headType);
}

public class SelectedSkinChecker : IShopVisitor
{
    public bool IsSelected { get; private set; }

    private ShopAndSkinsData _data;

    public SelectedSkinChecker(ShopAndSkinsData data) => _data = data;

    public void Visit(BodyColorItem item)
        => IsSelected = _data.SelectedBodycolorItem == item.bodyColorType;
    public void Visit(HeadItem item)
        => IsSelected = _data.SelectedHeadItem == item.headType;
}

public class SkinSelector : IShopVisitor
{
    private ShopAndSkinsData _data;

    public SkinSelector(ShopAndSkinsData data) => _data = data;

    public void Visit(BodyColorItem item)
        => _data.SelectedBodycolorItem = item.bodyColorType;
    public void Visit(HeadItem item)
        => _data.SelectedHeadItem = item.headType;
}