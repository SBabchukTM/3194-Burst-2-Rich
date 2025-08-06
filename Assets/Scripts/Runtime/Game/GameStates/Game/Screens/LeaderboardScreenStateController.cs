using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Game.Services.UI;
using Runtime.Game.UI.Screen;
using ILogger = Runtime.Core.Infrastructure.Logger.ILogger;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class LeaderboardScreenStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly LeaderboardFactory _leaderboardFactory;
        
        private LeaderboardScreen _screen;
        
        public LeaderboardScreenStateController(ILogger logger, IUiService uiService,
            LeaderboardFactory leaderboardFactory) : base(logger)
        {
            _uiService = uiService;
            _leaderboardFactory = leaderboardFactory;
        }
        
        public override UniTask Enter(CancellationToken cancellationToken)
        {
            CreateScreen();
            SubscribeToEvents();
            
            return UniTask.CompletedTask;
        }
        
        public override async UniTask Exit()
        {
            await _uiService.HideScreen(ConstScreens.LeaderboardScreen);
        }
        
        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<LeaderboardScreen>(ConstScreens.LeaderboardScreen);
            _screen.Initialize(_leaderboardFactory.GetLeaderboard());
            _screen.ShowAsync().Forget();
        }
        
        private void SubscribeToEvents()
        {
            _screen.OnBackPressed += async () => await GoTo<MenuStateController>();
        }    
    }
}