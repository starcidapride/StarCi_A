using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

using static AuthApiService;
using static ApiUtils;
using static ImageUtils;
using static Constants.ButtonNames;


public class SignInModalController : Singleton<SignInModalController>
{
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
        email = emailTextInput.text;

        password = passwordTextInput.text;

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
            }, ClientErrorHandler, 
            SignInFailedResponseHandler

        );

        yield return new WaitUntil(() => responseTask.IsCompleted);

        var response = responseTask.Result;

        if (response == null) yield break;

        SaveAuthenticationTokens(response.AuthTokenSet.AccessToken, response.AuthTokenSet.RefreshToken);

        var presentableUser = response.PresentableUser;

        var user = new User
        {
            Email = presentableUser.Email,

            Username = presentableUser.Username,

            Picture = DecodeBase64Image(presentableUser.Picture),

            Bio = presentableUser.Bio,

            FirstName = presentableUser.FirstName,

            LastName = presentableUser.LastName,
        };

        UserManager.Instance.UpdateUser(user);

        AlertController.Instance.Show(AlertCaption.Success, "Sign in was successful. You will now be redirected to the home page.");

        yield return new WaitForSeconds(3);

        AlertController.Instance.Hide();

        ModalController.Instance.CloseNearestModal();

        LoadingSceneManager.Instance.LoadScene(SceneName.Home, false);
    }
  
    private void SignInFailedResponseHandler(string response, HttpStatusCode code)
    {
        var failedResponse = JsonConvert.DeserializeObject<SignInFailedResponse>(response);

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

public class SignInFailedResponse
{
    [JsonProperty("statusCode")]
    public int StatusCode { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("error")]
    public string Error { get; set; }
}

