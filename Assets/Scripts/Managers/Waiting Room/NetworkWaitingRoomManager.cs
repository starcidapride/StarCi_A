
using System.Collections;
using Unity.Collections;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;

using static LobbyUtils;

public class NetworkWaitingRoomManager : SingletonNetwork<NetworkWaitingRoomManager>
{   
    public static Lobby Lobby { get; set; }

    public NetworkVariable<FixedString32Bytes> lobbyCode = new NetworkVariable<FixedString32Bytes>();

    public override void OnNetworkSpawn()
    {
        if (IsHost)
        {
            lobbyCode.Value = Lobby.LobbyCode;

            Debug.Log("i am host");
            MaintainLobbyHeartbeat(Lobby.Id);
        }
    }

    private void Update()
    {
        Debug.Log(lobbyCode.Value);
    }
}
