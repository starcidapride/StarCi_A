using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
   public void SetInteractability(bool state = true)
    {
        GameObjectUtils.SetInteractability(transform, state);
    }


}