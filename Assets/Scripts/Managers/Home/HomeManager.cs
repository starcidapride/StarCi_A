
using System.Collections;
using UnityEngine;
using UnityEngine.Profiling;

using static AnimatorUtils;

public class HomeManager : Singleton<HomeManager>
{
    [SerializeField]
    private UserInventory userInventory;

    [SerializeField]
    private Transform setupProfileModal;

    [SerializeField]
    private Transform ui;

    private void Start()
    {
        if (string.IsNullOrEmpty(userInventory.Username))
        {
            ModalController.Instance.InstantiateModal(setupProfileModal);
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