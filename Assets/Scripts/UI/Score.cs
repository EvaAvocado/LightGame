using System;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private Text _text;
    
        private int _points;
    
        public Action OnAddScore;
        public Action OnScoreReady;

        public void Init(List<InteractObject> interactObjects)
        {
            foreach (var obj in interactObjects)
            {
                obj.OnReady += AddScore;
            }
        }

        private void AddScore()
        {
            _points++;
            _text.text = _points + "/10";
            OnAddScore?.Invoke();
        
            if (_points == 10) OnScoreReady?.Invoke();
        }
    }
}
