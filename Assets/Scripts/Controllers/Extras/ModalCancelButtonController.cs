using UnityEngine;
using UnityEngine.UI;

public class ModalCancelButtonController : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        ModalController.Instance.CloseNearestModal();
    }
}