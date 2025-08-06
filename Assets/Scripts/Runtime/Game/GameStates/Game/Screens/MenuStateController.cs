using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.UI.Popup;
using Runtime.Game.GameStates.Game.Controllers;
using Runtime.Game.Services.UI;
using Runtime.Game.UI.Screen;
using ILogger = Runtime.Core.Infrastructure.Logger.ILogger;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class MenuStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly GameplayData _gameplayData;

        private MenuScreen _screen;

        public MenuStateController(ILogger logger, IUiService uiService, GameplayData gameplayData) : base(logger)
        {
            _uiService = uiService;
            _gameplayData = gameplayData;
        }

        public override UniTask Enter(CancellationToken cancellationToken)
        {
            CreateScreen();
            SubscribeToEvents();
            
            return UniTask.CompletedTask;
        }

        public override async UniTask Exit()
        {
            await _uiService.HideScreen(ConstScreens.MenuScreen);
        }

        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<MenuScreen>(ConstScreens.MenuScreen);
            _screen.Initialize();
            _screen.ShowAsync().Forget();
        }

        private void SubscribeToEvents()
        {
            _screen.OnLeadButtonPressed += async () => await GoTo<LeaderboardScreenStateController>();
            _screen.OnShopButtonPressed += async () => await GoTo<InitShopState>();
            _screen.OnSetButtonPressed += async () => await GoTo<SettingsScreenStateController>();
            _screen.OnProfButtonPressed += async () => await GoTo<ProfileScreenStateController>();
            _screen.OnHtpButtonPressed += async () => await _uiService.ShowPopup(ConstPopups.HowToPlayPopup);
            _screen.OnStartButtonPressed += OpenDifficultyPopup;
        }

        private async void OpenDifficultyPopup()
        {
            var popup = await _uiService.ShowPopup(ConstPopups.DifficultySelectPopup) as DifficultySelectPopup;

            popup.OnDifficultySelected += async (id) =>
            {
                _gameplayData.DifficultyId = id;
                popup.DestroyPopup();
                await GoTo<GameplayScreenStateController>();
            };
        }
    }
}