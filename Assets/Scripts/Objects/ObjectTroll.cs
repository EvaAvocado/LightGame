using System;
using System.Collections.Generic;
using Animations;
using Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace Objects
{
    [RequireComponent(typeof(Timer))]
    public class ObjectTroll : InteractObject
    {
        [SerializeField] private AudioClip _noSound;
        [SerializeField] private AudioClip _wakeUpSound;
        [SerializeField] private AudioClip _bonusSound;
        [SerializeField] private float _pitchOnWakeUp = 1.75f;
        [FormerlySerializedAs("_objectTrollAnimation")] [SerializeField] private ObjectTrollEndAnimation objectTrollEndAnimation;

        private bool _isCanWakeUp;
        private bool _isTumble;
        private Timer _timer;
        
        private static readonly int TumbleTrigger = Animator.StringToHash("tumble");
        private static readonly int WakeUpTrigger = Animator.StringToHash("wakeUp");

        public Action OnTumble;

        protected override void OnStart()
        {
            if (_scoreController!=null) _scoreController.OnScoreReady += SetIsCanWakeUp;
            _timer = GetComponent<Timer>();
            _timer.OnTimerEnd += WakeUp;
        }

        protected override void OnEnter()
        {
            if (!_isCanWakeUp && !_isTumble)
            {
                _audioController.PlayOneShot(_audioSource, _noSound);
                _animator.SetTrigger(TumbleTrigger);
                OnTumble?.Invoke();
            }
            else if (_isCanWakeUp && !_isTumble)
            {
                _timer.Begin();
            }
        }

        protected override void OnExit()
        {
            if (_isCanWakeUp)
            {
                _timer.Reset();
            }
        }

        private void WakeUp()
        {
            _audioController.PlayOneShot(_audioSource, _bonusSound); 
            _animator.SetTrigger(WakeUpTrigger);
            objectTrollEndAnimation.StartAnimation(_audioController, _audioSource, _sceneSwitchController);
        }

        private void SetIsCanWakeUp()
        {
            _isCanWakeUp = true;
        }
        
        // Called from the Animation when Troll starts tumbling
        public void TumbleOn()
        {
            _isTumble = true;
        }

        // Called from the Animation when Troll finishes tumbling
        public void TumbleOff()
        {
            _isTumble = false;
        }

        // Called from the Animation when Troll wakes up
        public void WakeUpSound()
        {
            _audioController.PlayOneShot(_audioSource, _wakeUpSound, _pitchOnWakeUp);;
        }
        
        private void OnDisable()
        {
            if (_scoreController!=null) _scoreController.OnScoreReady -= SetIsCanWakeUp;
            _timer.OnTimerEnd -= WakeUp;
        }
        
        
    }
}