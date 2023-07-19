using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using static GameObjectUtils;

public class TableRowController : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public string LobbyId { get; set; }

    public void OnDeselect(BaseEventData eventData)
    {   
        if (!IsMousePositionInsideRectTransformArea((RectTransform) JoinLobbyButtonController.Instance.GetTransform()))
        LobbyTableController.Instance.SelectedLobbyId = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        LobbyTableController.Instance.SelectedLobbyId = LobbyId;
    }
}
