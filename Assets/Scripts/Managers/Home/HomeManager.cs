
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
        if (string.IsNullOrEmpty(userInventory.username))
        {
            ModalController.Instance.InstantiateModal(setupProfileModal);
            return;
        }

        DisplayProfileUI();
    }

    public void DisplayProfileUI()
    {
        StartCoroutine(DisplayProfileUICoroutine());
    }
    public IEnumerator DisplayProfileUICoroutine()
    {
        ui.gameObject.SetActive(true);

        yield return WaitForAnimationCompletion(ui);
    }
}