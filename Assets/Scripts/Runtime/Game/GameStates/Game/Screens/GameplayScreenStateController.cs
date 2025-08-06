using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Game.Gameplay;
using Runtime.Game.GameStates.Game.Popups;
using Runtime.Game.Services.UI;
using Runtime.Game.Services.UserData.Data;
using Runtime.Game.UI.Screen;
using ILogger = Runtime.Core.Infrastructure.Logger.ILogger;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class GameplayScreenStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly InputProvider _inputProvider;
        private readonly CoinSpawner _coinSpawner;
        private readonly CoinsPool _coinsPool;
        private readonly GameplayEnabler _gameplayEnabler;
        private readonly IUserInventoryService _userInventoryService;
        private readonly CoinCalculator _coinCalculator;
        private readonly PausePopupStateController _pausePopupStateController;
        private readonly GameOverPopupStateController _gameOverPopupStateController;
        
        private GameplayScreen _screen;
        
        private CancellationTokenSource _cancellationTokenSource;
        
        public GameplayScreenStateController(ILogger logger, IUiService uiService, InputProvider inputProvider,
            CoinSpawner coinSpawner, GameplayEnabler gameplayEnabler, IUserInventoryService userInventoryService,
            CoinsPool coinsPool, CoinCalculator coinCalculator, PausePopupStateController pausePopupStateController,
            GameOverPopupStateController gameOverPopupStateController) : base(logger)
        {
            _uiService = uiService;
            _inputProvider = inputProvider;
            _coinSpawner = coinSpawner;
            _gameplayEnabler = gameplayEnabler;
            _userInventoryService = userInventoryService;
            _coinsPool = coinsPool;
            _coinCalculator = coinCalculator;
            _pausePopupStateController = pausePopupStateController;
            _gameOverPopupStateController = gameOverPopupStateController;
            
            _coinSpawner.OnCoinMissed += ProcessGameEnd;
        }

        public override UniTask Enter(CancellationToken cancellationToken)
        {
            _cancellationTokenSource = new();
            
            CreateScreen();
            SubscribeToEvents();
            
            _inputProvider.Enable(true);
            _coinSpawner.StartSpawning(_cancellationTokenSource.Token).Forget();
            _gameplayEnabler.StartGame(_userInventoryService.GetUsedGameItem().ItemSprite);
            _coinCalculator.Reset();
            
            return UniTask.CompletedTask;
        }

        public override async UniTask Exit()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            
            _inputProvider.Enable(false);
            _gameplayEnabler.EndGame();
            _coinsPool.ReturnAll();
            _coinCalculator.RecordCoins();
            
            await _uiService.HideScreen(ConstScreens.GameplayScreen);
        }

        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<GameplayScreen>(ConstScreens.GameplayScreen);
            _screen.Initialize();
            _screen.ShowAsync().Forget();
        }

        private void SubscribeToEvents()
        {
            _screen.OnPausePressed += () => _pausePopupStateController.Enter().Forget();
        }

        private void ProcessGameEnd()
        {
            _gameOverPopupStateController.Enter().Forget();
        }
    }
}