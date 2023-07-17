using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

using static LobbyUtils;
using static RelayUtils;
using static Constants.LobbyService;


public class PlayMenuController : Singleton<PlayMenuController>
{
    [SerializeField]
    private Button goToLobbyRoomButton;

    [SerializeField]
    private Button quickJoinButton;

    private void Start()
    {
        goToLobbyRoomButton.onClick.AddListener(OnGoToLobbyRoomButtonClick);

        quickJoinButton.onClick.AddListener(OnQuickJoinButtonClick);
    }

    private void OnGoToLobbyRoomButtonClick()
    {
        LoadingSceneManager.Instance.LoadScene(SceneName.LobbyRoom, false);
    }

    private async void OnQuickJoinButtonClick()
    {
        var lobby = await QuickJoin(UserManager.Instance.Username);
        
        if (lobby == null) return;

        var joinCode = lobby.Data[RELAY_CODE].Value;

        var result = await JoinRelay(joinCode);

        if (!result) return;

        NetworkManager.Singleton.StartClient();
    }
}
