
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static ImageUtils;
using static EnumUtils;

public class EquipmentCardAttributes
{
    public Texture2D Image { get; set; }
    public string CardName { get; set; }
    public int EquipmentPrice { get; set; }
    public EquipmentClass EquipmentClass { get; set; }

    public string Stats { get; set; }
    public string Description { get; set; }
}
public class EquipmentCardController: MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private TMP_Text cardNameText;

    [SerializeField]
    private TMP_Text equipmentClassText;

    [SerializeField]
    private TMP_Text equipmentPriceText;

    [SerializeField]
    private TMP_Text statsText;

    [SerializeField]
    private TMP_Text descriptionText;

    public void RenderDisplay(EquipmentCardAttributes attributes)
    {
        image.sprite = CreateSpriteFromTexture(attributes.Image);

        cardNameText.text = attributes.CardName;

        equipmentClassText.text = GetDescription(attributes.EquipmentClass);

        equipmentPriceText.text = attributes.EquipmentPrice.ToString() + " Golds";

        statsText.text = attributes.Stats;

        descriptionText.text = attributes.Description;
    }
    }