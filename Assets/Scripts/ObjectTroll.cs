using System;
using UnityEngine;

public class ObjectTroll : MonoBehaviour
{
    private Animator _animator;
    
    private bool _isWakeUp;
    
    private readonly float _neededTime = 0.8f;
    private bool _isTimer;
    private float _time;
    
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
    }

    private void OnMouseEnter()
    {
        if (!_isWakeUp)
        {
            _animator.SetTrigger(TumbleTrigger);
        }
        else if (_isWakeUp && !_isTimer)
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
}
