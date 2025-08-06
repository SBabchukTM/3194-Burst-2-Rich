using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.UI.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Core.UI.Popup
{
    public class GameOverPopup : BasePopup
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _leaveButton;
        [SerializeField] private TextMeshProUGUI _difficultyText;
        [SerializeField] private TextMeshProUGUI _rewardText;
        
        public event Action OnRestartButtonPressed;
        public event Action OnLeaveButtonPressed;

        public override UniTask Show(BasePopupData data, CancellationToken cancellationToken = default)
        {
            _restartButton.onClick.AddListener(() => OnRestartButtonPressed?.Invoke());
            _leaveButton.onClick.AddListener(() => OnLeaveButtonPressed?.Invoke());
            return base.Show(data, cancellationToken);
        }

        public void SetData(int difficulty, int reward)
        {
            switch (difficulty)
            {
                case 0:
                    _difficultyText.text = "Easy";
                    break;
                case 1:
                    _difficultyText.text = "Medium";
                    break;
                case 2:
                    _difficultyText.text = "Hard";
                    break;
                case 3:
                    _difficultyText.text = "Very Hard";
                    break;
            }
            
            _rewardText.text = "+" + reward;
        }
    }
}