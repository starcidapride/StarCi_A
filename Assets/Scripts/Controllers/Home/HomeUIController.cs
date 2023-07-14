using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeUIController : Singleton<HomeUIController>
{
    public void SetInteractability(bool state = true)
    {
        GameObjectUtils.SetInteractability(transform, state);
    }
}