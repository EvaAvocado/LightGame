using System;
using UnityEngine;

namespace Tools
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float _neededTime;

        private bool _isTimer;
        private float _time;

        public Action OnTimerEnd;
    
        private void Update()
        {
            if (_isTimer)
            {
                _time += Time.deltaTime;
                if (_time >= _neededTime)
                {
                    OnTimerEnd?.Invoke();
                    _isTimer = false;
                }
            }
            else if (!_isTimer)
            {
                _time = 0;
            }
        }
    
        public void Begin()
        {
            _isTimer = true;
        }

        public void Reset()
        {
            _isTimer = false;
            _time = 0;
        }
    }
}