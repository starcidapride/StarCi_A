using UnityEngine;
using UnityEngine.EventSystems;

using static CardUtils;
using static GameObjectUtils;

public class CardDragFromDeckController : CardEventController, IBeginDragHandler, IDragHandler, IEndDragHandler
{      
    private Transform dragCard;
    private Vector2 localPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragCard = InstantiateCard(CardName, CardShowcaseDragAreaController.Instance.GetTransform());
        
        dragCard.localScale = Vector3.one / 4;

        localPosition = GetMousePositionRelativeToRectTransform((RectTransform)transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var mousePos = GetMousePos();
      

        dragCard.position = mousePos - localPosition;

        Debug.Log(localPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(dragCard.gameObject);
    }
}

