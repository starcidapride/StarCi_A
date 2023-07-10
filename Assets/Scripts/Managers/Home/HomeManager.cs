
using UnityEngine;
using UnityEngine.Profiling;

public class HomeManager : Singleton<HomeManager>
{
    [SerializeField]
    private UserInventory userInventory;

    [SerializeField]
    private Transform initUserProfile;

    [SerializeField]
    private Transform profileUI;

    private void Start()
    {
        if (string.IsNullOrEmpty(userInventory.username))
        {
            ModalController.Instance.InstantiateModal(initUserProfile);
        }
    }
}