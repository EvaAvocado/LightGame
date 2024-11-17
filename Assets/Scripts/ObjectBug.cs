using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBug : MonoBehaviour
{ 
    [SerializeField] private Animator _animatorMove;
    [SerializeField] private Animator _animatorBug;
    
    private readonly float _neededTime = 0.35f;
    private bool _isTimer;
    private float _time;
    private bool _isFlying;
    
    private static readonly int Trigger = Animator.StringToHash("trigger");
    private static readonly int Flying = Animator.StringToHash("flying");
    
    public static event Action OnReady;
    
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
    }
    
    public void Ready()
    {
        OnReady?.Invoke();
        gameObject.SetActive(false);
    }
}
