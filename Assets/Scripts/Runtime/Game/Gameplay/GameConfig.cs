using System.Collections;
using System.Collections.Generic;
using Runtime.Core.Infrastructure.SettingsProvider;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig",menuName = "Config/GameConfig")]
public class GameConfig : BaseSettings
{
    public List<DifficultyConfig> DifficultyConfigs = new List<DifficultyConfig>();
}
