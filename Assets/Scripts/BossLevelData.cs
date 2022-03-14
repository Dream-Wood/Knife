using System;
using UnityEngine;

[CreateAssetMenu]
public class BossLevelData : ScriptableObject
{
    [SerializeField] private Sprite _bossSpite;

    [SerializeField] private StageData _stage;

    [SerializeField] private Sprite _reward;

    public Sprite BossSpite => _bossSpite;

    public StageData Stage => _stage;

    public Sprite Reward => _reward;
}