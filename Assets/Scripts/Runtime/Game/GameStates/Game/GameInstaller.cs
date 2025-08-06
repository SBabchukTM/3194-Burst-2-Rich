using Runtime.Game.Gameplay;
using Runtime.Game.GameStates.Game.Controllers;
using Runtime.Game.GameStates.Game.Popups;
using Runtime.Game.GameStates.Game.Screens;
using Runtime.Game.Services.Shop;
using Runtime.Game.Services.UserData.Data;
using Runtime.Game.ShopSystem;
using UnityEngine;
using Zenject;

namespace Runtime.Game.GameStates.Game
{
    [CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
    public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameplayScreenStateController>().AsSingle();
            Container.Bind<LeaderboardScreenStateController>().AsSingle();
            Container.Bind<MenuStateController>().AsSingle();
            Container.Bind<ProfileScreenStateController>().AsSingle();
            Container.Bind<SettingsScreenStateController>().AsSingle();
            Container.Bind<PausePopupStateController>().AsSingle();
            Container.Bind<GameOverPopupStateController>().AsSingle();
            Container.Bind<StartSettingsController>().AsSingle();
            Container.Bind<InitShopState>().AsSingle();
            Container.Bind<ShopStateController>().AsSingle();

            Container.Bind<ProcessPurchaseService>().AsSingle();
            Container.Bind<PurchaseEffectsService>().AsSingle();
            Container.Bind<SelectPurchaseItemService>().AsSingle();
            Container.Bind<ShopItemsDisplayService>().AsSingle();
            Container.Bind<ShopItemDisplayModel>().AsTransient();
            Container.Bind<ShopItemsStorage>().AsSingle();
            Container.Bind<ShopService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LeaderboardFactory>().AsSingle();
            Container.Bind<ShopItemDisplayController>().AsSingle();
            Container.BindInterfacesAndSelfTo<CoinsPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputProvider>().AsSingle();
            Container.Bind<GameplayData>().AsSingle();
            Container.Bind<CoinSpawner>().AsSingle();
            Container.Bind<CoinCalculator>().AsSingle().NonLazy();
            Container.Bind<IUserInventoryService>().To<UserInventoryService>().AsSingle();

            Container.Bind<GameplayEnabler>().FromComponentInHierarchy().AsSingle();
        }
    }
}