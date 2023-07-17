using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardSearchBoxController : Singleton<CardSearchBoxController>
{
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

    public string CardName { get; set; }

    public CardTypeDropdown CardType { get; set; }

    public CharacterRoleDropdown CharacterRole { get; set; }

    public EquipmentClassDropdown EquipmentClass { get; set; }

    public void UpdateCardSearchBox(CardSearchBox data)
    {
        if (data.CardName != null)
            CardName = data.CardName;

        if (data.CardType != CardTypeDropdown.None)
            CardType = data.CardType;

        if (data.CharacterRole != CharacterRoleDropdown.None)
            CharacterRole = data.CharacterRole;

        if (data.CharacterRole != CharacterRoleDropdown.None)
            EquipmentClass = data.EquipmentClass;
    }


    public void UpdateCardSearchBoxThenNotify(CardSearchBox data)
    {
        UpdateCardSearchBox(data);

        ExecuteNotify();
    }

    private void Start()
    {

        CardName = cardNameTextInput.text;

        CardType = (CardTypeDropdown)cardTypeDropdownInput.value;

        CharacterRole = (CharacterRoleDropdown)characterRoleDropdownInput.value;

        EquipmentClass = (EquipmentClassDropdown)equipmentClassDropdownInput.value;
         

        cardNameTextInput.onEndEdit.AddListener(OnCardNameTextInputEndEdit);

        cardTypeDropdownInput.onValueChanged.AddListener(OnCardTypeDropdownInputValueChanged);

        characterRoleDropdownInput.onValueChanged.AddListener(OnCharacterRoleDropdownInputValueChanged);

        equipmentClassDropdownInput.onValueChanged.AddListener(OnEquipmentClassDropdownInputValueChanged);

        findButton.onClick.AddListener(OnFindButtonClick);
    }

    private void OnCardNameTextInputEndEdit(string value)
    {
        CardName = value;
    }

    private void OnCardTypeDropdownInputValueChanged(int value)
    {
        CardType = (CardTypeDropdown) value;

        switch (CardType) {
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
        CharacterRole = (CharacterRoleDropdown) value;
    }

    private void OnEquipmentClassDropdownInputValueChanged(int value)
    {
        EquipmentClass = (EquipmentClassDropdown) value;
    }

    private void OnFindButtonClick()
    {
        ExecuteNotify();
    }

    public delegate void NotifyEventHandler();

    public event NotifyEventHandler Notify;

    private void ExecuteNotify()
    {
        Notify?.Invoke();
    }
}
public class CardSearchBox
{
    public string CardName { get; set; }
    public CardTypeDropdown CardType { get; set; }
    public CharacterRoleDropdown CharacterRole { get; set; }
    public EquipmentClassDropdown EquipmentClass { get; set; }
}

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