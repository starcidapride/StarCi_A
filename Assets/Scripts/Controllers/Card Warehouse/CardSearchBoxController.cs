using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSearchBoxController : Singleton<CardSearchBoxController>
{
    [SerializeField] 
    private CardSearchBoxInventory inventory;

    [SerializeField]
    private TMP_InputField cardNameTextInput;

    [SerializeField]
    private TMP_Dropdown cardTypeDropdownInput;

    [SerializeField]
    private TMP_Dropdown characterRoleDropdownInput;

    [SerializeField]
    private TMP_Dropdown equipmentClassDropdownInput;

    [SerializeField]
    private Button findButton;

    private void Start()
    {
        inventory.UpdateInventory(
            new CardSearchBox
            {
                CardName = cardNameTextInput.text,
                
                CardTypeInput = (CardTypeDropdown) cardTypeDropdownInput.value,

                CharacterRole = (CharacterRoleDropdown) characterRoleDropdownInput.value,

                EquipmentClass = (EquipmentClassDropdown) equipmentClassDropdownInput.value,
            }
            );

        cardNameTextInput.onEndEdit.AddListener(OnCardNameTextInputEndEdit);

        cardTypeDropdownInput.onValueChanged.AddListener(OnCardTypeDropdownInputValueChanged);

        characterRoleDropdownInput.onValueChanged.AddListener(OnCharacterRoleDropdownInputValueChanged);

        equipmentClassDropdownInput.onValueChanged.AddListener(OnEquipmentClassDropdownInputValueChanged);

        findButton.onClick.AddListener(OnFindButtonClick);
    }

    private void OnCardNameTextInputEndEdit(string value)
    {
        inventory.cardName = value;
    }

    private void OnCardTypeDropdownInputValueChanged(int value)
    {
        inventory.cardType = (CardTypeDropdown) value;

        switch (inventory.cardType) {
            case CardTypeDropdown.Character:
                characterRoleDropdownInput.transform.parent.gameObject.SetActive(true);
                equipmentClassDropdownInput.transform.parent.gameObject.SetActive(false);
                break;

            case CardTypeDropdown.Equipment:
                characterRoleDropdownInput.transform.parent.gameObject.SetActive(false);
                equipmentClassDropdownInput.transform.parent.gameObject.SetActive(true);
                break;

            default:
                characterRoleDropdownInput.transform.parent.gameObject.SetActive(false);
                equipmentClassDropdownInput.transform.parent.gameObject.SetActive(false);
                break;
        }
    }

    private void OnCharacterRoleDropdownInputValueChanged(int value)
    {
        inventory.characterRole = (CharacterRoleDropdown) value;
    }

    private void OnEquipmentClassDropdownInputValueChanged(int value)
    {
        inventory.equipmentClass = (EquipmentClassDropdown) value;
    }

    private void OnFindButtonClick()
    {
        inventory.Notify();
    }
}