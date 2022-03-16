using DG.Tweening;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public Thrower OriginThrower { get; set; }

    [SerializeField] private Rigidbody2D _rigidbody;
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private BoxCollider2D _collider;

    [SerializeField] private bool _isFailed;
    
    [SerializeField] private bool _attach;
    
    public void Initialized()
    {
        _rigidbody.simulated = false;
        _spriteRenderer.DOFade(0,0);
        transform.DOMoveY(OriginThrower.transform.position.y, .1f);
        _spriteRenderer.DOFade(1, .1f);
    }

    public void Run(float force)
    {
        _rigidbody.simulated = true;
        _rigidbody.AddForce(Vector2.up * force);
    }

    public void Detach()
    {
        _rigidbody.constraints = RigidbodyConstraints2D.None;
        _rigidbody.AddTorque(30);
        _spriteRenderer.DOFade(0, 1f);
        _collider.enabled = false;
        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out ThrowObject tmp) && !_isFailed && !_attach)
        {
            _isFailed = true;
            _rigidbody.velocity = _rigidbody.velocity / 3;
            _rigidbody.AddTorque(30);
            OriginThrower.ThrowFail();
            _collider.enabled = false;
            Vibration.VibratePeek();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Spiner spin) && !_isFailed && !_attach)
        {
            _attach = true;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            spin.Attach(this);
            Vibration.VibratePop();
            OriginThrower.ThrowSuccess();
        }

        if (other.TryGetComponent(out Apple apple) && !_isFailed && !_attach)
        {
            apple.DestroyApple();
            OriginThrower.CollectApple();
        }
    }
}
