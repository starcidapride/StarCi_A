using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static ImageUtils;

public class ProfileController : Singleton<ProfileController>
{
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

        UserManager.Instance.Notify += OnNotify; ;

        profileButton.onClick.AddListener(OnProfileButtonClick);
    }

    private void OnNotify()
    {
        throw new System.NotImplementedException();
    }

    private void OnNotifyEvent()
    {
        RenderDisplay();
    }

    private void RenderDisplay()
    {
        usernameText.text = UserManager.Instance.Username;
        bioText.text = UserManager.Instance.Bio;
        picture.sprite = CreateSpriteFromTexture(UserManager.Instance.Picture) ?? CreateSpriteFromTexture(defaultPicture);
    }

    private void OnProfileButtonClick()
    {
        ModalController.Instance.InstantiateModal(profileModal);
    }
    private void OnDestroy()
    {
        UserManager.Instance.Notify -= OnNotify;
    }
}
