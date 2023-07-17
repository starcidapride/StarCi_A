using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

using static LobbyUtils;
using static RelayUtils;

using static Constants.LobbyService;
using System.Threading.Tasks;

public class JoinLobbyButtonController : Singleton<JoinLobbyButtonController>
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    private async void OnButtonClick()
    { 
            GetComponent<Button>().interactable = false;

            var lobby = await JoinLobbyById(LobbyTableController.Instance.SelectedLobbyId, UserManager.Instance.Username);

            if (lobby == null) return;

            var joinCode = lobby.Data[RELAY_CODE].Value;

            var result = await JoinRelay(joinCode);

            if (!result) return;

            NetworkManager.Singleton.StartClient();     
    }


}
