using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShowcaseController : Singleton<CardShowcaseController>
{
    [SerializeField]
    private CardSearchBoxInventory inventory;

    [SerializeField]
    private Transform container;

    private void Start()
    {
    }

}
