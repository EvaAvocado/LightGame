using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ObjectLamps : MonoBehaviour
{
    [SerializeField] private Light2D _light2D;
    [SerializeField] private AudioClip _lampSound;
    [SerializeField] private AudioClip _onSound;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    
    private float _neededTime = 3f;
    private bool _isTimer;
    private bool _isOn;
    private float _time;

    private bool _isOffSound;
    private bool _isOnMusic;
    
    private static readonly int Trigger = Animator.StringToHash("trigger");
    private static readonly int TurnOn = Animator.StringToHash("turn-on");
    private static readonly int TurnOff = Animator.StringToHash("turn-off");
    
    public static event Action OnReady;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseEnter()
    {
        if (!_isOn)
        {
            _isTimer = true;
            _animator.SetTrigger(Trigger);
        }
    }

    private void OnMouseExit()
    {
        if (!_isOn)
        {
            _isTimer = false;
            _animator.SetTrigger(TurnOff);
        }
    }

    private void Update()
    {
        if (_isTimer && !_isOn)
        {
            _time += Time.deltaTime;
            if (_time >= _neededTime)
            {
                _animator.SetTrigger(TurnOn);
                _isOn = true;
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

    public void OffMusic()
    {
        _isOffSound = true;
    }

    public void OnLight()
    {
        if (!_isOffSound && !_isOnMusic)
        {
            _audioSource.PlayOneShot(_lampSound);
        }
        else if (_isOffSound && !_isOnMusic)
        {
            _audioSource.loop = true;
            _audioSource.volume = 0.02f;
            _audioSource.clip = _onSound;
            _audioSource.Play();
            _isOnMusic = true;
        }
        
        _light2D.lightCookieSprite = _spriteRenderer.sprite;
    }

    public void OffLight()
    {
        _light2D.lightCookieSprite = null;
    }
}
