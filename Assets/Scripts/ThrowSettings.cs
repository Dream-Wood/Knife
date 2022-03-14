using UnityEngine;

[CreateAssetMenu]
public class ThrowSettings : ScriptableObject
{
    [SerializeField] private float _throwForce;
    
    [SerializeField] private float _throwDelay;

    public float ThrowForce => _throwForce;
    
    public float ThrowDelay => _throwDelay;
}