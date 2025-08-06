using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.GameStateMachine;
using Runtime.Core.Infrastructure.Logger;
using Runtime.Core.UI.Popup;
using Runtime.Game.Services.UI;

namespace Runtime.Game.GameStates.Game.Popups
{
    public class DifficultySelectPopupStateController : StateController
    {
        private readonly IUiService _uiService;
        
        public DifficultySelectPopupStateController(ILogger logger, IUiService uiService) : base(logger)
        {
            _uiService = uiService;
        }

        public override async UniTask Enter(CancellationToken cancellationToken = default)
        {
            DifficultySelectPopup popup = await _uiService.ShowPopup(ConstPopups.DifficultySelectPopup) as DifficultySelectPopup;
            
        }
    }
}