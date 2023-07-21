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

    private void OnButtonClick()
    {
        BootstrapManager.Instance.block = false;

        AlertController.Instance.Hide();
    }
}