using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

using static ProfileApiService;
using static ImageUtils;
using Newtonsoft.Json;

public class WaitingPlayersController : SingletonNetwork<WaitingPlayersController>
{

    [SerializeField]
    private Image opponent;

    [SerializeField]
    private Transform opponentContainer;

    [SerializeField]
    private Image yourImage;

    [SerializeField]
    private Image opponentsPicture;

    [SerializeField]
    private TMP_Text yourUserame;

    [SerializeField]
    private TMP_Text opponentsUsername;

    [SerializeField]
    private Transform yourHostIcon;

    [SerializeField]
    private Transform oppponentHostIcon;

    [SerializeField]
    private Transform yourReady;

    [SerializeField]
    private Transform opponentsReady;

    public override void OnNetworkSpawn()
    {
        NetworkGameManager.Instance.Notify += OnNotify;

        StartCoroutine(OnNetworkSpawnCoroutine());
    }

    private void OnNotify()
    {
        if (NetworkGameManager.Instance.Opponent != null)
        {
            HandleOpponentJoin();
        }
        else
        {
            HandleOpponentLeave();
        }
    }


    private IEnumerator OnNetworkSpawnCoroutine()
    {
        yield return new WaitUntil(() => NetworkGameManager.Instance.You != null);

        yourImage.sprite = CreateSpriteFromTexture(NetworkGameManager.Instance.You.Picture);

        yourUserame.text = NetworkGameManager.Instance.You.Username;
    }

    private void HandleOpponentJoin()
    {
        opponent.color = Color.white;

        opponentContainer.gameObject.SetActive(true);

        opponentsPicture.sprite = CreateSpriteFromTexture(NetworkGameManager.Instance.Opponent.Picture);

        opponentsUsername.text = NetworkGameManager.Instance.Opponent.Username;
    }

    private void HandleOpponentLeave()
    {

    }
}
