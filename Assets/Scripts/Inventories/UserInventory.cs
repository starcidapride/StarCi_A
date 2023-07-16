using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

using static DeckApiService;
using static ImageUtils;
using static Constants.ButtonNames;
using static CardUtils;
using System.Net;

public class AddDeckResponseError
{
    [JsonProperty("deckNameError")]
    public string DeckNameError { get; set; }
}
public class User
{
    public string Email { get; set; }

    public string Username { get; set; }

    public Texture2D Picture { get; set; }

    public string Bio { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DeckCollection DeckCollection { get; set; }
}

[CreateAssetMenu(fileName = "User", menuName = "Inventories/User")]
public class UserInventory : ScriptableObject
{
    public string Email { get; set; }

    public string Username { get; set; }

    public Texture2D Picture { get; set; }

    public string Bio { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DeckCollection DeckCollection { get; set; }

    public void UpdateInventory(User user)
    {
        if (user.Email != null)
            Email = user.Email;

        if (user.Username != null)
            Username = user.Username;

        if (user.Picture != null)
            Picture = user.Picture;

        if (user.Bio != null)
            Bio = user.Bio;

        if (user.FirstName != null)
            FirstName = user.FirstName;

        if (user.LastName != null)
            LastName = user.LastName;

        if (user.DeckCollection != null)
            DeckCollection = user.DeckCollection;
    }

    public void UpdateInventoryThenNotify(User user)
    {
        UpdateInventory(user);

        ExecuteInventoryTrigger();
    }
    
    public void UpdateSelectedDeckIndex(int index)
    {
        DeckCollection.SelectedDeckIndex = index;

        ExecuteInventoryTrigger();
    }
    public async void Save()
    {
        var deck = DeckCollection.Decks[DeckCollection.SelectedDeckIndex];
        await ExecuteSaveDeck(
            new SaveDeckRequest()
            {
                DeckName = deck.DeckName,
                PlayCardNames = deck.PlayDeck,
                CharacterCardNames = deck.CharacterDeck,
            }, ClientErrorHandler
        );

        AlertController.Instance.Show(
            AlertCaption.Success, 
            "Save deck complete.",
            new List<AlertButton>()
            {
                new AlertButton()
                {
                    ButtonText = CANCEL,
                    Script = typeof(AlertCancelButtonController)
                }
            });
    }

    public void AlterSelectedDeckThenNotify(int index)
    {
        if (DeckCollection.SelectedDeckIndex != index)
        {
            DeckCollection.SelectedDeckIndex = index;

            ExecuteInventoryTrigger();
        }
    }

    public async void Default()
    {
        await ExecuteDefaultDeck(
            new DefaultDeckRequest()
            {
                DefaultDeckIndex = DeckCollection.SelectedDeckIndex
            }, ClientErrorHandler
        );

        string deckName = DeckCollection.Decks[DeckCollection.SelectedDeckIndex].DeckName;

        AlertController.Instance.Show(
            AlertCaption.Success,
            $"Deck '{deckName}' has been saved as the default deck.",
            new List<AlertButton>()
            {
        new AlertButton()
        {
            ButtonText = CANCEL,
            Script = typeof(AlertCancelButtonController)
        }
            }
        );
    }

    public void AddCard(string cardName, ComponentDeckType componentDeckType)
    {
        var deck = DeckCollection.Decks[DeckCollection.SelectedDeckIndex];

        var componentDeck = componentDeckType == ComponentDeckType.Play ? deck.PlayDeck : deck.CharacterDeck;

        var additionResult = ValidateCardAddition(componentDeckType, deck, cardName);

        if (additionResult == CardAdditionResult.Success) componentDeck.Add(cardName);
    }

    public void RemoveCard(string cardName, ComponentDeckType componentDeckType)
    {
        var deck = DeckCollection.Decks[DeckCollection.SelectedDeckIndex];

        var componentDeck = componentDeckType == ComponentDeckType.Play ? deck.PlayDeck : deck.CharacterDeck;

        if (!componentDeck.Contains(cardName)) return;

        componentDeck.Remove(cardName);
    }

    private void FailedExecuteAddDeckResponseHandler(string response, HttpStatusCode code)
    {
        var responseObject = JsonConvert.DeserializeObject<AddDeckResponseError>(response);
        AlertController.Instance.Show(
           AlertCaption.Error,
           responseObject.DeckNameError,
           new List<AlertButton>()
           {
        new AlertButton()
        {
            ButtonText = CANCEL,
            Script = typeof(AlertCancelButtonController)
        }
           });
    }
    public async void AddDeck(string deckName)
    {
        var user = await ExecuteAddDeck(
            new AddDeckRequest()
            {
                DeckName = deckName,

            }, FailedExecuteAddDeckResponseHandler, ClientErrorHandler
            );

        if (user == null) return;

        UpdateInventoryThenNotify(GetUser(user));

        AlertController.Instance.Show(
           AlertCaption.Success,
           $"Deck '{deckName}' has been created.",
           new List<AlertButton>()
           {
        new AlertButton()
        {
            ButtonText = CANCEL,
            Script = typeof(AlertCancelButtonController)
        }
           }
       );
    }

    public void Init()
    {
        Email = string.Empty;

        Username = string.Empty;

        Picture = null;

        Bio = string.Empty;

        FirstName = string.Empty;

        LastName = string.Empty;

        DeckCollection = null;

    }

    public static User GetUser(PresentableUser user)
    {
        return new User()
        {
            Email = user.Email,

            Username = user.Username,

            Picture = DecodeBase64Image(user.Picture),

            Bio = user.Bio,

            FirstName = user.FirstName,

            LastName = user.LastName,

            DeckCollection = user.DeckCollection
        };
    }

    public delegate void InventoryTriggeredEventHandler();

    public event InventoryTriggeredEventHandler InventoryTriggered;

    private void ExecuteInventoryTrigger()
    {
        InventoryTriggered?.Invoke();
    }

    private void ClientErrorHandler(HttpRequestException ex)
    {
        Debug.Log(ex);
    }
}