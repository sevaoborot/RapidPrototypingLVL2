public class HappinessUIElement : CreatureNeedUIElement
{
    public override void OnInitialize(CreatureNeeds needs)
    {
        base.OnInitialize(needs);
        if (!_isSubscribed)
        {
            _needs.OnHappinessChanged += UpdateUIElement;
            _isSubscribed = true;
        }
    }

    private void OnEnable()
    {
        if (_needs != null && !_isSubscribed)
        {
            _needs.OnHappinessChanged += UpdateUIElement;
            _isSubscribed = true;
        }
    }

    private void OnDisable()
    {
        if (_needs != null && _isSubscribed)
        {
            _needs.OnHappinessChanged -= UpdateUIElement;
            _isSubscribed = false;
        }
    }
}
