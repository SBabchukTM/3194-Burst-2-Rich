using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class LeaderboardScreen : UiScreen
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private RectTransform _parent;
        
        public event Action OnBackPressed;
        
        public void Initialize(List<LeaderboardRecordDisplay> records)
        {
            SubscribeEvents();
            
            foreach (var record in records) 
                record.transform.SetParent(_parent, false);
        }
        
        private void SubscribeEvents()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
        }
    }
}