using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour
{
    [SerializeField] private Image _image;
    
    public void Use()
    {
        _image.DOColor(Color.black, .5f);
        _image.DOFade(0.2f, .5f);
    }
}
