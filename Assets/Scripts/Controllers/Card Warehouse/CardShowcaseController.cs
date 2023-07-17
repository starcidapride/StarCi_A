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
    private Transform container;

    private static int PresentPage;

    private void Start()
    {
        PresentPage = 0;

        CardSearchBoxController.Instance.Notify += OnNotify;
    }

    private void OnNotify()
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
            var cardNameFilter = kvp.Key.ContainsInsensitive(CardSearchBoxController.Instance.CardName);

            var cardTypeFilter = CardSearchBoxController.Instance.CardType == CardTypeDropdown.None || kvp.Value.Item1 == (CardType) CardSearchBoxController.Instance.CardType;

            bool IsMatchCharacterRole(CharacterRoleDropdown characterRole)
            {
                var cardType = map[kvp.Key].Item1;
                if (cardType != CardType.Character) return false;

                var cardClass = map[kvp.Key].Item2;

                var card = (ICharacterCard) Activator.CreateInstance(cardClass);

                if (card.CharacterRole != (CharacterRole)characterRole && CardSearchBoxController.Instance.CharacterRole != CharacterRoleDropdown.None) return false;

                return true;
            }

            bool IsMatchEquipmentRole(EquipmentClassDropdown equipmentClass)
            {
                var cardType = map[kvp.Key].Item1;
                if (cardType != CardType.Equipment) return false;

                var cardClass = map[kvp.Key].Item2;

                var card = (IEquipmentCard) Activator.CreateInstance(cardClass);

                if (card.EquipmentClass != (EquipmentClass)equipmentClass && CardSearchBoxController.Instance.EquipmentClass != EquipmentClassDropdown.None) return false;

   
                return true;
            }


            var characterRoleFilter =
            CardSearchBoxController.Instance.CardType != CardTypeDropdown.Character 
            || CardSearchBoxController.Instance.CharacterRole == CharacterRoleDropdown.None
            || IsMatchCharacterRole(CardSearchBoxController.Instance.CharacterRole);

            var equipmentClassFilter =
            CardSearchBoxController.Instance.CardType != CardTypeDropdown.Equipment
            || CardSearchBoxController.Instance.EquipmentClass == EquipmentClassDropdown.None
            || IsMatchEquipmentRole(CardSearchBoxController.Instance.EquipmentClass);

            return cardNameFilter && cardTypeFilter && characterRoleFilter && equipmentClassFilter;

        }).ToDictionary(kvp => kvp.Key, kvp => kvp.Value));

        var cardNames = sortedMap.Select(kvp => kvp.Key).ToList();

        for (int i = 12 * PresentPage; i < math.min(12 * (PresentPage + 1), cardNames.Count); i++) {

            StartCoroutine(InstantiateAndSetupCardCoroutine(cardNames[i], cardPositions[i].Center, new Vector2(0.25f, 0.25f), container, new List<Type> { typeof(CardShowcaseClickEventController), typeof(CardDragToDeckController) }));
        }

        UIController.Instance.SetInteractability(false);

        yield return new WaitForSeconds(0.6f);

        UIController.Instance.SetInteractability();
    }

    private void OnDestroy()
    {
        UserManager.Instance.Notify -= OnNotify;
    }

}
