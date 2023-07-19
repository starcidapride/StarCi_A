using System.Collections;
using UnityEngine;

using static AnimatorUtils;

public class WaitingRoomManager : Singleton<WaitingRoomManager>
{
    [SerializeField]
    private Transform ui;
    public void DisplayUI()
    {
        StartCoroutine(DisplayUICoroutine());
    }
    public IEnumerator DisplayUICoroutine()
    {
        ui.gameObject.SetActive(true);

        yield return WaitForAnimationCompletion(ui);
    }
}
