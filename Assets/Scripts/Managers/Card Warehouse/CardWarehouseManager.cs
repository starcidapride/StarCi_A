using System.Collections;
using UnityEngine;

using static AnimatorUtils;

public class CardWarehouseManager : Singleton<CardWarehouseManager>
{
    [SerializeField]
    private UserInventory inventory;

    [SerializeField]
    private Transform createFirstDeckModal;

    [SerializeField]
    private Transform createNewDeckModal;

    [SerializeField]
    private Transform ui;


    private void Start()
    {
        /*if (inventory.DeckCollection == null)
        {
            ModalController.Instance.InstantiateModal(createFirstDeckModal);
            return;
        }*/
    }

    public void DisplayUI()
    {
        StartCoroutine(DisplayUICoroutine());
    }
    public IEnumerator DisplayUICoroutine()
    {
        ui.gameObject.SetActive(true);

        yield return WaitForAnimationCompletion(ui);
    }
}
