using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static ImageUtils;
using static GameObjectUtils;

public class CardWarehouseTooltipController : Singleton<CardWarehouseTooltipController>
{
    [SerializeField]
    private Transform container;

    [SerializeField]
    private Image abilityImage;

    [SerializeField]
    private TMP_Text abilityNameText;

    [SerializeField]
    private TMP_Text abilityDescriptionText;

    public bool IsContainerHasActived()
    {
        return container.gameObject.activeSelf;
    }
    public void Show(Texture2D image, string abilityName, string abilityDescription)
    {
        abilityImage.sprite = CreateSpriteFromTexture(image);

        abilityNameText.text = abilityName;

        abilityDescriptionText.text = abilityDescription;

        container.gameObject.SetActive(true);
    }

    public void MoveContainerToMousePointer()
    {
        var mousePos = GetMousePos();

        var containerSize = ((RectTransform) container.transform).sizeDelta;
        Debug.Log(containerSize / 2);
        container.transform.position = mousePos + containerSize / 2;

    }
    public void Hide()
    {
        container.gameObject.SetActive(false);
    }
}
