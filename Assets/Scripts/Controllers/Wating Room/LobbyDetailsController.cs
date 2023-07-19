using System.Collections;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using static LobbyUtils;

public class LobbyDetailsController : Singleton<LobbyDetailsController>
{
    [SerializeField]
    private TMP_Text lobbyNameText;

    [SerializeField]
    private Transform privateIcon;

    [SerializeField]
    private TMP_Text lobbyCodeText;

    [SerializeField]
    private TMP_Text description;

    private void Start()
    {
        lobbyNameText.text = NetworkGameManager.Instance.LobbyName.Value.ToString();

        privateIcon.gameObject.SetActive(NetworkGameManager.Instance.Private.Value);

        lobbyCodeText.text = NetworkGameManager.Instance.LobbyCode.Value.ToString();

        description.text = NetworkGameManager.Instance.Description.Value.ToString();
    }
}
