using System;
using System.Collections;
using System.Collections.Generic;
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

        UIController.Instance.SetInteractability(false);
        yield return InstantiateAndSetupCardCoroutine(CardName, Vector2.zero, Vector2.one * 3/4, CardPreviewPanelController.Instance.GetTransform(), new List<Type> { typeof(CardPreviewPanelHoverEventController) });
        UIController.Instance.SetInteractability();
    }



}

