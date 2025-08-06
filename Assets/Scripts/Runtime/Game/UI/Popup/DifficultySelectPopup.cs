using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.UI.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Core.UI.Popup
{
    public class DifficultySelectPopup : BasePopup
    {
        [SerializeField] private Button _easyButton;
        [SerializeField] private Button _mediumButton;
        [SerializeField] private Button _hardButton;
        [SerializeField] private Button _veryHardButton;

        public event Action<int> OnDifficultySelected;

        public override UniTask Show(BasePopupData data, CancellationToken cancellationToken = default)
        {
            _easyButton.onClick.AddListener(() => OnDifficultySelected?.Invoke(0));
            _mediumButton.onClick.AddListener(() => OnDifficultySelected?.Invoke(1));
            _hardButton.onClick.AddListener(() => OnDifficultySelected?.Invoke(2));
            _veryHardButton.onClick.AddListener(() => OnDifficultySelected?.Invoke(3));
            return base.Show(data, cancellationToken);
        }
    }
}