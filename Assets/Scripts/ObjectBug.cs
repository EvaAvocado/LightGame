using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBug : MonoBehaviour
{ 
    [SerializeField] private Animator _animatorMove;
    [SerializeField] private Animator _animatorBug;
    [SerializeField] private List<AudioClip> _bugSounds;
    [SerializeField] private AudioClip _bugBonusSound;
    [SerializeField] private AudioClip _readySound;
    [SerializeField] private AudioSource _bonusAudioSource;

    [SerializeField] private GameObject _lightGO;

    private AudioSource _audioSource;
    
    private readonly float _neededTime = 0.35f;
    private bool _isTimer;
    private float _time;
    private bool _isFlying;

    private int _counter;
    
    private static readonly int Trigger = Animator.StringToHash("trigger");
    private static readonly int Flying = Animator.StringToHash("flying");
    
    public static event Action OnReady;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        Tools.Shuffle(_bugSounds);
    }

    private void OnMouseEnter()
    {
        if (!_isFlying) _isTimer = true;
    }

    private void OnMouseExit()
    {
        if (!_isFlying)
        {
            _isTimer = false;
            _time = 0;
        }
    }

    private void Update()
    {
        if (_isTimer && !_isFlying)
        {
            _time += Time.deltaTime;
            if (_time >= _neededTime)
            {
                _animatorMove.SetTrigger(Trigger);
                _animatorBug.SetFloat(Flying, 1);
                _audioSource.PlayOneShot(_bugSounds[_counter]);

                _counter++;
                if (_counter == _bugSounds.Count) _counter = 0;
                
                _isFlying = true;
                _isTimer = false;
            }
        }
        else if (!_isTimer)
        {
            _time = 0;
        }
    }

    public void StopBug()
    {
        _animatorBug.SetFloat(Flying, 0);
        _isFlying = false;

        _bonusAudioSource.PlayOneShot(_bugBonusSound);
        _bonusAudioSource.pitch += 0.45f;
    }
    
    public void Ready()
    {
        _animatorBug.SetFloat(Flying, 0);
        _isFlying = false;
        
        _audioSource.PlayOneShot(_readySound);
        
        OnReady?.Invoke();
        _lightGO.SetActive(false);
    }
}
