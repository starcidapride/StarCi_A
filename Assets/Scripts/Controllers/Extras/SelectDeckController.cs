using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectDeckController: Singleton<SelectDeckController>
{
    [SerializeField]
    private UserInventory inventory;

    [SerializeField]
    private TMP_Dropdown selectDeckDropdownInput;

    private void Start()
    {
        RenderDisplay();

        selectDeckDropdownInput.onValueChanged.AddListener(OnSelectDeckDropdownInputValueChanged);

        inventory.InventoryTriggered += OnInventoryTriggered;
        
    }
    private void OnInventoryTriggered()
    {
        RenderDisplay();
    }

    public void RenderDisplay()
    {
        var options = new List<TMP_Dropdown.OptionData>();

        foreach (var deck in inventory.DeckCollection.Decks)
        {
            options.Add(new TMP_Dropdown.OptionData()
            {
                text = deck.DeckName
            });
        }
        selectDeckDropdownInput.options = options;

        selectDeckDropdownInput.value = inventory.DeckCollection.SelectedDeckIndex;
    }

    private void OnSelectDeckDropdownInputValueChanged(int value)
    {
        inventory.AlterSelectedDeckThenNotify(value);
    }

    private void OnDestroy()
    {
        inventory.InventoryTriggered -= OnInventoryTriggered;
    }

}