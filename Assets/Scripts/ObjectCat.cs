using System.Collections.Generic;
using UnityEngine;

public class ObjectCat : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _catSounds;
    
    private readonly float _neededTime = 0.8f;
    private Animator _animator;
    private AudioSource _audioSource;
    
    private bool _isTimer;
    private float _time;
    
    private static readonly int Trigger = Animator.StringToHash("trigger");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseEnter()
    {
        _isTimer = true;
    }

    private void OnMouseExit()
    {
        _isTimer = false;
        _time = 0;
    }

    private void Update()
    {
        if (_isTimer)
        {
            _time += Time.deltaTime;
            if (_time >= _neededTime)
            {
                _animator.SetTrigger(Trigger);
                _isTimer = false;
            }
        }
        else if (!_isTimer)
        {
            _time = 0;
        }
    }

    public void PlayRandomSound()
    {
        Tools.Shuffle(_catSounds);
        _audioSource.PlayOneShot(_catSounds[0]);
    }
}
