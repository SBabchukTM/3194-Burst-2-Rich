using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Core.UI.Data;
using Runtime.Core.UI.Popup;
using Runtime.Game.UI.Screen;
using UnityEngine;

namespace Runtime.Game.Services.UI
{
    public interface IUiService
    {
        UniTask Initialize();
        bool IsScreenShowed(string id);
        T GetScreen<T>(string id) where T : UiScreen;
        UniTask ShowScreen(string id, CancellationToken cancellationToken = default);
        UniTask HideScreen(string id, CancellationToken cancellationToken = default);
        void HideScreenImmediately(string id);
        UniTask<BasePopup> ShowPopup(string id, BasePopupData data = null, CancellationToken cancellationToken = default);
        T GetPopup<T>(string id) where T : BasePopup;
        void HideAllScreensImmediately();
        UniTask HideAllAsyncScreens(CancellationToken cancellationToken = default);
        UniTask FadeInAsync(
            Color? color = null,
            float? duration = null,
            CancellationToken cancellationToken = default
        );

        UniTask FadeOutAsync(
            Color? color = null,
            float? duration = null,
            CancellationToken cancellationToken = default
        );
    }
}