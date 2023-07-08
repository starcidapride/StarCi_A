using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ApiUtils;

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
    public string LastName { get; set;}

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
    [JsonProperty ("playDeck")]
    public ComponentDeck PlayDeck { get; set; }

    [JsonProperty ("characterDeck")]
    public ComponentDeck ComponentDeck { get; set; }
}

public class ComponentDeck
{
    [JsonProperty ("cardNames")]
    public List<string> CardNames;
}

public class TitleScreenManager : Singleton<TitleScreenManager>
{
    [SerializeField]
    private Transform signInModal;

    [SerializeField]
    private Transform signUpModal;

    public static bool Continue { get; set; }
    private IEnumerator Start()
    {
        var authResultTask = AuthenticationUtils.InitiateAnonymousSignIn();

        yield return new WaitUntil(() => authResultTask.IsCompleted);

        if (!authResultTask.Result)
        {
            var buttons = new List<AlertButton>()
            {
                new AlertButton()
                {
                    ButtonText = "Quit",
                    Script = typeof(QuitButtonController)
                },
                new AlertButton()
                {
                    ButtonText = "Try Again",
                    Script = typeof(TryAgainConnectButtonController)
                },
            };
            AlertController.Instance.Show(AlertCaption.Error, "Unable to establish an internet connection. Please ensure your network settings are configured correctly and attempt to reconnect.", buttons);
            
            yield return new WaitUntil(() => Continue);
        }
        
        var accessToken = GetAuthTokenFromPlayPrefs(AuthTokenType.AccessToken);

        if (!string.IsNullOrEmpty(accessToken))
        {
            LoadingSceneManager.Instance.LoadScene(SceneName.Home, false);
        }

    }
}
