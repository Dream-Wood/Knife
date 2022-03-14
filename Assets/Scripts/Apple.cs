using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private GameObject _destroyPrefab;

    public void DestroyApple()
    {
        GameObject tmp = Instantiate(_destroyPrefab, transform.position, Quaternion.Euler(0,0,180));
        Destroy(tmp, 2);
        Destroy(gameObject);
    }
}
