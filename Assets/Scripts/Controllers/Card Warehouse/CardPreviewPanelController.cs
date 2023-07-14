using UnityEngine;

public class CardPreviewPanelController : Singleton<CardPreviewPanelController>
{
    public Transform GetTransform()
    {
        return gameObject.transform;
    }
}