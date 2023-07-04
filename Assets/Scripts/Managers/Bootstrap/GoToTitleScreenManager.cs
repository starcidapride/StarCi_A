using System.Collections;
using UnityEngine;

public class GoToTitleScreenManager : Singleton<GoToTitleScreenManager>
{
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => LoadingSceneManager.Instance != null);

        LoadingSceneManager.Instance.LoadScene(SceneName.TitleScreen, false);
    }
}
