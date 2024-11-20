using System;
using System.Collections.Generic;
using Objects;
using UI;

namespace Controllers
{
    public class ScoreController
    {
        private ScoreView _scoreView;
        private List<InteractObject> _interactObjects;
        private int _points;
        private int _maxPoints;
    
        public Action OnAddScore;
        public Action OnScoreReady;

        public void Init(List<InteractObject> interactObjects, ScoreView scoreView, int maxPoints)
        {
            _interactObjects = interactObjects;
            foreach (var obj in _interactObjects)
            {
                obj.OnReady += AddScore;
            }

            _scoreView = scoreView;
            _maxPoints = maxPoints;
        }

        private void AddScore()
        {
            _points++;
            _scoreView.SetNewPointsInText(_points);
            OnAddScore?.Invoke();

            CheckScore();
        }

        private void CheckScore()
        {
            if (_points == _maxPoints)
            {
                OnScoreReady?.Invoke();
                foreach (var obj in _interactObjects)
                {
                    obj.OnReady -= AddScore;
                }
            }
        }
    }
}