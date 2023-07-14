using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

using static CardUtils;
using static GameObjectUtils;


public class CardShowcaseClickEventController : CardEventController, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(OnPointerClickCoroutine());
    }

    public IEnumerator OnPointerClickCoroutine()
    {
        DestroyAllChildGameObjects(CardPreviewPanelController.Instance.GetTransform());

        CardWarehouseUIController.Instance.SetInteractability(false);
        yield return InstantiateAndSetupCardCoroutine(CardName, Vector2.zero, Vector2.one * 3/4, CardPreviewPanelController.Instance.GetTransform(), typeof(CardPreviewPanelHoverEventController));
        CardWarehouseUIController.Instance.SetInteractability();
    }



}

