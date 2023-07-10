using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static AuthApiService;
using static ApiUtils;
using static ImageUtils;
using static Constants.ButtonNames;

public class SignInModalController : Singleton<SignInModalController>
{
    [SerializeField]
    private UserInventory inventory;

    [SerializeField]
    private TMP_InputField emailTextInput;

    [SerializeField]
    private TMP_InputField passwordTextInput;

    [SerializeField]
    private Button forgetPasswordTextAsButton;

    [SerializeField]
    private Button submitButton;

    [SerializeField]
    private Button cancelButton;

    private string email, password;

    private void Start()
    {
        Init();

        email = string.Empty; password = string.Empty;

        emailTextInput.onEndEdit.AddListener(OnEmailTextInputEndEdit);

        passwordTextInput.onEndEdit.AddListener(OnPasswordTextInputEndEdit);

        forgetPasswordTextAsButton.onClick.AddListener(OnForgetPasswordTextAsButtonClick);

        submitButton.onClick.AddListener(OnSubmitButtonClick);

        cancelButton.gameObject.AddComponent<ModalCancelButtonController>();
    }

    public void Init()
    {
        email = string.Empty; password = string.Empty;
    }

    private void OnEmailTextInputEndEdit(string value)
    {
        email = value;
    }
    private void OnPasswordTextInputEndEdit(string value)
    {
        password = value;
    }

    private void OnForgetPasswordTextAsButtonClick()
    {

    }

    private void OnSubmitButtonClick()
    {
        StartCoroutine(OnSubmitButtonClickCoroutine());
    }
    private IEnumerator OnSubmitButtonClickCoroutine()
    {

        var responseTask = ExecuteSignIn(
            new SignInRequest()
            {
                Email = email,
                Password = password
            }, FailedResponseHandler
            , ClientErrorHandler
        );

        yield return new WaitUntil(() => responseTask.IsCompleted);

        var response = responseTask.Result;

        if (response == null) yield break;

        SaveAuthenticationTokens(response.AuthTokenSet.AccessToken, response.AuthTokenSet.RefreshToken);

        var user = response.PresentableUser;

        inventory.email = user.Email;

        inventory.username = user.Username;

        inventory.picture = DecodeBase64Image(user.Picture);

        inventory.bio = user.Bio;

        inventory.firstName = user.FirstName;

        inventory.lastName = user.LastName;

        AlertController.Instance.Show(AlertCaption.Success, "Sign in successfully.");

        yield return new WaitForSeconds(3);

        AlertController.Instance.Hide();

        ModalController.Instance.CloseNearestModal();

        LoadingSceneManager.Instance.LoadScene(SceneName.Home, false);
    }

    public class FailedResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
    private void FailedResponseHandler(string response)
    {
        var failedResponse = JsonConvert.DeserializeObject<FailedResponse>(response);


        var cancelButton = new AlertButton()
        {
            ButtonText = CANCEL,
            Script = typeof(AlertCancelButtonController)
        };
        AlertController.Instance.Show(AlertCaption.Error, failedResponse.Message,
            new List<AlertButton>()
            {
                cancelButton
            }
           );
    }
    private void ClientErrorHandler(HttpRequestException ex)
    {
        Debug.Log(ex);
    }
}

