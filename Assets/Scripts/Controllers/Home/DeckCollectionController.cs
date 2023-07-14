using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckCollectionController : Singleton<DeckCollectionController>
{
    [SerializeField]
    private UserInventory inventory;

    [SerializeField]
    private TMP_Dropdown selectDeckDropdownInput;

    [SerializeField]
    private Button goToCardWarehouseButton;

    private void Start()
    {
        RenderDisplay();

        inventory.InventoryTriggered += OnInventoryTriggered;

        selectDeckDropdownInput.onValueChanged.AddListener(OnSelectDeckDropdownInputValueChanged);

        goToCardWarehouseButton.onClick.AddListener(OnGoToCardWarehouseButton);
    }

    private void OnInventoryTriggered()
    {
        RenderDisplay();
    }

    private void OnSelectDeckDropdownInputValueChanged(int value)
    {
        inventory.UpdateSelectedDeckIndex(value);
    }

    private void OnGoToCardWarehouseButton()
    {
        LoadingSceneManager.Instance.LoadScene(SceneName.CardWarehouse, false);
    }

    private void RenderDisplay()
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
    }

    private void OnDestroy()
    {
        inventory.InventoryTriggered -= OnInventoryTriggered;
    }
}
