using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

using static ApiUtils;
using static Constants.ButtonNames;
using static AuthApiService;
using static ImageUtils;
using System.Net.Http;

public class User
{
    [JsonProperty("username")]
    public string Username { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("bio")]
    public string Bio { get; set; }

    [JsonProperty("firstName")]
    public string FirstName { get; set; }

    [JsonProperty("lastName")]
    public string LastName { get; set; }

    [JsonProperty("deckCollection")]
    public DeckCollection DeckCollection { get; set; }
}

public class DeckCollection
{
    [JsonProperty("decks")]
    public List<Deck> Decks { get; set; }

    [JsonProperty("selectedDeckIndex")]
    public int SelectedDeckIndex { get; set; }
}

public class Deck
{
    [JsonProperty("playDeck")]
    public ComponentDeck PlayDeck { get; set; }

    [JsonProperty("characterDeck")]
    public ComponentDeck ComponentDeck { get; set; }
}

public class ComponentDeck
{
    [JsonProperty("cardNames")]
    public List<string> CardNames;
}


public class BootstrapManager : Singleton<BootstrapManager>
{
    [SerializeField]
    private UserInventory inventory;
    public static bool Continue { get; set; }
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => LoadingSceneManager.Instance != null);

        var authResultTask = AuthenticationUtils.InitiateAnonymousSignIn();

        yield return new WaitUntil(() => authResultTask.IsCompleted);

        if (!authResultTask.Result)
        {
            var buttons = new List<AlertButton>()
            {
                new AlertButton()
                {
                    ButtonText = QUIT,
                    Script = typeof(QuitButtonController)
                },
                new AlertButton()
                {
                    ButtonText = RECONNECT,
                    Script = typeof(ReconnectButtonController)
                },
            };
            AlertController.Instance.Show(AlertCaption.Error, "Unable to establish an internet connection. Please ensure your network settings are configured correctly and attempt to reconnect.", buttons);

            yield return new WaitUntil(() => Continue);
        }

        var accessToken = GetAuthTokenFromPlayPrefs(AuthTokenType.AccessToken);

        if (!string.IsNullOrEmpty(accessToken))
        {
            var initTask = ExecuteInit(ClientErrorHandler);

            yield return new WaitUntil(() => initTask.IsCompleted);

            var user = initTask.Result;

            if (user == null)
            {
                yield break;
            }

            inventory.Init();

            inventory.UpdateUser(
                new UserInventoryDTO()
                {
                    Email = user.Email,

                    Username = user.Username,

                    Picture = DecodeBase64Image(user.Picture),

                    Bio = user.Bio,

                    FirstName = user.FirstName,

                    LastName = user.LastName,
                }
                );

            LoadingSceneManager.Instance.LoadScene(SceneName.Home, false);
        }
        else
        {
            LoadingSceneManager.Instance.LoadScene(SceneName.Authentication, false);
        }
    }

    private void ClientErrorHandler(HttpRequestException ex)
    {
        Debug.Log(ex);
    }
}