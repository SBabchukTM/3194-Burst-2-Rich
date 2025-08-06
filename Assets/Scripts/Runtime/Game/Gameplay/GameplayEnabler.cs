using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEnabler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _bgSprite;
    [SerializeField] private BalloonController _balloon;

    public void StartGame(Sprite balloonSkin)
    {
        _bgSprite.enabled = true;
        _balloon.SetSprite(balloonSkin);
        _balloon.gameObject.SetActive(true);
        _balloon.transform.position = Vector2.zero;
    }

    public void EndGame()
    {
        _bgSprite.enabled = false;
        _balloon.gameObject.SetActive(false);
    }
}
