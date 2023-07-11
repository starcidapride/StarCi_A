using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using static ImageUtils;
using static ProfileApiService;
using static AnimatorUtils;

public class SetupProfileModalController : Singleton<SetupProfileModalController>
{
    [SerializeField]
    private UserInventory inventory;

    [SerializeField]
    private TMP_InputField usernameTextInput;

    [SerializeField]
    private Image previousPicture;

    [SerializeField]
    private Image presentPicture;

    [SerializeField]
    private Button uploadPictureButton;

    [SerializeField]
    private TMP_InputField bioTextInput;

    [SerializeField]
    private Button submitButton;

    [SerializeField]
    private Button cancelButton;


    private string username, bio;

    private Texture2D picture;

    private void Start()
    {

        username = usernameTextInput.text;

        picture = null;

        bio = bioTextInput.text;

        usernameTextInput.onEndEdit.AddListener(OnUsernameTextInputEndEdit);

        uploadPictureButton.onClick.AddListener(OnUploadPictureButtonClick);

        bioTextInput.onEndEdit.AddListener(OnBioTextInputEndEdit);

        submitButton.onClick.AddListener(OnSubmitButtonClick);

        cancelButton.gameObject.AddComponent<ModalCancelButtonController>();
    }

    private void OnUsernameTextInputEndEdit(string value)
    {
        username = value;
    }

    private void OnUploadPictureButtonClick()
    {
        StartCoroutine(OnUploadPictureButtonClickCoroutine());
    }
    private IEnumerator OnUploadPictureButtonClickCoroutine()
    {
        var beforeUploadPicture = presentPicture.sprite;

        previousPicture.sprite = beforeUploadPicture;

        picture = LoadImageFromFile();

        var updatePicture = CreateSpriteFromTexture(picture);

        presentPicture.sprite = updatePicture;

        yield return ExecuteTriggerThenWait(transform, "Transition Trigger");
    }

    private void OnBioTextInputEndEdit(string value)
    {
        bio = value;
    }

    private async void OnSubmitButtonClick()
    {
        await ExecuteInitUserDetails(
            new InitUserDetailsRequest()
            {
                Username = username,             
                Picture = EncodeBase64Image(picture),       
                Bio = bio
            }, ClientErrorHandler
            );

        inventory.UpdateUser(
            new UserInventoryDTO()
            {
                Username = username,
                Picture = picture,
                Bio = bio
            }
            );

        ModalController.Instance.CloseNearestModal();

        HomeManager.Instance.DisplayProfileUI();
    }

    private void ClientErrorHandler(HttpRequestException ex)
    {
        Debug.Log(ex);
    }
}
