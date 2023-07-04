using UnityEngine;

public class AlertController : SingletonPersistent<AlertController>
{
    [SerializeField]
    [Range(0f, 1f)]
    private float a;

    [SerializeField]
    private Transform messageBox;

    public static bool modalActive = false;

    public void Hide()
    {
        messageBox.gameObject.SetActive(false);
    }
    public void Show(string caption, string message)
    {
        messageBox.gameObject.SetActive(true);
    }
}
