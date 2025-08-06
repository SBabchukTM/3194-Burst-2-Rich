using System;
using System.Collections.Generic;
using Runtime.Core.Factory;
using Runtime.Core.Infrastructure.AssetProvider;
using Runtime.Game.Services.SettingsProvider;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class CoinsPool : IInitializable
{
    private const int PoolSize = 10;
    
    private readonly GameObjectFactory _gameObjectFactory;
    private readonly IAssetProvider _assetProvider;

    private readonly List<CoinItem> _items = new();

    private GameObject _prefab;
    
    public CoinsPool(IAssetProvider assetProvider, GameObjectFactory gameObjectFactory)
    {
        _gameObjectFactory = gameObjectFactory;
        _assetProvider = assetProvider;
    }
    
    public async void Initialize()
    {
        _prefab = await _assetProvider.Load<GameObject>(ConstPrefabs.CoinPrefab);
        
        for (int i = 0; i < PoolSize; i++) 
            _items.Add(CreateCoinItem());
    }

    public CoinItem GetCoinItem()
    {
        CoinItem result = null;

        if (_items.Count > 0)
        {
            result = _items[0];
            _items.RemoveAt(0);
        }
        else
            result = CreateCoinItem();

        result.gameObject.SetActive(true);
        return result;
    }

    public void ReturnAll()
    {
        CoinItem[] items = Object.FindObjectsOfType<CoinItem>(false);

        foreach (var item in items)
        {
            ReturnCoinItem(item);
        }
    }
    
    public void ReturnCoinItem(CoinItem coinItem)
    {
        coinItem.gameObject.SetActive(false);
        coinItem.Kill();
        _items.Add(coinItem);
    }
    
    private CoinItem CreateCoinItem()
    {
        CoinItem  item = _gameObjectFactory.Create<CoinItem>(_prefab);
        item.gameObject.SetActive(false);
        return item;
    }
}
