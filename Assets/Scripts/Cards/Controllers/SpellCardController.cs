using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using static ImageUtils;
public class SpellCardAttributes
{
    public Texture2D Image { get; set; }
    public string CardName { get; set; }
    
    public string Description { get; set; }
}
public class SpellCardController : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private TMP_Text cardNameText;

    [SerializeField]
    private TMP_Text descriptionText;


    private void Start()
    {
        
    }

    public void RenderDisplay(SpellCardAttributes attributes)
    {
        image.sprite = CreateSpriteFromTexture(attributes.Image);

        cardNameText.text = attributes.CardName;

        descriptionText.text = attributes.Description;

    }
}
