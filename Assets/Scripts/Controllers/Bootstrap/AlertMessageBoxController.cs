using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static EnumUtils;

public class AlertMessageBoxController : Singleton<AlertCancelButtonController>
{
    [SerializeField]
    private TMP_Text captionText;

    [SerializeField]
    private TMP_Text messageText;

    [SerializeField]
    private Button button1;

    [SerializeField]
    private Button button2;

    public static AlertCaption Caption { get; set; }

    public static string Message { get; set; }

    public static List<AlertButton> Buttons { get; set; }

    private void Start()
    {
        if (Buttons != null && (Buttons.Count < 0 || Buttons.Count > 2))
        {
            throw new ArgumentException("Invalid number of buttons. The allowed range is from 0 to 2.");
        }
        captionText.text = GetDescription(Caption);

        messageText.text = Message;

        if (Buttons != null)
        {
            if (Buttons.Count == 1)
            {
                button2.gameObject.SetActive(true);

                Type script = Buttons[0].Script;

                if (!script.IsSubclassOf(typeof(Component)))
                    throw new ArgumentException("Invalid component type. The Script property must be a subclass of UnityEngine.Component.");

                button2.GetComponentInChildren<TMP_Text>().text = Buttons[0].ButtonText;
                button2.gameObject.AddComponent(script);

            }
            else
            {
                var buttonText1 = Buttons[0].ButtonText;
                var buttonText2 = Buttons[1].ButtonText;

                Type script1 = Buttons[0].Script;
                Type script2 = Buttons[1].Script;

                if (!script1.IsSubclassOf(typeof(UnityEngine.Component)) || !script2.IsSubclassOf(typeof(Component)))
                    throw new ArgumentException("Invalid component type. The Script property must be a subclass of UnityEngine.Component.");

                button1.gameObject.SetActive(true);
                button2.gameObject.SetActive(true);

                button1.gameObject.AddComponent(script1);
                button2.gameObject.AddComponent(script2);

                button1.GetComponentInChildren<TMP_Text>().text = buttonText1;
                button2.GetComponentInChildren<TMP_Text>().text = buttonText2;
            }
        }
    }
}
