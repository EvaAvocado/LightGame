using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ObjectCandle : MonoBehaviour
{
    [SerializeField] private Light2D _light2D;
    [SerializeField] private List<AudioClip> _onSounds;
    [SerializeField] private List<AudioClip> _bonusSounds;
    
    private SpriteRenderer _spriteRenderer;
    
    private AudioSource _audioSource;
    private Animator _animator;
    
    private readonly float _neededTime = 1.5f;
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
        _audioSource = GetComponent<AudioSource>();
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
        Tools.Shuffle(_bonusSounds);
        _audioSource.PlayOneShot(_bonusSounds[0]);
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

    public void OnSound()
    {
        Tools.Shuffle(_onSounds);
        _audioSource.PlayOneShot(_onSounds[0]);
    }
}
