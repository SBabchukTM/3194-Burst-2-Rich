using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class CoinsDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsText;
    
    private GameplayData _gameplayData;

    [Inject]
    private void Construct(GameplayData gameplayData)
    {
        _gameplayData = gameplayData;
        
        _gameplayData.OnCoinCollected += UpdateCoins;
    }

    private void OnDestroy() => _gameplayData.OnCoinCollected -= UpdateCoins;

    private void UpdateCoins(int obj) => _coinsText.text = obj.ToString();
}
