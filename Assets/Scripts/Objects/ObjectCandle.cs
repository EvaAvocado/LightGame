using System.Collections.Generic;
using Tools;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Objects
{
    [RequireComponent(typeof(Timer))]
    public class ObjectCandle : InteractObject
    {
        [SerializeField] private List<AudioClip> _onSounds;
        [SerializeField] private List<AudioClip> _bonusSounds;
        [SerializeField] private Light2D _light2D;
        [SerializeField] private Timer[] _timers;
    
        private SpriteRenderer _spriteRenderer;
        private bool _isCanTrigger;
        private bool _isOn;
    
        private static readonly int Start = Animator.StringToHash("start");
        private static readonly int Stop = Animator.StringToHash("stop");
        private static readonly int On = Animator.StringToHash("on");

        protected override void OnStart()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _timers[0].OnTimerEnd += () =>
            {
                _isCanTrigger = true;
                CheckEnter();
            };
            _timers[1].OnTimerEnd += PlayAnim;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("RayLens"))
            {
                if (!_isCanTrigger)
                {
                    _timers[0].Begin();
                }

                CheckEnter();
            }
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("RayLens"))
            {
                if (!_isOn && _isCanTrigger)
                {
                    _timers[1].Reset();
                    _animator.SetTrigger(Stop);
                    _isCanTrigger = false;
                }
                
                if (!_isCanTrigger)
                {
                    _timers[0].Reset();
                }
            }
        }
        
        private void CheckEnter()
        {
            if (!_isOn && _isCanTrigger)
            {
                _timers[1].Begin();
                _animator.SetTrigger(Start);
            }
        }

        private void PlayAnim()
        {
            _isOn = true;
            _animator.SetTrigger(On);
        }
    
        // Called from the Animation when the candle lights up
        public override void Ready()
        {
            base.Ready();
            
            _audioController.PlayRandomSound(_audioSource, _bonusSounds);
        }
    
        // Called from the Animation when the light is on
        public void OnLight()
        {
            _light2D.lightCookieSprite = _spriteRenderer.sprite;
        }

        // Called from the Animation when the light is off
        public void OffLight()
        {
            _light2D.lightCookieSprite = null;
        }
        
        // Called from the Animation when the candle lights up
        public void OnSound()
        {
            _audioController.PlayRandomSound(_audioSource, _onSounds);
        }
        
        private void OnDisable()
        {
            _timers[0].OnTimerEnd -= () =>
            {
                _isCanTrigger = true;
                CheckEnter();
            };
            _timers[1].OnTimerEnd -= PlayAnim;
        }
    }
}
