using UnityEngine;

public class LoadingController : SingletonPersistent<LoadingController>
{
    [SerializeField]
    private Transform loadingBackdrop;


    public static bool modalActive = false;

    public void Hide()
    {
        loadingBackdrop.gameObject.SetActive(false);
    }
    public void Show()
    {
        loadingBackdrop.gameObject.SetActive(true);
    }
}
