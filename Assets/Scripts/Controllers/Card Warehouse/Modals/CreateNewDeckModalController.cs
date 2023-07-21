using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CreateNewDeckModalController : Singleton<CreateNewDeckModalController>
{
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
        var deckCollection = UserManager.Instance.DeckCollection;

        deckCollection.SelectedDeckIndex = deckCollection.Decks.Count;

        UserManager.Instance.AddDeck(deckName);

        ModalController.Instance.CloseNearestModal();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnSubmitButtonClick();
        }
    }
}