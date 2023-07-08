using UnityEngine;
using UnityEngine.UI;

public class AlertCancelButtonController : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        AlertController.Instance.Hide();
    }
}