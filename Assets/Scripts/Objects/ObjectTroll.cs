using System.Collections.Generic;
using DG.Tweening;
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
        
        [Header("Camera Settings")]
        [SerializeField] private Vector3 _cameraPosition;
        [SerializeField] private float _cameraScale;
        [SerializeField] private float _cameraDuration;

        [Header("End Sprite Settings")] 
        [SerializeField] private GameObject _endSprites;
        [SerializeField] private GameObject _circleSprite;
        [SerializeField] private Vector3 _gameObjectPosition;
        [SerializeField] private Vector3 _gameObjectScale;
        [SerializeField] private float _gameObjectDuration;
        [SerializeField] private float _gameObjectCompleteDuration;
        
        [Header("The End Settings")]
        [SerializeField] private AudioClip _theEndSound;
        [SerializeField] private GameObject _theEndSprite;
        [SerializeField] private Vector3 _theEndScale;
        [SerializeField] private float _theEndDuration;
        [SerializeField] private List<GameObject> _offGameObjects;

        private bool _isCanWakeUp;
        private bool _isTumble;
        private Timer _timer;

        private static readonly int TumbleTrigger = Animator.StringToHash("tumble");
        private static readonly int WakeUpTrigger = Animator.StringToHash("wakeUp");

        protected override void OnStart()
        {
            _scoreController.OnScoreReady += SetIsCanWakeUp;
            _timer = GetComponent<Timer>();
            _timer.OnTimerEnd += WakeUp;
        }

        protected override void OnEnter()
        {
            if (!_isCanWakeUp && !_isTumble)
            {
                _audioController.PlayOneShot(_audioSource, _noSound);
                _animator.SetTrigger(TumbleTrigger);
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
            
            foreach (var obj in _offGameObjects)
            {
                obj.SetActive(false);
            }
            MoveCamera();
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
            _scoreController.OnScoreReady -= SetIsCanWakeUp;
            _timer.OnTimerEnd -= WakeUp;
        }
        
        private void MoveCamera()
        {
            _cameraController.MoveCamera(Camera.main, _cameraPosition, _cameraDuration);
            _cameraController.ScaleCamera(Camera.main, _cameraScale, _cameraDuration, MoveGameObject);
        }
        
        private void MoveGameObject()
        {
            _endSprites.SetActive(true);
            Vector3 worldPosition = _circleSprite.transform.parent.TransformPoint(_gameObjectPosition);
            _gameObjectController.MoveObject(_circleSprite, worldPosition, _gameObjectDuration, Ease.Linear);
            _gameObjectController.ScaleObject(_circleSprite, _gameObjectScale, _gameObjectDuration, CompleteGameObject, Ease.Linear);
        }

        private void CompleteGameObject()
        {
            _gameObjectController.ScaleObject(_circleSprite, Vector3.zero, _gameObjectCompleteDuration, MoveTheEnd, Ease.InOutBack);
        }
        
        private void MoveTheEnd()
        {
            _audioController.PlayOneShot(_audioSource, _theEndSound, 1f);
            _gameObjectController.ScaleObject(_theEndSprite, _theEndScale, _theEndDuration, Ease.OutBack);
        }
    }
}