using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static Constants.Apis;

public class SignInModalController : Singleton<SignInModalController>
{
    [SerializeField]
    private TMP_InputField emailTextInput;

    [SerializeField]
    private TMP_InputField passwordTextInput;

    [SerializeField]
    private Button forgetPasswordButtonAsText;

    [SerializeField]
    private Button submitButton;

    [SerializeField]
    private Button cancelButton;

    private string email, password;

    private void Start()
    {
        email = string.Empty; password = string.Empty;

        emailTextInput.onEndEdit.AddListener(OnEmailTextInputEndEdit);

        passwordTextInput.onEndEdit.AddListener(OnPasswordTextInputEndEdit);

        forgetPasswordButtonAsText.onClick.AddListener(OnForgetPasswordButtonAsTextClick);

        submitButton.onClick.AddListener(OnSubmitButtonClick);

        cancelButton.gameObject.AddComponent<ModalCancelButtonHandler>();
    }

    private async void OnEmailTextInputEndEdit(string value)
    {
    }
    private void OnPasswordTextInputEndEdit(string value)
    {

    }

    private void OnForgetPasswordButtonAsTextClick()
    {

    }

    private void OnSubmitButtonClick()
    {

    }
}

