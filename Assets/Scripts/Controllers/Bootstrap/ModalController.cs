using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static GameObjectUtils;
public class ModalController : SingletonPersistent<ModalController>
{
    [SerializeField]
    [Range(0f, 1f)]
    private float a;

    [SerializeField]
    private Image modalBackdrop;

    [SerializeField]
    private Transform modalsContainer;

    public static bool modalActive = false;

    private void Hide()
    {
        var color = modalBackdrop.color;
        color.a = 0;

        modalBackdrop.color = color;

        modalActive = false;

        modalBackdrop.gameObject.SetActive(false);
    }
    private void Show()
    {
        modalBackdrop.gameObject.SetActive(true);

        modalActive = true;

        var color = modalBackdrop.color;
        color.a = a;

        modalBackdrop.color = color;
    }

    private bool HasActived()
    {
        return modalActive;
    }

    public void InstantiateModal(Transform modal)
    {
        if (!HasActived())
        {
            Show();
        }

        var modalInstance = Instantiate(modal, modalsContainer);
        modalInstance.name = modal.name;

        modalInstance.localPosition = Vector2.zero;

        var closestYoungerSibling = GetClosestSiblingGameObject(modalInstance);
        if (closestYoungerSibling != null)
        {
            var canvasGroup = closestYoungerSibling.GetComponent<CanvasGroup>();
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

    }

    public void CloseNearestModal()
    {
        var numSiblings = modalsContainer.childCount;

        if (numSiblings == 0) return;

        var oldestSibling = modalsContainer.GetChild(numSiblings - 1);

        var closetYoungerSibling = GetClosestSiblingGameObject(oldestSibling);

        Destroy(oldestSibling.gameObject);

        if (closetYoungerSibling != null)
        {
            var group = closetYoungerSibling.GetComponent<CanvasGroup>();
            group.interactable = true;
            group.blocksRaycasts = true;

            return;
        }

        Hide();
    }
}
