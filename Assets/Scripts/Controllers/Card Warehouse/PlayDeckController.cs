using System.Collections.Generic;
using UnityEngine;
using System;

using static GridUtils;
using static CardUtils;
using System.Collections;

public class PlayDeckController : Singleton<PlayDeckController>
{
    [SerializeField]
    private UserInventory inventory;

    [SerializeField]
    private Transform container;

    private Dictionary<int, Cell> grids;
    private void Start()
    {
        grids = SplitSpriteIntoIndexedGrids(container, 4, 10);

        RenderDisplay();

        inventory.InventoryTriggered += OnInventoryTriggered;
        
    }

    public void AddCard(string cardName)
    {
 
    }

    private void OnInventoryTriggered()
    {
        RenderDisplay();
    }

    public Transform GetTransform()
    {
        return container.transform;
    }

    public void RenderDisplay()
    {
        StartCoroutine(RenderDisplayCoroutine());
    }
    public IEnumerator RenderDisplayCoroutine()
    {
        var selectedPlayDeck = inventory.DeckCollection.Decks[inventory.DeckCollection.SelectedDeckIndex].PlayDeck;
        
        for (int i = 0; i < selectedPlayDeck.Count; i++)
        {
            StartCoroutine(InstantiateAndSetupCardCoroutine(selectedPlayDeck[i],
                grids[i].Center, 
                Vector2.one / 4, 
                container, 
                new Type[] {typeof(CardDragFromDeckController)}));
        }

        yield return new WaitForSeconds(0.6f);
    }
}