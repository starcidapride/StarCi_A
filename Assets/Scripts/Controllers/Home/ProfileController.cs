using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static ImageUtils;

public class ProfileController : Singleton<ProfileController>
{
    [SerializeField]
    private UserInventory inventory;

    [SerializeField]
    private TMP_Text usernameText;

    [SerializeField]
    private TMP_Text bioText;

    [SerializeField]
    private Image picture;

    [SerializeField] 
    private Button profileButton;

    [SerializeField]
    private Transform profileModal;

    [SerializeField]
    private Texture2D defaultPicture;

    void Start()
    {
        RenderDisplay();

        inventory.InventoryTriggered += OnInventoryTriggered;

        profileButton.onClick.AddListener(OnProfileButtonClick);
    }

    private void OnInventoryTriggered()
    {
        RenderDisplay();
    }

    private void RenderDisplay()
    {
        usernameText.text = inventory.Username;
        bioText.text = inventory.Bio;
        picture.sprite = CreateSpriteFromTexture(inventory.Picture) ?? CreateSpriteFromTexture(defaultPicture);
    }

    private void OnProfileButtonClick()
    {
        ModalController.Instance.InstantiateModal(profileModal);
    }

}
