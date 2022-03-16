using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text _appleCount;

    [SerializeField] private TMP_Text _score;

    [SerializeField] private Vector3 _scoreMenuPosition;
    
    [SerializeField] private Vector3 _scoreDefaultPos;

    [SerializeField] private GameObject _knifeContainer;
    
    [SerializeField] private Vector3 _knifeMenuPosition;
    
    [SerializeField] private Vector3 _knifeDefaultPos;

    [SerializeField] private Icon _knifeIcon;

    private float _xOffsetPercent = 5; // %
    private float _yOffsetPercent = 5; // %

    private readonly List<Icon> _iconsKnife = new List<Icon>();

    private int _index;

    private void Awake()
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        
        _scoreDefaultPos.y = screenSize.y / 100 * 5 - screenSize.y / 2;
        _knifeDefaultPos.x = screenSize.x / 100 * 5 - screenSize.x / 2;
        _knifeMenuPosition.x = -screenSize.x / 100 * 5 - screenSize.x / 2;
    }

    public void AppleCountChanged(int val)
    {
        _appleCount.text = val.ToString();
    }

    public void ScoreChanged(int val)
    {
        _score.text = val.ToString();
    }

    public void InitKnifeCounter(int knifeCount)
    {
        foreach (var i in _iconsKnife)
        {
            Destroy(i.gameObject);
        }
            
        _iconsKnife.Clear();
        
        for (int i = 0; i < knifeCount; i++)
        {
            Icon tmp = Instantiate(_knifeIcon, _knifeContainer.transform);
            _iconsKnife.Add(tmp);
        }
        _iconsKnife.Reverse();

        _index = knifeCount;
    }

    public void DeleteKnife()
    {
        _index--;
        _iconsKnife[_index].Use();
    }

    public void MenuMode()
    {
        _score.rectTransform.DOLocalMove(_scoreMenuPosition, 1f);
        _score.DOColor(Color.black, 1f);

        _knifeContainer.transform.DOLocalMove(_knifeMenuPosition, 1f);
    }
    
    public void GameMode()
    {
        _score.rectTransform.DOLocalMove(_scoreDefaultPos, 1f);
        _score.DOColor(Color.white, 1f);

        _knifeContainer.transform.DOLocalMove(_knifeDefaultPos, 1f);
    }
}