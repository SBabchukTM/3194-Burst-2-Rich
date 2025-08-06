using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.UI.Popup;
using Runtime.Game.GameStates.Game.Screens;
using Runtime.Game.Services.UI;
using UnityEngine;
using ILogger = Runtime.Core.Infrastructure.Logger.ILogger;

namespace Runtime.Game.GameStates.Game.Popups
{
    public class PausePopupStateController : StateController
    {
        private readonly IUiService _uiService;
        
        public PausePopupStateController(ILogger logger, IUiService uiService) : base(logger)
        {
            _uiService = uiService;
        }

        public override async UniTask Enter(CancellationToken cancellationToken = default)
        {
            PausePopup popup = await _uiService.ShowPopup(ConstPopups.PausePopup) as PausePopup;
            
            Time.timeScale = 0;

            popup.OnResumePressed += () =>
            {
                Time.timeScale = 1;
                popup.DestroyPopup();
            };
            
            popup.OnRestartPressed += async () =>
            {
                Time.timeScale = 1;
                popup.DestroyPopup();

                await GoTo<GameplayScreenStateController>();
            };
            
            popup.OnLeavePressed += async () =>
            {
                Time.timeScale = 1;
                popup.DestroyPopup();
                await GoTo<MenuStateController>(); 
            };
        }
    }
}