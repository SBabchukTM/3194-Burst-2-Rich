using System;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class GameplayScreen : UiScreen
    {
        [SerializeField] private Button _pauseButton;

        public event Action OnPausePressed;
        
        public void Initialize()
        {
            _pauseButton.onClick.AddListener(() => OnPausePressed?.Invoke());
        }
    }
}