using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

using static LobbyUtils;
using static RelayUtils;
using static Constants.LobbyService;
using System.Threading.Tasks;

public class HomeButtonsController : Singleton<HomeButtonsController>
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

    private bool isQuickJoinButtonBlocked = true;
    private async void OnQuickJoinButtonClick()
    {   
        if (isQuickJoinButtonBlocked)
        {
            try
            {
                isQuickJoinButtonBlocked = false;

                var lobby = await QuickJoin(UserManager.Instance.Username);

                if (lobby == null) return;

                LoadingSceneManager.Instance.JoinRelayAndStartClient(lobby);
            } finally
            {
                isQuickJoinButtonBlocked = true;
            }
        }
    }
}
