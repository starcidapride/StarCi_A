using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using static ImageUtils;
using static Constants.AbilityNames;
public class CharacterCardAttributes
{
    public Texture2D Image { get; set; }
    public string CardName { get; set; }
    public CharacterRole CharacterRole { get; set; }
    public int Experience { get; set; }
    public int MaxHealth { get; set; }
    public int AttackDamage { get; set; }
    public int Armor { get; set; }
    public int MagicResistance { get; set; }
    public Texture2D Passive { get; set; }
    public Texture2D Q { get; set; }
    public Texture2D E { get; set; }
    public Texture2D R { get; set; }
}

public class CharacterCardController : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private TMP_Text cardNameText;

    [SerializeField]
    private TMP_Text characterRoleText;

    [SerializeField]
    private TMP_Text experienceText;

    [SerializeField]
    private TMP_Text maxHealthText;

    [SerializeField]
    private TMP_Text attackDamageText;

    [SerializeField]
    private TMP_Text armorText;

    [SerializeField]
    private TMP_Text magicResistanceText;

    [SerializeField]
    private Image passiveImage;

    [SerializeField]
    private Image QImage;

    [SerializeField]
    private Image EImage;

    [SerializeField]
    private Image RImage;

    private void Start()
    {

    }

    public Dictionary<string, Transform> GetAbilityTransforms()
    {
        return new Dictionary<string, Transform>()
        {
            { PASSIVE, passiveImage.transform },
            { Q, QImage.transform },
            { E, EImage.transform },
            { R, RImage.transform },
        };
    }
    public void RenderDisplay(CharacterCardAttributes attributes)
    {
        image.sprite = CreateSpriteFromTexture(attributes.Image);

        cardNameText.text = attributes.CardName;

        experienceText.text = attributes.Experience.ToString() + " Exps";

        maxHealthText.text = "Max Health : " + attributes.MaxHealth.ToString();

        attackDamageText.text = "Attack Damage : " + attributes.AttackDamage.ToString();

        armorText.text = "Armor : " + attributes.Armor.ToString();

        magicResistanceText.text = "Magic Resistance : " + attributes.MagicResistance.ToString();

        passiveImage.sprite = CreateSpriteFromTexture(attributes.Passive);

        QImage.sprite = CreateSpriteFromTexture(attributes.Q);

        EImage.sprite = CreateSpriteFromTexture(attributes.E);

        RImage.sprite = CreateSpriteFromTexture(attributes.R);
    }
}


