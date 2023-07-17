using System.Collections;
using TMPro;
using UnityEngine;

public class LobbyDetailsController : SingletonNetwork<LobbyDetailsController>
{
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
        yield return new WaitUntil(() => !string.IsNullOrEmpty(NetworkWaitingRoomManager.Instance.lobbyCode.ToString()));

        lobbyCodeText.text = NetworkWaitingRoomManager.Instance.lobbyCode.Value.ToString();
    }

}
