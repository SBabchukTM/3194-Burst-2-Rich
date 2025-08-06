using System;
using Runtime.Game.Services.UserData.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class SettingsScreen : UiScreen
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Button _touButton;
        [SerializeField] private Button _ppButton;


        public event Action OnBackPressed;
        public event Action<float> OnSoundChanged;
        public event Action<float> OnMusicChanged;
        public event Action OnTouPressed;
        public event Action OnPpPressed;

        public void Initialize(SettingsData settingsData)
        {
            _soundSlider.value = settingsData.SoundVolume;
            _musicSlider.value = settingsData.MusicVolume;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
            _touButton.onClick.AddListener(() => OnTouPressed?.Invoke());
            _ppButton.onClick.AddListener(() => OnPpPressed?.Invoke());
            _soundSlider.onValueChanged.AddListener((value) => OnSoundChanged?.Invoke(value));
            _musicSlider.onValueChanged.AddListener((value) => OnMusicChanged?.Invoke(value));
        }
    }
}