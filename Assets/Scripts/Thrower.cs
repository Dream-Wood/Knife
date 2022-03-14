using System;
using System.Collections;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private ThrowObject[] _throwObjects;

    [SerializeField] private ThrowSettings _throwSettings;

    [SerializeField] private bool _delay;

    private ThrowObject _throwObject;
    
    private bool _isInti;

    public event Action OnFail;
    
    public event Action OnSuccess;
    
    public event Action OnCollectApple;

    public void Initialized()
    {
        _throwObject = Instantiate(_throwObjects[0], new Vector3(0,-5,0), quaternion.identity);
        _throwObject.OriginThrower = this;
        _throwObject.Initialized();
        _isInti = true;
    }

    public void Throw()
    {
        if (!_delay && _isInti)
        {
            StartCoroutine(nameof(Throwing));
            _delay = true;
        }
    }

    IEnumerator Throwing()
    {
        _throwObject.Run(_throwSettings.ThrowForce);
        Initialized();
        yield return new WaitForSeconds(_throwSettings.ThrowDelay);
        _delay = false;
    }

    public void ThrowFail()
    {
        OnFail?.Invoke();
    }
    
    public void ThrowSuccess()
    {
        OnSuccess?.Invoke();
    }
    
    public void CollectApple()
    {
        OnCollectApple?.Invoke();
    }
}
