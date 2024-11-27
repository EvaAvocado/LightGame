using System.Collections.Generic;
using Controllers;
using Data;
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
        [SerializeField] private GameConfig _config;
    
        private AudioController _audioController;
        private ScoreController _scoreController;
        private CameraController _cameraController;
        private GameObjectController _gameObjectController;

        private void Awake()
        {
            _audioController = new AudioController();
            _scoreController = new ScoreController();
            _cameraController = new CameraController();
            _gameObjectController = new GameObjectController();
        
            foreach (var obj in _objects)
            {
                obj.Init(_audioController, _scoreController, _cameraController, _gameObjectController);
            }
        
            _scoreController.Init(_objects, _scoreView, _config.MaxScore);
            _ray.Init(_scoreController);
        }
    }
}