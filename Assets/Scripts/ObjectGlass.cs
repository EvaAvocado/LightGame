using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGlass : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _bonusSounds;
    [SerializeField] private List<AudioClip> _tapSounds;

    private Animator _animator;
    private AudioSource _audioSource;

    private bool _isTrigger;
    private bool _isEnter;
    
    private static readonly int Trigger = Animator.StringToHash("trigger");
    
    public static event Action OnReady;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat(Trigger, 0);
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0.2f;
    }

    private void OnMouseEnter()
    {
        if (!_isTrigger)
        {
            _animator.SetFloat(Trigger, 1);
            _isEnter = true;
        }
    }

    private void OnMouseExit()
    {
        if (!_isTrigger)
        {
            _animator.SetFloat(Trigger, 0);
        }
    }

    public void SetIsMeltTrue()
    {
        _isTrigger = true;
        _animator.SetFloat(Trigger, 0);
        Ready();
    }
    
    public void Ready()
    {
        OnReady?.Invoke();
        
        Tools.Shuffle(_bonusSounds);
        _audioSource.volume = 1f;
        _audioSource.PlayOneShot(_bonusSounds[0]);
    }

    public void Tap()
    {
        if (_isEnter)
        {
            Tools.Shuffle(_tapSounds);
            _audioSource.PlayOneShot(_tapSounds[0]);
        }
    }
}
