using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Net.Http;

using static ApiUtils;
using static Constants.ButtonNames;
using static AuthApiService;
using static ImageUtils;
using static UserInventory;

public class BootstrapManager : Singleton<BootstrapManager>
{
    [SerializeField]
    private UserInventory inventory;
    public static bool Continue { get; set; }
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => LoadingSceneManager.Instance != null);

        var authResultTask = AuthenticationUtils.InitiateAnonymousSignIn();

        yield return new WaitUntil(() => authResultTask.IsCompleted);

        if (!authResultTask.Result)
        {
            var buttons = new List<AlertButton>()
            {
                new AlertButton()
                {
                    ButtonText = QUIT,
                    Script = typeof(QuitButtonController)
                },
                new AlertButton()
                {
                    ButtonText = RECONNECT,
                    Script = typeof(ReconnectButtonController)
                },
            };
            AlertController.Instance.Show(AlertCaption.Error, "Unable to establish an internet connection. Please ensure your network settings are configured correctly and attempt to reconnect.", buttons);

            yield return new WaitUntil(() => Continue);
        }

        var accessToken = GetAuthTokenFromPlayPrefs(AuthTokenType.AccessToken);

        if (!string.IsNullOrEmpty(accessToken))
        {
            var initTask = ExecuteInit(ClientErrorHandler);

            yield return new WaitUntil(() => initTask.IsCompleted);

            var user = initTask.Result;

            if (user == null)
            {
                yield break;
            }

            inventory.Init();

            inventory.UpdateInventory(
                GetUser(user)
                );

            LoadingSceneManager.Instance.LoadScene(SceneName.Home, false);
        }
        else
        {
            LoadingSceneManager.Instance.LoadScene(SceneName.Authentication, false);
        }
    }

    private void ClientErrorHandler(HttpRequestException ex)
    {
        Debug.Log(ex);
    }
}