
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Collections;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using System.Linq;

using static LobbyUtils;
using static Constants.LobbyService;
using static ProfileApiService;
using Newtonsoft.Json.Serialization;

public class NetworkGameManager : SingletonNetworkPersistent<NetworkGameManager>
{  
    public static Lobby Lobby { get; set; }

    public NetworkVariable<FixedString32Bytes> LobbyCode = new ();

    public NetworkVariable<FixedString32Bytes> LobbyName = new();

    public NetworkVariable<FixedString512Bytes> Description = new();

    public NetworkVariable<bool> Private = new();

    public NetworkVariable<NetworkUsers> ConnectedUsers = new(new NetworkUsers() { users = new List<NetworkUser>(),
    }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner
    );

    public User You { get; set; }

    private User opponent;
    public User Opponent
    {
        get { return opponent; }
        set {  if (opponent != value) {

                opponent = value;

                ExecuteNotify();
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void UpdateConnectedUsersServerRpc(NetworkUsers users, ServerRpcParams serverParams = default)
    {
        ConnectedUsers.Value = users;
    }

    public delegate void NotifyEventHandler();

    public event NotifyEventHandler Notify;

    private void ExecuteNotify()
    {
        Notify?.Invoke();
    }

    private bool canUpdate = false;



    public override void OnNetworkSpawn()
    {
        Debug.Log("Spawned");

        if (IsHost)
        {
            LobbyCode.Value = Lobby.LobbyCode;

            LobbyName.Value = Lobby.Name;

            Description.Value = Lobby.Data[DESCRIPTION].Value;

            Private.Value = Lobby.IsPrivate;

            MaintainLobbyHeartbeat(Lobby.Id);
        }

        You = UserManager.Instance.GetUser();

            var currentConnectedUser = ConnectedUsers.Value;
            
            var deck = You.DeckCollection.Decks[You.DeckCollection.SelectedDeckIndex];


        currentConnectedUser.users.Add(new NetworkUser()
        {
            clientId = NetworkManager.LocalClientId,
            email = You.Email,
            username = You.Username,

            gameSnapshot = new NetworkGameSnapshot()
            {
                playDeckCards = deck.PlayDeck.Select(card => new FixedString32Bytes(card)).ToList(),

                characterDeckCards = deck.CharacterDeck.Select(card => new FixedString32Bytes(card)).ToList(),

                handCards = new List<FixedString32Bytes>()
            }
        });

        if (IsClient)
        {
            UpdateConnectedUsersServerRpc(currentConnectedUser);
        }
            
        canUpdate = true;
    }

    private async void Update()
    {
        if (canUpdate)
        {
            if (ConnectedUsers.Value.users.Count > 1)
            {

                    var user = await ExecuteGetProfile(
                    ConnectedUsers.Value.users.
                    Where(user => user.clientId != NetworkManager.LocalClientId).
                    First().email.ToString());

                
                if (user == null) return;

                Opponent = UserManager.MapPresentableUserToUser(user);

                canUpdate = false;
            }
        }
    }

}



public struct NetworkUsers : INetworkSerializable
{
    public List<NetworkUser> users;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        var usersArray = new NetworkUser[0];

        if (!serializer.IsReader)
        {
            usersArray = users.ToArray();
        }

            int usersLength = 0;

        if (!serializer.IsReader)
        {
            usersLength = usersArray.Length;
        }

        serializer.SerializeValue(ref usersLength);

        if (serializer.IsReader)
        {
            usersArray = new NetworkUser[usersLength];
        }

        for (int i = 0; i < usersLength; i++)
        {
            serializer.SerializeValue(ref usersArray[i]);
        }

        if (serializer.IsReader)
        {
            users = usersArray.ToList();
        }
    }
}
    public struct NetworkUser : INetworkSerializable
    {
        public ulong clientId;
        public FixedString64Bytes email;
        public FixedString64Bytes username;

        public NetworkGameSnapshot gameSnapshot;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref clientId);

            serializer.SerializeValue(ref email);

            serializer.SerializeValue(ref username);

            serializer.SerializeValue(ref gameSnapshot);
        }

    }

public struct NetworkGameSnapshot : INetworkSerializable
{
    public List<FixedString32Bytes> playDeckCards;

    public List<FixedString32Bytes> characterDeckCards;

    public List<FixedString32Bytes> handCards;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        var playDeckCardsArray = new FixedString32Bytes[0];

        var characterDeckCardsArray = new FixedString32Bytes[0];

        var handCardsArray = new FixedString32Bytes[0];

        if (!serializer.IsReader)
        {
            playDeckCardsArray = playDeckCards.ToArray();

            characterDeckCardsArray = characterDeckCards.ToArray ();

            handCardsArray = handCards.ToArray ();
        }

        int playDeckCardsLength = 0;

        int characterDeckCardsLength = 0;

        int handCardsLength = 0;

        if (!serializer.IsReader)
        {
            playDeckCardsLength = playDeckCardsArray.Length;

            characterDeckCardsLength = characterDeckCardsArray.Length;

            handCardsLength = handCardsArray.Length;
        }

        serializer.SerializeValue(ref playDeckCardsLength);

        serializer.SerializeValue(ref characterDeckCardsLength);

        serializer.SerializeValue(ref handCardsLength);

        if (serializer.IsReader)
        {
            playDeckCardsArray = new FixedString32Bytes[playDeckCardsLength];

            characterDeckCardsArray = new FixedString32Bytes[characterDeckCardsLength];

            handCardsArray = new FixedString32Bytes[handCardsLength] ;
        }

        for (int i = 0; i < playDeckCardsLength; i++)
        {
            serializer.SerializeValue(ref playDeckCardsArray[i]);
        }

        for (int i = 0; i < characterDeckCardsLength; i++)
        {
            serializer.SerializeValue(ref characterDeckCardsArray[i]);
        }

        for (int i = 0; i < handCardsLength; i++)
        {
            serializer.SerializeValue(ref handCardsArray[i]);
        }

        if (serializer.IsReader)
        {
            playDeckCards = playDeckCardsArray.ToList();

            characterDeckCards = characterDeckCardsArray.ToList();

            handCards = handCardsArray.ToList();
        }
    }
}

    
