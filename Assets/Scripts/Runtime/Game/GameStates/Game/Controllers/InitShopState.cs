using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Core.Infrastructure.SettingsProvider;
using Runtime.Game.GameStates.Game.Screens;
using Runtime.Game.Services.Shop;
using Runtime.Game.Services.UI;
using Runtime.Game.ShopSystem;
using Runtime.Game.UI.Screen;

namespace Runtime.Game.GameStates.Game.Controllers
{
    public class InitShopState : StateController
    {
        private readonly ShopItemsStorage _shopItemsStorage;
        private readonly IUiService _uiService;
        private readonly ShopItemsDisplayService _shopItemsDisplayService;
        private readonly ISettingProvider _settingProvider;
        private readonly ShopService _shopService;
        private readonly ShopItemDisplayController _shopItemDisplayController;
        private readonly List<ISetShopSetup> _setShopConfigs;

        public InitShopState(ILogger logger, ShopItemsStorage shopItemsStorage, IUiService uiService, 
                ShopItemsDisplayService shopItemsDisplayService, ISettingProvider settingProvider,
                ProcessPurchaseService processPurchaseService, PurchaseEffectsService purchaseEffectsService,
                SelectPurchaseItemService selectPurchaseItemService, ShopService shopService, ShopItemDisplayController shopItemDisplayController) : base(logger)
        {
            _shopItemsStorage = shopItemsStorage;
            _uiService = uiService;
            _shopItemsDisplayService = shopItemsDisplayService;
            _settingProvider = settingProvider;
            _shopService = shopService;
            _shopItemDisplayController = shopItemDisplayController;

            _setShopConfigs = new()
            {
                processPurchaseService,
                purchaseEffectsService,
                selectPurchaseItemService,
                shopItemsDisplayService
            };
        }

        public override UniTask Enter(CancellationToken cancellationToken = default)
        {
            SetShopConfig();
            var screen = CreateScreen();
            Subscribe(screen);
            
            return GoTo<ShopStateController>(cancellationToken);
        }

        private ShopScreen CreateScreen()
        {
            var screen = _uiService.GetScreen<ShopScreen>(ConstScreens.ShopScreen);
            screen.ShowAsync().Forget();
            _shopItemDisplayController.SetShop(_shopService);
            _shopItemsDisplayService.CreateShopItems();
            _shopItemsDisplayService.UpdateItemsStatus();
            screen.SetShopItems(_shopItemsStorage.GetItemDisplay());

            return screen;
        }

        private void Subscribe(ShopScreen screen) =>
                screen.OnBackPressed += () => GoTo<MenuStateController>().Forget(); 
        
        private void SetShopConfig()
        {
            var shopConfig = _settingProvider.Get<ShopSetup>();

            _shopItemsStorage.SetShopStateConfigs(shopConfig.ShopItemStateConfigs);

            foreach (var setShopConfig in _setShopConfigs)
                setShopConfig.SetShopSetup(shopConfig);
        }
    }
}