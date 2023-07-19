using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

using static LobbyUtils;
using static RelayUtils;

using static Constants.LobbyService;

public class JoinLobbyButtonController : Singleton<JoinLobbyButtonController>
{
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        
        button.onClick.AddListener(OnButtonClick);

        LobbyTableController.Instance.Notify += OnNotify;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    private void OnNotify()
    {
        if (LobbyTableController.Instance.SelectedLobbyId == null)
        {
            button.interactable = false;
        } else
        {
            button.interactable = true;
        }
    }

    private async void OnButtonClick()
    {
            var lobby = await JoinLobbyById(LobbyTableController.Instance.SelectedLobbyId, UserManager.Instance.Username);

            if (lobby == null) return;

        LoadingSceneManager.Instance.JoinRelayAndStartClient(lobby);
    }

}
