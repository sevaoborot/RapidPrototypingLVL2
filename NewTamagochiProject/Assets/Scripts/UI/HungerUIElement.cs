public class HungerUIElement : CreatureNeedUIElement
{
    public override void OnInitialize(CreatureNeeds needs)
    {
        base.OnInitialize(needs);
        if (!_isSubscribed)
        {
            _needs.OnHungerChanged += UpdateUIElement;
            _isSubscribed = true;
        }
    }

    private void OnEnable()
    {
        if (_needs != null && !_isSubscribed)
        {
            _needs.OnHungerChanged += UpdateUIElement;
        }
    }

    private void OnDisable()
    {
        if (_needs != null && _isSubscribed)
        {
            _needs.OnHungerChanged -= UpdateUIElement;
        }
    }
}
