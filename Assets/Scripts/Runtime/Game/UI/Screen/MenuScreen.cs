using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class MenuScreen : UiScreen
    {
        [SerializeField] private Button _leadButton;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _setButton;
        [SerializeField] private Button _profButton;
        [SerializeField] private Button _htpButton;
        
        public event Action OnLeadButtonPressed;
        public event Action OnStartButtonPressed;
        public event Action OnShopButtonPressed;
        public event Action OnSetButtonPressed;
        public event Action OnProfButtonPressed;
        public event Action OnHtpButtonPressed;
        
        public void Initialize()
        {
            _leadButton.onClick.AddListener(() => OnLeadButtonPressed?.Invoke());
            _startButton.onClick.AddListener(() => OnStartButtonPressed?.Invoke());
            _shopButton.onClick.AddListener(() => OnShopButtonPressed?.Invoke());
            _setButton.onClick.AddListener(() => OnSetButtonPressed?.Invoke());
            _profButton.onClick.AddListener(() => OnProfButtonPressed?.Invoke());
            _htpButton.onClick.AddListener(() => OnHtpButtonPressed?.Invoke());
        }
    }
}