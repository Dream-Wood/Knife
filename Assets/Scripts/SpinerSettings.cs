using UnityEngine;

[CreateAssetMenu]
public class SpinerSettings : ScriptableObject
{
    [SerializeField] private float _spinerRadius;

    [SerializeField] private GameObject _spinerDestroyPrefab;
    
    [SerializeField] private GameObject _applePrefab;
    
    [SerializeField] private ThrowObject _defaultKnifePrefab;

    [SerializeField] private float _spawnAppleChange;

    public float SpinerRadius => _spinerRadius;
    
    public GameObject SpinerDestroyPrefab => _spinerDestroyPrefab;
    
    public GameObject ApplePrefab => _applePrefab;
    
    public ThrowObject DefaultKnifePrefab => _defaultKnifePrefab;
    
    public float SpawnAppleChange => _spawnAppleChange;
}
