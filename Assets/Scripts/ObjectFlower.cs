using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFlower : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _bonusSounds;
    [SerializeField] private List<AudioClip> _flowerSounds;

    private Animator _animator;
    private AudioSource _audioSource;

    private bool _isRise;
    private bool _isEnter;
    
    private static readonly int Rise = Animator.StringToHash("rise");
    
    public static event Action OnReady;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat(Rise, 0);
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0.2f;
    }

    private void OnMouseEnter()
    {
        if (!_isRise)
        {
            _animator.SetFloat(Rise, 1);
            _isEnter = true;
        }
    }

    private void OnMouseExit()
    {
        if (!_isRise)
        {
            _animator.SetFloat(Rise, -1);
        }
    }

    public void SetIsRiseTrue()
    {
        _isRise = true;
        Ready();
    }

    public void SetZeroSpeed()
    {
        if (!_isRise && _animator.GetFloat(Rise) <= -1)
        {
            _animator.SetFloat(Rise, 0);
        }
    }
    
    public void Ready()
    {
        OnReady?.Invoke();
        
        Tools.Shuffle(_bonusSounds);
        _audioSource.volume = 1;
        _audioSource.PlayOneShot(_bonusSounds[0]);
    }
    
    public void Tap()
    {
        if (_isEnter)
        {
            Tools.Shuffle(_flowerSounds);
            _audioSource.PlayOneShot(_flowerSounds[0]);
            _audioSource.PlayOneShot(_flowerSounds[0]);
        }
    }
}
