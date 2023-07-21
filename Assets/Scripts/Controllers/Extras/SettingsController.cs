using UnityEngine;
using UnityEngine.UI;

public class SettingsController : Singleton<SettingsController>
{
    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private Button soundButton;

    [SerializeField]
    private Transform settingsModal;

    [SerializeField]
    private Transform soundSettingsModal;

    private void Start()
    {
        settingsButton.onClick.AddListener(OnSettingsButtonClick);

        soundButton.onClick.AddListener(OnSoundButtonClick);
    }

    private void OnSettingsButtonClick()
    {
        ModalController.Instance.InstantiateModal(settingsModal);
    }

    private void OnSoundButtonClick()
    {
        ModalController.Instance.InstantiateModal(soundSettingsModal);
    }

}
