using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.Audio;
using Runtime.Core.GameStateMachine;
using Runtime.Game.Services.UI;
using Runtime.Game.Services.UserData;
using Runtime.Game.UI.Screen;
using ILogger = Runtime.Core.Infrastructure.Logger.ILogger;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class SettingsScreenStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly IAudioService _audioService;
        private readonly UserDataService _userDataService;
        
        private SettingsScreen _screen;
        
        public SettingsScreenStateController(ILogger logger, IUiService uiService,
            IAudioService audioService, UserDataService userDataService) : base(logger)
        {
            _uiService = uiService;
            _audioService = audioService;
            _userDataService = userDataService;
        }
        
        public override UniTask Enter(CancellationToken cancellationToken)
        {
            CreateScreen();
            SubscribeToEvents();
            
            return UniTask.CompletedTask;
        }
        
        public override async UniTask Exit()
        {
            await _uiService.HideScreen(ConstScreens.SettingsScreen);
        }
        
        private void CreateScreen()
        {
            _screen = _uiService.GetScreen<SettingsScreen>(ConstScreens.SettingsScreen);
            _screen.Initialize(_userDataService.GetUserData().SettingsData);
            _screen.ShowAsync().Forget();
        }
        
        private void SubscribeToEvents()
        {
            _screen.OnBackPressed += async () => await GoTo<MenuStateController>();
            _screen.OnMusicChanged += (volume) =>
            {
                _audioService.SetVolume(AudioType.Music, volume);
                _userDataService.GetUserData().SettingsData.MusicVolume = volume;
            };
            
            _screen.OnSoundChanged += (volume) =>
            {
                _audioService.SetVolume(AudioType.Sound, volume);
                _userDataService.GetUserData().SettingsData.SoundVolume = volume;
            };

            _screen.OnPpPressed += async () => await _uiService.ShowPopup(ConstPopups.PrivacyPolicyPopup);
            _screen.OnTouPressed += async () => await _uiService.ShowPopup(ConstPopups.TermsOfUsePopup);
        }    
    }
}