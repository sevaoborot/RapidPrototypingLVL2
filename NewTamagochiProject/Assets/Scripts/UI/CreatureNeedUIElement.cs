using UnityEngine;
using UnityEngine.UI;

public abstract class CreatureNeedUIElement : MonoBehaviour
{
    protected CreatureNeeds _needs;
    protected bool _isSubscribed = false;

    [SerializeField]private RectTransform _needSlider;
    private Image _needImage;

    public virtual void OnInitialize(CreatureNeeds needs)
    {
        _needs = needs;
        _needImage = _needSlider.GetComponent<Image>();
        if (_needSlider == null) Debug.LogError("RectTransform не найден на объекте!");
    }

    protected void UpdateUIElement(float needValue)
    {
        SetHeight(needValue);
        SetColor(needValue);
    }

    private void SetHeight(float needValue)
    {
        Vector2 newSize;
        if (needValue <= 5f)
        {
            newSize = new Vector2(_needSlider.sizeDelta.x, 5f);
            return;
        }
        newSize = new Vector2(_needSlider.sizeDelta.x, needValue);
        _needSlider.sizeDelta = newSize;
    }

    private void SetColor(float needValue)
    {
        switch(needValue)
        {
            case float value when value < 33f && value >= 0f:
                _needImage.color = new Color(255, 0, 0, 255);
                break;
            case float value when value >= 33f && value < 66f:
                _needImage.color = new Color(255, 255, 0, 255);
                break;
            case float value when value >= 66f && value <= 100f:
                _needImage.color = new Color(0, 255, 0, 255);
                break;
            default:
                Debug.Log($"Эксепшн со значениями этой вашей зелёненькой штучки, значение {needValue}");
                break;
        }
    }
}