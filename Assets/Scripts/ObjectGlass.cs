using System;
using UnityEngine;

public class ObjectGlass : MonoBehaviour
{
    private Animator _animator;

    private bool _isTrigger;
    
    private static readonly int Trigger = Animator.StringToHash("trigger");
    
    public static event Action OnReady;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat(Trigger, 0);
    }

    private void OnMouseEnter()
    {
        if(!_isTrigger) _animator.SetFloat(Trigger, 1);
    }

    private void OnMouseExit()
    {
        if(!_isTrigger) _animator.SetFloat(Trigger, 0);
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
    }
}
