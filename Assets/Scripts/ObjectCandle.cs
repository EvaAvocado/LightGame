using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ObjectCandle : MonoBehaviour
{
    [SerializeField] private Light2D _light2D;
    
    private SpriteRenderer _spriteRenderer;
    
    private readonly float _neededTime = 2f;
    private Animator _animator;
    
    private bool _isTimer;
    private float _time;

    private bool _isOn;
    
    private static readonly int Start = Animator.StringToHash("start");
    private static readonly int On = Animator.StringToHash("on");
    
    public static event Action OnReady;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RayLens"))
        {
            if (!_isOn)
            {
                _isTimer = true;
                _animator.SetTrigger(Start);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RayLens"))
        {
            if (!_isOn)
            {
                _isTimer = false;
                _time = 0;
                _animator.SetTrigger(Start);
            }
        }
    }

    private void Update()
    {
        if (_isTimer && !_isOn)
        {
            _time += Time.deltaTime;
            if (_time >= _neededTime)
            {
                _isOn = true;
                _animator.SetTrigger(On);
                _isTimer = false;
            }
        }
        else if (!_isTimer && !_isOn)
        {
            _time = 0;
        }
    }
    
    public void Ready()
    {
        OnReady?.Invoke();
    }
    
    public void OnLight()
    {
        _light2D.lightCookieSprite = _spriteRenderer.sprite;
    }

    public void OffLight()
    {
        _light2D.lightCookieSprite = null;
    }
}
