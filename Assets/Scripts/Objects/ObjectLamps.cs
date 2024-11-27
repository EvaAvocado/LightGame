using System;
using Tools;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Objects
{
    [RequireComponent(typeof(Timer))]
    public class ObjectLamps : InteractObject
    {
        [SerializeField] private Light2D _light2D;
        [SerializeField] private AudioClip _lampSound;
        [SerializeField] private AudioClip _onSound;
        [SerializeField] private Timer[] _timers;
        [SerializeField] private AudioSource _newSource;

        private SpriteRenderer _spriteRenderer;
        private bool _isOn;
        private bool _isCanOnEnter;
        private bool _isOffSound;
        private bool _isOnMusic;

        private static readonly int Trigger = Animator.StringToHash("trigger");
        private static readonly int TurnOn = Animator.StringToHash("turn-on");
        private static readonly int TurnOff = Animator.StringToHash("turn-off");

        protected override void OnStart()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _timers[0].OnTimerEnd += () =>
            {
                _isCanOnEnter = true;
                CheckEnter();
            };
            _timers[1].OnTimerEnd += PlayAnim;

            _audioSource = _newSource;
        }

        protected override void OnEnter()
        {
            if (!_isCanOnEnter)
            {
                _timers[0].Begin();
            }
            
            CheckEnter();
        }

        protected override void OnExit()
        {
            if (!_isOn && _isCanOnEnter)
            {
                _timers[1].Reset();
                _animator.SetTrigger(TurnOff);
                _isCanOnEnter = false;
            }
            
            if (!_isCanOnEnter)
            {
                _timers[0].Reset();
            }
        }

        private void CheckEnter()
        {
            if (!_isOn && _isCanOnEnter)
            {
                _timers[1].Begin();
                _animator.SetTrigger(Trigger);
            }
        }

        private void PlayAnim()
        {
            _animator.SetTrigger(TurnOn);
            _isOn = true;
        }

        // Called from the Animation when the lamps light up
        public override void Ready()
        {
            OnReady?.Invoke();
            _isOffSound = true;
        }

        // Called from the Animation when the light is on
        public void OnLight()
        {
            if (!_isOffSound && !_isOnMusic)
            {
                _audioController.PlayOneShot(_audioSource, _lampSound);
            }
            else if (_isOffSound && !_isOnMusic)
            {
                _audioController.PlayMusic(_audioSource, _onSound, true, 0.02f);
                _isOnMusic = true;
            }

            _light2D.lightCookieSprite = _spriteRenderer.sprite;
        }

        // Called from the Animation when the light is off
        public void OffLight()
        {
            _light2D.lightCookieSprite = null;
        }

        private void OnDisable()
        {
            _timers[0].OnTimerEnd -= () =>
            {
                _isCanOnEnter = true;
                CheckEnter();
            };
            _timers[1].OnTimerEnd -= PlayAnim;
        }
    }
}