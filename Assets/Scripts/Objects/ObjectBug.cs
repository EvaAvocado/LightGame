using System;
using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Objects
{
    [RequireComponent(typeof(Timer))]
    public class ObjectBug : InteractObject
    { 
        [SerializeField] private Animator _animatorBug;
        [SerializeField] private List<AudioClip> _bugSounds;
        [SerializeField] private AudioClip _bugBonusSound;
        [SerializeField] private AudioClip _readySound;

        private Timer _timer;
        private bool _isFlying;
        private int _counter;
        private float _pitch;
        
        private readonly float _basePitch = 1.21f;
    
        private static readonly int Trigger = Animator.StringToHash("trigger");
        private static readonly int Flying = Animator.StringToHash("flying");

        protected override void OnStart()
        {
            _timer = GetComponent<Timer>();
            _timer.OnTimerEnd += PlayAnim;
            _pitch = 1;
        }

        protected override void OnEnter()
        {
            if (!_isFlying)
            {
                _timer.Begin();
            }
        }

        protected override void OnExit()
        {
            if (!_isFlying)
            {
                _timer.Reset();
            }
        }

        private void PlayAnim()
        {
            _animator.SetTrigger(Trigger);
            _animatorBug.SetFloat(Flying, 1);
            
            _counter = _audioController.PlayNextSound(_audioSource, _bugSounds, _counter, _basePitch);
                
            _isFlying = true;
        }
        
        // Called from the Animation when the beetle stops
        public void StopBug()
        {
            _animatorBug.SetFloat(Flying, 0);
            _isFlying = false;

            _audioController.PlayOneShot(_audioSource, _bugBonusSound, _pitch);
            _pitch += 0.45f;
        }
        
        // Called from the Animation when the beetle finishes the last animation
        public override void Ready()
        {
            base.Ready();
            
            _animatorBug.SetFloat(Flying, 0);
            _animatorBug.gameObject.SetActive(false);
            _audioController.PlayOneShot(_audioSource, _readySound);
            _isFlying = false;
        }

        private void OnDisable()
        {
            _timer.OnTimerEnd -= PlayAnim;
        }
    }
}
