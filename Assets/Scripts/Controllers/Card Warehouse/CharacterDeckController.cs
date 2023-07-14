using UnityEngine;

public class CharacterDeckController : Singleton<CharacterDeckController>
{
    [SerializeField]
    private UserInventory inventory;

    [SerializeField]
    private Transform container;
    
    public Transform GetTransform()
    {
        return container.transform;
    }
}