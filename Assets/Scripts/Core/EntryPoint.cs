using System.Collections.Generic;
using Controllers;
using Objects;
using UI;
using UnityEngine;
using Ray = Playable.Ray;

namespace Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private List<InteractObject> _objects;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private Ray _ray;
    
        private AudioController _audioController;
        private ScoreController _scoreController;

        private void Awake()
        {
            _audioController = new AudioController();
            _scoreController = new ScoreController();
        
            foreach (var obj in _objects)
            {
                obj.Init(_audioController, _scoreController);
            }
        
            _scoreController.Init(_objects, _scoreView);
            _ray.Init(_scoreController);
        }
    }
}