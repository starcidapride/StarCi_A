using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using Newtonsoft.Json;

using static LobbyUtils;
using static RelayUtils;
using static Constants.LobbyService;

using static GameObjectUtils;

public class LobbyTableController : Singleton<LobbyTableController>
{
    [SerializeField]
    private Transform createLobbyModal;

    [SerializeField]
    private Transform joinLobbyByCodeModal;

    [SerializeField]
    private Button createLobbyButton;

    [SerializeField]
    private Button joinLobbyByCodeButton;

    [SerializeField]
    private TMP_InputField searchTextInput;

    [SerializeField]
    private Button searchButton;

    [SerializeField]
    private Button refreshButton;

    [SerializeField]
    private Transform tableBody;

    [SerializeField]
    private Transform tableRow;

    private string searchValue;

    private string selectedLobbyId;
    public string SelectedLobbyId
    {
        get { return selectedLobbyId; }
        set
        {
            if (selectedLobbyId != value)
            {   
                selectedLobbyId = value;

                ExecuteNotify();
            }
        }
    }

    private IEnumerator Start()
    {   
        searchValue = searchTextInput.text;

        yield return new WaitForEndOfFrame();

        RenderDisplay();

        createLobbyButton.onClick.AddListener(OnCreateLobbyButtonClick);

        joinLobbyByCodeButton.onClick.AddListener(OnJoinLobbyByCodeButtonClick);

        searchTextInput.onEndEdit.AddListener(OnSearchInputEndEdit);

        searchButton.onClick.AddListener(OnSearchButtonClick);

        refreshButton.onClick.AddListener(OnRefreshButtonClick);
    }

    private async void RenderDisplay(string searchValue = null)
    {
        DestroyAllChildGameObjects(tableBody);

        var lobbies = await GetLobbies();
        
        if (!string.IsNullOrEmpty(searchValue))
        {
           

            lobbies = lobbies.Where(lobby =>
            {
                var lobbyNameFilter = lobby.Name.ContainsInsensitive(searchValue);

                var hostFilter = lobby.Data[HOST].Value.ContainsInsensitive(searchValue);

                var descriptionFilter = lobby.Data[DESCRIPTION].Value.ContainsInsensitive(searchValue);

                return lobbyNameFilter || hostFilter || descriptionFilter;
            }).ToList();
        }

        foreach (var lobby in lobbies)
        {
            var tableRowObject = Instantiate(tableRow, tableBody);

            tableRowObject.GetComponent<TableRowController>().LobbyId = lobby.Id;

            tableRowObject.Find("Lobby Name").GetComponent<TMP_Text>().text = lobby.Name;

            tableRowObject.Find("Host").GetComponent<TMP_Text>().text = lobby.Data[HOST].Value;

            tableRowObject.Find("Players").GetComponent<TMP_Text>().text = $"{lobby.Players.Count} / {lobby.MaxPlayers}";
            
            tableRowObject.Find("Description").GetComponent<TMP_Text>().text = lobby.Data[DESCRIPTION].Value;

            tableRowObject.Find("Status").GetComponent<TMP_Text>().text = lobby.Data[STATUS].Value;
        }
    }

    private void OnCreateLobbyButtonClick()
    {
        ModalController.Instance.InstantiateModal(createLobbyModal);
    }

    private void OnJoinLobbyByCodeButtonClick()
    {
        ModalController.Instance.InstantiateModal(joinLobbyByCodeModal);
    }

    private void OnSearchInputEndEdit(string value)
    {
        searchValue = value;
    }

    private void OnSearchButtonClick()
    {
        RenderDisplay(searchValue);
    }

    private void OnRefreshButtonClick()
    {
        RenderDisplay();
    }

    public delegate void NotifyEventHandler();

    public event NotifyEventHandler Notify;

    private void ExecuteNotify()
    {
        Notify?.Invoke();
    }
}
