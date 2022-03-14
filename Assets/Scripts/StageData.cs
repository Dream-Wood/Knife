using System;
using UnityEngine;

[Serializable]
public class StageData
{
    [SerializeField] private AnimationCurve _curve;

    [SerializeField] private int _objective;
    
    [Header("Set 0 for random value")] 
    [SerializeField] private int _knifeCount = 0;
    
    public AnimationCurve Curve => _curve;
    
    public int Objective => _objective;

    public int KnifeCount => _knifeCount;
}