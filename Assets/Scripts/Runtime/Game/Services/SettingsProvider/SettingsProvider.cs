using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Core.Audio;
using Runtime.Core.Infrastructure.AssetProvider;
using Runtime.Core.Infrastructure.SettingsProvider;
using Runtime.Game.Leaderboard;
using Runtime.Game.ShopSystem;

namespace Runtime.Game.Services.SettingsProvider
{
    public class SettingsProvider : ISettingProvider
    {
        private readonly IAssetProvider _assetProvider;

        private Dictionary<Type, BaseSettings> _settings = new ();

        public SettingsProvider(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask Initialize()
        {
            var audioConfig = await _assetProvider.Load<AudioConfig>(ConstConfigs.AudioConfig);
            var shopSetup = await _assetProvider.Load<ShopSetup>(ConstConfigs.ShopSetup);
            var spritesConfig = await _assetProvider.Load<SpritesConfig>(ConstConfigs.SpritesConfig);
            var gameConfig = await _assetProvider.Load<GameConfig>(ConstConfigs.GameConfig);

            Set(audioConfig);
            Set(shopSetup);
            Set(spritesConfig);
            Set(gameConfig);
        }

        public T Get<T>() where T : BaseSettings
        {
            if (_settings.ContainsKey(typeof(T)))
            {
                var setting = _settings[typeof(T)];
                return setting as T;
            }

            throw new Exception("No setting found");
        }

        public void Set(BaseSettings config)
        {
            if (_settings.ContainsKey(config.GetType()))
                return;

            _settings.Add(config.GetType(), config);
        }
    }
}