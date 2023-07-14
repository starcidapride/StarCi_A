using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardWarehouseUIController : Singleton<CardWarehouseUIController>
{
   public void SetInteractability(bool state = true)
    {
        GameObjectUtils.SetInteractability(transform, state);
    }
}