using System;
using System.Collections.Generic;
using DG.Tweening;
using Runtime.Game.ShopSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class ShopScreen : UiScreen
    {
        [SerializeField] private Button _goBackButton;
        [SerializeField] private RectTransform _shopItemsParent;
        [SerializeField] private TextMeshProUGUI _errorText;
        
        private Sequence _sequence;
        
        public event Action OnBackPressed;
        
        private void Start()
        {
            SubscribeToEvents();
        }

        private void OnDestroy() =>
                UnSubscribe();

        public void SetShopItems(List<ShopItemDisplayView> items)
        {
            foreach (var item in items)
                item.transform.SetParent(_shopItemsParent, false);
        }

        private void SubscribeToEvents()
        {
            _goBackButton.onClick.AddListener(() => OnBackPressed?.Invoke());
        }

        private void UnSubscribe()
        {
            _goBackButton.onClick.RemoveAllListeners();
        }

        public void DisplayError()
        {
            _sequence?.Kill();
            
            _sequence = DOTween.Sequence();

            _sequence.Append(_errorText.DOFade(1, 0.15f));
            _sequence.AppendInterval(0.7f);
            _sequence.Append(_errorText.DOFade(0, 0.15f));
        }
    }
}