using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Spiner _spiner;

    [SerializeField] private Thrower _thrower;

    [SerializeField] private HUD _hud;

    [SerializeField] private UI _ui;

    [SerializeField] private LevelData[] _levels;

    [SerializeField] private UnityAnalyticsInit _anal;
    [SerializeField] private TMP_InputField _field;

    private int _appleCount;

    private int _score;
    
    private bool _firstBoot = false;

    private int _highScore;

    private int _objective;

    private int _stage;

    private string _name;

    private int _level;

    private bool _isBossStage;

    private bool _pause;

    public void BuyContest(int cost)
    {
        if (_appleCount < cost)
        {
            return;
        }

        _appleCount -= cost;
        _anal.SendBuyContestEvent();
        _hud.AppleCountChanged(_appleCount);
        SaveData();
    }

    public void AddNewUser()
    {
        if (_field == null)
        {
            return;
        }

        _name = _field.text;
        _ui.ShowRegScreen(false);
        _firstBoot = true;
        SaveData();
    }

    public void StartGame()
    {
        _thrower.Initialized();
        _hud.GameMode();
        _hud.ScoreChanged(_score);
        _pause = false;
        _ui.ShowMenuScreen(false);
        
        NextStage();
    }

    public void StopGame()
    {
        SceneManager.LoadScene(0);
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        DOTween.Init();
        Vibration.Init();
        _pause = true;
        _hud.MenuMode();
        _ui.ShowMenuScreen(true);

        _highScore = PlayerPrefs.GetInt("Score");
        _appleCount = PlayerPrefs.GetInt("Apple");
        _name = PlayerPrefs.GetString("PlayerName");
        _firstBoot = PlayerPrefs.GetInt("FB") != 0;
        
        _hud.ScoreChanged(_highScore);
        _hud.AppleCountChanged(_appleCount);
        
        if (_name == "" || !_firstBoot)
        {
            _ui.ShowRegScreen(true);
        }
    }

    private void OnEnable()
    {
        _thrower.OnSuccess += OnScoreChanged;
        _thrower.OnCollectApple += OnAppleCollect;
        _thrower.OnFail += OnFail;
    }

    private void OnDisable()
    {
        _thrower.OnSuccess -= OnScoreChanged;
        _thrower.OnCollectApple -= OnAppleCollect;
        _thrower.OnFail -= OnFail;
    }

    private void OnAppleCollect()
    {
        _appleCount++;
        _hud.AppleCountChanged(_appleCount);
    }

    private void OnFail()
    {
        _hud.MenuMode();
        _pause = true;
        _ui.ShowFailScreen(true);
        SaveData();
    }

    private void OnScoreChanged()
    {
        _objective--;
        _score++;
        _hud.ScoreChanged(_score);
        _hud.DeleteKnife();

        if (_objective == 0)
        {
            Vibration.VibrateNope();

            if (_isBossStage)
            {
                _isBossStage = false;
                if (_levels.Length - 1 > _level)
                {
                    _level++;
                }

                _stage = 0;
                NextStage();
                return;
            }

            if (_stage == _levels[_level].Stages.Length - 1)
            {
                BossStage();
                _stage++;
                return;
            }

            _stage++;
            NextStage();
        }
    }

    private void NextStage()
    {
        _objective = _levels[_level].Stages[_stage].Objective;
        _spiner.Reclaim(_levels[_level].SpinerSprite, _levels[_level].Stages[_stage].Curve);
        _spiner.CreateDefaultKnife(_levels[_level].Stages[_stage].KnifeCount);
        _hud.InitKnifeCounter(_objective);
    }

    private void BossStage()
    {
        _objective = _levels[_level].BossLevel.Stage.Objective;
        _spiner.Reclaim(_levels[_level].BossLevel.BossSpite, _levels[_level].BossLevel.Stage.Curve);
        _spiner.CreateDefaultKnife(_levels[_level].BossLevel.Stage.KnifeCount);
        _hud.InitKnifeCounter(_objective);
        _isBossStage = true;
    }

    private void Update()
    {
        if (_pause)
        {
            return;
        }

        _spiner.GameUpdate();

        if (Input.GetMouseButtonDown(0))
        {
            _thrower.Throw();
        }
    }

    private void SaveData() //Вообще необходимо вынеси в отдельный класс, но это PP
    {
        if (_highScore < _score)
        {
            PlayerPrefs.SetInt("Score", _score);
        }

        PlayerPrefs.SetInt("FB", _firstBoot ? 1 : 0);
        PlayerPrefs.SetInt("Apple", _appleCount);
        PlayerPrefs.SetString("PlayerName", _name);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}