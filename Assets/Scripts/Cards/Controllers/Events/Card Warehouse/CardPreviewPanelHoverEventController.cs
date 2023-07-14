﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

using static CardUtils;
using static GameObjectUtils;
using static CardMaps;

public class CardPreviewPanelHoverEventController : CardEventController, IPointerEnterHandler, IPointerExitHandler
{
    private CardType cardType;

    private bool isHovering;
    private void Start()
    {
        cardType = GetMap()[CardName].Item1;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    private void Update()
    {
        if (isHovering)
        {
            switch (cardType)
            {
                case CardType.Character:
                    var characterCardController = gameObject.GetComponent<CharacterCardController>();
                    var abilities = characterCardController.GetAbilityTransforms();

                    bool isMousePosInsideAbilities = false;
                    foreach (var ability in abilities)
                    {
                        var isMousePosInsideAbility = IsPositionInsideRectTransformArea(GetMousePos(), (RectTransform)ability.Value);
                        if (isMousePosInsideAbility)
                        {
                            isMousePosInsideAbilities = true;

                            var abilityAttributes = GetAbilityAttributes(CardName);
                            var key = ability.Key;

                            var abilityImage = abilityAttributes[key].AbilityImage;
                            var abilityName = abilityAttributes[key].AbilityName;
                            var abilityDescription = abilityAttributes[key].AbilityDescription;

                            if (!CardWarehouseTooltipController.Instance.IsContainerHasActived())
                            {
                                CardWarehouseTooltipController.Instance.Show(abilityImage, $"{key} - {abilityName}", abilityDescription);
                            }

                            CardWarehouseTooltipController.Instance.MoveContainerToMousePointer();

                        }

                    }

                    if (!isMousePosInsideAbilities)
                    {
                        CardWarehouseTooltipController.Instance.Hide();
                    }
                    break;
            }
        }

    }

}

