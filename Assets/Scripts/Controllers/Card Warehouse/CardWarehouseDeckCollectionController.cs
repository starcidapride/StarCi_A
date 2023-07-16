using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardWarehouseDeckCollectionController : Singleton<CardWarehouseDeckCollectionController>
{
    [SerializeField]
    private Transform createNewDeckModal;

    [SerializeField]
    private UserInventory inventory;

    [SerializeField]
    private Button createDeckButton;

    [SerializeField]
    private Button renameDeckButton;

    [SerializeField]
    private Button deleteDeckButton;

    [SerializeField]
    private Button saveDeckButton;

    [SerializeField]
    private Button defaultDeckButton;

    private void Start()
    {
        createDeckButton.onClick.AddListener(OnCreateDeckButtonClick);

        renameDeckButton.onClick.AddListener(OnRenameDeckButtonClick);

        deleteDeckButton.onClick.AddListener(OnDeleteDeckButtonClick);

        saveDeckButton.onClick.AddListener(OnSaveDeckButtonClick);

        defaultDeckButton.onClick.AddListener(OnDefaultDeckButtonClick);
    }

    private void OnCreateDeckButtonClick()
    {
        ModalController.Instance.InstantiateModal(createNewDeckModal);
    }

    private void OnRenameDeckButtonClick()
    {
    }

    private void OnDeleteDeckButtonClick()
    { 
    }

    private void OnSaveDeckButtonClick()
    {
        inventory.Save();
    }

    private void OnDefaultDeckButtonClick()
    {
        inventory.Default();
    }
}
