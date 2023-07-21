using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using UnityEngine;

using static Constants.LobbyService;
using static EnumUtils;
using static Constants.ButtonNames;
using Unity.Services.Authentication;

public enum LobbyStatus
{
    [Description("Available")]
    Available,

    [Description("Not Available")]
    NotAvailable
}

public class LobbyUtils
{
  

    public static async Task<Lobby> CreateLobby(string lobbyName, string username, string joinCode, string description, bool isPrivate = false)
    {
        try
        {
            var player = new Player()
            {

                Data = new Dictionary<string, PlayerDataObject>()
                  {
                      { USERNAME, new PlayerDataObject(
                          PlayerDataObject.VisibilityOptions.Member,
                          username
                          ) }
                  },
            };

            return

              await LobbyService.Instance.CreateLobbyAsync(
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

                  { STATUS, new DataObject(
                      DataObject.VisibilityOptions.Public,
                      GetDescription(LobbyStatus.Available)
                      ) },

                  { RELAY_CODE, new DataObject(
                         DataObject.VisibilityOptions.Public,
                         joinCode
                        ) },
              }
          }
              );
        }
        catch (LobbyServiceException ex)
        {
            Debug.Log(ex);

            HandleTimedOut(ex);

            return null;
        }

    }

    public static async void MaintainLobbyHeartbeat(string lobbyId)
    {
        await Task.Run(async () =>
        {
            while (true)
            {
                try
                {
                    await LobbyService.Instance.SendHeartbeatPingAsync(lobbyId); ;

                    await Task.Delay(TimeSpan.FromSeconds(25));
                }
                catch (LobbyServiceException ex)
                {
                    HandleTimedOut(ex);
                }
            }
        });
    }

    public static async Task<Lobby> QuickJoin(string username)
    {
        try
        {
            var player = new Player
            {
                Data = new Dictionary<string, PlayerDataObject>
            {
                {
                    USERNAME, new PlayerDataObject(
                        PlayerDataObject.VisibilityOptions.Member,
                        username
                    )
                }
            }
            };

            return await LobbyService.Instance.QuickJoinLobbyAsync(
                new QuickJoinLobbyOptions
                {
                    Player = player
                }
            );
        }

        catch (LobbyServiceException ex)
        {

            HandleExceedRateLimit(ex);

            HandleNoAvailableLobbies(ex);
            
            HandleTimedOut(ex);

            return null;
        }
    }

    public static async Task<Lobby> JoinLobbyByCode(string lobbyCode, string username)
    {
        try
        {
            var player = new Player
            {
                Data = new Dictionary<string, PlayerDataObject>
            {
                {
                    USERNAME, new PlayerDataObject(
                        PlayerDataObject.VisibilityOptions.Member,
                        username
                    )
                }
            }
            };

            return await LobbyService.Instance.JoinLobbyByCodeAsync(
                lobbyCode,
                new JoinLobbyByCodeOptions
                {
                    Player = player
                }
            );
        }

        catch (LobbyServiceException ex)
        {
            Debug.Log(ex);

            HandleLobbyCodeIncorrect(ex);

            HandleTimedOut(ex);

            return null;
        }
    }

    public static async Task<Lobby> JoinLobbyById(string lobbyId, string username)
    {
        try
        {
            var player = new Player
            {
                Data = new Dictionary<string, PlayerDataObject>
            {
                {
                    USERNAME, new PlayerDataObject(
                        PlayerDataObject.VisibilityOptions.Member,
                        username
                    )
                }
            }
            };

            return await LobbyService.Instance.JoinLobbyByIdAsync(
                lobbyId,
                new JoinLobbyByIdOptions
                {
                    Player = player
                }
            );
        }

        catch (LobbyServiceException ex)
        {
            Debug.Log(ex);

            HandleTimedOut(ex);

            return null;
        }
    }

    public static async Task<bool> LeaveLobby(string lobbyId)
    {
        try
        {
            Debug.Log(lobbyId);

            await LobbyService.Instance.RemovePlayerAsync(lobbyId, AuthenticationService.Instance.PlayerId);
            return true;
        }
        catch (LobbyServiceException ex)
        {
            Debug.Log(ex);

            HandleTimedOut(ex);

            return false;
        }
    }

    public static async Task<List<Lobby>> GetLobbies()
    {
        try
        {
            var queryResults = await Lobbies.Instance.QueryLobbiesAsync();

            return queryResults.Results;

        }
        catch (LobbyServiceException ex)
        {
            Debug.Log(ex);

            HandleTimedOut(ex);

            return null;
        }
    }

    private static void HandleTimedOut(LobbyServiceException ex)
    {
        if (ex.ErrorCode == 16998)
        {
            AlertController.Instance.Show(
            AlertCaption.Error,
            "The request to lobby service has timed out. Please try again later.",
            new List<AlertButton>()
        {
                  new AlertButton()
                  {
                      ButtonText = CANCEL,
                      Script = typeof(AlertCancelButtonController)
                  }
        });
        }
    }

    private static void HandleExceedRateLimit(LobbyServiceException ex)
    {
        if (ex.ErrorCode == 16429)
        {
            AlertController.Instance.Show(
            AlertCaption.Error,
          "The request to lobby service cannot be processed at the moment due to exceeding the rate limit. Please wait a while and try again later.",
          new List<AlertButton>()
      {
                  new AlertButton()
    {
        ButtonText = CANCEL,
                      Script = typeof(AlertCancelButtonController)
                  }
});
        }
    }

    private static void HandleLobbyCodeIncorrect(LobbyServiceException ex)
    {
        if (ex.ErrorCode == 16010)
        {
            AlertController.Instance.Show(
            AlertCaption.Error,
            "The entered lobby code is incorrect. Please attempt again.",
            new List<AlertButton>()
        {
                  new AlertButton()
                  {
                      ButtonText = CANCEL,
                      Script = typeof(AlertCancelButtonController)
                  }
        });
        }
    }

    private static void HandleNoAvailableLobbies(LobbyServiceException ex)
    {
        if (ex.ErrorCode == 16006)
        {
            AlertController.Instance.Show(
            AlertCaption.Error,
            "There are no available lobbies at the moment. Please try again.",
            new List<AlertButton>()
        {
                  new AlertButton()
                  {
                      ButtonText = CANCEL,
                      Script = typeof(AlertCancelButtonController)
                  }
        });
        }
    }

}