using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardRecordDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _placeText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Image _bgImage;
    
    public void Initialize(int place, string name, int score, Sprite bgSprite)
    {
        _placeText.text = place.ToString();
        _nameText.text = name;
        _scoreText.text = score.ToString();
        _bgImage.sprite = bgSprite;
    }
}
