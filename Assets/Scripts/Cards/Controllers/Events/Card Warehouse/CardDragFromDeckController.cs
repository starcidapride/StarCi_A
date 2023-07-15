using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

using static CardUtils;
using static CardMaps;
using static GameObjectUtils;

public class CardDragFromDeckController : CardEventController, IBeginDragHandler, IDragHandler, IEndDragHandler
{      
    private Transform dragCard;
    private Vector2 localPosition;
    public void OnBeginDrag(PointerEventData eventData)
    {
        SetVisibility(transform, false);

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

        var map = GetMap();

        var inPlayDeck = map[CardName].Item1 != CardType.Character;

        if (inPlayDeck && !IsPositionInsideRectTransformArea(cardPosition, (RectTransform)PlayDeckController.Instance.GetTransform()))
        {
            PlayDeckController.Instance.RemoveCard(CardName);
        }
        else if (!inPlayDeck && !IsPositionInsideRectTransformArea(cardPosition, (RectTransform)CharacterDeckController.Instance.GetTransform()))
        {
            CharacterDeckController.Instance.RemoveCard(CardName);
        } else
        {
            SetVisibility(transform, true);
        }

        Destroy(dragCard.gameObject);
    }
}

