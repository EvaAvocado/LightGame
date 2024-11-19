using System;
using Controllers;
using UI;
using UnityEngine;

namespace Objects
{
    [RequireComponent(typeof(Animator), typeof(AudioSource))]
    public class InteractObject : MonoBehaviour
    {
        protected Animator _animator;
        protected AudioSource _audioSource;
        protected AudioController _audioController;
        protected Score _score;

        public Action OnReady;
        
        public void Init(AudioController audioController, Score score)
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _score = score;
            _audioController = audioController;
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