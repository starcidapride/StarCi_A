﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CreateNewDeckModalController : Singleton<CreateNewDeckModalController>
{
    [SerializeField]
    private UserInventory inventory;

    [SerializeField]
    private TMP_InputField deckNameTextInput;

    [SerializeField]
    private Button submitButton;

    private string deckName;

    private void Start()
    {
        deckNameTextInput.onEndEdit.AddListener(OnDeckNameTextInputEndEdit);

        submitButton.onClick.AddListener(OnSubmitButtonClick);
    }

    private void OnDeckNameTextInputEndEdit(string value)
    {
        deckName = value;
    }

    private void OnSubmitButtonClick()
    {
        inventory.DeckCollection.SelectedDeckIndex = inventory.DeckCollection.Decks.Count;

        inventory.AddDeck(deckName);
            

        ModalController.Instance.CloseNearestModal();
    }

}