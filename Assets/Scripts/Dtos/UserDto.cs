using Newtonsoft.Json;
using UnityEngine;

public class UserDto
{
    public class DeckCollection
    {
        [JsonProperty("selectedDeckIndex")]
        public int SelectedDeckIndex { get; set; }

        [JsonProperty("decks")]
        public Deck[] Decks { get; set; }
    }

    public class Deck
    {
        [JsonProperty("deckName")]
        public string DeckName { get; set; }

        [JsonProperty("playDeck")]
        public ComponentDeck PlayDeck { get; set; }

        [JsonProperty("characterDeck")]
        public ComponentDeck CharacterDeck { get; set; }

    }

    public class ComponentDeck
    {
        [JsonProperty("cards")]
        public string[] Cards { get; set; }
    }

    public class User
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("picture")]
        public Texture2D Picture { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("deckCollection")]
        public DeckCollection DeckCollection { get; set; }
    }

}