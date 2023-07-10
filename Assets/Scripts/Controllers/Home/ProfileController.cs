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
        usernameText.text = inventory.username;
        bioText.text = inventory.bio;
        picture.sprite = CreateSpriteFromTexture(inventory.picture);
    }

    private void OnProfileButtonClick()
    {
        ModalController.Instance.InstantiateModal(profileModal);
    }

}
