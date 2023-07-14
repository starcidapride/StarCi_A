using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using static ImageUtils;
public class OtherCardAttributes
{
    public Texture2D Frame { get; set; }
    public Texture2D Image { get; set; }
    public string CardName { get; set; }
    public string Description { get; set; }
}
    public class OtherCardController : MonoBehaviour
{
    [SerializeField]
    private Image frame;

    [SerializeField]
    private Image image; 

    [SerializeField]
    private TMP_Text cardNameText;

    [SerializeField]
    private TMP_Text descriptionText;

    private void Start()
    {
        
    }

    public void RenderDisplay(OtherCardAttributes attributes)
    {
        frame.sprite = CreateSpriteFromTexture(attributes.Frame);

        image.sprite = CreateSpriteFromTexture(attributes.Image);

        cardNameText.text = attributes.CardName;

        descriptionText.text = attributes.Description;

    }
}
