using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckCollectionController : Singleton<DeckCollectionController>
{
    [SerializeField]
    private Button goToCardWarehouseButton;

    private void Start()
    {
        goToCardWarehouseButton.onClick.AddListener(OnGoToCardWarehouseButton);
    }

    private void OnGoToCardWarehouseButton()
    {
        LoadingSceneManager.Instance.LoadScene(SceneName.CardWarehouse, false);
    }
}
