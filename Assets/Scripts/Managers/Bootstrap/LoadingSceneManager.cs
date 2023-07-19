using System.Collections;
using System.ComponentModel;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

using static EnumUtils;
using static Constants.LobbyService;
using static RelayUtils;

using static LobbyUtils;
using static Constants.ButtonNames;

using System.Collections.Generic;

public enum SceneName : byte
{
    [Description("Bootstrap")]
    Bootstrap,

    [Description("Authentication")]
    Authentication,

    [Description("Lobby Room")]
    LobbyRoom,

    [Description("Card Warehouse")]
    CardWarehouse,

    [Description("Waiting Room")]
    WaitingRoom,

    [Description("Home")]
    Home
};
public class LoadingSceneManager : SingletonPersistent<LoadingSceneManager>
{

    private SceneName sceneActive;
    public SceneName SceneActive => sceneActive;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var currentScene =  GetEnumValueByDescription<SceneName>(SceneManager.GetActiveScene().name);

            switch (currentScene)
            {
                case SceneName.CardWarehouse:
                    LoadScene(SceneName.Home, false);
                    break;

                case SceneName.LobbyRoom:
                    LoadScene(SceneName.Home, false);
                    break;

                case SceneName.Home:
                    AlertController.Instance.Show(AlertCaption.Confirm, "Do you want to quit?",
                        new List<AlertButton>()
                        {
                            new AlertButton()
                            {
                                ButtonText = NO,
                                Script = typeof(AlertCancelButtonController)
                            },
                            new AlertButton()
                            {   
                                ButtonText = YES,
                                Script = typeof(QuitButtonController)
                            }
                        }
                        );
                    break; 
            }

        }
    }
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneChange;
    }

    private void OnSceneChange(Scene scene, LoadSceneMode mode)
    {

    }

    public void LoadScene(SceneName sceneToLoad, bool isNetworkSessionAction = true)
    {
        StartCoroutine(LoadSceneCoroutine(sceneToLoad, isNetworkSessionAction));
    }

    private IEnumerator LoadSceneCoroutine(SceneName sceneToLoad, bool isNetworkSessionActive = true)
    {
        LoadingFadeEffectController.Instance.FadeIn();
        yield return new WaitUntil(() => LoadingFadeEffectController.beginLoad);

        if (isNetworkSessionActive)
        {
            if (NetworkManager.Singleton.IsServer)
                LoadSceneNetwork(sceneToLoad);
        }
        else
        {
            LoadSceneLocal(sceneToLoad);
        }

        yield return new WaitForSeconds(0.5f);

        LoadingFadeEffectController.Instance.FadeOut();
    }

    private void LoadSceneLocal(SceneName sceneToLoad)
    {
        SceneManager.LoadScene(GetDescription(sceneToLoad));
        switch (sceneToLoad)
        {
        }
    }

    private void LoadSceneNetwork(SceneName sceneToLoad)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(
            GetDescription(sceneToLoad),
            LoadSceneMode.Single);
    }
    
    public void JoinRelayAndStartClient(Lobby lobby)
    {
        StartCoroutine(JoinRelayAndStartClientCoroutine(lobby));
    }

    private IEnumerator JoinRelayAndStartClientCoroutine(Lobby lobby)
    {
        var joinCode = lobby.Data[RELAY_CODE].Value;

        var resultTask = JoinRelay(joinCode);

        yield return new WaitUntil(() => resultTask.IsCompleted);

        if (!resultTask.Result) yield break;

        LoadingFadeEffectController.Instance.FadeIn();

        yield return new WaitUntil(() => LoadingFadeEffectController.beginLoad);

        NetworkManager.Singleton.StartClient();

        yield return new WaitForSeconds(1);

        LoadingFadeEffectController.Instance.FadeOut();
    }
}
