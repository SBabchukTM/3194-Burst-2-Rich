using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class BalloonController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _moveTimePerUnit;
    
    private InputProvider _inputProvider;
    
    private Sequence _sequence;

    [Inject]
    private void Construct(InputProvider inputProvider) => _inputProvider = inputProvider;

    private void Awake() => _inputProvider.OnPlayerTap += MoveTowards;

    private void OnDestroy() => _inputProvider.OnPlayerTap -= MoveTowards;
    
    public void SetSprite(Sprite sprite) => _spriteRenderer.sprite = sprite;

    private void MoveTowards(Vector3 targetPos)
    {
        float distance = Vector3.Distance(targetPos, transform.position);
        
        float moveTime = distance * _moveTimePerUnit;
        
        _sequence?.Kill();
        
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOMove(targetPos, moveTime)).SetEase(Ease.OutCirc);
        _sequence.SetLink(gameObject);
    }
}
