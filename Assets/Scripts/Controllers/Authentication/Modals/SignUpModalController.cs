using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static AuthApiService;
using static Constants.ButtonNames;

public class SignUpModalController : Singleton<SignUpModalController>
{
    [SerializeField]
    private TMP_InputField emailTextInput;

    [SerializeField]
    private TMP_InputField passwordTextInput;

    [SerializeField]
    private TMP_InputField confirmTextInput;

    [SerializeField]
    private TMP_InputField firstNameTextInput;

    [SerializeField]
    private TMP_InputField lastNameTextInput;

    [SerializeField]
    private Button forwardSignInTextAsButton;

    [SerializeField]
    private Button submitButton;

    [SerializeField]
    private Button cancelButton;

    [SerializeField]
    private Transform emailErrorText;

    [SerializeField]
    private Transform passwordErrorText;

    [SerializeField]
    private Transform confirmErrorText;

    [SerializeField]
    private Transform firstNameErrorText;

    [SerializeField]
    private Transform lastNameErrorText;

    [SerializeField]
    private Transform signInModal;

    private string email, password, confirm, firstName, lastName;
    private void Start()
    {
        email = emailTextInput.text;

        password = passwordTextInput.text;  

        confirm = confirmTextInput.text;

        firstName = firstNameTextInput.text;

        lastName = lastNameTextInput.text;

        emailTextInput.onEndEdit.AddListener(OnEmailTextInputEndEdit);

        passwordTextInput.onEndEdit.AddListener(OnPasswordTextInputEndEdit);

        confirmTextInput.onEndEdit.AddListener(OnConfirmTextInputEndEdit);

        firstNameTextInput.onEndEdit.AddListener(OnFirstNameTextInputEndEdit);

        lastNameTextInput.onEndEdit.AddListener(OnLastNameTextInputEndEdit);

        forwardSignInTextAsButton.onClick.AddListener(OnForwardSignInTextAsButtonClick);

        submitButton.onClick.AddListener(OnSubmitButtonClick);

        cancelButton.gameObject.AddComponent<ModalCancelButtonController>();
    }

    public void Init()
    {
        email = string.Empty;

        password = string.Empty;

        confirm = string.Empty;

        firstName = string.Empty;

        lastName = string.Empty;
    }

    private void OnEmailTextInputEndEdit(string value)
    {
        email = value;
    }

    private void OnPasswordTextInputEndEdit(string value)
    {
        password = value;
    }

    private void OnConfirmTextInputEndEdit(string value)
    {
        confirm = value;
    }

    private void OnFirstNameTextInputEndEdit(string value)
    {
        firstName = value;
    }

    private void OnLastNameTextInputEndEdit(string value)
    {
        lastName = value;
    }

    private void OnForwardSignInTextAsButtonClick()
    {
        ModalController.Instance.CloseNearestModal();
        ModalController.Instance.InstantiateModal(signInModal);
    }

    private async void OnSubmitButtonClick()
    {
        var response = await ExecuteSignUp(
            new SignUpRequest()
            {
                Email = email,
                Password = password,
                Confirm = confirm,
                FirstName = firstName,
                LastName = lastName,

            }, ClientErrorHandler,
            FailedResponseHandler
            );

        if (response == null) return;

        var cancelButton = new AlertButton()
        {
            ButtonText = CANCEL,
            Script = typeof(AlertCancelButtonController)
        };

        var resentButton = new AlertButton()
        {
            ButtonText = RESENT,
            Script = typeof(AlertCancelButtonController)
        };

        AlertController.Instance.Show(AlertCaption.Success, $"Sign up was successful. A confirmation email has been sent to {response.Email}. Please check your email for further instructions.",
          new List<AlertButton>()
          {
        cancelButton,
        resentButton
          }
      );

    }

    public class SignUpErrors
    {
        [JsonProperty("emailError")]
        public string EmailError { get; set; }

        [JsonProperty("passwordError")]
        public string PasswordError { get; set; }

        [JsonProperty("confirmError")]
        public string ConfirmError { get; set; }

        [JsonProperty("firstNameError")]
        public string FirstNameError { get; set; }

        [JsonProperty("lastNameError")]
        public string LastNameError { get; set; }
    }
    public class FailedResponse
    {
        [JsonProperty("errors")]
        public SignUpErrors Errors { get; set; }
    }

    private void FailedResponseHandler(string response, HttpStatusCode code)
    {
        var data = JsonConvert.DeserializeObject<FailedResponse>(response);

        if (code == HttpStatusCode.BadRequest || code == HttpStatusCode.Conflict)
        {

            var cancelButton = new AlertButton()
            {
                ButtonText = CANCEL,
                Script = typeof(AlertCancelButtonController)
            };
            AlertController.Instance.Show(AlertCaption.Error, "Validation faults found.",
                new List<AlertButton>()
                {
                cancelButton
                }
               );

            var errors = data.Errors;

            if (!string.IsNullOrEmpty(errors.EmailError))
            {
                emailErrorText.gameObject.SetActive(true);
                emailErrorText.GetComponent<TMP_Text>().text = errors.EmailError;
            }

            if (!string.IsNullOrEmpty(errors.PasswordError))
            {
                passwordErrorText.gameObject.SetActive(true);
                passwordErrorText.GetComponent<TMP_Text>().text = errors.PasswordError;
            }

            if (!string.IsNullOrEmpty(errors.ConfirmError))
            {
                confirmErrorText.gameObject.SetActive(true);
                confirmErrorText.GetComponent<TMP_Text>().text = errors.ConfirmError;
            }

            if (!string.IsNullOrEmpty(errors.FirstNameError))
            {
                firstNameErrorText.gameObject.SetActive(true);
                firstNameErrorText.GetComponent<TMP_Text>().text = errors.FirstNameError;
            }

            if (!string.IsNullOrEmpty(errors.LastNameError))
            {
                lastNameErrorText.gameObject.SetActive(true);
                lastNameErrorText.GetComponent<TMP_Text>().text = errors.LastNameError;
            }

        }
    }
    private void ClientErrorHandler(HttpRequestException ex)
    {
        Debug.Log(ex);
    }
}

