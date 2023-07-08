using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static GameObjectUtils;
public class TitleScreenMenuController : SingletonPersistent<TitleScreenMenuController>
{
    [SerializeField]
    private Button signInButton;

    [SerializeField] 
    private Button signUpButton;


    [SerializeField]
    private Button aboutButton;

    [SerializeField]
    private Button settingsButton;
  
    [SerializeField]
    private Button quitButton;

    [SerializeField]
    private Transform signInModal;

    [SerializeField]
    private Transform signUpModal;

    private void Start()
    {
        signInButton.onClick.AddListener(OnSignInButtonClick);

        signUpButton.onClick.AddListener(OnSignUpButtonClick);

        aboutButton.onClick.AddListener(OnAboutButtonClick);

        settingsButton.onClick.AddListener(OnSettingsButtonClick);

        quitButton.gameObject.AddComponent<QuitButtonController>();
    }

    private void OnSignInButtonClick()
    {
        ModalController.Instance.InstantiateModal(signInModal);   
    }

    private void OnSignUpButtonClick()
    {
        ModalController.Instance.InstantiateModal(signUpModal);
    }

    private void OnAboutButtonClick()
    {

    }

    private void OnSettingsButtonClick()
    {

    }

}
