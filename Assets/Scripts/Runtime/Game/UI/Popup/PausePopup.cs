using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.UI.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Core.UI.Popup
{
    public class PausePopup : BasePopup
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _leaveButton;
        
        public event Action OnResumePressed;
        public event Action OnRestartPressed;
        public event Action OnLeavePressed;

        public override UniTask Show(BasePopupData data, CancellationToken cancellationToken = default)
        {
            _resumeButton.onClick.AddListener(() => OnResumePressed?.Invoke());
            _restartButton.onClick.AddListener(() => OnRestartPressed?.Invoke());
            _leaveButton.onClick.AddListener(() => OnLeavePressed?.Invoke());
            return base.Show(data, cancellationToken);
        }
    }
}