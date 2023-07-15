using UnityEngine;
using UnityEngine.EventSystems;

using static CardUtils;
using static GameObjectUtils;

public class CardDragToDeckController : CardEventController, IBeginDragHandler, IDragHandler, IEndDragHandler
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
    }

    public void OnEndDrag(PointerEventData eventData)
    {   
        var cardPosition = dragCard.position;

        if (IsPositionInsideRectTransformArea(cardPosition, (RectTransform) PlayDeckController.Instance.GetTransform()))
        {
            PlayDeckController.Instance.AddCard(CardName);
        } 
        else if (IsPositionInsideRectTransformArea(cardPosition, (RectTransform) CharacterDeckController.Instance.GetTransform()))
        {
            CharacterDeckController.Instance.AddCard(CardName);
        }

        Destroy(dragCard.gameObject);
    }
}

