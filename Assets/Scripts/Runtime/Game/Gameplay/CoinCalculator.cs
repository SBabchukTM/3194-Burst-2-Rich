using Runtime.Core.Audio;
using Runtime.Core.Infrastructure.SettingsProvider;
using Runtime.Game.Services.Audio;
using Runtime.Game.Services.UserData.Data;

namespace Runtime.Game.Gameplay
{
    public class CoinCalculator
    {
        private const int BaseCoinValue = 10;
        private readonly GameplayData _gameplayData;
        private readonly ISettingProvider _settingProvider;
        private readonly CoinSpawner _coinSpawner;
        private readonly IUserInventoryService _userInventoryService;
        private readonly IAudioService _audioService;

        public CoinCalculator(GameplayData gameplayData, ISettingProvider settingProvider, CoinSpawner coinSpawner,
            IUserInventoryService userInventoryService, IAudioService audioService)
        {
            _gameplayData = gameplayData;
            _settingProvider = settingProvider;
            _coinSpawner = coinSpawner;
            _userInventoryService = userInventoryService;
            _audioService = audioService;
            
            _coinSpawner.OnCoinCollected += CoinSpawnerOnOnCoinCollected;
        }

        public void Reset() => _gameplayData.CoinsCollected = 0;

        public void RecordCoins() => _userInventoryService.AddBalance(_gameplayData.CoinsCollected);

        private void CoinSpawnerOnOnCoinCollected()
        {
            var difficultyId = _gameplayData.DifficultyId;
            var multiplier = _settingProvider.Get<GameConfig>().DifficultyConfigs[difficultyId].Multiplier;
            
            _gameplayData.CoinsCollected += (int)(multiplier * BaseCoinValue);
            _audioService.PlaySound(ConstAudio.CoinSound);
        }
    }
}