using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using static EnumUtils;
using static AnimatorUtils;
using static GameObjectUtils;
public enum AlertCaption
{
    [Description("Success")]
    Success,
    [Description("Error")]
    Error,
    [Description("Confirm")]
    Confirm
}

public class AlertButton
{
    public string ButtonText { get; set; }

    public Type Script { get; set; }

}

public class AlertController : SingletonPersistent<AlertController>
{
    [SerializeField]
    [Range(0f, 1f)]
    private float a;

    [SerializeField]
    private Transform alertBackdrop;

    [SerializeField]
    private Transform alertMessageBox;

    public void Hide()
    {
        DestroyAllChildGameObjects(alertBackdrop);

        alertBackdrop.gameObject.SetActive(false);

        LoadingSceneManager.isInputBlocked = false;
    }

    public void Show(AlertCaption caption, string message, List<AlertButton> buttons = null)
    {
        LoadingSceneManager.isInputBlocked = true;

        DestroyAllChildGameObjects(alertBackdrop);

        AlertMessageBoxController.Caption = caption;

        AlertMessageBoxController.Message = message;

        AlertMessageBoxController.Buttons = buttons;

        Instantiate(alertMessageBox, alertBackdrop);

        alertBackdrop.gameObject.SetActive(true);
    }
}
