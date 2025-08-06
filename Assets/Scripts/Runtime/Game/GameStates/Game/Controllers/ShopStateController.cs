using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Game.Services.Shop;
using Runtime.Game.Services.UI;
using Runtime.Game.ShopSystem;

namespace Runtime.Game.GameStates.Game.Controllers
{
    public class ShopStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly ShopItemsStorage _shopItemsStorage;
        private readonly ProcessPurchaseService _processPurchaseService;

        private CancellationTokenSource _cancellationTokenSource;

        public ShopStateController(ILogger logger, IUiService uiService, ShopItemsStorage shopItemsStorage, 
                ProcessPurchaseService processPurchaseService) : base(logger)
        {
            _uiService = uiService;
            _shopItemsStorage = shopItemsStorage;
            _processPurchaseService = processPurchaseService;
        }

        public override UniTask Enter(CancellationToken cancellationToken = default)
        {
            _cancellationTokenSource = new();
            
            Subscribe(cancellationToken);
            
            return UniTask.CompletedTask;
        }

        public override async UniTask Exit()
        {
            _shopItemsStorage.Cleanup();
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            
            await _uiService.HideScreen(ConstScreens.ShopScreen);
        }

        private void Subscribe(CancellationToken cancellationToken)
        {
            foreach (var shopItemDisplay in _shopItemsStorage.GetItemDisplay())
                shopItemDisplay.OnPurchasePressed += _ =>  _processPurchaseService.ProcessPurchaseAttempt(shopItemDisplay, cancellationToken);
        }
    }
}