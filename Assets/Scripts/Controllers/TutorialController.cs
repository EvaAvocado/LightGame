using System.Collections.Generic;
using Objects;
using UnityEngine;

namespace Controllers
{
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _tutorialObjects;
        [SerializeField] private ObjectTroll _objectTroll;
        [SerializeField] private ObjectFlower _objectFlower;
        [SerializeField] private AudioClip _nextClip;
        [SerializeField] private AudioClip _bonusClip;
        
        private int _counter;
        private bool _isTumble;
        private bool _isRise;
        
        private AudioSource _audioSource;
        private SceneSwitchController _sceneSwitchController;
        private AudioController _audioController;

        public void Init(SceneSwitchController sceneSwitchController, AudioController audioController)
        {
            _audioSource = GetComponent<AudioSource>();
            _sceneSwitchController = sceneSwitchController;
            _audioController = audioController;
            
            ShowNext();
            _objectTroll.OnTumble += ShowNextTumble;
            _objectFlower.OnReady += ShowNextRise;
        }

        public void ShowNext()
        {
            if (_counter == _tutorialObjects.Count) return;

            if (_counter != 0)
            {
                _tutorialObjects[_counter - 1].SetActive(false);
            }

            _tutorialObjects[_counter].SetActive(true);
            _counter++;

            if (!_isRise)
            {
                _audioController.PlayOneShot(_audioSource, _nextClip);
            }
            else
            {
                _audioController.PlayOneShot(_audioSource, _bonusClip);
            }
            
        }

        private void ShowNextTumble()
        {
            if (_isTumble) return;
            
            _isTumble = true;
            ShowNext();
        }
        
        private void ShowNextRise()
        {
            if (_isRise) return;
            
            _isRise = true;
            ShowNext();
        }

        public void GoToMenu()
        {
            _sceneSwitchController.SwitchScene("Menu");
        }

        private void OnDisable()
        {
            _objectTroll.OnTumble -= ShowNextTumble;
            _objectFlower.OnReady -= ShowNextRise;
        }
    }
}