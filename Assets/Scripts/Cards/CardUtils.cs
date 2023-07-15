using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static CardMaps;

using static AnimatorUtils;

using static Constants.Prefabs.Cards;
using static Constants.Triggers.Card;
using static Constants.AbilityNames;
using static Constants.DeckLimits;
using static Constants.CardNames.Others;


public enum ComponentDeckType
{
    Play,
    Character
}

public enum CardAdditionResult
{
    Success,
    CardNotAllowed,
    DeckReachedLimit,
    MaxCardOccurrences,
}



public class CardUtils
{
    private static ICharacterCard GetCharacterCardObject(string cardName)
    {
        var characterCardPrefab = Resources.Load<Transform>(CHARACTER_CARD);

        characterCardPrefab.name = cardName;

        var map = GetCharactersMap();

        var cardClass = map.ContainsKey(cardName) ? map[cardName] : null;

        if (cardClass != null)
        {
            return (ICharacterCard)Activator.CreateInstance(cardClass);
        }
        return null;
    }
    private static Transform InstantiateCharaterCard(string cardName, Transform parent = null)
    {
        var characterCardPrefab = Resources.Load<Transform>(CHARACTER_CARD);

        var cardClassObject = GetCharacterCardObject(cardName);

        var card = UnityEngine.Object.Instantiate(characterCardPrefab, parent);

        var attributes = new CharacterCardAttributes()
        {
            CardName = cardName,

            Image = cardClassObject.Image,

            CharacterRole = cardClassObject.CharacterRole,

            Experience = cardClassObject.Experience,

            MaxHealth = cardClassObject.MaxHealth,

            AttackDamage = cardClassObject.AttackDamage,

            Armor = cardClassObject.Armor,

            MagicResistance = cardClassObject.MagicResistance,

            Passive = cardClassObject.PassiveImage,

            Q = cardClassObject.QImage,

            E = cardClassObject.EImage,

            R = cardClassObject.RImage,
        };

        var controller = card.GetComponent<CharacterCardController>();
        controller.RenderDisplay(attributes);

        return card;
    }

    private static Transform InstantiateEquipmentCard(string cardName, Transform parent = null)
    {
        var equipmentCardPrefab = Resources.Load<Transform>(EQUIPMENT_CARD);

        equipmentCardPrefab.name = cardName;

        var map = GetEquipmentMap();

        Type cardClass = map.ContainsKey(cardName) ? map[cardName] : null;

        if (cardClass == null) return null;

        var card = UnityEngine.Object.Instantiate(equipmentCardPrefab, parent);

        var cardClassObject = (IEquipmentCard)Activator.CreateInstance(cardClass);

        var attributes = new EquipmentCardAttributes
        {
            CardName = cardName,

            Image = cardClassObject.Image,

            EquipmentClass = cardClassObject.EquipmentClass,

            EquipmentPrice = cardClassObject.Price,

            Stats = cardClassObject.Stats,

            Description = cardClassObject.Description
        };

        var controller = card.GetComponent<EquipmentCardController>();

        controller.RenderDisplay(attributes);

        return card;
    }

    private static Transform InstantiateSpellCard(string cardName, Transform parent = null)
    {
        var spellCardPrefab = Resources.Load<Transform>(SPELL_CARD);

        spellCardPrefab.name = cardName;

        var map = GetSpellMap();

        Type cardClass = map.ContainsKey(cardName) ? map[cardName] : null;

        if (cardClass == null) return null;

        var card = UnityEngine.Object.Instantiate(spellCardPrefab, parent);

        var cardClassObject = (ISpellCard)Activator.CreateInstance(cardClass);

        var attributes = new SpellCardAttributes()
        {
            CardName = cardName,

            Image = cardClassObject.Image,

            Description = cardClassObject.Description
        };

        var controller = card.GetComponent<SpellCardController>();

        controller.RenderDisplay(attributes);

        return card;
    }

    private static Transform InstantiateOtherCard(string cardName, Transform parent = null)
    {
        var otherCardPrefab = Resources.Load<Transform>(OTHER_CARD);

        otherCardPrefab.name = cardName;

        var map = GetOtherMap();

        Type cardClass = map.ContainsKey(cardName) ? map[cardName] : null;

        if (cardClass == null) return null;

        var card = UnityEngine.Object.Instantiate(otherCardPrefab, parent);

        var cardClassObject = (IOtherCard)Activator.CreateInstance(cardClass);

        var attributes = new OtherCardAttributes()
        {
            CardName = cardName,

            Frame = cardClassObject.Frame,

            Image = cardClassObject.Image,

            Description = cardClassObject.Description
        };

        var controller = card.GetComponent<OtherCardController>();

        controller.RenderDisplay(attributes);

        return card;
    }

