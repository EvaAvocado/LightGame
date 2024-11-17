using System;
using UnityEngine;

public class ObjectFlower : MonoBehaviour
{
    private Animator _animator;

    private bool _isRise;
    
    private static readonly int Rise = Animator.StringToHash("rise");
    
    public static event Action OnReady;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat(Rise, 0);
    }

    private void OnMouseEnter()
    {
        if(!_isRise) _animator.SetFloat(Rise, 1);
    }

    private void OnMouseExit()
    {
        if(!_isRise) _animator.SetFloat(Rise, -1);
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
    }
}
