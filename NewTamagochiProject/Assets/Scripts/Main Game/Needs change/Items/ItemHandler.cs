using UnityEngine;
using UnityEngine.EventSystems;

public class ItemHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private ItemSO _item;

    private Camera _camera;
    private RectTransform _rectTransform;
    private Vector3 _originalPosition;

    public void OnInitialize()
    {
        _camera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
        _originalPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Ray ray = _camera.ScreenPointToRay(eventData.position);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, 20);
        if (hit && hit.transform.TryGetComponent(out CreatureNeedsChange creatureNeedsChange))
        {
            _item.UpdateNeed(creatureNeedsChange);
            
        } else
        {
            Debug.Log("Вы кинули предмет куда-то не туда...");
        }
        _rectTransform.anchoredPosition = _originalPosition;
    }
}
