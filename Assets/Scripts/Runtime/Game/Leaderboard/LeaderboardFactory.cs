using System.Collections.Generic;
using System.Linq;
using Runtime.Core.Factory;
using Runtime.Core.Infrastructure.AssetProvider;
using Runtime.Core.Infrastructure.SettingsProvider;
using Runtime.Game.Leaderboard;
using Runtime.Game.Services.SettingsProvider;
using Runtime.Game.Services.UserData;
using UnityEngine;
using UserProfile;
using Zenject;

public class LeaderboardFactory : IInitializable
{
    private readonly GameObjectFactory _factory;
    private readonly IAssetProvider _assetProvider;
    private readonly UserDataService _userDataService;
    private readonly ISettingProvider _settingProvider;

    private GameObject _prefab;

    public async void Initialize()
    {
        _prefab = await _assetProvider.Load<GameObject>(ConstPrefabs.LeaderboardPrefab);
    }

    public LeaderboardFactory(GameObjectFactory factory, IAssetProvider assetProvider, 
        UserDataService userDataService, ISettingProvider settingProvider)
    {
        _factory = factory;
        _assetProvider = assetProvider;
        _userDataService = userDataService;
        _settingProvider = settingProvider;
    }

    public List<LeaderboardRecordDisplay> GetLeaderboard()
    {
        var result = new List<LeaderboardRecordDisplay>();

        var data = GetSortedUserRecords();

        Sprite top1Sprite = _settingProvider.Get<SpritesConfig>().Top1Sprite;
        Sprite top4Sprite = _settingProvider.Get<SpritesConfig>().Top4Sprite;
        
        for (int i = 0; i < data.Count; i++)
        {
            Sprite sprite = i < 3 ? top1Sprite : top4Sprite;
            
            var display = _factory.Create<LeaderboardRecordDisplay>(_prefab);
            display.Initialize(i + 1, data[i].Name, data[i].Balance, sprite);
            result.Add(display);
        }

        return result;
    }

    private List<UserRecord> GetSortedUserRecords()
    {
        var result = GetFakeRecords();
        result.Add(GetPlayerRecord());
        return result.OrderByDescending(x => x.Balance).ToList();
    }

    private List<UserRecord> GetFakeRecords()
    {
        return new List<UserRecord>
        {
            new() { Name = "John", Balance = 90320 },
            new() { Name = "Jane", Balance = 45302 },
            new() { Name = "Michael", Balance = 70231 },
            new() { Name = "Emily", Balance = 18706 },
            new() { Name = "David", Balance = 99801 },
            new() { Name = "Sarah", Balance = 6304 },
            new() { Name = "Chris", Balance = 8420 },
            new() { Name = "Laura", Balance = 23405 },
            new() { Name = "Daniel", Balance = 12908 },
            new() { Name = "Emma", Balance = 78600 },
            new() { Name = "James", Balance = 56030 },
            new() { Name = "Olivia", Balance = 22001 },
            new() { Name = "Matthew", Balance = 40399 },
            new() { Name = "Sophia", Balance = 8501 },
            new() { Name = "Andrew", Balance = 61002 },
            new() { Name = "Chloe", Balance = 9999 },
            new() { Name = "Joseph", Balance = 34050 },
            new() { Name = "Ava", Balance = 7082 },
            new() { Name = "Joshua", Balance = 4893 },
            new() { Name = "Isabella", Balance = 91004 },
            new() { Name = "Ryan", Balance = 31056 },
            new() { Name = "Mia", Balance = 1240 },
            new() { Name = "Brandon", Balance = 60660 },
            new() { Name = "Abigail", Balance = 47200 },
            new() { Name = "Tyler", Balance = 100031 }
        };
    }

    private UserRecord GetPlayerRecord()
    {
        return new UserRecord
        {
            Name = UserProfileStorage.UserName,
            Balance = _userDataService.GetUserData().UserInventory.Balance,
        };
    }

    private class UserRecord
    {
        public string Name;
        public int Balance;
    }
}