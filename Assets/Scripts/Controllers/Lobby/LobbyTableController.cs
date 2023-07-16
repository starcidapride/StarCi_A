using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static LobbyUtils;
using static RelayUtils;

public class LobbyTableController : MonoBehaviour
{
    [SerializeField]
    private Transform createLobbyModal;

    [SerializeField]
    private Button createLobbyButton;

    [SerializeField]
    private Button joinLobbyByCodeButton;

    [SerializeField]
    private Transform tableBody;

    private void Start()
    {
        createLobbyButton.onClick.AddListener(OnCreateLobbyButtonClick);

        joinLobbyByCodeButton.onClick.AddListener(OnJoinLobbyByCodeButtonClick);
    }

    private void OnCreateLobbyButtonClick()
    {
        ModalController.Instance.InstantiateModal(createLobbyModal);
    }

    private void OnJoinLobbyByCodeButtonClick()
    {

    }
}
