using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Net.Http;

using static GridUtils;
using static CardUtils;
using static GameObjectUtils;


public class PlayDeckController : Singleton<PlayDeckController>
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

    public void AddCard(string cardName)
    {
        var result = ValidateCardAddition(ComponentDeckType.Play,
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
                inventory.AddCard(cardName, ComponentDeckType.Play);

                RenderDisplay();
                break;

            case CardAdditionResult.CardNotAllowed:
                AlertController.Instance.Show(AlertCaption.Error, $"Card '{cardName}' is not allowed in play deck.", button);
                break;

            case CardAdditionResult.MaxCardOccurrences:
                AlertController.Instance.Show(AlertCaption.Error, $"Card '{cardName}' has reached the max occurrences.", button);
                break;

            case CardAdditionResult.DeckReachedLimit:
                AlertController.Instance.Show(AlertCaption.Error, $"Play deck has reached the limit.", button);
                break;
        }
    }

    public void RemoveCard(string cardName)
    {
        inventory.RemoveCard(cardName, ComponentDeckType.Play);

        RenderDisplay(true);
    }

    public Transform GetTransform()
    {
        return container.transform;
    }



    public void RenderDisplay(bool firstRender = false)
    {
        StartCoroutine(RenderDisplayCoroutine(firstRender));
    }
    public IEnumerator RenderDisplayCoroutine(bool firstRender = false)
    {
        DestroyAllChildGameObjects(container);

        var grids = SplitSpriteIntoIndexedGrids(transform, 4, 10);
        var selectedPlayDeck = inventory.DeckCollection.Decks[inventory.DeckCollection.SelectedDeckIndex].PlayDeck;
        if (selectedPlayDeck.Count == 0) yield break;

        var isAnimations = new Dictionary<int, bool>
        {
            { selectedPlayDeck.Count - 1, true }
        };

        if (selectedPlayDeck.Count - 1 > 0)
        {
            for (int i = 0; i < selectedPlayDeck.Count - 1; i++)
            {
                isAnimations.Add(i, firstRender);
            }
        }

        for (int i = 0; i < selectedPlayDeck.Count; i++)
            {
                if (isAnimations[i])
            {
                StartCoroutine(InstantiateAndSetupCardCoroutine(selectedPlayDeck[i],
                                  grids[i].Center,
                                  Vector2.one / 4,
                                  container,
                                  new List<Type> { typeof(CardDragFromDeckController),
                                  typeof(CardShowcaseClickEventController)
                                  }));
            } 
            else {
                InstantiateAndSetupCard(selectedPlayDeck[i],
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
    public void OnDestroy()
    {
        inventory.InventoryTriggered -= OnInventoryTriggered;
    }
}