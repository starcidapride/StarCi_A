using System;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static EnumUtils;
public enum AlertCaption
{
    [Description("Success")]
    Success,
    [Description("Error")]
    Error
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
    private Transform messageBox;

    [SerializeField]
    private TMP_Text captionText;

    [SerializeField]
    private TMP_Text messageText;

    [SerializeField]
    private Button button1;

    [SerializeField]
    private Button button2;

    public void Hide()
    {
        alertBackdrop.gameObject.SetActive(false);
        messageBox.gameObject.SetActive(false);
    }
    public void Show(AlertCaption caption, string message, List<AlertButton> buttons = null)
    {

        if (buttons != null && (buttons.Count < 0 || buttons.Count > 2))
        {
            throw new ArgumentException("Invalid number of buttons. The allowed range is from 0 to 2.");
        }
        captionText.text = GetDescription(caption);

        messageText.text = message;


        if (buttons != null && buttons.Count == 1)
        {
            button2.gameObject.SetActive(true);

            Type script = buttons[0].Script;

            if (!script.IsSubclassOf(typeof(UnityEngine.Component))) 
                throw new ArgumentException("Invalid component type. The Script property must be a subclass of UnityEngine.Component.");

            button2.gameObject.AddComponent(script);

        }
        else
        {
            Type script1 = buttons[0].Script;
            Type script2 = buttons[1].Script;

            if (!script1.IsSubclassOf(typeof(UnityEngine.Component)) || !script2.IsSubclassOf(typeof(UnityEngine.Component)))
                throw new ArgumentException("Invalid component type. The Script property must be a subclass of UnityEngine.Component.");

            button1.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);

            button1.gameObject.AddComponent(script1);
            button2.gameObject.AddComponent(script2);
        }

        alertBackdrop.gameObject.SetActive(true);
        messageBox.gameObject.SetActive(true);
    }
}
