using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Game.UI.Screen
{
    public class SplashScreen : UiScreen
    {
        [SerializeField] private Slider _slider;
        
        public override async UniTask HideAsync(CancellationToken cancellationToken = default)
        {
            await WaitSplashScreenAnimationFinish(cancellationToken);
            await base.HideAsync(cancellationToken);
        }

        private async UniTask WaitSplashScreenAnimationFinish(CancellationToken cancellationToken)
        {
            _slider.DOValue(1, 2);
            await UniTask.Delay(2000, cancellationToken: cancellationToken);
        }
    }
}