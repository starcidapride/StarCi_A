using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using static ImageUtils;
using static GameObjectUtils;

public class CardShowcaseDragAreaController : Singleton<CardShowcaseDragAreaController>
{   
    public Transform GetTransform()
    {
        return transform;
    }
}
