using UnityEngine;
using UnityEngine.UI;

public class TryAgainConnectButtonController : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private async void OnButtonClick()
    {
        var authResult = await AuthenticationUtils.InitiateAnonymousSignIn();
        if (authResult)
        {
            TitleScreenManager.Continue = true;
        }
    }
}