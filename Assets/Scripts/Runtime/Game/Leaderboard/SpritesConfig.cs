using Runtime.Core.Infrastructure.SettingsProvider;
using UnityEngine;

namespace Runtime.Game.Leaderboard
{
    [CreateAssetMenu(fileName = "SpritesConfig", menuName = "Config/SpritesConfig")]
    public class SpritesConfig : BaseSettings
    {
        public Sprite Top1Sprite;
        public Sprite Top2Sprite;
        public Sprite Top3Sprite;
        public Sprite Top4Sprite;
    }
}