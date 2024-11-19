using System;
using System.Collections.Generic;
using Objects;
using UI;

namespace Controllers
{
    public class ScoreController
    {
        private ScoreView _scoreView;
        private int _points;
    
        public Action OnAddScore;
        public Action OnScoreReady;

        public void Init(List<InteractObject> interactObjects, ScoreView scoreView)
        {
            foreach (var obj in interactObjects)
            {
                obj.OnReady += AddScore;
            }

            _scoreView = scoreView;
        }

        private void AddScore()
        {
            _points++;
            _scoreView.SetNewPointsInText(_points);
            OnAddScore?.Invoke();
        
            if (_points == 10) OnScoreReady?.Invoke();
        }
    }
}