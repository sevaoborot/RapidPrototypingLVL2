using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeShop : MonoBehaviour
{
    [SerializeField] private Shop _shop;

    private void Awake()
    {
        _shop.OnInitialize();
    }
}
