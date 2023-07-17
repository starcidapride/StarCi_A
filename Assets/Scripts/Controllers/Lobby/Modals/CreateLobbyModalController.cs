
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System.Text.RegularExpressions;

using static LobbyUtils;
using static RelayUtils;
using static Constants.ButtonNames;

public class CreateLobbyModalController : Singleton<CreateLobbyModalController>
{
    [SerializeField]
    private TMP_InputField lobbyNameTextInput;

    [SerializeField]
    private Transform lobbyNameError;

    [SerializeField]
    private Toggle privateToggleInput;

    [SerializeField]
    private TMP_InputField descriptionTextInput;

    [SerializeField]
    private Button cancelButton;

    [SerializeField]
    private Button submitButton;

    private string lobbyName, description;
    private bool _private;

    private void Start()
    {
        lobbyName = lobbyNameTextInput.text;

        description = descriptionTextInput.text;

        _private = privateToggleInput.isOn;

        lobbyNameTextInput.onEndEdit.AddListener(OnLobbyNameTextInputEndEdit);
        
        privateToggleInput.onValueChanged.AddListener(OnPrivateToggleInputValueChanged);
        
        descriptionTextInput.onEndEdit.AddListener(OnDescriptionTextInputEndEdit);

        cancelButton.gameObject.AddComponent<ModalCancelButtonController>();

        submitButton.onClick.AddListener(OnSubmitButtonClick);
    }

    private void OnLobbyNameTextInputEndEdit(string value)
    {
        lobbyName = value;
    }
    private void OnPrivateToggleInputValueChanged(bool value)
    {
        _private = value;
    }

    private void OnDescriptionTextInputEndEdit(string value)
    {
        description = value;
    }



    private async void OnSubmitButtonClick() {

        var hasError = false;

        if (!Regex.IsMatch(lobbyName, @"^.{6,20}$"))
        {
            lobbyNameError.GetComponent<TMP_Text>().text = "Lobby name must be between 6 and 20 characters.";
            lobbyNameError.gameObject.SetActive(true);

            hasError = true;
        }
        else
        {
            lobbyNameError.gameObject.SetActive(false);
        }

        if (hasError) return;

        var button = new List<AlertButton>()
            {
        new AlertButton()
        {
            ButtonText = CANCEL,
            Script = typeof(AlertCancelButtonController)
        } };

        LoadingController.Instance.Show();
        var relayResponse = await CreateRelay();

        if (relayResponse == null) return;

        var lobby = await CreateLobby(
            lobbyName,
            UserManager.Instance.Username,
            relayResponse,
            description,
            _private
            );

        if (lobby == null) return;
        
        NetworkManager.Singleton.StartHost();
        
        LoadingController.Instance.Hide();

        ModalController.Instance.CloseNearestModal();

        NetworkWaitingRoomManager.Lobby = lobby;

        LoadingSceneManager.Instance.LoadScene(SceneName.WaitingRoom, true);
    }
}
