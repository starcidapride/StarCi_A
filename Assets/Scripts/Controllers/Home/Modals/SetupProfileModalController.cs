using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

using static ImageUtils;
using static ProfileApiService;
using static AnimatorUtils;
using static Constants.Triggers.Modal.SetupProfileModal;

public class SetupProfileModalController : Singleton<SetupProfileModalController>
{
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

        picture = LoadImageFromFile();

        if (picture == null) yield break;

        var beforeUploadPicture = presentPicture.sprite;

        previousPicture.sprite = beforeUploadPicture;

        var updatePicture = CreateSpriteFromTexture(picture);

        presentPicture.sprite = updatePicture;

        yield return ExecuteTriggerThenWait(transform, SETUP_PROFILE_MODAL_TRANSITION_TRIGGER);
    }

    private void OnBioTextInputEndEdit(string value)
    {
        bio = value;
    }

    private async void OnSubmitButtonClick()
    {
        await ExecuteSetupProfile
            (
            new SetupProfileRequest()
            {
                Username = username,
                Picture = EncodeBase64Image(picture),
                Bio = bio
            }, ClientErrorHandler, X
            );

        UserManager.Instance.UpdateUser(
            new User
            {
                    Username = username,
                    Picture = picture,
                    Bio = bio
            }
            );

        ModalController.Instance.CloseNearestModal();

        HomeManager.Instance.DisplayUI();
    }

    private void X(string ex, HttpStatusCode code)
    {
        Debug.Log(ex);
    }
    private void ClientErrorHandler(HttpRequestException ex)
    {
        Debug.Log(ex);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnSubmitButtonClick();
        }
    }
}
