using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static ImageUtils;
using static Constants.Colors;
using Unity.Netcode;
using System.Linq;

public class WaitingPlayersController : Singleton<WaitingPlayersController>
{

    [SerializeField]
    private Image opponent;

    [SerializeField]
    private Transform opponentContainer;

    [SerializeField]
    private Image yourPicture;

    [SerializeField]
    private Image opponentsPicture;

    [SerializeField]
    private TMP_Text yourUserame;

    [SerializeField]
    private TMP_Text opponentsUsername;

    [SerializeField]
    private Transform yourHost;

    [SerializeField]
    private Transform oppponentHost;

    [SerializeField]
    private Transform yourReady;

    [SerializeField]
    private Transform yourNotReady;

    [SerializeField]
    private Transform opponentsReady;

    [SerializeField]
    private Transform opponentsNotReady;

    public void Start()
    {
        NetworkGameManager.Instance.Notify += OnNotify;
        
        RenderDisplay();
    }

    private void RenderDisplay()
    {
        yourPicture.sprite = CreateSpriteFromTexture(NetworkGameManager.Instance.You.Picture);
        
        yourUserame.text = NetworkGameManager.Instance.You.Username;

        if (NetworkGameManager.Instance.ConnectedUsers.Value.users.
            First(user => user.email.ToString() == NetworkGameManager.Instance.You.Email).isReady)
        {
            yourReady.gameObject.SetActive(true);
            yourNotReady.gameObject.SetActive(false);
        } else
        {
            yourReady.gameObject.SetActive(false);
            yourNotReady.gameObject.SetActive(true);
        }

        yourHost.gameObject.SetActive(NetworkManager.Singleton.IsHost);

        if (NetworkGameManager.Instance.Opponent != null)
        {
            opponent.color = Color.white;

            opponentContainer.gameObject.SetActive(true);

            opponentsUsername.text = NetworkGameManager.Instance.Opponent.Username;

            opponentsPicture.sprite = CreateSpriteFromTexture(NetworkGameManager.Instance.Opponent.Picture);


            if (NetworkGameManager.Instance.ConnectedUsers.Value.users.
            First(user => user.email.ToString() != NetworkGameManager.Instance.You.Email).isReady)
            {
                opponentsReady.gameObject.SetActive(true);
                opponentsNotReady.gameObject.SetActive(false);
            }
            else
            {
                opponentsReady.gameObject.SetActive(false);
                opponentsNotReady.gameObject.SetActive(true);
            }

            oppponentHost.gameObject.SetActive(!NetworkManager.Singleton.IsHost);
        } else
        {
            opponent.color = GRAY;

            opponentContainer.gameObject.SetActive(false);
        }

    }

    private void OnNotify()
    {
        RenderDisplay();
    }
}
