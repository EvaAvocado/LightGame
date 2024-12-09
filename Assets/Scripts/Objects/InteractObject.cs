using System;
using Controllers;
using UnityEngine;

namespace Objects
{
    [RequireComponent(typeof(Animator), typeof(AudioSource))]
    public class InteractObject : MonoBehaviour
    {
        protected Animator _animator;
        protected AudioSource _audioSource;
        protected AudioController _audioController;
        protected ScoreController _scoreController;

        public Action OnReady;
        protected SceneSwitchController _sceneSwitchController;
        
        public void Init(AudioController audioController, ScoreController scoreController, SceneSwitchController sceneSwitchController)
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _scoreController = scoreController;
            _audioController = audioController;
            _sceneSwitchController = sceneSwitchController;
            
            OnStart();
        }

        private void OnMouseEnter()
        {
            OnEnter();
        }
        
        private void OnMouseExit()
        {
            OnExit();
        }

        protected virtual void OnEnter()
        {
        }

        protected virtual void OnExit()
        {
        }

        protected virtual void OnStart()
        {
        }

        public virtual void Ready()
        {
            OnReady?.Invoke();
        }
    }

    
}