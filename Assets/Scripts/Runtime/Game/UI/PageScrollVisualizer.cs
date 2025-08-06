using DG.Tweening;
using UnityEngine;

namespace Runtime.Gameplay.UI
{
    public class PageScrollVisualizer : MonoBehaviour
    {
        private readonly Vector3 TargetScale = Vector3.one * 1.3f;
        private const float AnimTime = 0.3f;
    
        [SerializeField] private Transform[] _circles;
        [SerializeField] private PageScroller _pageScroller;

        private int _prevPageIndex = 0;

        private void Awake()
        {
            _pageScroller.OnPageChanged += PlayAnim;
        
            _prevPageIndex = 0;
            PlayScaleUpAnim(_prevPageIndex);
        }

        private void OnDestroy()
        {
            _pageScroller.OnPageChanged -= PlayAnim;
        }

        private void PlayAnim(int newPageIndex)
        {
            PlayScaleUpAnim(newPageIndex);
            PlayScaleDownAnim(_prevPageIndex);
        
            _prevPageIndex = newPageIndex;
        }

        private void PlayScaleUpAnim(int index)
        {
            var transform = _circles[index];
            transform.DOScale(TargetScale, AnimTime).SetLink(gameObject);
        }
    
        private void PlayScaleDownAnim(int index)
        {
            var transform = _circles[index];
            transform.DOScale(Vector3.one, AnimTime).SetLink(gameObject);
        }
    }
}
