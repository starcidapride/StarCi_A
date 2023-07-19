using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.VisualScripting;


using static LobbyUtils;
using static RelayUtils;
using static Constants.LobbyService;
using System;

public class JoinLobbyByCodeModalController : Singleton<JoinLobbyByCodeModalController>
{
    [SerializeField]
    private TMP_InputField enterCodeTextInput;

    [SerializeField]
    private Transform enterCodeError;

    [SerializeField]
    private Button submitButton;

    [SerializeField]
    private Button cancelButton;

    private string code;
    public void Start()
    {
        code = enterCodeTextInput.text;

        enterCodeTextInput.onEndEdit.AddListener(OnEnterCodeTextInputEndEdit);

        submitButton.onClick.AddListener(OnSubmitButtonClick);

        cancelButton.AddComponent<ModalCancelButtonController>();
    }

    private void OnEnterCodeTextInputEndEdit(string value)
    {
        code = value;
    }

    private async void OnSubmitButtonClick()
    {
        var hasError = false;

        if (!string.IsNullOrEmpty(code) && code.Length < 6)
        {
            enterCodeError.GetComponent<TMP_Text>().text = "Lobby code must be exactly 6 characters.";
            enterCodeError.gameObject.SetActive(true);

            hasError = true;
        }
        else
        {
            enterCodeError.gameObject.SetActive(false);
        }

        if (hasError) return;


        var lobby = await JoinLobbyByCode(code, UserManager.Instance.Username);

        if (lobby == null) return;

        LoadingSceneManager.Instance.JoinRelayAndStartClient(lobby);
    }
}
