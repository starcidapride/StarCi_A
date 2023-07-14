using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateFirstDeckModalController : Singleton<CreateFirstDeckModalController>
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
        inventory.AddDeck(
            new Deck()
            {
                DeckName = deckName,

                PlayDeck = new List<string>(),

                CharacterDeck = new List<string>()
            });

        ModalController.Instance.CloseNearestModal();

        CardWarehouseManager.Instance.DisplayUI();
    }

}