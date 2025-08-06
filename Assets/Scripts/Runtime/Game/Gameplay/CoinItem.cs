using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class CoinItem : MonoBehaviour
{
    [Inject]
    private CoinsPool _pool;
    
    [Inject]
    private CoinSpawner _spawner;
    
    private Sequence _sequence;

    private void OnCollisionEnter2D(Collision2D other)
    {
        _sequence?.Kill();
        _spawner.InvokeCollected();
        _pool.ReturnCoinItem(this);
    }

    public void Spawn(float growTime, float shrinkTime)
    {
        _sequence?.Kill();
        
        transform.localScale = Vector3.zero;

        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOScale(Vector3.one, growTime)).SetEase(Ease.OutCubic);
        _sequence.Append(transform.DOScale(Vector3.zero, shrinkTime)).SetEase(Ease.Linear);
        _sequence.OnComplete(() =>
        {
            _spawner.InvokeMissed();
            _pool.ReturnCoinItem(this);
        });
        _sequence.SetLink(gameObject);
    }

    public void Kill()
    {
        _sequence?.Kill();
    }
}
