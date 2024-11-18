using System;
using UnityEngine;

public class ObjectTroll : MonoBehaviour
{
    [SerializeField] private AudioClip _noSound;
    [SerializeField] private AudioClip _wakeUpSound;
    [SerializeField] private AudioClip _bonusSound;
    
    private AudioSource _audioSource;
    private Animator _animator;
    
    private bool _isWakeUp;
    
    private readonly float _neededTime = 0.8f;
    private bool _isTimer;
    private float _time;
    private bool _isTumble;
    
    private static readonly int TumbleTrigger = Animator.StringToHash("tumble");
    private static readonly int WakeUpTrigger = Animator.StringToHash("wakeUp");

    private void OnEnable()
    {
        Score.OnScoreReady += WakeUp;
    }

    private void OnDisable()
    {
        Score.OnScoreReady -= WakeUp;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseEnter()
    {
        if (!_isWakeUp && !_isTumble)
        {
            _audioSource.PlayOneShot(_noSound);
            _animator.SetTrigger(TumbleTrigger);
        }
        else if (_isWakeUp && !_isTimer && !_isTumble)
        {
            _isTimer = true;
        }
    }

    private void OnMouseExit()
    {
        if (_isWakeUp && _isTimer)
        {
            _isTimer = false;
            _time = 0;
        }
    }
    
    private void Update()
    {
        if (_isTimer)
        {
            _time += Time.deltaTime;
            if (_time >= _neededTime)
            {
                _animator.SetTrigger(WakeUpTrigger);
                _isTimer = false;
            }
        }
        else if (!_isTimer)
        {
            _time = 0;
        }
    }

    private void WakeUp()
    {
        _isWakeUp = true;
    }

    public void TumbleOn()
    {
        _isTumble = true;
    }
    
    public void TumbleOff()
    {
        _isTumble = false;
    }

    public void BonusSound()
    {
        _audioSource.PlayOneShot(_bonusSound);
    }

    public void WakeUpSound()
    {
        _audioSource.pitch = 1.75f;
        _audioSource.PlayOneShot(_wakeUpSound);
    }
}
