using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsModalController : Singleton<SoundSettingsModalController>
{
    [SerializeField]
    private Slider volumnSliderInput;

    [SerializeField]
    private Toggle muteToggleInput;

    [SerializeField]
    private Button cancelButton;

    [SerializeField]
    private Button submitButton;

    private float volume;

    private bool isMute;


    private void Start()
    {
        volumnSliderInput.value = AudioManager.Instance.audioSource.volume;

        muteToggleInput.isOn = AudioManager.Instance.audioSource.mute;

        
        volume = volumnSliderInput.value;

        isMute = muteToggleInput.isOn;


        volumnSliderInput.onValueChanged.AddListener(OnVolumeSliderInputValueChanged);

        muteToggleInput.onValueChanged.AddListener(OnMuteToggleInputValueChanged);

        cancelButton.gameObject.AddComponent<ModalCancelButtonController>();

        submitButton.onClick.AddListener(OnSubmitButtonClick);
    }

    private void OnVolumeSliderInputValueChanged(float value)
    {
        volume = value;
    }

    private void OnMuteToggleInputValueChanged(bool value)
    {
        isMute = value;
    }

    private void OnSubmitButtonClick()
    {
        AudioManager.Instance.SetVolume(volume);

        AudioManager.Instance.SetMute(isMute);

        ModalController.Instance.CloseNearestModal();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnSubmitButtonClick();
        }
    }
}
