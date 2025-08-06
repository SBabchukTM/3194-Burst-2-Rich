using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Game.Services.UI;
using Runtime.Game.UI.Screen;
using ILogger = Runtime.Core.Infrastructure.Logger.ILogger;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class ProfileScreenStateController : StateController
    {
        private readonly IUiService _uiService;
        
        private ProfileScreen _screen;
        
        public ProfileScreenStateController(ILogger logger, IUiService uiService) : base(logger)
        {
            _uiService = uiService;
        }
        
        public override UniTask Enter(CancellationToken cancellationToken)
        {
            CreateScreen();
            SubscribeToEvents();
            
            return UniTask.CompletedTask;
        }
        
        public override async UniTask Exit()
        {
            await _uiService.HideScreen(ConstScreens.ProfileScreen);
        }
        
        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<ProfileScreen>(ConstScreens.ProfileScreen);
            _screen.Initialize();
            _screen.ShowAsync().Forget();
        }
        
        private void SubscribeToEvents()
        {
            _screen.OnBackPressed += async () => await GoTo<MenuStateController>();
        }    
    }
}