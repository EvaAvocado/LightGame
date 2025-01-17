using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using UnityEngine;

namespace Animations
{
    public class ObjectTrollEndAnimation : MonoBehaviour
    {
        [Header("Camera Settings")] [SerializeField]
        private Vector3 _cameraPosition;

        [SerializeField] private float _cameraScale;
        [SerializeField] private float _cameraDuration;

        [Header("End Sprite Settings")] [SerializeField]
        private AudioClip _endSound;

        [SerializeField] private GameObject _endSprites;
        [SerializeField] private GameObject _circleSprite;
        [SerializeField] private Vector3 _gameObjectPosition;
        [SerializeField] private Vector3 _gameObjectScale;
        [SerializeField] private float _gameObjectDuration;
        [SerializeField] private float _gameObjectCompleteDuration;

        [Header("The End Settings")] [SerializeField]
        private AudioClip _theEndSound;

        [SerializeField] private GameObject _theEndSprite;
        [SerializeField] private Vector3 _theEndScale;
        [SerializeField] private float _theEndDuration;
        [SerializeField] private List<GameObject> _offGameObjects;

        private Camera _camera;
        private AudioController _audioController;
        private AudioSource _audioSource;
        private SceneSwitchController _sceneSwitchController;

        public void StartAnimation(AudioController audioController, AudioSource audioSource,
            SceneSwitchController sceneSwitchController)
        {
            _camera = Camera.main;
            _audioController = audioController;
            _audioSource = audioSource;
            _sceneSwitchController = sceneSwitchController;

            foreach (var obj in _offGameObjects)
            {
                obj.SetActive(false);
            }

            MoveCamera();
        }

        private void MoveCamera()
        {
            _camera.transform.DOMove(_cameraPosition, _cameraDuration);
            _camera.DOOrthoSize(_cameraScale, _cameraDuration).OnComplete(MoveGameObject);
        }

        private void MoveGameObject()
        {
            _endSprites.SetActive(true);
            Vector3 worldPosition = _circleSprite.transform.parent.TransformPoint(_gameObjectPosition);
            _circleSprite.transform.DOMove(worldPosition, _gameObjectDuration).SetEase(Ease.Linear);
            _circleSprite.transform.DOScale(_gameObjectScale, _gameObjectDuration).SetEase(Ease.Linear)
                .OnComplete(CompleteGameObject);
        }

        private void CompleteGameObject()
        {
            _audioController.PlayOneShot(_audioSource, _endSound, 1f);
            _circleSprite.transform.DOScale(Vector3.zero, _gameObjectCompleteDuration).SetEase(Ease.InOutBack)
                .OnComplete(MoveTheEnd);
        }

        private void MoveTheEnd()
        {
            _audioController.PlayOneShot(_audioSource, _theEndSound, 1f);
            _theEndSprite.transform.DOScale(_theEndScale, _theEndDuration).SetEase(Ease.OutBack).OnComplete((() =>
            {
                _theEndSprite.transform.DOScale(_theEndScale, _theEndDuration)
                    .OnComplete(() => _sceneSwitchController.SwitchScene("Menu"));
            }));
        }
    }
}