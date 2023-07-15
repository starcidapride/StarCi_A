using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardWarehouseDeckCollectionController : Singleton<CardWarehouseDeckCollectionController>
{
    [SerializeField]
    private UserInventory inventory;

    [SerializeField]
    private TMP_Dropdown selectDeckDropdownInput;

    [SerializeField]
    private Button createDeckButton;

    [SerializeField]
    private Button renameDeckButton;

    [SerializeField]
    private Button deleteDeckButton;

    [SerializeField]
    private Button saveDeckButton;

    [SerializeField]
    private Button defaultDeckButton;

    private void Start()
    {
        RenderDisplay();

        selectDeckDropdownInput.value = inventory.DeckCollection.SelectedDeckIndex;

        selectDeckDropdownInput.onValueChanged.AddListener(OnSelectDeckDropdownInputValueChanged);

        createDeckButton.onClick.AddListener(OnCreateDeckButtonClick);

        renameDeckButton.onClick.AddListener(OnRenameDeckButtonClick);

        deleteDeckButton.onClick.AddListener(OnDeleteDeckButtonClick);

        saveDeckButton.onClick.AddListener(OnSaveDeckButtonClick);

        defaultDeckButton.onClick.AddListener(OnDefaultDeckButtonClick);
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
        
    }

    private void OnSelectDeckDropdownInputValueChanged(int value)
    {
        inventory.AlterSelectedDeckThenNotify(value);
    }

    private void OnCreateDeckButtonClick()
    {

    }

    private void OnRenameDeckButtonClick()
    {

    }

    private void OnDeleteDeckButtonClick()
    {

    }

    private void OnSaveDeckButtonClick()
    {
        inventory.Save();
    }

    private void OnDefaultDeckButtonClick()
    {
        inventory.Default();
    }
}
