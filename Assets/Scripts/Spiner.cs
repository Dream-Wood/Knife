using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spiner : MonoBehaviour
{
    [SerializeField] private GameObject _spinObject;

    [SerializeField] private ParticleSystem _particle;

    [SerializeField] private SpriteRenderer _spinerSpriteRenderer;

    [SerializeField] private float _iterationStep = 0.1f;

    [SerializeField] private SpinerSettings _settings;

    private Vector3 _spinObjectScale;

    private readonly List<ThrowObject> _attachObjects = new List<ThrowObject>();

    private AnimationCurve _rotationPattern;

    private bool _isFirst = true;

    private float _knifeAttachOffset = 0.5f;

    [SerializeField] private float _iteration;

    private void Start()
    {
        _spinObjectScale = _spinObject.transform.localScale;
    }

    private void CreateApple()
    {
        var range = Random.Range(0f, 101f);
        if (_settings.SpawnAppleChange > range)
        {
            Vector3 spawnPos = transform.position +
                               new Vector3(Random.value - 0.5f, Random.value - 0.5f, 0f).normalized *
                               _settings.SpinerRadius;

            GameObject apple = Instantiate(_settings.ApplePrefab, spawnPos, Quaternion.identity);

            apple.transform.rotation = Quaternion.Euler(0, 0,
                180 + Mathf.Atan2(transform.position.y - apple.transform.position.y,
                    transform.position.x - apple.transform.position.x) * Mathf.Rad2Deg - 90);

            apple.transform.parent = transform;
        }
    }

    public void CreateDefaultKnife(int count)
    {
        int _count = count;
        if (count == 0)
        {
            _count = Random.Range(1, 4);
        }

        for (int i = 0; i < _count; i++)
        {
            Vector3 pos = new Vector3(Random.value - 0.5f, Random.value - 0.5f, 0f).normalized;
            Vector3 targetPos = transform.position + pos * (_settings.SpinerRadius - .3f);
            Vector3 spawnPos = transform.position + pos * (_settings.SpinerRadius * 15);

            ThrowObject knife = Instantiate(_settings.DefaultKnifePrefab, spawnPos, Quaternion.identity);

            knife.transform.rotation = Quaternion.Euler(0, 0,
                   + Mathf.Atan2(transform.position.y - targetPos.y,
                    transform.position.x - targetPos.x) * Mathf.Rad2Deg - 90);

            knife.transform.DOMove(targetPos, .75f).OnComplete(() =>
            {
                Attach(knife, false); 
            });
        }
    }

    private void Initialize()
    {
        _spinObject.SetActive(true);
        CreateApple();
    }

    public void Reclaim(Sprite spinSprite, AnimationCurve rotationPattern)
    {
        _rotationPattern = rotationPattern;
        _spinerSpriteRenderer.sprite = spinSprite;
        _spinObject.SetActive(false);

        if (!_isFirst)
        {
            GameObject tmp = Instantiate(_settings.SpinerDestroyPrefab, transform.position, Quaternion.identity);
            Destroy(tmp, 1);
        }

        foreach (var o in _attachObjects)
        {
            o.Detach();
        }

        _attachObjects.Clear();
        _isFirst = false;

        Invoke(nameof(Initialize), 0.25f);
    }

    public void GameUpdate()
    {
        transform.Rotate(Vector3.forward, _rotationPattern.Evaluate(_iteration) * Time.deltaTime);
        _iteration += _iterationStep * Time.deltaTime;
    }

    public void Attach(ThrowObject trObject, bool playParticle = true)
    {
        if (playParticle) _particle.Play();
        
        trObject.transform.parent = transform;
        _attachObjects.Add(trObject);

        _spinObject.transform.DOScale(_spinObjectScale + Vector3.one / 12, .25f)
            .OnComplete(() => _spinObject.transform.DOScale(_spinObjectScale, .25f));
        _spinerSpriteRenderer.DOColor(Color.yellow, .25f)
            .OnComplete(() => _spinerSpriteRenderer.DOColor(Color.white, .25f));
    }
}