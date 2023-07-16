using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Networking.Transport.Relay;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

using static Constants.LobbyService;
public class LobbyResponse
{
    public string PlayerId { get; set; }
    public Lobby Lobby { get; set; }

}

public class LobbyUtils
{
    public static async Task<LobbyResponse> CreateLobby(string lobbyName, string username, RelayResponse relayResponse, string description, bool isPrivate = false)
    {
        try
        {
            var player = new Player()
            {

                Data = new Dictionary<string, PlayerDataObject>()
                  {
                    { RELAY_ALLOCATION_ID, new PlayerDataObject(
                        PlayerDataObject.VisibilityOptions.Private,
                          relayResponse.AllocationId.ToString()
                          )
                  },
                      { USERNAME, new PlayerDataObject(
                          PlayerDataObject.VisibilityOptions.Member,
                          username
                          ) }
                  },
            };

            return new LobbyResponse()
            {
                PlayerId = player.Id,

                Lobby = await LobbyService.Instance.CreateLobbyAsync(
          lobbyName,
          2,
          new CreateLobbyOptions()
          {
              IsPrivate = isPrivate,

              Player = player,

              Data = new Dictionary<string, DataObject>()
              {
                  { HOST, new DataObject(
                         DataObject.VisibilityOptions.Public,
                         username
                        ) },

                  { DESCRIPTION, new DataObject(
                         DataObject.VisibilityOptions.Public,
                         description
                        ) },
                  { RELAY_CODE , new DataObject(
                         DataObject.VisibilityOptions.Public,
                         relayResponse.JoinCode
                        ) },

              }
          })
            };
        }
        catch (LobbyServiceException ex)
        {
            Debug.Log(ex);
            return null;
        }
    }

    public static async void MaintainLobbyHeartbeat(string lobbyId)
    {
        await Task.Run(async () =>
        {
            while (true)
            {
                await LobbyService.Instance.SendHeartbeatPingAsync(lobbyId); ;

                await Task.Delay(TimeSpan.FromSeconds(25));
            }
        });
    }

    public static async Task<LobbyResponse> JoinLobby(string lobbyCode, string username, Guid relayAllocationId)
    {
        try
        {
            var player = new Player
            {
                Data = new Dictionary<string, PlayerDataObject>()
                  {
                        {RELAY_ALLOCATION_ID, new PlayerDataObject(
                        PlayerDataObject.VisibilityOptions.Private,
                          relayAllocationId.ToString()
                          )
                  },
                      { USERNAME, new PlayerDataObject(
                          PlayerDataObject.VisibilityOptions.Member,
                          username
                          ) }
                  }
            };


            return new LobbyResponse()
            {
                PlayerId = player.Id,

                Lobby =
                await LobbyService.Instance.JoinLobbyByCodeAsync(lobbyCode,
            new JoinLobbyByCodeOptions()
            {
                Player = player
            })
            };
        }
        catch (LobbyServiceException ex)
        {
            Debug.Log(ex);
            return null;
        }
    }

}