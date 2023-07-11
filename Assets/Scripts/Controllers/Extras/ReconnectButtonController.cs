using UnityEngine;
using UnityEngine.UI;

public class ReconnectButtonController : MonoBehaviour
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
            BootstrapManager.Continue = true;
        }
    }
}