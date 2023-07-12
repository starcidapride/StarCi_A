using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UserDto;

public enum CardTypeDropdown
{
    None,

    Character,

    Equipment,

    Spell,

    Other
}

public enum CharacterRoleDropdown
{
    None,

    Warrior,

    Tank,

    Support,

    Mage,

    Marksman,

    Assassin
}

public enum EquipmentClassDropdown
{
    None,

    Attack,

    Magic,

    Defense
}

public class CardSearchBox
{
    public string CardName { get; set; }

    public CardTypeDropdown CardTypeInput { get; set; }
    public CharacterRoleDropdown CharacterRole { get; set; }
    public EquipmentClassDropdown EquipmentClass { get; set; }
}

[CreateAssetMenu(fileName = "Card Search Box", menuName = "Inventories/Card Search Box")]
public class CardSearchBoxInventory : ScriptableObject
{
    public string cardName;

    public CardTypeDropdown cardType;

    public CharacterRoleDropdown characterRole;

    public EquipmentClassDropdown equipmentClass;

    public void UpdateInventory(CardSearchBox data)
    {
        if (data.CardName != null)
            cardName = data.CardName;

        if (data.CharacterRole != CharacterRoleDropdown.None)
            characterRole = data.CharacterRole;

        if (data.CharacterRole != CharacterRoleDropdown.None)
            equipmentClass = data.EquipmentClass;
    }

    public void UpdateInventoryThenNotify(CardSearchBox data)
    {
        UpdateInventory(data);

        ExecuteInventoryTrigger();
    }

    public void Notify()
    {
        ExecuteInventoryTrigger();
    }

    public void Init()
    {
        cardName = string.Empty;

        cardType = CardTypeDropdown.None;

        characterRole = CharacterRoleDropdown.None;

        equipmentClass = EquipmentClassDropdown.None;
    }

    public delegate void InventoryTriggeredEventHandler();

    public event InventoryTriggeredEventHandler InventoryTriggered;

    private void ExecuteInventoryTrigger()
    {
        InventoryTriggered?.Invoke();
    }
}