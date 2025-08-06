using Runtime.Core.UI.Data;
using Runtime.Game.Services.UserData.Data;

namespace Runtime.Game.UI.Popup.Data
{
    public class SettingsPopupData : BasePopupData
    {
        private float _isSoundVolume;
        private float _isMusicVolume;

        public float IsSoundVolume => _isSoundVolume;
        public float IsMusicVolume => _isMusicVolume;

        public SettingsPopupData(float isSoundVolume, float isMusicVolume)
        {
            _isSoundVolume = isSoundVolume;
            _isMusicVolume = isMusicVolume;
        }
    }
}