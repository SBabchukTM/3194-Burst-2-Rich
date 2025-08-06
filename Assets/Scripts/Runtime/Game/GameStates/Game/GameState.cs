using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Game.GameStates.Game.Controllers;
using Runtime.Game.GameStates.Game.Popups;
using Runtime.Game.GameStates.Game.Screens;
using ILogger = Runtime.Core.Infrastructure.Logger.ILogger;

namespace Runtime.Game.GameStates.Game
{
    public class GameState : StateController
    {
        private readonly StateMachine _stateMachine;

        private readonly GameplayScreenStateController _gameplayScreenStateController;
        private readonly LeaderboardScreenStateController _leaderboardScreenStateController;
        private readonly ProfileScreenStateController _profileScreenStateController;
        private readonly SettingsScreenStateController _settingsScreenStateController;
        private readonly PausePopupStateController _pausePopupStateController;
        private readonly GameOverPopupStateController _gameOverPopupStateController;
        private readonly MenuStateController _menuStateController;
        private readonly InitShopState _initShopState;
        private readonly ShopStateController _shopStateController;
        private readonly UserDataStateChangeController _userDataStateChangeController;

        public GameState(ILogger logger,
            GameplayScreenStateController gameplayScreenStateController,
            LeaderboardScreenStateController leaderboardScreenStateController,
            ProfileScreenStateController profileScreenStateController,
            SettingsScreenStateController settingsScreenStateController,
            PausePopupStateController pausePopupStateController,
            GameOverPopupStateController gameOverPopupStateController,
            MenuStateController menuStateController,
            InitShopState initShopState,
            ShopStateController shopStateController,
            StateMachine stateMachine,
            UserDataStateChangeController userDataStateChangeController) : base(logger)
        {
            _stateMachine = stateMachine;
            _gameplayScreenStateController = gameplayScreenStateController;
            _leaderboardScreenStateController = leaderboardScreenStateController;
            _profileScreenStateController = profileScreenStateController;
            _settingsScreenStateController = settingsScreenStateController;
            _pausePopupStateController = pausePopupStateController;
            _gameOverPopupStateController = gameOverPopupStateController;
            _menuStateController = menuStateController;
            _initShopState = initShopState;
            _shopStateController = shopStateController;
            _userDataStateChangeController = userDataStateChangeController;
        }

        public override async UniTask Enter(CancellationToken cancellationToken)
        {
            await _userDataStateChangeController.Run(default);

            _stateMachine.Initialize(_gameplayScreenStateController, _leaderboardScreenStateController, 
                _profileScreenStateController, _settingsScreenStateController,
                _pausePopupStateController, _gameOverPopupStateController, 
                _menuStateController, _initShopState, _shopStateController);
            
            _stateMachine.GoTo<MenuStateController>().Forget();
        }
    }
}