using Newtonsoft.Json;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static UserDto;

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
        if (inventory.DeckCollection == null) return;


    }

    private void OnDestroy()
    {
        inventory.InventoryTriggered -= OnInventoryTriggered;
    }
}
