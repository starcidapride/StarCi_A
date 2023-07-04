using System.Collections;
using System.ComponentModel;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

using static EnumUtils;

public enum SceneName : byte
{
    [Description("Bootstrap")]
    Bootstrap,

    [Description("Title Scene")]
    TitleScreen,

    [Description("Lobby")]
    Lobby,

    [Description("Card Warehouse")]
    CardWarehouse,

    [Description("Waiting Room")]
    WaitingRoom
};
public class LoadingSceneManager : SingletonPersistent<LoadingSceneManager>
{

    private SceneName sceneActive;
    public SceneName SceneActive => sceneActive;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneChange;
    }

    private void OnSceneChange(Scene scene, LoadSceneMode mode)
    {

        var sceneName = GetEnumValueByDescription<SceneName>(scene.name);

        switch (sceneName)
        {
            case SceneName.Lobby:

                return;

            case SceneName.WaitingRoom:

                return;
        }
    }

    public void LoadScene(SceneName sceneToLoad, bool isNetworkSessionAction = true)
    {
        StartCoroutine(Loading(sceneToLoad, isNetworkSessionAction));
    }

    private IEnumerator Loading(SceneName sceneToLoad, bool isNetworkSessionActive = true)
    {
        LoadingFadeEffectManager.Instance.FadeIn();
        yield return new WaitUntil(() => LoadingFadeEffectManager.beginLoad);

        if (isNetworkSessionActive)
        {
            if (NetworkManager.Singleton.IsServer)
                LoadSceneNetwork(sceneToLoad);
        }
        else
        {
            LoadSceneLocal(sceneToLoad);
        }

        yield return new WaitForSeconds(1);

        LoadingFadeEffectManager.Instance.FadeOut();
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

}
