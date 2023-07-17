using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectDeckController: Singleton<SelectDeckController>
{
    [SerializeField]
    private TMP_Dropdown selectDeckDropdownInput;

    private void Start()
    {
        RenderDisplay();

        selectDeckDropdownInput.onValueChanged.AddListener(OnSelectDeckDropdownInputValueChanged);

        UserManager.Instance.Notify += OnNotify;
        
    }
    private void OnNotify()
    {
        RenderDisplay();
    }

    public void RenderDisplay()
    {
        var options = new List<TMP_Dropdown.OptionData>();

        foreach (var deck in UserManager.Instance.DeckCollection.Decks)
        {
            options.Add(new TMP_Dropdown.OptionData()
            {
                text = deck.DeckName
            });
        }
        selectDeckDropdownInput.options = options;

        selectDeckDropdownInput.value = UserManager.Instance.DeckCollection.SelectedDeckIndex;
    }

    private void OnSelectDeckDropdownInputValueChanged(int value)
    {
        UserManager.Instance.AlterSelectedDeckThenNotify(value);
    }

    private void OnDestroy()
    {
        UserManager.Instance.Notify -= OnNotify;
    }

}