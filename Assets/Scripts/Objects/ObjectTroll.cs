using Tools;
using UnityEngine;

namespace Objects
{
    [RequireComponent(typeof(Timer))]
    public class ObjectTroll : InteractObject
    {
        [SerializeField] private AudioClip _noSound;
        [SerializeField] private AudioClip _wakeUpSound;
        [SerializeField] private AudioClip _bonusSound;
        [SerializeField] private float _pitchOnWakeUp = 1.75f;

        private bool _isWakeUp;
        private bool _isTumble;
        private Timer _timer;

        private static readonly int TumbleTrigger = Animator.StringToHash("tumble");
        private static readonly int WakeUpTrigger = Animator.StringToHash("wakeUp");

        protected override void OnStart()
        {
            _scoreController.OnScoreReady += WakeUp;
            _timer = GetComponent<Timer>();
            _timer.OnTimerEnd += PlayAnim;
        }

        protected override void OnEnter()
        {
            if (!_isWakeUp && !_isTumble)
            {
                _audioController.PlayOneShot(_audioSource, _noSound);
                _animator.SetTrigger(TumbleTrigger);
            }
            else if (_isWakeUp && !_isTumble)
            {
                _timer.Begin();
            }
        }

        protected override void OnExit()
        {
            if (_isWakeUp)
            {
                _timer.Reset();
            }
        }

        private void PlayAnim()
        {
            _audioController.PlayOneShot(_audioSource, _bonusSound); 
            _animator.SetTrigger(WakeUpTrigger);
        }

        private void WakeUp()
        {
            _isWakeUp = true;
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
    }
}