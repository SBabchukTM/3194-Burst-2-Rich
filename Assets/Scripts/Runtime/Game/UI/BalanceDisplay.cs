using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Game.Services.UserData.Data;
using TMPro;
using UnityEngine;
using Zenject;

public class BalanceDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceText;
    
    [Inject]
    private IUserInventoryService _userInventoryService;

    private void Awake()
    {
        _userInventoryService.BalanceChangedEvent += UpdateBalance;
        
        UpdateBalance(_userInventoryService.GetBalance());
    }

    private void OnDestroy() => _userInventoryService.BalanceChangedEvent -= UpdateBalance;

    private void UpdateBalance(int obj) => _balanceText.text = obj.ToString();
}
