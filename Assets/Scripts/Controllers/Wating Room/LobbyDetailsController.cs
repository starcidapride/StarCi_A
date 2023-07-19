using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static LobbyUtils;

public class LobbyDetailsController : SingletonNetwork<LobbyDetailsController>
{
    [SerializeField]
    private TMP_Text lobbyNameText;

    [SerializeField]
    private Transform privateIcon;

    [SerializeField]
    private TMP_Text lobbyCodeText;

    [SerializeField]
    private TMP_Text description;

    public override void OnNetworkSpawn()
    {
        StartCoroutine(OnNetworkSpawnCoroutine());
    }

    private IEnumerator OnNetworkSpawnCoroutine()
    {
        yield return new WaitUntil(() =>
        !string.IsNullOrEmpty(NetworkGameManager.Instance.LobbyCode.ToString())
        && !string.IsNullOrEmpty(NetworkGameManager.Instance.LobbyName.ToString())
        && !string.IsNullOrEmpty(NetworkGameManager.Instance.Description.ToString())
        && !string.IsNullOrEmpty(NetworkGameManager.Instance.Private.ToString())
        );

        lobbyNameText.text = NetworkGameManager.Instance.LobbyName.Value.ToString();

        privateIcon.gameObject.SetActive(NetworkGameManager.Instance.Private.Value);

        lobbyCodeText.text = NetworkGameManager.Instance.LobbyCode.Value.ToString();
        
        description.text = NetworkGameManager.Instance.Description.Value.ToString();
    }

}
