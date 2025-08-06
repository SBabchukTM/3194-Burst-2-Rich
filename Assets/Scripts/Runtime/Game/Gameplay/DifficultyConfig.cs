using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyConfig",menuName = "Config/DifficultyConfig")]
public class DifficultyConfig : ScriptableObject
{
    public float MinSpawnDelay;
    public float MaxSpawnDelay;

    public float ShrinkTime;

    public float TimeReductionRate;

    public float Multiplier;
}
