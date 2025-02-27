using System;
using System.Collections.Generic;
using UnityEngine;

public class GM_BattleMode : MonoBehaviour
{
    [Header("Player positions")]
    [SerializeField] List<Transform> playerPositions = new List<Transform>();
    [SerializeField] Transform playerTransform;
    [SerializeField] int position;

    public bool IsPlayerTurn { get; private set; }

    PlayerControls _playerInput;

    void Start()
    { 
        IsPlayerTurn = false;
    }

    private void Update()
    {
        if (IsPlayerTurn)
        {
            
            //чото делает игрок 
        }
        else
        {
            //ему палкой по горбам даёт враг
        }
    }

    void PlayerTurn()
    {
        _playerInput.UI.Enable();
    }
} 