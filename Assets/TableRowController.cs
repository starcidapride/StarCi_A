using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TableRowController : MonoBehaviour, ISelectHandler, IDeselectHandler
{
  
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Selected");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("Deselect");
    }


    void Start()
    {
       
    }

    void Update()
    {
        
    }
}
