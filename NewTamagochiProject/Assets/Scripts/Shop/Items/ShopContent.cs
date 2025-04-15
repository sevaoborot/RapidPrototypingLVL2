using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Asset/Shop/Shop list")]
public class ShopContent : ScriptableObject
{
    [SerializeField] private List<HeadItem> _headItems;
    [SerializeField] private List<BodyColorItem> _bodyColors;

    public IEnumerable<HeadItem> HeadItems => _headItems;
    public IEnumerable<BodyColorItem> BodyColors => _bodyColors;

    private void OnValidate()
    {
        var headItemsDuplicates = _headItems.GroupBy(item => item.headType)
            .Where(array =>  array.Count() > 1);
        if (headItemsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_headItems));
        var bodyColorsDuplicates = _bodyColors.GroupBy(item => item.bodyColorType)
            .Where(array => array.Count() > 1);
        if (headItemsDuplicates.Count() > 0)
            throw new InvalidOperationException(nameof(_bodyColors));
    }
}
