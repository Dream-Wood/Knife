using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{ 
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject failScreen;
    [SerializeField] private GameObject shopScreen;
    [SerializeField] private GameObject regScreen;
    
    public void ShowMenuScreen(bool val)
    {
        if (val)
        {
            MoveIn(menuScreen.transform);
        }
        else
        {
            MoveOut(menuScreen.transform);
        }
    }
    
    public void ShowFailScreen(bool val)
    {
        if (val)
        {
            MoveIn(failScreen.transform);
        }
        else
        {
            MoveOut(failScreen.transform);
        }
    }
    
    public void ShowRegScreen(bool val)
    {
        regScreen.SetActive(val);
    }
    
    public void ShowShopScreen(bool val)
    {
        if (val)
        {
            MoveIn(shopScreen.transform);
        }
        else
        {
            MoveOut(shopScreen.transform);
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
