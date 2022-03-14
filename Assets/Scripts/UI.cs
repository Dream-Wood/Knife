using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _menuScreen;
    
    [SerializeField] private GameObject _failScreen;

    public void ShowMenuScreen(bool val)
    {
        if (val)
        {
            MoveIn(_menuScreen.transform);
        }
        else
        {
            MoveOut(_menuScreen.transform);
        }
    }
    
    public void ShowFailScreen(bool val)
    {
        if (val)
        {
            MoveIn(_failScreen.transform);
        }
        else
        {
            MoveOut(_failScreen.transform);
        }
    }

    private void MoveIn(Transform rt)
    {
        rt.DOLocalMove(Vector3.zero, 1f);
    }
    
    private void MoveOut(Transform rt)
    {
        rt.DOLocalMove(Vector3.up * 3000, 1f);
    }
}
