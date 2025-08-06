using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayData
{
    private int _coinsCollected;

    public event Action<int> OnCoinCollected;
    
    public int CoinsCollected
    {
        get => _coinsCollected;
        set
        {
            OnCoinCollected?.Invoke(value);
            _coinsCollected = value;
        }
    }
    
    public int DifficultyId;
}
