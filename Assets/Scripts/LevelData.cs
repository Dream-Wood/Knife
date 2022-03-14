using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    [SerializeField] private Sprite _spinerSprite;

    [SerializeField] private StageData[] _stages;

    [SerializeField] private BossLevelData _bossLevel;

    public Sprite SpinerSprite => _spinerSprite;

    public StageData[] Stages => _stages;

    public BossLevelData BossLevel => _bossLevel;
}