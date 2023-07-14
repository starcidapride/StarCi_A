using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using System;
using Unity.Mathematics;

using static GridUtils;
using static CardMaps;
using static CardUtils;
using static GameObjectUtils;

public class CardShowcaseController : Singleton<CardShowcaseController>
{
    [SerializeField]
    private CardSearchBoxInventory inventory;

    [SerializeField]
    private Transform container;

    private static int PresentPage;

    private void Start()
    {
        PresentPage = 0;

        inventory.InventoryTriggered += OnInventoryTriggered;
    }

    private void OnInventoryTriggered()
    {
        RenderDisplay();
    }

    public void RenderDisplay()
    {
        StartCoroutine(RenderDisplayCoroutine());
    }
    public IEnumerator RenderDisplayCoroutine()
    {
        DestroyAllChildGameObjects(container);

        var cardPositions = SplitSpriteIntoIndexedGrids(container, 4, 3);

        var map = GetMap();

        var sortedMap = new SortedDictionary<string, (CardType, Type)>(map.Where(kvp =>
        {
            var cardNameFilter = kvp.Key.ContainsInsensitive(inventory.cardName);

            var cardTypeFilter = inventory.cardType == CardTypeDropdown.None || kvp.Value.Item1 == (CardType)inventory.cardType;

            bool IsMatchCharacterRole(CharacterRoleDropdown characterRole)
            {
                var cardType = map[kvp.Key].Item1;
                if (cardType != CardType.Character) return false;

                var cardClass = map[kvp.Key].Item2;

                var card = (ICharacterCard) Activator.CreateInstance(cardClass);

                if (card.CharacterRole != (CharacterRole)characterRole && inventory.characterRole != CharacterRoleDropdown.None) return false;

                return true;
            }

            bool IsMatchEquipmentRole(EquipmentClassDropdown equipmentClass)
            {
                var cardType = map[kvp.Key].Item1;
                if (cardType != CardType.Equipment) return false;

                var cardClass = map[kvp.Key].Item2;

                var card = (IEquipmentCard) Activator.CreateInstance(cardClass);

                if (card.EquipmentClass != (EquipmentClass)equipmentClass && inventory.equipmentClass != EquipmentClassDropdown.None) return false;

   
                return true;
            }


            var characterRoleFilter =
            inventory.cardType != CardTypeDropdown.Character 
            || inventory.characterRole == CharacterRoleDropdown.None
            || IsMatchCharacterRole(inventory.characterRole);

            var equipmentClassFilter =
            inventory.cardType != CardTypeDropdown.Equipment
            || inventory.equipmentClass == EquipmentClassDropdown.None
            || IsMatchEquipmentRole(inventory.equipmentClass);

            return cardNameFilter && cardTypeFilter && characterRoleFilter && equipmentClassFilter;

        }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value));

        var cardNames = sortedMap.Select(kvp => kvp.Key).ToList();

        for (int i = 12 * PresentPage; i < math.min(12 * (PresentPage + 1), cardNames.Count); i++){

            StartCoroutine(InstantiateAndSetupCardCoroutine(cardNames[i], cardPositions[i].Center, new Vector2(0.25f, 0.25f), container, typeof(CardShowcaseClickEventController)));
        }

        CardWarehouseUIController.Instance.SetInteractability(false);

        yield return new WaitForSeconds(0.6f);

        CardWarehouseUIController.Instance.SetInteractability();
    }

    private void OnDestroy()
    {
        inventory.InventoryTriggered -= OnInventoryTriggered;
    }

}
