using System.Collections;
using UnityEngine;

public class GoToTitleScreenController : Singleton<GoToTitleScreenController>
{
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => LoadingSceneManager.Instance != null);

        LoadingSceneManager.Instance.LoadScene(SceneName.TitleScreen, false);
    }
}
