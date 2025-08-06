using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.Audio;
using Runtime.Core.GameStateMachine;
using Runtime.Core.UI.Popup;
using Runtime.Game.GameStates.Game.Screens;
using Runtime.Game.Services.Audio;
using Runtime.Game.Services.UI;
using UnityEngine;
using ILogger = Runtime.Core.Infrastructure.Logger.ILogger;

namespace Runtime.Game.GameStates.Game.Popups
{
    public class GameOverPopupStateController : StateController
    {
        private readonly IUiService _uiService;
        private readonly GameplayData _gameplayData;
        private readonly IAudioService _audioService;
        
        public GameOverPopupStateController(ILogger logger, IUiService uiService, GameplayData gameplayData, IAudioService audioService) : base(logger)
        {
            _uiService = uiService;
            _gameplayData = gameplayData;
            _audioService = audioService;
        }

        public override async UniTask Enter(CancellationToken cancellationToken = default)
        {
            GameOverPopup popup = await _uiService.ShowPopup(ConstPopups.GameOverPopup) as GameOverPopup;
            
            Time.timeScale = 0;

            _audioService.PlaySound(ConstAudio.GameOverSound);
            popup.SetData(_gameplayData.DifficultyId, _gameplayData.CoinsCollected);
            
            popup.OnLeaveButtonPressed += async () =>
            {
                Time.timeScale = 1;
                popup.DestroyPopup();
                await GoTo<MenuStateController>();
            };

            popup.OnRestartButtonPressed += async () =>
            {
                Time.timeScale = 1;
                popup.DestroyPopup();
                await GoTo<GameplayScreenStateController>();
            };
        }
    }
}