    public static Transform InstantiateCard(string cardName, Transform parent = null)
    {
        var cardType = GetMap()[cardName].Item1;

        return cardType switch
        {
            CardType.Character => InstantiateCharaterCard(cardName, parent),

            CardType.Equipment => InstantiateEquipmentCard(cardName, parent),

            CardType.Spell => InstantiateSpellCard(cardName, parent),

            _ => InstantiateOtherCard(cardName, parent)
        };
    }

    public static Transform InstantiateAndSetupCard(string cardName, Vector2 position, Vector2 scale, Transform parent = null, List<Type> scripts = null)
    {
        var card = InstantiateCard(cardName, parent);

        card.localPosition = position;

        card.transform.localScale = scale;

        if (scripts != null)
        {
            foreach (Type script in scripts)
            {
                if (script != null && typeof(Component).IsAssignableFrom(script))
                {
                    var cardEventController = card.gameObject.AddComponent(script);

                    if (cardEventController is CardEventController)
                    {
                        var cardEvent = (CardEventController)cardEventController;
                        cardEvent.CardName = cardName;
                    }
                }
            }
        }
        
        return card;
    }


    public static IEnumerator InstantiateAndSetupCardCoroutine(string cardName, Vector2 position, Vector2 scale, Transform parent = null, List<Type> scripts = null)
    {
        var card = InstantiateAndSetupCard(cardName, position, scale, parent, scripts);

        yield return ExecuteTriggerThenWait(card, CARD_INSTANTIATION_TRIGGER);
    }

    public static Dictionary<string, AbilityAttributes> GetAbilityAttributes(string cardName)
    {
        var characterCardClassObject = GetCharacterCardObject(cardName);

        if (characterCardClassObject == null) return null;

        return new Dictionary<string, AbilityAttributes>()
        {
            {PASSIVE, new AbilityAttributes(){
                AbilityImage = characterCardClassObject.PassiveImage,
                AbilityName = characterCardClassObject.PassiveName,
                AbilityDescription = characterCardClassObject.PassiveDescription
            }},
            {Q, new AbilityAttributes(){
                AbilityImage = characterCardClassObject.QImage,
                AbilityName = characterCardClassObject.QName,
                AbilityDescription = characterCardClassObject.QDescription
            }},
            {E, new AbilityAttributes(){
                AbilityImage = characterCardClassObject.EImage,
                AbilityName = characterCardClassObject.EName,
                AbilityDescription = characterCardClassObject.EDescription
            }},
            {R, new AbilityAttributes(){
                AbilityImage = characterCardClassObject.RImage,
                AbilityName = characterCardClassObject.RName,
                AbilityDescription = characterCardClassObject.RDescription
            }},
        };
    }

    public static CardAdditionResult ValidateCardAddition(ComponentDeckType deckType, Deck deck, string cardName)
    {
        var maxCards = deckType == ComponentDeckType.Play ? MAX_PLAY_CARDS : MAX_CHARACTER_CARDS;
        var maxOccurrences = deckType == ComponentDeckType.Play ? MAX_PLAY_OCCURRENCES : MAX_CHARACTER_OCCURRENCES;
        var componentDeck = deckType == ComponentDeckType.Play ? deck.PlayDeck : deck.CharacterDeck;

        var map = GetMap();

        if (! (map[cardName].Item1 == CardType.Character ^ deckType == ComponentDeckType.Play))
            return CardAdditionResult.CardNotAllowed;

        if (componentDeck.Count >= maxCards) return CardAdditionResult.DeckReachedLimit;

        var cardOccurrences = componentDeck.Count(_cardName => _cardName == cardName);

        if (cardOccurrences >= (cardName == INVOCATION ? MAX_INVOCATION_OCCURRENCES : maxOccurrences))
            return CardAdditionResult.MaxCardOccurrences;
        
        return CardAdditionResult.Success;
    }

}

public class AbilityAttributes
{
    public Texture2D AbilityImage { get; set; }
    public string AbilityName { get; set; }
    public string AbilityDescription { get; set; }
}