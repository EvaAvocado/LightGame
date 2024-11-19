using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class ObjectGlass : InteractObject
    {
        [SerializeField] private List<AudioClip> _bonusSounds;
        [SerializeField] private List<AudioClip> _tapSounds;

        private bool _isTrigger;
        private bool _isEnter;
    
        private static readonly int Trigger = Animator.StringToHash("trigger");
        
        protected override void OnStart()
        {
            _animator.SetFloat(Trigger, 0);
            _audioSource.volume = 0.2f;
        }

        protected override void OnEnter()
        {
            if (!_isTrigger)
            {
                _animator.SetFloat(Trigger, 1);
                _isEnter = true;
            }
        }

        protected override void OnExit()
        {
            if (!_isTrigger)
            {
                _animator.SetFloat(Trigger, 0);
            }
        }

        // Called from the Animation when is the last frame
        public void SetIsMeltTrue()
        {
            _isTrigger = true;
            _animator.SetFloat(Trigger, 0);
            Ready();
        }

        public override void Ready()
        {
            base.Ready();
        
            _audioSource.volume = 1f;
            _audioController.PlayRandomSound(_audioSource, _bonusSounds);
        }

        // Called from the Animation when the frame is switched 
        public void Tap()
        {
            if (_isEnter)
            {
                _audioController.PlayRandomSound(_audioSource, _tapSounds);
            }
        }
    }
}
