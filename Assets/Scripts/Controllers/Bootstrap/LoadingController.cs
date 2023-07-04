using UnityEngine;

public class LoadingController : SingletonPersistent<LoadingController>
{

    [SerializeField]
    private Transform loadingSpinner;

    public static bool modalActive = false;

    public void Hide()
    {
        loadingSpinner.gameObject.SetActive(false);
    }
    public void Show(string caption, string message)
    {
        loadingSpinner.gameObject.SetActive(true);
    }
}
