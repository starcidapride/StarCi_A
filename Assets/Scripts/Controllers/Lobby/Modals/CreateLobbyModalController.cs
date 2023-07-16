using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using static LobbyUtils;
using static RelayUtils;

public class CreateLobbyModalController : Singleton<CreateLobbyModalController>
{
    [SerializeField]
    private UserInventory inventory;

    [SerializeField]
    private TMP_InputField lobbyNameTextInput;

    [SerializeField]
    private TMP_InputField descriptionTextInput;

    [SerializeField]
    private Toggle privateToggleInput;

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

        descriptionTextInput.onEndEdit.AddListener(OnDescriptionTextInputEndEdit);

        privateToggleInput.onValueChanged.AddListener(OnPrivateToggleInputValueChanged);

        cancelButton.AddComponent<ModalCancelButtonController>();

        submitButton.onClick.AddListener(OnSubmitButtonClick);
    }

    private void OnLobbyNameTextInputEndEdit(string value)
    {
        lobbyName = value;
    }

    private void OnDescriptionTextInputEndEdit(string value)
    {
        description = value;
    }

    private void OnPrivateToggleInputValueChanged(bool value)
    {
        _private = value;
    }

    private async void OnSubmitButtonClick() {

        LoadingController.Instance.Show();
        var relayResponse = await CreateRelay();

        await CreateLobby(
            lobbyName,
            inventory.Username,
            relayResponse,
            description,
            _private
            );

        LoadingController.Instance.Hide();

        ModalController.Instance.CloseNearestModal();

    }
}
