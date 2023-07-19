using System.Collections;
using System.Linq;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class WaitingRoomButtonsController : Singleton<WaitingRoomButtonsController>
{
    [SerializeField]
    private Button readyButton;

    [SerializeField]
    private Button startButton;

    private void Start()
    {
        NetworkGameManager.Instance.Notify += OnNotify;

        readyButton.onClick.AddListener(NetworkGameManager.Instance.OnReadyButtonClick);

        startButton.onClick.AddListener(NetworkGameManager.Instance.OnReadyButtonClick);
    }

    private void OnNotify()
    {   
        if (NetworkManager.Singleton.IsHost)
        {
            var ready = NetworkGameManager.Instance.ConnectedUsers.Value.users.Select(user => user.isReady).
                Where(ready => ready == true).ToArray().Length == 2;

            if  ( ready ) {
                startButton.interactable = true;
            } else
            {
                startButton.interactable = false;
            }
        }
    }
}
