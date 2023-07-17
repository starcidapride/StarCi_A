using System.Collections;
using UnityEngine;

using static AnimatorUtils;

public class CardWarehouseManager : Singleton<CardWarehouseManager>
{
    [SerializeField]
    private Transform createFirstDeckModal;

    [SerializeField]
    private Transform createNewDeckModal;

    [SerializeField]
    private Transform ui;

    private void Start()
    {
        if (UserManager.Instance.DeckCollection.Decks.Count == 0)
        {
            ModalController.Instance.InstantiateModal(createFirstDeckModal);
            return;
        }

        DisplayUI();
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
