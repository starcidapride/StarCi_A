using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToLobbyButtonController : Singleton<GoToLobbyButtonController>
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnGoToLobbyButtonClick);
    }

    private void OnGoToLobbyButtonClick()
    {
        LoadingSceneManager.Instance.LoadScene(SceneName.Lobby, false);
    }
}
