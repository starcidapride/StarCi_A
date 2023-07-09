using UnityEngine;
using UnityEngine.UI;

public class ResentButtonController : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private async void OnButtonClick()
    {

    }
}