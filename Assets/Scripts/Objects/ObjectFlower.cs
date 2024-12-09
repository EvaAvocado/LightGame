using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class ObjectFlower : InteractObject
    {
        [SerializeField] private List<AudioClip> _flowerSounds;
        
        private bool _isRise;
        private bool _isEnter;
    
        private static readonly int Rise = Animator.StringToHash("rise");

        protected override void OnStart()
        {
            _animator.SetFloat(Rise, 0);
            _audioSource.volume = 0.2f;
        }

        protected override void OnEnter()
        {
            if (!_isRise)
            {
                _animator.SetFloat(Rise, 1);
                _isEnter = true;
            }
        }

        protected override void OnExit()
        {
            if (!_isRise)
            {
                _animator.SetFloat(Rise, -1);
            }
        }

        public void SetIsRiseTrue()
        {
            _isRise = true;
            Ready();
        }

        // Called from the Animation when is the first frame
        public void SetZeroSpeed()
        {
            if (!_isRise && _animator.GetFloat(Rise) <= -1)
            {
                _animator.SetFloat(Rise, 0);
            }
        }
    
        // Called from the Animation when is the last frame
        public override void Ready()
        {
            base.Ready();
        }
    
        // Called from the Animation when the frame is switched
        public void Tap()
        {
            if (_isEnter)
            {
                _audioController.PlayRandomSound(_audioSource, _flowerSounds);
            }
        }
    }
}
