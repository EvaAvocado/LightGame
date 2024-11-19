using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Objects
{
    [RequireComponent(typeof(Timer))]
    public class ObjectCat : InteractObject
    {
        [SerializeField] private List<AudioClip> _catSounds;
        
        private int _counter;
        private Timer _timer;
    
        private static readonly int Trigger = Animator.StringToHash("trigger");

        protected override void OnStart()
        {
            _timer = GetComponent<Timer>();
            _timer.OnTimerEnd += PlayAnim;
        }

        protected override void OnEnter()
        {
            _timer.Begin();
        }
        
        protected override void OnExit()
        {
            _timer.Reset();
        }

        private void PlayAnim()
        {
            _animator.SetTrigger(Trigger);
        }

        // Called from the Animation when the cat meows
        public void PlayNextSound()
        {
            _counter = _audioController.PlayNextSound(_audioSource, _catSounds, _counter);
        }
    }
}