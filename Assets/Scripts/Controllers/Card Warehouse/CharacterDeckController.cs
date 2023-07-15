using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GridUtils;
using static CardUtils;
using static GameObjectUtils;
using System;

public class CharacterDeckController : Singleton<CharacterDeckController>
{
    [SerializeField]
    private UserInventory inventory;

    [SerializeField]
    private Transform container;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();

        RenderDisplay(true);

        inventory.InventoryTriggered += OnInventoryTriggered;
    }

    private void OnInventoryTriggered()
    {
        RenderDisplay(true);
    }

    public Transform GetTransform()
    {
        return container.transform;
    }

    public void AddCard(string cardName)
    {
        var result = ValidateCardAddition(ComponentDeckType.Character,
            inventory.DeckCollection.Decks[inventory.DeckCollection.SelectedDeckIndex],
            cardName
            );

        var button = new List<AlertButton>()
{
    new AlertButton()
    {
        ButtonText = "CANCEL",
        Script = typeof(AlertCancelButtonController)
    }
};

        switch (result)
        {
            case CardAdditionResult.Success:
                inventory.AddCard(cardName, ComponentDeckType.Character);

                RenderDisplay();
                break;

            case CardAdditionResult.CardNotAllowed:
                AlertController.Instance.Show(AlertCaption.Error, $"Card '{cardName}' is not allowed in character deck.", button);
                break;

            case CardAdditionResult.MaxCardOccurrences:
                AlertController.Instance.Show(AlertCaption.Error, $"Card '{cardName}' has reached the max occurrences.", button);
                break;

            case CardAdditionResult.DeckReachedLimit:
                AlertController.Instance.Show(AlertCaption.Error, $"Character deck has reached the limit.", button);
                break;
        }
    }


    public void RenderDisplay(bool firstRender = false)
    {
        StartCoroutine(RenderDisplayCoroutine(firstRender));
    }
    public IEnumerator RenderDisplayCoroutine(bool firstRender = false)
    {
        DestroyAllChildGameObjects(container);

        var grids = SplitSpriteIntoIndexedGrids(transform, 1, 10);
        var selectedCharacterDeck = inventory.DeckCollection.Decks[inventory.DeckCollection.SelectedDeckIndex].CharacterDeck;
        if (selectedCharacterDeck.Count == 0) yield break;

        var isAnimations = new Dictionary<int, bool>
        {
            { selectedCharacterDeck.Count - 1, true }
        };

        if (selectedCharacterDeck.Count - 1 > 0)
        {
            for (int i = 0; i < selectedCharacterDeck.Count - 1; i++)
            {
                isAnimations.Add(i, firstRender);
            }
        }

        for (int i = 0; i < selectedCharacterDeck.Count; i++)
        {
            if (isAnimations[i])
            {
                StartCoroutine(InstantiateAndSetupCardCoroutine(selectedCharacterDeck[i],
                                  grids[i].Center,
                                  Vector2.one / 4,
                                  container,
                                  new List<Type> { typeof(CardDragFromDeckController),
                                  typeof(CardShowcaseClickEventController)
                                  }));
            }
            else
            {
                InstantiateAndSetupCard(selectedCharacterDeck[i],
                                   grids[i].Center,
                                   Vector2.one / 4,
                                   container,
                                   new List<Type> { typeof(CardDragFromDeckController),
                                   typeof(CardShowcaseClickEventController)
                                   });
            }
        }
        yield return new WaitForSeconds(0.6f);
    }

    public void RemoveCard(string cardName)
    {
        inventory.RemoveCard(cardName, ComponentDeckType.Character);

        RenderDisplay(true);
    }

    public void OnDestroy()
    {
        inventory.InventoryTriggered -= OnInventoryTriggered;
    }
}