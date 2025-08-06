using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.Infrastructure.SettingsProvider;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner
{
    private readonly CoinsPool _pool;
    private readonly ISettingProvider _settingProvider;
    private readonly GameplayData _gameplayData;

    private Bounds _bounds;
    
    public event Action OnCoinCollected;
    public event Action OnCoinMissed;

    public CoinSpawner(CoinsPool pool, ISettingProvider settingProvider, GameplayData gameplayData)
    {
        _pool = pool;
        _settingProvider = settingProvider;
        _gameplayData = gameplayData;
    }
    
    public void InvokeCollected() => OnCoinCollected?.Invoke();
    public void InvokeMissed() => OnCoinMissed?.Invoke();

    public async UniTask StartSpawning(CancellationToken token)
    {
        var difficulty = _gameplayData.DifficultyId;
        var difficultyConfig = _settingProvider.Get<GameConfig>().DifficultyConfigs[difficulty];

        float spawnDelayMin = difficultyConfig.MinSpawnDelay;
        float spawnDelayMax = difficultyConfig.MaxSpawnDelay;
        float delayReduction = difficultyConfig.TimeReductionRate;
        float shrinkTime = difficultyConfig.ShrinkTime;
        float growTime = shrinkTime / 4;
        
        float defaultSpawnMinDelay = difficultyConfig.MinSpawnDelay;
        float defaultSpawnMaxDelay = difficultyConfig.MaxSpawnDelay;
        
        var spawnBounds = GetSpawnBounds();
        
        while (!token.IsCancellationRequested)
        {
            float spawnDelay = Random.Range(spawnDelayMin, spawnDelayMax);
            
            await UniTask.WaitForSeconds(spawnDelay, cancellationToken: token);
            
            var spawnPos = SelectRandomSpawnPos(spawnBounds);
            var coin = _pool.GetCoinItem();
            
            coin.transform.position = spawnPos;
            coin.Spawn(growTime, shrinkTime);

            spawnDelayMin *= delayReduction;
            spawnDelayMax *= delayReduction;
            
            spawnDelayMin = Mathf.Clamp(spawnDelayMin, defaultSpawnMinDelay / 2, defaultSpawnMinDelay);
            spawnDelayMax = Mathf.Clamp(spawnDelayMax, defaultSpawnMaxDelay / 2, defaultSpawnMaxDelay);
            
            growTime *= delayReduction;
            shrinkTime *= delayReduction;
        }
    }

    private Vector3 SelectRandomSpawnPos(Bounds bounds)
    {
        float xPos = Random.Range(bounds.min.x, bounds.max.x);
        float yPos = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector3(xPos, yPos, 0);
    }
    
    private Bounds GetSpawnBounds()
    {
        Camera cam = Camera.main;

        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0f, 0.25f, cam.nearClipPlane));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1f, 0.75f, cam.nearClipPlane));

        Vector3 center = (bottomLeft + topRight) / 2f;
        Vector3 size = topRight - bottomLeft;

        return new Bounds(center, size);
    }
}
