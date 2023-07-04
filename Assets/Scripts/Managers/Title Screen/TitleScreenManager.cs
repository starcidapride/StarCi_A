using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

using static MongoDBUtils;
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
    private async void Start()
    {
        var authResult = await AuthenticationUtils.InitiateAnonymousSignIn();
        if (!authResult) return;

        var username = PlayerPrefs.GetString("username");

        if (username == null) return;

    }
}
